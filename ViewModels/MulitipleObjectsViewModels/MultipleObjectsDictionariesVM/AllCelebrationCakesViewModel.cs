using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.CelebrationCakeVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllCelebrationCakesViewModel
        : AllViewModel<CelebrationCakeViewModel>
    {
        #region Constructor
        public AllCelebrationCakesViewModel()
            : base("Celebration Cakes")
        {
            DisplayCommand = new RelayCommand<CelebrationCakeViewModel>(ShowDisplay);
            UpdateCommand = new RelayCommand<CelebrationCakeViewModel>(ShowUpdate);
            DeleteCommand = new RelayCommand<CelebrationCakeViewModel>(ShowDelete);

            Messenger.Default.Register<string>(this, "CelebrationCake", _ => Load());
        }
        #endregion

        #region Commands
        public RelayCommand<CelebrationCakeViewModel> DisplayCommand { get; }
        public RelayCommand<CelebrationCakeViewModel> UpdateCommand { get; }
        public RelayCommand<CelebrationCakeViewModel> DeleteCommand { get; }
        #endregion

        #region Command handlers

        private void ShowDisplay(CelebrationCakeViewModel cake)
        {
            if (cake == null) return;

            var entity = SharedData_Entities.CelebrationCakes
                .First(c => c.CelebrationCakeId == cake.CelebrationCakeId);

            var displayVM = new DisplayCelebrationCakeViewModel();
            displayVM.item = entity;

            Messenger.Default.Send(displayVM, "CelebrationCakeDisplay");
        }


        private void ShowUpdate(CelebrationCakeViewModel cake)
        {
            if (cake == null) return;

            var updateVM = new UpdateCelebrationCakeViewModel();
            updateVM.Load(cake.CelebrationCakeId);

            Messenger.Default.Send(updateVM, "CelebrationCakeUpdate");
        }

        private void ShowDelete(CelebrationCakeViewModel cake)
        {
            if (cake == null) return;

            var deleteVM = new DeleteCelebrationCakeViewModel();
            deleteVM.Load(cake.CelebrationCakeId);

            Messenger.Default.Send(deleteVM, "CelebrationCakeDelete");
        }

        #endregion

        #region Load / Sort / Find

        public override void Load()
        {
            List = new ObservableCollection<CelebrationCakeViewModel>
            (
                SharedData_Entities.CelebrationCakes
                    .Include(c => c.CakeFillingNavigation)
                    .Where(c => c.IsActive == true)
                    .AsEnumerable()
                    .Select((c, i) =>
                    {
                        var vm = new CelebrationCakeViewModel(c);
                        vm.Position = i + 1;
                        return vm;
                    })
            );
        }

        public override void Sort() { }

        public override System.Collections.Generic.List<string> getComboboxSortList()
            => new();

        public override System.Collections.Generic.List<string> getComboboxFindList()
            => new();

        #endregion
    }
}
