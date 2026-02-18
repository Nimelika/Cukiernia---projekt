using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DesktopApp.ViewModels.SingleObjectViewModels;

namespace DesktopApp.ViewModels.AbstractViewModels

{
    public abstract class CreateViewModel<T>
        : SingleViewModel<T>
        where T : class, new()
    {
        protected CreateViewModel(string displayName)
            : base(displayName)
        {
            item = new T();
            InitializeNewItem();
        }

        // Możliwość ustawienia wartości domyślnych (np. IsActive = true)
        protected virtual void InitializeNewItem() { }

        

        
    }
}

