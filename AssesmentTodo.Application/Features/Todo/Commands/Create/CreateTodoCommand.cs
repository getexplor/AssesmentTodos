using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Application.Features
{
    public record CreateTodoCommand(string Name, string Description) : IRequest;
}
