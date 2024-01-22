using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Domain
{
    public class Todo : BaseEntity
    {
        public Todo()
        {
            IsDone = false;
        }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DoneAt { get; set; }
    }
}
