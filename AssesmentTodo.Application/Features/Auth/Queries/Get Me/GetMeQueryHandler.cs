using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Application.Features.Auth.Queries.Get_Me
{
    public class GetMeQueryHandler : IRequestHandler<GetMeQuery, UserModel>
    {
        private readonly ICurrentUserService _currentUser;

        public GetMeQueryHandler(ICurrentUserService currentUser) => _currentUser = currentUser;

        public async Task<UserModel> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            return new UserModel()
            {
                IdUser = Guid.Parse(_currentUser.IdUser),
                UserName = _currentUser.UserName,
                FullName = _currentUser.FullName
            };
        }
    }
}
