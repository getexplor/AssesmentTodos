using AssesmentTodo.IntegrationTest.Features.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using AssesmentTodo.Infrastructure;
using AssesmentTodo.Application.Features;
using Newtonsoft.Json;

namespace AssesmentTodo.IntegrationTest.Features.Auth.Queries
{
    public class LoginTest : Setup
    {
        [Fact]
        public async Task Login_Should_Be_Not_Null()
        {
            // Arrange
            var content = new
            {
                userName = "agung",
                password = "password123"
            };

            // Act
            var json = JsonContent.Create(content);
            var response = await _httpClient.PostAsync($"{ConstStringUrl.AuthUrl}/login", json);

            // Assert
            var data = await response.Content.ReadAsAsync<BaseResponse>();
            data.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var payload = data.Payload.ToString();
            payload.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_Should_Be_BadRequest()
        {
            var content = new
            {
                userName = "agung",
                password = "password1234"
            };

            var json = JsonContent.Create(content);
            var response = await _httpClient.PostAsync($"{ConstStringUrl.AuthUrl}/login", json);

            var result = await response.Content.ReadAsAsync<BaseResponse>();
            result.IsSuccess.Should().BeFalse();
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}