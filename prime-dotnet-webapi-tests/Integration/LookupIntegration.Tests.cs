using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Prime.Models;
using PrimeTests.Utils;
using Xunit;

namespace PrimeTests.Integration
{
    public class LookupIntegrationTests : BaseIntegrationTests
    {
        public LookupIntegrationTests(CustomWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [Fact]
        public async void testGetLookups()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                // create a request with an AUTH token
                var request = TestUtils.CreateRequest(HttpMethod.Get, "/api/lookups", Guid.NewGuid());

                // send the request
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var apiResponse = TestUtils.DeserializeResponse<ApiOkResponse<LookupEntity>>(response).Result;
                Assert.NotNull(apiResponse);
                Assert.NotEmpty(apiResponse.Result.Colleges);
            }
        }
    }
}