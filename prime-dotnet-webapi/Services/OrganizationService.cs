using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

// TODO add logging
namespace Prime.Services
{
    public class OrganizationService : BaseService, IOrganizationService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IPartyService _partyService;

        public OrganizationService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IBusinessEventService businessEventService,
            IPartyService partyService)
            : base(context, httpContext)
        {
            _businessEventService = businessEventService;
            _partyService = partyService;
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync()
        {
            return await this.GetBaseOrganizationQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync(int partyId)
        {
            return await this.GetBaseOrganizationQuery()
                .Where(o => o.SigningAuthorityId == partyId)
                .ToListAsync();
        }

        public async Task<Organization> GetOrganizationAsync(int organizationId)
        {
            return await this.GetBaseOrganizationQuery()
                .SingleOrDefaultAsync(o => o.Id == organizationId);
        }

        public async Task<int> CreateOrganizationAsync(Party signingAuthority)
        {
            if (signingAuthority == null)
            {
                throw new ArgumentNullException(nameof(signingAuthority), "Could not create a site, the passed in Party cannot be null.");
            }

            var partyExists = await _partyService.PartyExistsAsync(signingAuthority.Id);
            var signingAuthorityId = signingAuthority.Id;

            if (!partyExists)
            {
                signingAuthorityId = await _partyService.CreatePartyAsync(signingAuthority);
            }

            var organization = new Organization
            { SigningAuthorityId = signingAuthorityId };

            _context.Organizations.Add(organization);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create Organization.");
            }

            await _businessEventService.CreateOrganizationEventAsync(organization.Id, signingAuthorityId, "Organization Created");

            return organization.Id;
        }

        public async Task<int> UpdateOrganizationAsync(int organizationId, Organization updatedOrganization, bool isCompleted = false)
        {
            // TODO signing authority needs a partial update to non-BCSC fields

            var currentOrganization = await this.GetOrganizationAsync(organizationId);
            var acceptedAgreementDate = currentOrganization.AcceptedAgreementDate;
            var submittedDate = currentOrganization.SubmittedDate;
            var currentIsCompleted = currentOrganization.Completed;

            // BCSC Fields
            var userId = currentOrganization.SigningAuthority.UserId;

            this._context.Entry(currentOrganization).CurrentValues.SetValues(updatedOrganization);
            this._context.Entry(currentOrganization.SigningAuthority).CurrentValues.SetValues(updatedOrganization.SigningAuthority);

            if (updatedOrganization.SigningAuthority?.PhysicalAddress != null)
            {
                if (currentOrganization.SigningAuthority?.PhysicalAddress == null)
                {
                    currentOrganization.SigningAuthority.PhysicalAddress = updatedOrganization.SigningAuthority.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(currentOrganization.SigningAuthority.PhysicalAddress).CurrentValues.SetValues(updatedOrganization.SigningAuthority.PhysicalAddress);
                }
            }

            if (updatedOrganization.SigningAuthority?.MailingAddress != null)
            {
                if (currentOrganization.SigningAuthority?.MailingAddress == null)
                {
                    currentOrganization.SigningAuthority.MailingAddress = updatedOrganization.SigningAuthority.MailingAddress;
                }
                else
                {
                    this._context.Entry(currentOrganization.SigningAuthority.MailingAddress).CurrentValues.SetValues(updatedOrganization.SigningAuthority.MailingAddress);
                }
            }

            // Keep userId the same from BCSC card, do not update
            currentOrganization.SigningAuthority.UserId = userId;

            // Managed through separate API endpoint, and should never be updated
            currentOrganization.AcceptedAgreementDate = acceptedAgreementDate;
            currentOrganization.SubmittedDate = submittedDate;

            // Registration has been completed
            currentOrganization.Completed = (isCompleted == true)
                ? isCompleted
                : currentIsCompleted;

            await _businessEventService.CreateOrganizationEventAsync(currentOrganization.Id, currentOrganization.SigningAuthorityId, "Organization Updated");

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task DeleteOrganizationAsync(int organizationId)
        {
            var organization = await this.GetBaseOrganizationQuery()
                .SingleOrDefaultAsync(o => o.Id == organizationId);

            if (organization == null)
            {
                return;
            }

            // _context.Addresses.Remove(organization.SigningAuthority.PhysicalAddress);
            // _context.Parties.Remove(organization.SigningAuthority);
            _context.Organizations.Remove(organization);

            await _context.SaveChangesAsync();
        }

        public async Task<Organization> SubmitRegistrationAsync(int organizationId)
        {
            var organization = await GetOrganizationAsync(organizationId);
            organization.SubmittedDate = DateTimeOffset.Now;
            _context.Update(organization);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not submit the organization.");
            }

            return organization;
        }

        public async Task<Organization> GetOrganizationNoTrackingAsync(int organizationId)
        {
            return await this.GetBaseOrganizationQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == organizationId);
        }

        public async Task<int> AcceptCurrentOrganizationAgreementAsync(int organizationId)
        {
            var organization = await GetOrganizationAsync(organizationId);

            organization.AcceptedAgreementDate = DateTimeOffset.Now;

            return await _context.SaveChangesAsync();
        }

        public async Task<Organization> GetOrganizationByPartyIdAsync(int partyId)
        {
            return await _context.Organizations
                .SingleOrDefaultAsync(o => o.SigningAuthorityId == partyId);
        }

        public async Task<SignedAgreement> AddSignedAgreementAsync(int organizationId, Guid documentGuid, string filename)
        {
            var signedAgreement = new SignedAgreement
            {
                DocumentGuid = documentGuid,
                OrganizationId = organizationId,
                FileName = filename,
                UploadedDate = DateTimeOffset.Now
            };

            _context.SignedAgreements.Add(signedAgreement);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not add business licence.");
            }

            return signedAgreement;
        }

        public async Task<IEnumerable<SignedAgreement>> GetSignedAgreementsAsync(int organizationId)
        {
            return await _context.SignedAgreements
                .Where(a => a.OrganizationId == organizationId)
                .ToListAsync();
        }

        public async Task<SignedAgreement> GetLatestSignedAgreementAsync(int organizationId)
        {
            return await _context.SignedAgreements
                .Where(sa => sa.OrganizationId == organizationId)
                .OrderByDescending(sa => sa.UploadedDate)
                .FirstOrDefaultAsync();
        }

        private IQueryable<Organization> GetBaseOrganizationQuery()
        {
            return _context.Organizations
                .Include(o => o.Locations)
                .Include(o => o.SignedAgreements)
                .Include(o => o.SigningAuthority)
                    .ThenInclude(p => p.PhysicalAddress)
                .Include(o => o.SigningAuthority)
                    .ThenInclude(p => p.MailingAddress);
        }
    }
}
