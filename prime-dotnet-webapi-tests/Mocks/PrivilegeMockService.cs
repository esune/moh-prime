using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class PrivilegeServiceMock : BaseMockService, IPrivilegeService
    {
        public PrivilegeServiceMock() : base()
        { }

        public Task AssignPrivilegesToEnrolleeAsync(int enrolleeId, Enrollee enrollee)
        {
            // TODO Finish test, currently just sending enrollee back
            // throw new System.NotImplementedException();
            return Task.FromResult(this.GetHolder<int, Enrollee>().Values?.SingleOrDefault(e => e.UserId == enrollee.UserId));
        }

        public Task<ICollection<AssignedPrivilege>> GetAssignedPrivilegesForEnrolleeAsync(int enrolleeId)
        {
            return Task.FromResult<ICollection<AssignedPrivilege>>(null);
        }

        public Task<ICollection<Privilege>> GetPrivilegesForEnrolleeAsync(Enrollee enrollee)
        {
            return Task.FromResult<ICollection<Privilege>>(null);
        }

        public override void SeedData()
        {
            // no data to seed, as it is done in the base class
        }
    }
}
