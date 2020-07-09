using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VerifiableCredentialController : ControllerBase
    {
        private readonly IVerifiableCredentialService _verifiableCredentialsService;

        public VerifiableCredentialController(
            IVerifiableCredentialService verifiableCredentialService
        )
        {
            _verifiableCredentialsService = verifiableCredentialService;
        }

        // POST: api/topic/:topic/{guid}
        /// <summary>
        /// Handle webhook events sent from the issuing agent.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        // Webhooks explained "In Soviet Russia API call you!" - Jason Aitchison 2020
        [HttpPost("/api/webhooks/{api-key}/topic/{topic}", Name = nameof(Webhook))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // TODO update to response code 202 when queue has been added for webhooks
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Webhook(string apiKey, string topic, [FromBody] JObject data)
        {
            if (apiKey != PrimeConstants.VERIFIABLE_CREDENTIAL_WEBHOOK_KEY)
            {
                return Forbid();
            }
            await _verifiableCredentialsService.WebhookAsync(data, topic);
            return NoContent();
        }
    }
}
