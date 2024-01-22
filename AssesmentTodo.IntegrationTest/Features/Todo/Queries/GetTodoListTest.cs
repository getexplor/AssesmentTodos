using Microsoft.AspNetCore.Mvc.Testing;
using AssesmentTodo.WebApi;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentAssertions;
using System.Net;
using AssesmentTodo.Application;

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
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<TodoModel>>()).Should().NotBeNull();
        }
    }
}