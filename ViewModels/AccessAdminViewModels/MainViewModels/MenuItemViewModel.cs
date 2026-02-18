using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApp.ViewModels.AccessAdminViewModels.MainViewModels
{
    public class MenuItemViewModel
    {
        public string Header { get; }
        public string ModuleCode { get; }
        public ICommand Command { get; }

        public MenuItemViewModel(string header, string moduleCode, ICommand command)
        {
            Header = header;
            ModuleCode = moduleCode;
            Command = command;
        }
    }

}
