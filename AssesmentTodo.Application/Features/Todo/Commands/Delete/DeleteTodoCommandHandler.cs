using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssesmentTodo.Domain;

namespace AssesmentTodo.Application.Features
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteTodoCommandHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive == StatusDataEnum.Active);
            if (query == null)
            {
                throw new NotFoundException("Data Not Found !");
            }

            query.IsActive = StatusDataEnum.InActive;

            _dbContext.Todos.Update(query);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
