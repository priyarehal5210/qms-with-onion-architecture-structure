using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class AssignTasks:BasicEntities
    {
        public int registeredUsersId { get; set; }
        public RegisteredUsers registeredUsers { get; set; }
        public int tasksId { get; set; }
        public Tasks tasks { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public status Status { get; set; }

        public bool isChecked { get; set; }
        public enum status
        {
            notstarted,
            started,
            progress,
            completed
        }
    }
}
