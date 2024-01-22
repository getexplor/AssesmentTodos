using AssesmentTodo.Application;
using AssesmentTodo.Application.Features;
using AssesmentTodo.Infrastructure;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.IntegrationTest.Features.Todo.Command
{
    public class DeleteTodoTest : Setup
    {
        [Theory]
        [InlineData("BC0CF084-56E4-48E7-B4F4-955946F24F0C")]
        public async Task DeleteTodo_ById_ShoudBe_ExpectedOk(string id)
        {
            // Arrange
            await Authentication();

            // Act
            var response = await _httpClient.DeleteAsync($"{ConstStringUrl.TodoUrl}/{id}");

            // Assert
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<BaseResponse>();
                data.StatusCode.Should().Be((int)HttpStatusCode.OK);

                data.Payload.Should().BeNull();
            }
            else
            {
                var result = await response.Content.ReadAsAsync<BaseResponse>();
                result.IsSuccess.Should().BeFalse();
                result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            }
        }

        [Theory]
        [InlineData("BFF84DAD-FED0-4D6A-B4C5-01C7C70601D1")]
        public async Task DeleteTodo_ShoudBe_NotFound(string id)
        {
            // Arrange
            await Authentication();

            // Act
            var response = await _httpClient.DeleteAsync($"{ConstStringUrl.TodoUrl}/{id}");

            // Assert
            var result = await response.Content.ReadAsAsync<BaseResponse>();
            result.IsSuccess.Should().BeFalse();
            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

    }
}
