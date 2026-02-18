using BusinessLogic.Models.EntitiesForView;
using DesktopApp.Helpers;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.RegionVM
{
    public class CreateRegionViewModel
        : CreateViewModel<Region>
    {
        public CreateRegionViewModel()
            : base("Add Region")
        {
        }

        protected override void InitializeNewItem()
        {
            item.IsActive = true;
        }

        public string RegionName
        {
            get => item.RegionName;
            set
            {
                if (item.RegionName != value)
                {
                    item.RegionName = value;
                    OnPropertyChanged(() => RegionName);
                }
            }
        }

        public int Country
        {
            get => item.CountryId;
            set
            {
                if (item.CountryId != value)
                {
                    item.CountryId = value;
                    OnPropertyChanged(() => Country);
                }
            }
        }

        public IQueryable<KeyAndValue> CountryComboBoxItems =>
            (from country in sharedData_Entities.Countries
             select new KeyAndValue
             {
                 Key = country.Id,
                 Value = country.CountryName
             }).ToList().AsQueryable();

        
    }
}




