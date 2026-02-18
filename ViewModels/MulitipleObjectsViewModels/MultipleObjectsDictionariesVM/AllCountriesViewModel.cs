
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MakeAWishDB.Entities;
using DesktopApp.ViewModels.AbstractViewModels;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllCountriesViewModel : AllViewModel<Country> //pod typ generyczny wstawiamy kraje
    {
        #region Konstruktor
        public AllCountriesViewModel()
            : base("All Countries")
        {
          //  Messenger.Default.Register<203>(this, Load);
        }
		#endregion

		#region Commands
		private Country _SelectedCountry;

		public Country SelectedCountry
		{
			set 
            {
                if (value != _SelectedCountry) 
                {
					_SelectedCountry = value;
                    //Wysy³amy wybrany kraj do okna Create Bank
                    Messenger.Default.Send(_SelectedCountry);
                    OnRequestClose();//zamkniêcie okna 
				}
                 
            }
            get { return _SelectedCountry; }
		}

		#endregion

		#region Helpers
		public override void Load()
        {
			//z klasy 
			//tworzymy ObservableCollection inicjuj¹c j¹ krajami
			List = new ObservableCollection<Country> //pod listê podstawiamy new ObservableCollection na krajach
				(
                //inicjalizacja listy tu korzystamy z DbSetów wygenerowanych przez Microsoft w SharedData_Entities.cs, które zwracaj¹ kolekcjê krajów pobierane z bazy danych, ale to pobiera wszystkie kolumny z tabeli, te niepotrzebne te¿
                SharedData_Entities.Countries
				);//alternatywna inicjalizacja listy (tu bêdzie zapytanie LINQu zwracaj¹cy z wszystkich krajów te kolumny, które s¹ potrzebne)
        }



		public override List<string> getComboboxSortList()
		{
			return new List<string> { "Country Abbreviation", "Country Name" };
		}
		public override void Sort()
		{

			if (SortField == "Country Abbreviation")
				List = new ObservableCollection<Country>(List.OrderBy(item => item.CountryAbbreviation));
			if (SortField == "Country Name")
				List = new ObservableCollection<Country>(List.OrderBy(item => item.CountryName));			
		}
		public override List<string> getComboboxFindList()
		{
			return new List<string> { "Country Abbreviation", "Country Name" };
		}
		public override void Find()
		{
            base.Find();
            if (FindField == "Country Abbreviation")
              
            List = new ObservableCollection<Country>(List.Where(item => item.CountryAbbreviation
		   != null && item.CountryAbbreviation.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));
			if (FindField == "Country Name")
				List = new ObservableCollection<Country>(List.Where(item => item.CountryName != null && item.CountryName.StartsWith(FindTextBox, StringComparison.CurrentCultureIgnoreCase)));
		}

		#endregion
	}
}



