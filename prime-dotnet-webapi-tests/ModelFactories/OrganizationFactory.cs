using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class EnrolleeOrganizationTypeFactory : Faker<EnrolleeOrganizationType>
    {
        private static int IdCounter = 1;

        public EnrolleeOrganizationTypeFactory(Enrollee owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.OrganizationTypeCode, f => f.PickRandom(OrganizationTypeLookup.All).Code);

            Ignore(x => x.OrganizationType);
        }
    }
}
