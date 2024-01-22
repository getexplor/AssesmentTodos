using AssesmentTodo.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Infrastructure
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IdUser = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimConstant.IdUser);
            UserName = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimConstant.UserName);
            FullName = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimConstant.FullName);
        }

        public string? IdUser { get; }

        public string? UserName {  get; }

        public string? FullName { get; }
    }
}
