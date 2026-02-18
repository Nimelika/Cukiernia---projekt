using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.RegionVM
{
    public class DisplayRegionViewModel : DisplayViewModel<Region>
    {
        #region Konstruktor
        public DisplayRegionViewModel()
            : base("Display Region")
        {
        }
        #endregion

        #region Data

        public string RegionName => item?.RegionName;

        public string CountryName => sharedData_Entities.Countries
            .Where(c => c.Id == item.CountryId)
            .Select(c => c.CountryName)
            .FirstOrDefault();
        #endregion
    }
}
