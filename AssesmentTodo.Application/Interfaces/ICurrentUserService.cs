using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Application
{
    public interface ICurrentUserService
    {
        string IdUser { get; }
        string UserName { get; }
        string FullName { get; }
    }
}
