using System.Collections.Generic;
using System.Linq;

using Prime.Models;

namespace PrimeTests.Mocks
{
    public abstract class BaseMockService
    {
        public const int DEFAULT_ENROLMENTS_SIZE = 5;
        public const int DEFAULT_ENROLLEES_SIZE = 5;

        private Dictionary<string, object> _fakeDb;

        private readonly string ENROLMENT_KEY = typeof(Enrollee).FullName;
        private readonly string ENROLLEE_KEY = typeof(Enrollee).FullName;
        private readonly string COLLEGE_KEY = typeof(College).FullName;
        private readonly string JOB_NAME_KEY = typeof(JobName).FullName;
        private readonly string LICENSE_KEY = typeof(License).FullName;
        private readonly string ORGANIZATION_TYPE_KEY = typeof(OrganizationType).FullName;
        private readonly string PRACTICE_KEY = typeof(Practice).FullName;
        private readonly string STATUS_KEY = typeof(Status).FullName;
        private readonly string COUNTRY_KEY = typeof(Country).FullName;
        private readonly string PROVINCE_KEY = typeof(Province).FullName;
        private readonly string STATUS_REASON_KEY = typeof(StatusReason).FullName;
        private readonly string PRIVILEGE_GROUP_KEY = typeof(PrivilegeGroup).FullName;
        private readonly string PRIVILEGE_TYPE_KEY = typeof(PrivilegeType).FullName;

        public BaseMockService()
        {
            this.InitializeDb();
        }

        public void InitializeDb()
        {
            // create the fake database object
            _fakeDb = new Dictionary<string, object>();

            // create the holder for each fake table type
            _fakeDb.Add(ENROLLEE_KEY, new Dictionary<int, Enrollee>());

            // seed the lookup tables
            _fakeDb.Add(COLLEGE_KEY, new Dictionary<int, College> {
                { 1, new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91" } },
                { 2, new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1" } },
                { 3, new College { Code = 3, Name = "College of Registered Nurses of BC (CRNBC)", Prefix = "96" } },
                { 4, new College { Code = 4, Name = "None", Prefix = null } }
            });

            _fakeDb.Add(LICENSE_KEY, new Dictionary<int, License> {
                { 1, new License { Code = 1, Name = "Full - General" } },
                { 2, new License { Code = 2, Name = "Full - Pharmacist" } },
                { 3, new License { Code = 3, Name = "Full - Specialty" } },
                { 4, new License { Code = 4, Name = "Registered Nurse" } },
                { 5, new License { Code = 5, Name = "Temporary Registered Nurse" } }
            });

            _fakeDb.Add(PRACTICE_KEY, new Dictionary<int, Practice> {
                { 1, new Practice { Code = 1, Name = "Remote Practice" } },
                { 2, new Practice { Code = 2, Name = "Reproductive Care" } },
                { 3, new Practice { Code = 3, Name = "Sexually Transmitted Infections (STI)" } },
                { 4, new Practice { Code = 4, Name = "None" } }
            });

            _fakeDb.Add(JOB_NAME_KEY, new Dictionary<int, JobName> {
                { 1, new JobName { Code = 1, Name = "Medical Office Assistant" } },
                { 4, new JobName { Code = 2, Name = "Pharmacy Assistant" } },
                { 6, new JobName { Code = 3, Name = "Registration Clerk" } },
                { 7, new JobName { Code = 4, Name = "Ward Clerk" } }
            });

            _fakeDb.Add(ORGANIZATION_TYPE_KEY, new Dictionary<int, OrganizationType> {
                { 1, new OrganizationType { Code = 1, Name = "Health Authority" } },
                { 2, new OrganizationType { Code = 2, Name = "Pharmacy" } }
            });

            _fakeDb.Add(COUNTRY_KEY, new Dictionary<int, Country> {
                { 1, new Country { Code = "CA", Name = "Canada" } }
            });

            _fakeDb.Add(PROVINCE_KEY, new Dictionary<int, Province> {
                { 1, new Province { Code = "AB", Name = "Alberta" } },
                { 2, new Province { Code = "BC", Name = "British Columbia" } },
                { 3, new Province { Code = "MB", Name = "Manitoba" } },
                { 4, new Province { Code = "NB", Name = "New Brunswick" } },
                { 5, new Province { Code = "NL", Name = "Newfoundland and Labrador" } },
                { 6, new Province { Code = "NS", Name = "Nova Scotia" } },
                { 7, new Province { Code = "ON", Name = "Ontario" } },
                { 8, new Province { Code = "PE", Name = "Prince Edward Island" } },
                { 9, new Province { Code = "QC", Name = "Quebec" } },
                { 10, new Province { Code = "SK", Name = "Saskatchewan" } },
                { 11, new Province { Code = "NT", Name = "Northwest Territories" } },
                { 12, new Province { Code = "NU", Name = "Nunavut" } },
                { 13, new Province { Code = "YT", Name = "Yukon" } }
            });

            _fakeDb.Add(STATUS_REASON_KEY, new Dictionary<int, StatusReason> {
                { 1, new StatusReason { Code = 1, Name = "Automatic" } },
                { 2, new StatusReason { Code = 2, Name = "Manual" } },
                { 3, new StatusReason { Code = 2, Name = "Name Discrepancy" } },
                { 4, new StatusReason { Code = 2, Name = "Not in PharmaNet" } },
                { 5, new StatusReason { Code = 2, Name = "Insulin Pump Provider" } },
                { 6, new StatusReason { Code = 2, Name = "Licence Class" } },
                { 7, new StatusReason { Code = 2, Name = "Self Declaration" } },
                { 8, new StatusReason { Code = 2, Name = "Contact address or Identity Address Out of British Columbia" } }
            });

            _fakeDb.Add(PRIVILEGE_GROUP_KEY, new Dictionary<int, PrivilegeGroup> {
                { 1, new PrivilegeGroup { Code = 4, PrivilegeTypeCode = 1, Name = "Roles" } }
            });

            _fakeDb.Add(PRIVILEGE_TYPE_KEY, new Dictionary<int, PrivilegeType> {
                { 1, new PrivilegeType { Code = 1, Name = "Role" } }
            });

            this.SeedData();
        }

        public abstract void SeedData();

        protected Dictionary<TKey, T> GetHolder<TKey, T>() where T : class
        {
            string key = typeof(T).FullName;
            return _fakeDb[key] as Dictionary<TKey, T>;
        }

        protected static int GetNewId(ICollection<int> ids)
        {
            return ids.DefaultIfEmpty(0).Max() + 1;
        }
    }
}
