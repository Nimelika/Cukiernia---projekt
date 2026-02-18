using BusinessLogic.Models.EntitiesForView;
using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.RegionVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllRegionsViewModel : AllViewModel<RegionForView>
    {
        #region Constructor
        public AllRegionsViewModel()
            : base("Regions")
        {
            DetailsCommand = new RelayCommand<RegionForView>(ShowDetails);
            UpdateCommand = new RelayCommand<RegionForView>(ShowUpdate);
            DeleteCommand = new RelayCommand<RegionForView>(ShowDelete);
            Messenger.Default.Register<string>(this, "Region", msg => { Load(); });// po otrzymaniu komunikatu (nazwy zak³adki) "Region" odœwie¿y listê 
        }
        #endregion

        #region Properties
        private RegionForView _SelectedRegion;
        public RegionForView SelectedRegion
        {
            get => _SelectedRegion;
            set
            {
                if (value != _SelectedRegion)
                {
                    _SelectedRegion = value;
                    Messenger.Default.Send(_SelectedRegion);
                }
            }
        }

        #endregion

        #region Commands
        public RelayCommand<RegionForView> DetailsCommand { get; set; }
        public RelayCommand<RegionForView> UpdateCommand { get; set; }
        public RelayCommand<RegionForView> DeleteCommand { get; set; }


        private void ShowDetails(RegionForView region)
        {
            if (region == null) return;

            var regionEntity = SharedData_Entities.Regions
                .FirstOrDefault(r => r.RegionId == region.RegionId);

            if (regionEntity != null)
            {
                var displayVM = new DisplayRegionViewModel();
                displayVM.item = regionEntity; 
                Messenger.Default.Send(displayVM, "RegionDisplay");
            }
        }
        private void ShowUpdate(RegionForView region)
        {
            if (region == null) return;

            var regionEntity = SharedData_Entities.Regions
                .FirstOrDefault(r => r.RegionId == region.RegionId);

            if (regionEntity != null)
            {
                var updateVM = new UpdateRegionViewModel();
                updateVM.Load(region.RegionId);
                Messenger.Default.Send(updateVM, "RegionUpdate");
            }
        }

        private void ShowDelete(RegionForView region)
        {
            if (region == null) return;

            var regionEntity = SharedData_Entities.Regions
                .FirstOrDefault(r => r.RegionId == region.RegionId);

            if (regionEntity != null)
            {
                var deleteVM = new DeleteRegionViewModel();
                deleteVM.item = regionEntity;

                // Wyœlij VM z tokenem, który odbierze i otworzy DeleteRegionView
                Messenger.Default.Send(deleteVM, "RegionDelete");
            }
        }


        #endregion

        #region Helpers
       
        public override void Load()
        {



            List = new ObservableCollection<RegionForView>
                (
                //Wersja w LINQ:
                SharedData_Entities.Regions
                .Where(r => r.IsActive)
                .Include(r => r.Country)
                .AsEnumerable()//przeniesienie dlaszej czêœci zapytania do c# (bo Select z pozycj¹ nie jest wspierany przez EF)
                .Select((r, index) => new RegionForView 
                { 
                    Position = index + 1,
                    RegionId = r.RegionId, 
                    RegionName = r.RegionName, 
                    CountryName = r.Country?.CountryName 
                }
                )
                /* Alternatywnie:
                from region in SharedData_Entities.Regions 
                where region.IsActive
                select new RegionForView 
                { RegionId = region.RegionId, 
                    RegionName = region.RegionName, 
                    CountryName = region.Country.CountryName, }*/
                );

        }
       

        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Region", "Country" };
        }

        public override void Sort()
        {
            if (SortField == "Region")
                List = new ObservableCollection<RegionForView>(List.OrderBy(item => item.RegionName));
            if (SortField == "Country")
                List = new ObservableCollection<RegionForView>(List.OrderBy(item => item.CountryName));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Region", "Country" };
        }

        public override void Find()
        {
            base.Find();
            if (FindField == "Region")
                List = new ObservableCollection<RegionForView>(List.Where(item => item.RegionName != null && item.RegionName.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));
            if (FindField == "Country")
                List = new ObservableCollection<RegionForView>(List.Where(item => item.CountryName != null && item.CountryName.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));
        }
        public override void Cleanup()
        {
            // logika specyficzna dla AllRegionsViewModel
            // np. czyszczenie w³asnych zasobów

            base.Cleanup(); // wywo³uje Cleanup() z AllViewModel
        }

        #endregion
    }
}
