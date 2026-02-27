using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.AccessAdminViewModels.RoleAccessViewModels
{
    public class ModuleAccessForView
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool HasAccess { get; set; }
    }
}
