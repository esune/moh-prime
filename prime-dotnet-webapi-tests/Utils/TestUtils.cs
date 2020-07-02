using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Bogus;
using Prime;
using Prime.Auth;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json.Linq;

namespace PrimeTests.Utils
{
    public class TestUtils
    {

        public static string[] countries = new[] { "CA" };

        public static string[] provinces = new[] { "AB", "BC", "MB", "NB", "NL", "NS", "ON", "PE", "QC", "SK", "NT", "NU", "YT" };

        public static Faker<PhysicalAddress> PhysicalAddressFaker = new Faker<PhysicalAddress>()
            .RuleFor(a => a.CountryCode, f => f.PickRandom(countries))
            .RuleFor(a => a.ProvinceCode, TestUtils.RandomProvinceCode())
            .RuleFor(a => a.Street, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Postal, f => f.Address.ZipCode("?#?#?#"));

        public static Faker<MailingAddress> MailingAddressFaker = new Faker<MailingAddress>()
            .RuleFor(a => a.CountryCode, f => f.PickRandom(countries))
            .RuleFor(a => a.ProvinceCode, TestUtils.RandomProvinceCode())
            .RuleFor(a => a.Street, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Postal, f => f.Address.ZipCode("?#?#?#"));

        public static Faker<Certification> CertificationFaker = new Faker<Certification>()
            .RuleFor(c => c.CollegeCode, f => f.Random.Int(1, 5))
            .RuleFor(c => c.LicenseNumber, f => f.Random.Int(100000, 999999).ToString().Substring(1))
            .RuleFor(c => c.LicenseCode, f => f.Random.Int(1, 4))
            .RuleFor(c => c.RenewalDate, f => f.Date.Future(1))
            .RuleFor(c => c.PracticeCode, f => f.Random.Int(1, 4));

        public static Faker<Certification> ManualCertificationFaker = new Faker<Certification>()
            .RuleFor(c => c.CollegeCode, f => f.Random.Int(1, 5))
            .RuleFor(c => c.LicenseNumber, f => f.Random.Int(100000, 999999).ToString().Substring(1))
            .RuleFor(c => c.LicenseCode, f => f.Random.Int(3, 3))
            .RuleFor(c => c.RenewalDate, f => f.Date.Future(1))
            .RuleFor(c => c.PracticeCode, f => f.Random.Int(1, 4));

        public static Faker<Job> JobFaker = new Faker<Job>()
            .RuleFor(j => j.Title, f => f.Random.Word());

        public static Faker<EnrolleeOrganizationType> EnrolleeOrganizationTypeFaker = new Faker<EnrolleeOrganizationType>()
            .RuleFor(o => o.OrganizationTypeCode, f => f.Random.Int(1, 2));

        public static Faker<EnrolmentStatus> EnrolmentStatusFaker = new Faker<EnrolmentStatus>()
            .RuleFor(es => es.StatusCode, f => (int)StatusType.Editable)
            // .RuleFor(es => es.Status, f => new Status { Code = Status.ACTIVE_CODE, Name = "Active Code" })
            .RuleFor(es => es.StatusDate, f => DateTime.Now);

        public static Faker<Enrollee> EnrolleeFaker = new Faker<Enrollee>()
            .RuleFor(e => e.UserId, f => Guid.NewGuid())
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            .RuleFor(e => e.MiddleName, f => f.Name.FirstName())
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
            .RuleFor(e => e.PhysicalAddress, f => PhysicalAddressFaker.Generate())
            .RuleFor(e => e.MailingAddress, f => MailingAddressFaker.Generate())
            .RuleFor(e => e.Certifications, f => CertificationFaker.Generate(2))
            .RuleFor(e => e.DeviceProviderNumber, TestUtils.RandomDeviceProviderNumber())
            .RuleFor(e => e.IsInsulinPumpProvider, f => f.Random.Bool())
            .RuleFor(e => e.Jobs, f => JobFaker.Generate(2))
            .RuleFor(e => e.EnrolleeOrganizationTypes, f => EnrolleeOrganizationTypeFaker.Generate(2))
            .RuleFor(e => e.EnrolmentStatuses, f => EnrolmentStatusFaker.Generate(1))
            .RuleFor(e => e.IdentityAssuranceLevel, f => 3);

