using AssesmentTodo.Application.Features.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentTodo.WebApi.Controllers.v1
{
    public class AuthController : BaseApiController
    {
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "login")]
        public async Task<ActionResult> Login([FromBody] LoginQuery login)
        {
            return Ok(await Mediator.Send(login));
        }
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetMe()
        {
            var query = new GetMeQuery();

            return Ok(await Mediator.Send(query));
        }
    }
}
