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

namespace AssesmentTodo.IntegrationTest.Features.Todo.Create
{
    public class CreateTodoTest : Setup
    {
        [Fact]
        public async Task CreateTodo_ShoudBe_ExpectedOk()
        {
            // Arrange
            await Authentication();

            // Act
            var content = new CreateTodoCommand("test integration test 17", "test integration test");
            var json = JsonContent.Create(content);
            var response = await _httpClient.PostAsync(ConstStringUrl.TodoUrl, json);

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
                result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task CreateTodo_ShoudBe_BadRequest()
        {
            // Arrange
            await Authentication();

            // Act
            var content = new CreateTodoCommand("test integration test 3", "test integration test");
            var json = JsonContent.Create(content);
            var response = await _httpClient.PostAsync(ConstStringUrl.TodoUrl, json);

            // Assert
            var result = await response.Content.ReadAsAsync<BaseResponse>();
            result.IsSuccess.Should().BeFalse();
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

    }
}