        public static Faker<AccessTerm> AccessTermFaker = new Faker<AccessTerm>()
            .RuleFor(x => x.EnrolleeId, f => f.Random.Int(1, 5))
            .RuleFor(x => x.GlobalClauseId, f => f.Random.Int(1, 5))
            .RuleFor(x => x.UserClauseId, f => f.Random.Int(1, 5))
            .RuleFor(x => x.LimitsConditionsClauseId, f => f.Random.Int(1, 5))
            .RuleFor(es => es.CreatedDate, f => DateTime.Now.AddDays(-5))
            .RuleFor(es => es.AcceptedDate, f => DateTime.Now)
            .RuleFor(es => es.ExpiryDate, f => DateTime.Now.AddYears(1));

        public static Faker<EnrolleeProfileVersion> EnrolleeProfileVersionFaker = new Faker<EnrolleeProfileVersion>()
            .RuleFor(x => x.EnrolleeId, f => f.Random.Int(1, 5))
            .RuleFor(x => x.ProfileSnapshot, f => JObject.FromObject(EnrolleeFaker.Generate()))
            .RuleFor(x => x.CreatedDate, f => DateTime.Now.AddDays(-5));

        public static string RandomProvinceCode(params string[] excludedProvinceCodes)
        {
            return new Faker().PickRandom(provinces.Except(excludedProvinceCodes));
        }

        public static string RandomDeviceProviderNumber()
        {
            return new Faker().Random.Int(100000, 999999).ToString().Substring(1);
        }

        public static void AddAdminRoleToUser(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim(ClaimTypes.Role, AuthConstants.PRIME_ADMIN_ROLE));
        }

        public static void RemoveAdminRoleFromUser(ClaimsPrincipal user)
        {
            var claim = user.Claims
                .Where(c => c.Value == AuthConstants.PRIME_ADMIN_ROLE)
                .Single();
            var identity = user.Identity as ClaimsIdentity;
            identity.RemoveClaim(claim);
        }

