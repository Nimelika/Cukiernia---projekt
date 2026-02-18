using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System.Linq;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.RegionVM
{
    public class DeleteRegionViewModel : DeleteViewModel<Region>
    {
        public DeleteRegionViewModel()
            : base("Delete Region")
        {
        }

        protected override string RefreshMessageTag => "RefreshRegions";

        public string RegionName => item?.RegionName;

        public string CountryName => sharedData_Entities.Countries
            .Where(c => c.Id == item.CountryId)
            .Select(c => c.CountryName)
            .FirstOrDefault();
    }
}
