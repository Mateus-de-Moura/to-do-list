using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tasksWebApi.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public bool Completed { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
