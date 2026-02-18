using DesktopApp.Helpers;
using DesktopApp.ViewModels.SingleObjectViewModels;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels.AbstractViewModels
{
    public abstract class DeleteViewModel<T> : SingleViewModel<T>
        where T : class
    {
        protected DeleteViewModel(string displayName)
            : base(displayName)
        {
        }

        protected abstract string RefreshMessageTag { get; }     // Każdy Delete View Model musi przesłac nazwę komunikatu refresh

        public ICommand DeleteCommand => new BaseCommand(Delete);
        public ICommand CancelCommand => new BaseCommand(Cancel);

        protected virtual void Delete()
        {
            if (item == null)
                return;

           
            var result = MessageBox.Show(
                "Do you want to delete this item?",
                "Confirm delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            
            var property = item.GetType().GetProperty("IsActive");// ustawienie IsActive = false
            if (property != null)
            {
                property.SetValue(item, false);
            }

            
            sharedData_Entities.Update(item);// aktualizacja rekordu w bazie danych
            sharedData_Entities.SaveChanges();


            Messenger.Default.Send("refresh", DisplayName);// odświeżenie listy wskazanej przez nazwę zakładki


            OnRequestClose();// zamknięcie okna
        }

        protected void Cancel()
        {
            OnRequestClose();
        }
    }
}
