using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class RegisteredUsers:BasicEntities
    { 
        public string userName { get; set; }
        public string email { get; set; }
        public bool emailConfirm { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string role { get; set; }
        public bool approved { get; set; }
        [NotMapped]
        public string token { get; set; }
    }
}
