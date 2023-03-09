using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class UserSuccess:BasicEntities
    {
        public string date { get; set; }
        public string hours { get; set; }
        public int assignTasksId { get; set; }
        public AssignTasks assignTasks { get; set; }
        public string success { get; set; }
    }
}
