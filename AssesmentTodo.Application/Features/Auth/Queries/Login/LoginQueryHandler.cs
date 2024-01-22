using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Application.Features.Auth
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IJwtProvider _jwtProvider;

        public LoginQueryHandler(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        private List<UserModel> _users = new List<UserModel>()
        {
            new UserModel
            {
                IdUser = Guid.Parse("808694be-c57c-4b76-b70d-a8f149c2264e"),
                UserName = "Agung",
                FullName = "Agung Ramadhan Aghnaddinar",
                Password = "password123"
            }
        };

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var query = _users.FirstOrDefault(x => x.UserName!.ToLower().Contains(request.UserName!.ToLower()) && x.Password!.ToLower().Contains(request.Password!.ToLower()));

            if (query == null)
            {
                throw new BadRequestException("username or password is incorrect");
            }

            var token = await _jwtProvider.GenerateToken(query);

            return token;
        }
    }
}
