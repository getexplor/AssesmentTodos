using MediatR;
using AssesmentTodo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Application.Features.Todo.Commands.Create
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTodoCommandHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var duplicateTodo = _dbContext.Todos.Any(x => x.Name == request.Name);

            if (duplicateTodo)
            {
                throw new BadRequestException("Name Cannot Duplicate");
            }

            _dbContext.Todos.Add(new Domain.Todo()
            {
                Name = request.Name,
                Description = request.Description
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
