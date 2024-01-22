using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Application.Features
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public UpdateTodoCommandHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
        public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (query == null) throw new NotFoundException("Data Not Found !");

            var duplicateName = _dbContext.Todos.Any(x => x.Id != request.Id && x.Name == request.Name);

            if (duplicateName) throw new BadRequestException("Name Already Exsist !");


            query.Description = request.Description;
            query.Name = request.Name;
            query.IsDone = request.IsDone;
            query.DoneAt = request.IsDone ? DateTime.UtcNow : null;

            _dbContext.Todos.Update(query);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
