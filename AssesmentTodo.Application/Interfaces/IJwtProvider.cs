using AssesmentTodo.Application.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Application
{
    public interface IJwtProvider
    {
        Task<string> GenerateToken(UserModel user);
    }
}
