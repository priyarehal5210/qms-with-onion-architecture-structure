using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Dtos
{
    public class AssignTasksDto
    {
        public int Id { get; set; }

        public int registeredUsersId { get; set; }
        public int tasksId { get; set; }

    }
}
