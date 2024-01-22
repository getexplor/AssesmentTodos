using MediatR;
using Microsoft.EntityFrameworkCore;
using AssesmentTodo.Domain;

namespace AssesmentTodo.Application.Features.Todo.Queries.GetListTodo
{
    public class GetListTodoQueryHandler : IRequestHandler<GetListTodoQuery, List<TodoModel>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetListTodoQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<TodoModel>> Handle(GetListTodoQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Todos.Where(x => x.IsActive == StatusDataEnum.Active)
                .Select(x => new TodoModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsDone = x.IsDone,
                    DoneAt = x.DoneAt,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt.HasValue ? x.UpdatedAt.Value : null,
                    UpdatedBy = x.UpdatedBy,
                    IsActive = x.IsActive
                }).ToListAsync(cancellationToken);
        }
    }
}
