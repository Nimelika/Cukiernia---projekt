
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllCakeFillingsViewModel : AllViewModel<CakeFilling> //pod typ generyczny wstawiamy rkremy
    {
        #region Konstruktor
        public AllCakeFillingsViewModel()
            : base("All Cake Fillings")
        {

        }
        #endregion

        #region Helpers
        public override void Load()
        {
            //z klasy 
            //tworzymy ObservableCollection inicjuj¹c j¹ kremami
            List = new ObservableCollection<CakeFilling> //pod listê podstawiamy new ObservableCollection na kremach
                (
                SharedData_Entities.CakeFillings //inicjalizacja listy tu korzystamy z DbSetów wygenerowanych przez Microsoft w redData_Entities.cs, które zwracaj¹ kolekcjê krajów pobierane z bazy danych, ale to pobiera wszystkie kolumny z tabeli, te niepotrzebne te¿
                );//alternatywna inicjalizacja listy (tu bêdzie zapytanie LINQu zwracaj¹cy z wszystkich krajów te kolumny, które s¹ potrzebne)
        }
		public override List<string> getComboboxSortList()
		{
			return new List<string> { "Cake Filling"};
		}
		public override void Sort()
		{

			if (SortField == "Cake Filling")
				List = new ObservableCollection<CakeFilling>(List.OrderBy(item => item.Name));
		}	
		public override List<string> getComboboxFindList()
		{
			return new List<string> { "Cake Filling" };
		}
		public override void Find()
		{
            base.Find();
            if (FindField == "Cake Filling")
				List = new ObservableCollection<CakeFilling>(List.Where(item => item.Name
		   != null && item.Name.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));
			
		}

		#endregion
	}
}



