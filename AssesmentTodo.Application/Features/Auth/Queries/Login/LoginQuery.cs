using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Application.Features.Auth
{
    public class LoginQuery : IRequest<string>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
