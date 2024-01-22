using AssesmentTodo.IntegrationTest.Features.Todo;
using FluentAssertions;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssesmentTodo.Application.Features;

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
            var data = response.Content.ReadAsAsync<UserModel>();
            data.Should().NotBeNull();
        }
    }
}
