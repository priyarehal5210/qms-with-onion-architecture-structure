using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Dtos
{
    public class UserSuccessDto
    {
        public int Id { get; set; }

        public string date { get; set; }
        public string hours { get; set; }
        public int assignTasksId { get; set; }
        public string success { get; set; }
    }
}
