
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
    public class AllPageHeadersViewModel : AllViewModel<PageHeader> //pod typ generyczny wstawiamy kraje
    {
        #region Konstruktor
        public AllPageHeadersViewModel()
            : base("All ID Documents")
        {

        }
        #endregion

        #region Helpers
        public override void Load()
        {
			//z klasy 
			//tworzymy ObservableCollection inicjuj¹c j¹ krajami
			List = new ObservableCollection<PageHeader> //pod listê podstawiamy new ObservableCollection na krajach
				(
				SharedData_Entities.PageHeaders
                );//alternatywna inicjalizacja listy (tu bêdzie zapytanie LINQu zwracaj¹cy z wszystkich krajów te kolumny, które s¹ potrzebne)
        }
		public override List<string> getComboboxSortList()
		{
			return new List<string> { "Internal Name" };
		}
		public override void Sort()
		{

			if (SortField == "Internal Name")
				List = new ObservableCollection<PageHeader>(List.OrderBy(item => item.InternalName));
		}
		public override List<string> getComboboxFindList()
		{
			return new List<string> { "Internal Name" };
		}
		public override void Find()
		{
			base.Find();
            if (FindField == "Internal Name")
				List = new ObservableCollection<PageHeader>(List.Where(item => item.InternalName
           != null && item.InternalName.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));

		}

		#endregion
	}
}