        public static void InitializeDbForTests(ApiDbContext db)
        {
            // db.Enrollees.AddRange(EnrolleeFaker.Generate(5));

            if (!db.Set<College>().Any())
            {
                db.AddRange(new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91" });
                db.AddRange(new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1" });
                db.AddRange(new College { Code = 3, Name = "College of Registered Nurses of BC (CRNBC)", Prefix = "96" });
                db.AddRange(new College { Code = 4, Name = "None", Prefix = null });
            }

            if (!db.Set<License>().Any())
            {
                db.AddRange(new License { Code = 1, Manual = false, Validate = true, Name = "Full - Family" });
                db.AddRange(new License { Code = 2, Manual = false, Validate = true, Name = "Full - Specialty" });
                db.AddRange(new License { Code = 3, Manual = true, Validate = true, Name = "Special" });
                db.AddRange(new License { Code = 4, Manual = false, Validate = true, Name = "Osteopathic" });
                db.AddRange(new License { Code = 5, Manual = true, Validate = true, Name = "Provisional - Family" });
            }

            if (!db.Set<CollegeLicense>().Any())
            {
                db.AddRange(new CollegeLicense { CollegeCode = 1, LicenseCode = 2 });
                db.AddRange(new CollegeLicense { CollegeCode = 1, LicenseCode = 3 });
                db.AddRange(new CollegeLicense { CollegeCode = 2, LicenseCode = 4 });
                db.AddRange(new CollegeLicense { CollegeCode = 2, LicenseCode = 5 });
                db.AddRange(new CollegeLicense { CollegeCode = 3, LicenseCode = 1 });
                db.AddRange(new CollegeLicense { CollegeCode = 3, LicenseCode = 5 });
            }

            if (!db.Set<Practice>().Any())
            {
                db.AddRange(new Practice { Code = 1, Name = "Remote Practice" });
                db.AddRange(new Practice { Code = 2, Name = "Reproductive Care" });
                db.AddRange(new Practice { Code = 3, Name = "Sexually Transmitted Infections (STI)" });
                db.AddRange(new Practice { Code = 4, Name = "None" });
            }

            if (!db.Set<JobName>().Any())
            {
                db.AddRange(new JobName { Code = 1, Name = "Medical Office Assistant" });
                db.AddRange(new JobName { Code = 2, Name = "Pharmacy Assistant" });
                db.AddRange(new JobName { Code = 3, Name = "Registration Clerk" });
                db.AddRange(new JobName { Code = 4, Name = "Ward Clerk" });
            }

            if (!db.Set<OrganizationType>().Any())
            {
                db.AddRange(new OrganizationType { Code = 1, Name = "Health Authority" });
                db.AddRange(new OrganizationType { Code = 2, Name = "Pharmacy" });
            }

            if (!db.Set<Status>().Any())
            {
                db.AddRange(new Status { Code = (int)StatusType.Editable, Name = "Editable" });
                db.AddRange(new Status { Code = (int)StatusType.UnderReview, Name = "Under Review" });
                db.AddRange(new Status { Code = (int)StatusType.RequiresToa, Name = "Requires TOA" });
                db.AddRange(new Status { Code = (int)StatusType.Locked, Name = "Locked" });
            }

            if (!db.Set<Country>().Any())
            {
                db.AddRange(new Country { Code = "CA", Name = "Canada" });
            }

            if (!db.Set<Province>().Any())
            {
                db.AddRange(new Province { Code = "AB", Name = "Alberta" });
                db.AddRange(new Province { Code = "BC", Name = "British Columbia" });
                db.AddRange(new Province { Code = "MB", Name = "Manitoba" });
                db.AddRange(new Province { Code = "NB", Name = "New Brunswick" });
                db.AddRange(new Province { Code = "NL", Name = "Newfoundland and Labrador" });
                db.AddRange(new Province { Code = "NS", Name = "Nova Scotia" });
                db.AddRange(new Province { Code = "ON", Name = "Ontario" });
                db.AddRange(new Province { Code = "PE", Name = "Prince Edward Island" });
                db.AddRange(new Province { Code = "QC", Name = "Quebec" });
                db.AddRange(new Province { Code = "SK", Name = "Saskatchewan" });
                db.AddRange(new Province { Code = "NT", Name = "Northwest Territories" });
                db.AddRange(new Province { Code = "NU", Name = "Nunavut" });
                db.AddRange(new Province { Code = "YT", Name = "Yukon" });
            }

            if (!db.Set<StatusReason>().Any())
            {
                db.AddRange(
                    new StatusReason { Code = 1, Name = "Automatic" },
                    new StatusReason { Code = 2, Name = "Manual" },
                    new StatusReason { Code = 3, Name = "Could not verify College Licence with PharmaNet" },
                    new StatusReason { Code = 4, Name = "Not in PharmaNet" },
                    new StatusReason { Code = 5, Name = "Name Discrepancy with PharmaNet College Licence" },
                    new StatusReason { Code = 6, Name = "Birthdate Discrepancy with PharmaNet College Licence" },
                    new StatusReason { Code = 7, Name = "Listed as Non-Practicing on PharmaNet College Licence" },
                    new StatusReason { Code = 8, Name = "Insulin Pump Provider" },
                    new StatusReason { Code = 9, Name = "Licence Class" },
                    new StatusReason { Code = 10, Name = "Self Declaration" },
                    new StatusReason { Code = 11, Name = "Contact address or Identity Address Out of British Columbia" }
                );
            }

            if (!db.Set<PrivilegeGroup>().Any())
            {
                db.AddRange(new PrivilegeGroup { Code = 1, Name = "Submit and Access Claims" });
                db.AddRange(new PrivilegeGroup { Code = 2, Name = "Record Medical History" });
                db.AddRange(new PrivilegeGroup { Code = 3, Name = "Access Medical History" });
                db.AddRange(new PrivilegeGroup { Code = 4, Name = "Role" });
                db.AddRange(new PrivilegeGroup { Code = 5, Name = "RU with OBOs" });
            }

            if (!db.Set<Privilege>().Any())
            {
                db.AddRange(new Privilege { Id = 1, PrivilegeGroupCode = 1, TransactionType = "TAC", Description = "Update Claims History" });
                db.AddRange(new Privilege { Id = 2, PrivilegeGroupCode = 1, TransactionType = "TDT", Description = "Query Claims History" });
                db.AddRange(new Privilege { Id = 3, PrivilegeGroupCode = 1, TransactionType = "TPM", Description = "Pt Profile Mail Request" });
                db.AddRange(new Privilege { Id = 4, PrivilegeGroupCode = 1, TransactionType = "TCP", Description = "Maintain Pt Keyword" });
                db.AddRange(new Privilege { Id = 5, PrivilegeGroupCode = 2, TransactionType = "TPH", Description = "New PHN" });
                db.AddRange(new Privilege { Id = 6, PrivilegeGroupCode = 2, TransactionType = "TPA", Description = "Address Update" });
                db.AddRange(new Privilege { Id = 7, PrivilegeGroupCode = 2, TransactionType = "TMU", Description = "Medication Update" });
                db.AddRange(new Privilege { Id = 8, PrivilegeGroupCode = 3, TransactionType = "TDR", Description = "Drug Monograph" });
                db.AddRange(new Privilege { Id = 9, PrivilegeGroupCode = 3, TransactionType = "TID", Description = "Patient Details" });
                db.AddRange(new Privilege { Id = 10, PrivilegeGroupCode = 3, TransactionType = "TIL", Description = "Location Details" });
                db.AddRange(new Privilege { Id = 11, PrivilegeGroupCode = 3, TransactionType = "TIP", Description = "Prescriber Details" });
                db.AddRange(new Privilege { Id = 12, PrivilegeGroupCode = 3, TransactionType = "TPN", Description = "Name Search" });
                db.AddRange(new Privilege { Id = 13, PrivilegeGroupCode = 3, TransactionType = "TRP", Description = "Pt Profile Request" });
                db.AddRange(new Privilege { Id = 14, PrivilegeGroupCode = 3, TransactionType = "TBR", Description = "Most Recent Profile" });
                db.AddRange(new Privilege { Id = 15, PrivilegeGroupCode = 3, TransactionType = "TRS", Description = "Filled Elsewhere Profile" });
                db.AddRange(new Privilege { Id = 16, PrivilegeGroupCode = 3, TransactionType = "TDU", Description = "DUE Inquiry" });
                db.AddRange(new Privilege { Id = 17, PrivilegeGroupCode = 4, TransactionType = "RU", Description = "Registered User" });
                db.AddRange(new Privilege { Id = 18, PrivilegeGroupCode = 4, TransactionType = "OBO", Description = "On Behalf of User" });
                db.AddRange(new Privilege { Id = 19, PrivilegeGroupCode = 5, TransactionType = "RU with OBOs", Description = "Registered User who can have OBOs" });
            }

            if (!db.Set<DefaultPrivilege>().Any())
            {
                // Non-practicing Licensed Practical Nurse
                db.AddRange(new DefaultPrivilege { LicenseCode = 54, PrivilegeId = 18 });

                // Full Pharmacist
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 1 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 2 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 3 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 4 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 5 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 6 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 7 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 8 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 9 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 10 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 11 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 12 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 13 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 14 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 15 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 16 });
                db.AddRange(new DefaultPrivilege { LicenseCode = 25, PrivilegeId = 17 });
            }

            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApiDbContext db)
        {
            // db.Enrollees.RemoveRange(db.Enrollees);
            InitializeDbForTests(db);
        }


