using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.CelebrationCakeVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllCelebrationCakesViewModel : AllViewModel<CelebrationCakeViewModel>
    {
        public AllCelebrationCakesViewModel()
            : base("Celebration Cakes")
        {
            UpdateCommand = new RelayCommand<CelebrationCakeViewModel>(ShowUpdate);
            Messenger.Default.Register<string>(this, "CelebrationCake", _ => Load());
        }

        public RelayCommand<CelebrationCakeViewModel> UpdateCommand { get; }

        private void ShowUpdate(CelebrationCakeViewModel cake)
        {
            if (cake == null) return;

            var updateVM = new UpdateCelebrationCakeViewModel();
            updateVM.Load(cake.CelebrationCakeId);
            Messenger.Default.Send(updateVM, "CelebrationCakeUpdate");
        }

        public override void Load()
        {
            List = new ObservableCollection<CelebrationCakeViewModel>(
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
        public override System.Collections.Generic.List<string> getComboboxSortList() => new();
        public override System.Collections.Generic.List<string> getComboboxFindList() => new();
    }
}

