using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System.Linq;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.CelebrationCakeVM
{
    public class DeleteCelebrationCakeViewModel
        : DeleteViewModel<CelebrationCake>
    {
        public DeleteCelebrationCakeViewModel()
            : base("Delete Celebration Cake")
        {
        }

        protected override string RefreshMessageTag => "AllCelebrationCakes";

        public void Load(int id)
        {
            item = sharedData_Entities.CelebrationCakes
                .First(c => c.CelebrationCakeId == id);
        }

        public string Name => item?.Name;
        public string ImageAlt => item?.ImageAlt;
    }
}
