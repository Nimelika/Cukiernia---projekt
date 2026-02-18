
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
    public class AllPaymentMethodsViewModel : AllViewModel<PaymentMethod> //pod typ generyczny wstawiamy kraje
    {
        #region Konstruktor
        public AllPaymentMethodsViewModel()
            : base("All Payment Methods")
        {

        }
        #endregion

        #region Helpers
        public override void Load()
        {
            //z klasy 
            //tworzymy ObservableCollection inicjuj¹c j¹ krajami
            List = new ObservableCollection<PaymentMethod> //pod listê podstawiamy new ObservableCollection na krajach
                (
				SharedData_Entities.PaymentMethods
                //inicjalizacja listy tu korzystamy z DbSetów wygenerowanych przez Microsoft w SharedData_Entities, które zwracaj¹ kolekcjê metod p³atnoœci pobierane z bazy danych, ale to pobiera wszystkie kolumny z tabeli, te niepotrzebne te¿
                );//alternatywna inicjalizacja listy (tu bêdzie zapytanie LINQu zwracaj¹cy z wszystkich krajów te kolumny, które s¹ potrzebne)
        }

		public override List<string> getComboboxSortList()
		{
			return new List<string> { "Name" };
		}
		public override void Sort()
		{

			if (SortField == "Name")
				List = new ObservableCollection<PaymentMethod>(List.OrderBy(item => item.Name));
		}
		public override List<string> getComboboxFindList()
		{
			return new List<string> { "Name" };
		}
		public override void Find()
		{
			base.Find();
            if (FindField == "Name")
				List = new ObservableCollection<PaymentMethod>(List.Where(item => item.Name
		   != null && item.Name.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));

		}

		#endregion
	}
}



