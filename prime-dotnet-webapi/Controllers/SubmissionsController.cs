using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Services;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/enrollees")]
    [ApiController]
    [Authorize(Policy = AuthConstants.USER_POLICY)]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly IAdminService _adminService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enrolleeProfileVersionService;

        public SubmissionsController(
            ISubmissionService submissionService,
            IAdminService adminService,
            IEnrolleeService enrolleeService,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enrolleeProfileVersionService)
        {
            _submissionService = submissionService;
            _adminService = adminService;
            _enrolleeService = enrolleeService;
            _accessTermService = accessTermService;
            _enrolleeProfileVersionService = enrolleeProfileVersionService;
        }

        // POST: api/enrollees/5/submission
        /// <summary>
        /// Submits the given enrollee through Auto/manual adjudication.
        /// </summary>
        [HttpPost("{enrolleeId}/submission", Name = nameof(Submit))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> Submit(int enrolleeId, EnrolleeProfileViewModel updatedProfile)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }
            if (!User.CanEdit(enrollee))
            {
                return Forbid();
            }

            if (updatedProfile == null)
            {
                this.ModelState.AddModelError("EnrolleeProfileViewModel", "New profile cannot be null.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }
            if (!(await _enrolleeService.IsEnrolleeInStatusAsync(enrolleeId, StatusType.Editable)))
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", "Application can not be submitted when the current status is not 'Active'.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            updatedProfile.IdentityAssuranceLevel = User.GetIdentityAssuranceLevel();
            updatedProfile.IdentityProvider = User.GetIdentityProvider();
            await _submissionService.SubmitApplicationAsync(enrolleeId, updatedProfile);

            enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            return Ok(ApiResponse.Result(enrollee));
        }

        // POST: api/enrollees/5/submission/accept-toa
        /// <summary>
        /// Performs a submission-related action on an Enrolle, such as an adjudicator approving an application.
        /// </summary>
        [HttpPost("{enrolleeId}/submission/{submissionAction:submissionAction}", Name = nameof(SubmissionAction))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> SubmissionAction(int enrolleeId, SubmissionAction submissionAction)
        {
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            if (enrollee == null)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }
            if (!User.CanEdit(enrollee))
            {
                return Forbid();
            }

            try
            {
                await _submissionService.PerformSubmissionActionAsync(enrolleeId, submissionAction, User.IsAdmin());
                enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
                return Ok(ApiResponse.Result(enrollee));
            }
            catch (SubmissionService.InvalidActionException)
            {
                this.ModelState.AddModelError("Enrollee.CurrentStatus", $"Action could not be performed.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }
        }

        // PUT: api/enrollees/5/always-manual
        /// <summary>
        /// Sets an Enrollee's always manual flag, forcing them to always be sent to manual adjudication
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpPut("{enrolleeId}/always-manual", Name = nameof(SetEnrolleeManualFlag))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SetEnrolleeManualFlag(int enrolleeId)
        {
            return await ManualFlagInternal(enrolleeId, true);
        }

        // DELETE: api/enrollees/5/always-manual
        /// <summary>
        /// Removes an Enrollee's always manual flag, allowing them to go through the adjudication rules engine normally.
        /// </summary>
        /// <param name="enrolleeId"></param>
        [HttpDelete("{enrolleeId}/always-manual", Name = nameof(RemoveEnrolleeManualFlag))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveEnrolleeManualFlag(int enrolleeId)
        {
            return await ManualFlagInternal(enrolleeId, false);
        }

        private async Task<ActionResult> ManualFlagInternal(int enrolleeId, bool alwaysManual)
        {
            var enrolleeExists = await _enrolleeService.EnrolleeExistsAsync(enrolleeId);
            if (!enrolleeExists)
            {
                return NotFound(ApiResponse.Message($"Enrollee not found with id {enrolleeId}."));
            }

            // TODO business event
            await _submissionService.UpdateAlwaysManualAsync(enrolleeId, alwaysManual);

            return NoContent();
        }
    }
}
