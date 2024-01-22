using AssesmentTodo.Application;
using AssesmentTodo.Application.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentTodo.WebApi.Controllers.v1
{
    [Authorize]
    public class TodoController : BaseApiController
    {
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpGet("list")]
        public async Task<ActionResult> GetListTodo()
        {
            return Ok(await Mediator.Send(new GetListTodoQuery()));
        }
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTodoById(Guid id)
        {
            return Ok(await Mediator.Send(new GetTodoByIdQuery()
            {
                Id = id
            }));
        }
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] CreateTodoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpPatch]
        public async Task<ActionResult> UpdateTodo([FromBody] UpdateTodoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteTodoCommand()
            {
                Id = id
            }));
        }
    }
}
