using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssesmentTodo.Domain;

namespace AssesmentTodo.Application.Features.Todo.Queries.GetById
{
    public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoModel>
    {
        private IApplicationDbContext _dbContext;

        public GetTodoByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<TodoModel> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive == StatusDataEnum.Active, cancellationToken);

            if (query == null)
            {
                throw new NotFoundException("Todo Not Found !");
            }

            return new TodoModel
            {
                Id = query.Id,
                Name = query.Name,
                Description = query.Description,
                IsDone = query.IsDone,
                DoneAt = query.DoneAt,
                CreatedAt = query.CreatedAt,
                UpdatedAt = query.UpdatedAt,
                CreatedBy = query.CreatedBy,
                UpdatedBy = query.UpdatedBy,
                IsActive = query.IsActive,
            };
        }
    }
}
