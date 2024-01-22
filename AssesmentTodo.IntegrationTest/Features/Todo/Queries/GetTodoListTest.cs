using Microsoft.AspNetCore.Mvc.Testing;
using AssesmentTodo.WebApi;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentAssertions;
using System.Net;
using AssesmentTodo.Application;
using AssesmentTodo.Infrastructure;
using Newtonsoft.Json;

namespace AssesmentTodo.IntegrationTest.Features.Todo.Get
{
    public class GetTodoListTest : Setup
    {
        [Fact]
        public async Task GetTodoList_Shoud_BeNotNull()
        {
            // Arrange
            await Authentication();

            // Act
            var response = await _httpClient.GetAsync($"{ConstStringUrl.TodoUrl}/list");

            // Assert
            var data = await response.Content.ReadAsAsync<BaseResponse>();
            data.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var payload = JsonConvert.DeserializeObject<List<TodoModel>>(data.Payload.ToString());
            payload.Should().NotBeNull();
        }
    }
}