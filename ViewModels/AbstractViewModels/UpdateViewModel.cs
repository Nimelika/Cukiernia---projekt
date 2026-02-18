using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DesktopApp.ViewModels.SingleObjectViewModels;

namespace DesktopApp.ViewModels.AbstractViewModels
{
    public abstract class UpdateViewModel<T>
        : SingleViewModel<T>, IDataErrorInfo
        where T : class
    {
        protected UpdateViewModel(string displayName)
            : base(displayName)
        {
        }

        // IDataErrorInfo
        public string Error => null;

        public string this[string propertyName]
        {
            get => ValidateProperty(propertyName);
        }

        // Każdy ViewModel musi zaimplementować walidację swoich pól
        protected abstract string ValidateProperty(string propertyName);

        // Każdy ViewModel musi zaimplementować ładowanie obiektu
        public abstract void Load(int id);

        // Ogólna walidacja
        public override bool IsValid()
        {
            // Pobieramy wszystkie właściwości publiczne
            var properties = GetType().GetProperties()
                .Where(p => p.CanRead);

            foreach (var prop in properties)
            {
                if (this[prop.Name] != null)
                    return false;
            }

            return true;
        }
    }
}

