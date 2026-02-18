using BusinessLogic.Models.EntitiesForView;
using BusinessLogic.Models.Validators;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.RegionVM

{
    public class UpdateRegionViewModel
        : UpdateViewModel<Region>
    {
        public UpdateRegionViewModel()
            : base("Update Region")
        {
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

        protected override string ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(RegionName))
                return StringValidator.StartsWithCapitalLetter(RegionName);

            return null;
        }

        public override void Load(int id)
        {
            item = sharedData_Entities.Regions
                .FirstOrDefault(r => r.RegionId == id);
        }
    }
}
