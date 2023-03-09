using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Dtos
{
    public class TasksDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

    }
}
