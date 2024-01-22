using AssesmentTodo.Application.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using AssesmentTodo.Infrastructure;

namespace AssesmentTodo.IntegrationTest.Features.Todo.Command
{
    public class UpdateTodoTest : Setup
    {
        [Fact]
        public async Task UpdateTodo_ShoudBe_ResponseNull()
        {
            // Arrange
            await Authentication();

            // Act
            var content = new UpdateTodoCommand(Guid.Parse("4EAF3BAA-8BA8-4371-BB18-F19FC61C7E27"), "test integration test 10", "test integration test", false);
            var json = JsonContent.Create(content);
            var response = await _httpClient.PatchAsync(ConstStringUrl.TodoUrl, json);

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = await response.Content.ReadAsStringAsync();
            data.Should().Be("{}");
        }

        [Fact]
        public async Task UpdateTodo_ShoudBe_BadRequest()
        {
            // Arrange
            await Authentication();

            // Act
            var content = new UpdateTodoCommand(Guid.Parse("4EAF3BAA-8BA8-4371-BB18-F19FC61C7E27"), "test integration test 4", "test integration test", false);
            var json = JsonContent.Create(content);
            var response = await _httpClient.PatchAsync(ConstStringUrl.TodoUrl, json);

            // Assert
            var result = await response.Content.ReadAsAsync<BaseResponse>();
            result.IsSuccess.Should().BeFalse();
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateTodo_ShoudBe_NotFound()
        {
            // Arrange
            await Authentication();

            // Act
            var content = new UpdateTodoCommand(Guid.Parse("4EAF3BAA-8BA8-4371-BB18-F19FC61C1234"), "test integration test 4", "test integration test", false);
            var json = JsonContent.Create(content);
            var response = await _httpClient.PatchAsync(ConstStringUrl.TodoUrl, json);

            // Assert
            var result = await response.Content.ReadAsAsync<BaseResponse>();
            result.IsSuccess.Should().BeFalse();
            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }
}
