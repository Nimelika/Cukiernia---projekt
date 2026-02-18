using DesktopApp.Helpers;
using DesktopApp.ViewModels.SingleObjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApp.ViewModels.AbstractViewModels
{
    public abstract class DisplayViewModel<T> : SingleViewModel<T>
        where T : class
    {
        protected DisplayViewModel(string displayName)
            : base(displayName)
        {
        }
        public ICommand ReturnCommand => new BaseCommand(Return);
        protected void Return()
        {
            OnRequestClose();
        }
    }
}