        public static StringContent GetStringContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }

        public static async Task<string> GetBodyFromResponse(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        public static T DeserializeBody<T>(string body)
        {
            return JsonConvert.DeserializeObject<T>(body);
        }

        public static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            return DeserializeBody<T>(await GetBodyFromResponse(response));
        }

        public static void DetachAllEntities(ApiDbContext _dbContext)
        {
            var changedEntriesCopy = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted ||
                            e.State == EntityState.Unchanged)
                .ToList();

            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }

        public static BearerTokenBuilder TokenBuilder()
        {
            return new BearerTokenBuilder()
                        .ForAudience(TestAuthorizationConstants.Audience)
                        .IssuedBy(TestAuthorizationConstants.Issuer)
                        .WithSigningCertificate(EmbeddedResourceReader.GetCertificate(TestAuthorizationConstants.CertificatePassword));
        }

        public static HttpRequestMessage CreateRequest(
            HttpMethod method,
            string requestUri,
            Guid subject)
        {
            return CreateRequest<string>(method, requestUri, subject, null);
        }

        public static HttpRequestMessage CreateAdminRequest(
            HttpMethod method,
            string requestUri,
            Guid subject)
        {
            return CreateAdminRequest<string>(method, requestUri, subject, null);
        }

        public static HttpRequestMessage CreateRequest<T>(
            HttpMethod method,
            string requestUri,
            Guid subject,
            T payload)
        {
            // create a request with an AUTH token
            var request = new HttpRequestMessage(method, requestUri);
            var _token = TestUtils.TokenBuilder()
                .ForAudience(Startup.StaticConfig["Jwt:Audience"])
                .ForSubject(subject.ToString())
                .WithClaim(ClaimTypes.Role, AuthConstants.PRIME_ENROLLEE_ROLE)
                .WithClaim(AuthConstants.ASSURANCE_LEVEL_CLAIM_TYPE, "3")
                .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            if (payload != null)
            {
                request.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            }

            return request;
        }

        public static HttpRequestMessage CreateAdminRequest<T>(
            HttpMethod method,
            string requestUri,
            Guid subject,
            T payload)
        {
            var request = CreateRequest<T>(method, requestUri, subject, payload);

            var audience = System.Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            if (audience == null)
            {
                audience = Startup.StaticConfig["Jwt:Audience"];
            }

            // replace the token - with an admin version of the token
            var _token = TestUtils.TokenBuilder()
                 .ForAudience(audience)
                 .ForSubject(subject.ToString())
                 .WithClaim(ClaimTypes.Role, AuthConstants.PRIME_ADMIN_ROLE)
                 .WithClaim(ClaimTypes.Role, AuthConstants.PRIME_READONLY_ADMIN)
                 .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            return request;
        }


        public static HttpRequestMessage CreateSuperAdminRequest<T>(
            HttpMethod method,
            string requestUri,
            Guid subject,
            T payload)
        {
            var request = CreateRequest<T>(method, requestUri, subject, payload);

            var audience = System.Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            if (audience == null)
            {
                audience = Startup.StaticConfig["Jwt:Audience"];
            }

            // replace the token - with an admin version of the token
            var _token = TestUtils.TokenBuilder()
                 .ForAudience(audience)
                 .ForSubject(subject.ToString())
                 .WithClaim(ClaimTypes.Role, AuthConstants.PRIME_SUPER_ADMIN_ROLE)
                 .BuildToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", _token);

            return request;
        }

    }
}
