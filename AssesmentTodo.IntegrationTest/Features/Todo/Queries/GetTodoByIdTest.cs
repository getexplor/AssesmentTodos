using AssesmentTodo.Application;
using AssesmentTodo.Application.Features;
using AssesmentTodo.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.IntegrationTest.Features.Todo.Get
{
    public class GetTodoByIdTest : Setup
    {
        [Theory]
        [InlineData("2DB535EF-C289-4E13-8E3D-227EF460D2BA")]
        public async Task GetTodo_ById_ShoudBe_NotNull_EqualId(string id)
        {
            // Arrange
            await Authentication();

            // Act
            var response = await _httpClient.GetAsync($"{ConstStringUrl.TodoUrl}/{id}");

            // Assert
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<BaseResponse>();
                data.StatusCode.Should().Be((int)HttpStatusCode.OK);

                var payload = JsonConvert.DeserializeObject<TodoModel>(data.Payload.ToString());
                payload.Should().NotBeNull();

                payload.Id.Should().Be(id);
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
        public async Task GetTodo_ById_ShoudBe_NotFound(string id)
        {
            // Arrange
            await Authentication();

            // Act
            var response = await _httpClient.GetAsync($"{ConstStringUrl.TodoUrl}/{id}");

            // Assert
            var result = await response.Content.ReadAsAsync<BaseResponse>();
            result.IsSuccess.Should().BeFalse();
            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }
}
