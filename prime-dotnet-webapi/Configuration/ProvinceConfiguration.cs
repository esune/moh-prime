using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class ProvinceConfiguration : SeededTable<Province>
    {
        public override ICollection<Province> SeedData
        {
            get
            {
                return new[] {
                new Province { Code = "AB", CountryCode = "CA", Name = "Alberta", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "BC", CountryCode = "CA", Name = "British Columbia", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MB", CountryCode = "CA", Name = "Manitoba", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NB", CountryCode = "CA", Name = "New Brunswick", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NL", CountryCode = "CA", Name = "Newfoundland and Labrador", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NS", CountryCode = "CA", Name = "Nova Scotia", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "ON", CountryCode = "CA", Name = "Ontario", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "PE", CountryCode = "CA", Name = "Prince Edward Island", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "QC", CountryCode = "CA", Name = "Quebec", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "SK", CountryCode = "CA", Name = "Saskatchewan", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NT", CountryCode = "CA", Name = "Northwest Territories", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NU", CountryCode = "CA", Name = "Nunavut", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "YT", CountryCode = "CA", Name = "Yukon", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "AL", CountryCode = "US", Name = "Alabama", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "AK", CountryCode = "US", Name = "Alaska", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "AS", CountryCode = "US", Name = "American Samoa", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "AZ", CountryCode = "US", Name = "Arizona", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "AR", CountryCode = "US", Name = "Arkansas", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "CA", CountryCode = "US", Name = "California", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "CO", CountryCode = "US", Name = "Colorado", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "CT", CountryCode = "US", Name = "Connecticut", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "DE", CountryCode = "US", Name = "Delaware", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "DC", CountryCode = "US", Name = "District of Columbia", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "FL", CountryCode = "US", Name = "Florida", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "GA", CountryCode = "US", Name = "Georgia", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "GU", CountryCode = "US", Name = "Guam", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "HI", CountryCode = "US", Name = "Hawaii", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "ID", CountryCode = "US", Name = "Idaho", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "IL", CountryCode = "US", Name = "Illinois", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "IN", CountryCode = "US", Name = "Indiana", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "IA", CountryCode = "US", Name = "Iowa", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "KS", CountryCode = "US", Name = "Kansas", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "KY", CountryCode = "US", Name = "Kentucky", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "LA", CountryCode = "US", Name = "Louisiana", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "ME", CountryCode = "US", Name = "Maine", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MD", CountryCode = "US", Name = "Maryland", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MA", CountryCode = "US", Name = "Massachusetts", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MI", CountryCode = "US", Name = "Michigan", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MN", CountryCode = "US", Name = "Minnesota", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MS", CountryCode = "US", Name = "Mississippi", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MO", CountryCode = "US", Name = "Missouri", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MT", CountryCode = "US", Name = "Montana", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NE", CountryCode = "US", Name = "Nebraska", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NV", CountryCode = "US", Name = "Nevada", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NH", CountryCode = "US", Name = "New Hampshire", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NJ", CountryCode = "US", Name = "New Jersey", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NM", CountryCode = "US", Name = "New Mexico", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NY", CountryCode = "US", Name = "New York", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "NC", CountryCode = "US", Name = "North Carolina", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "ND", CountryCode = "US", Name = "North Dakota", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "MP", CountryCode = "US", Name = "Northern Mariana Islands", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "OH", CountryCode = "US", Name = "Ohio", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "OK", CountryCode = "US", Name = "Oklahoma", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "OR", CountryCode = "US", Name = "Oregon", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "PA", CountryCode = "US", Name = "Pennsylvania", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "PR", CountryCode = "US", Name = "Puerto Rico", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "RI", CountryCode = "US", Name = "Rhode Island", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "SC", CountryCode = "US", Name = "South Carolina", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "SD", CountryCode = "US", Name = "South Dakota", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "TN", CountryCode = "US", Name = "Tennessee", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "TX", CountryCode = "US", Name = "Texas", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "UM", CountryCode = "US", Name = "United States Minor Outlying Islands", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "UT", CountryCode = "US", Name = "Utah", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "VT", CountryCode = "US", Name = "Vermont", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "VI", CountryCode = "US", Name = "Virgin Islands, U.S.", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "VA", CountryCode = "US", Name = "Virginia", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "WA", CountryCode = "US", Name = "Washington", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "WV", CountryCode = "US", Name = "West Virginia", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "WI", CountryCode = "US", Name = "Wisconsin", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Province { Code = "WY", CountryCode = "US", Name = "Wyoming", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
                };
            }
        }
    }
}
