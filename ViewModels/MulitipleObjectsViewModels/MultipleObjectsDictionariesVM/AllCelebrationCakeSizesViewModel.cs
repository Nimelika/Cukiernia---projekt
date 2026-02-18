
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
    public class AllCelebrationCakeSizesViewModel : AllViewModel<CelebrationCakeSize> //pod typ generyczny wstawiamy rozmiary
    {
        #region Konstruktor
        public AllCelebrationCakeSizesViewModel()
            : base("All Celebration Cake Sizes")
        {

        }
        #endregion

        #region Helpers
        public override void Load()
        {
            //z klasy 
            //tworzymy ObservableCollection inicjuj¹c j¹ rozmiarami z bazy danych
            List = new ObservableCollection<CelebrationCakeSize> //pod listê podstawiamy new ObservableCollection na rozmiarach
                (
				SharedData_Entities.CelebrationCakeSizes
                //inicjalizacja listy tu korzystamy z DbSetów wygenerowanych przez Microsoft w SharedData_Entities.cs, które zwracaj¹ kolekcjê krajów pobierane z bazy danych, ale to pobiera wszystkie kolumny z tabeli, te niepotrzebne te¿
                );//alternatywna inicjalizacja listy (tu bêdzie zapytanie LINQu zwracaj¹cy z wszystkich krajów te kolumny, które s¹ potrzebne)
        }
		public override List<string> getComboboxSortList()
		{
			return new List<string> { "Name" };
		}
		public override void Sort()
		{

			if (SortField == "Name")
				List = new ObservableCollection<CelebrationCakeSize>(List.OrderBy(item => item.Name));
		}

		public override List<string> getComboboxFindList()
		{
			return new List<string> { "Name" };
		}
		public override void Find()
		{
            base.Find();
            if (FindField == "Name")
				List = new ObservableCollection<CelebrationCakeSize>(List.Where(item => item.Name
		   != null && item.Name.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));

		}

		#endregion
	}
}



