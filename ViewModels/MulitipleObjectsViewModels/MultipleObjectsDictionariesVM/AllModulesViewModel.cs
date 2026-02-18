
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllModulesViewModel : AllViewModel<MakeAWishDB.Entities.Module> //pod typ generyczny wstawiamy Modu³ w ten sposób, ponie¿ nazwa odnosi³a siê zarówno do bazy danych jaki i plików systemowych
    {
        #region Konstruktor
        public AllModulesViewModel()
            : base("All Modules")
        {

        }
        #endregion

        #region Helpers
        public override void Load()
        {
            //z klasy 
            //tworzymy ObservableCollection inicjuj¹c j¹ krajami
            List = new ObservableCollection<MakeAWishDB.Entities.Module> //pod listê podstawiamy new ObservableCollection na modu³ach
                (
                SharedData_Entities.Modules //inicjalizacja listy tu korzystamy z DbSetów wygenerowanych przez Microsoft w SharedData_Entities.cs, które zwracaj¹ kolekcjê modu³ów pobierane z bazy danych, ale to pobiera wszystkie kolumny z tabeli, te niepotrzebne te¿
                );//alternatywna inicjalizacja listy (tu bêdzie zapytanie LINQu zwracaj¹cy z wszystkich krajów te kolumny, które s¹ potrzebne)
        }

		public override List<string> getComboboxSortList()
		{
			return new List<string> { "Module Name", "Description" };
		}
		public override void Sort()
		{

			if (SortField == "Module Name")
				List = new ObservableCollection<MakeAWishDB.Entities.Module>(List.OrderBy(item => item.ModuleName));
			if (SortField == "Description")
				List = new ObservableCollection<MakeAWishDB.Entities.Module>(List.OrderBy(item => item.Description));

		}
		public override List<string> getComboboxFindList()
		{
			return new List<string> { "Module Name", "Description" };
		}
		public override void Find()
		{
			base.Find();
            if (FindField == "Module Name")
				List = new ObservableCollection<MakeAWishDB.Entities.Module>(List.Where(item => item.ModuleName
		   != null && item.ModuleName.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));
			if (FindField == "Description")
				List = new ObservableCollection<MakeAWishDB.Entities.Module>(List.Where(item => item.Description != null && item.Description.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));
		}
		#endregion
	}
}



