using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.EntitiesForView
{
    public class UserAccessForView
    {
        public int Position { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string RoleName { get; set; }
        public string Modules { get; set; } 
    }

}
