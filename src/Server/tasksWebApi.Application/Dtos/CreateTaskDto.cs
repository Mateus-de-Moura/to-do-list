using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tasksWebApi.Application.Dtos
{
    public class CreateTaskDto
    {
        public bool Completed { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
