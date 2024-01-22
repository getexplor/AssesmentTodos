using AssesmentTodo.IntegrationTest.Features.Todo;
using FluentAssertions;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssesmentTodo.Application.Features;
using AssesmentTodo.Infrastructure;
using Newtonsoft.Json;

namespace AssesmentTodo.IntegrationTest.Features.Auth.Queries
{
    public class GetMeTest : Setup
    {
        [Fact]
        public async Task GetMe_Should_Be_Not_Null()
        {
            // Arrange
            await Authentication();

            // Act
            var response = await _httpClient.GetAsync($"{ConstStringUrl.AuthUrl}/me");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var data = await response.Content.ReadAsAsync<BaseResponse>();
            data.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var payload = JsonConvert.DeserializeObject<UserModel>(data.Payload.ToString());
            payload.Should().NotBeNull();
        }
    }
}
