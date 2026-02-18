using DesktopApp.Helpers;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Context;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;


namespace DesktopApp.ViewModels.AbstractViewModels
{
	//to jest klasa z której bêd¹ dziedziczyæ wszystkie ViewModele typu All. Ta klasa jest abstrakcyjna, bo zawiera metodê abstrakcyjn¹
	public abstract class AllViewModel<T> : WorkspaceViewModel //WorkspaceViewModel to wszystkie zak³adki; <T> poniewa¿ to jest typ generyczny, na nim dzia³a AllViewModels
	{
		#region DB
		// to jest obiekt reprezentuj¹cy ca³¹ bazê danych
		

		private readonly SharedData_Entities sharedData_Entities; //tu siedzi ca³a baza danych

		public SharedData_Entities SharedData_Entities
		{
			get
			{
				return sharedData_Entities;
            }
        }

        #endregion

        #region Command
        //komenda s³u¿y do tego, by j¹ pod³¹czaæ pod akcjê np. po naciœniêciu przycisku.
        //POd przycisk podpinamy komendê, która wywo³uje funkcjê Load(), która pobierze kraje z bazy danych
        //poni¿ej standardowy kod komendy, trzeba siê nauczyæ, ¿e tak siê robi komendê
        private BaseCommand _LoadCommand;

		public ICommand LoadCommand
		{
			get
			{
				if (_LoadCommand == null)
				{
					_LoadCommand = new BaseCommand(() => Load());
				}
				return _LoadCommand;
			}
		}
		//to jest komenda, która zostanie podiêta pod przycisk do dodawania rekordu. Wywo³a ona metodê Add
		private BaseCommand _AddCommand;

		public ICommand AddCommand
		{
			get
			{
				if (_AddCommand == null)
				{
					_AddCommand = new BaseCommand(() => Add());
				}
				return _AddCommand;
			}
		}

		private BaseCommand _SortCommand;
		private BaseCommand _FindCommand;

		//po tym, jak nast¹pi pobranie do comboboxa, w polu SortField jest zapisywane, po czym sortowaæ
		public string SortField { get; set; }
		public List<string> SortComboboxItems //wszystko to, co jest pobierane do comboboxa (co jest do wyboru przy sortowaniu)
		{
			get
			{
				return getComboboxSortList();
			}
		}
		public ICommand SortCommand//podpiête pod przycisk Sort, wywo³uje metodê Sort
		{
			get
			{
				if (_SortCommand == null)
				{
					_SortCommand = new BaseCommand(() => Sort());
				}
				return _SortCommand;
			}
		}
		public string FindField { get; set; }//to siê zapisuje wybrana pozycja do wyszukania
		public string FindTextBox { get; set; }//tu zapisuje siê tekst do wyszukiwania wpisany przez u¿ytkownika w polu tekstowym
		public List<string> FindComboboxItems//to, co jest do wyboru w comboboxie dotycz¹cym wyszukiwania
		{
			get
			{
				return getComboboxFindList();
			}
		}
		public ICommand FindCommand
		{
			get
			{
				if(_FindCommand == null)
				{
					_FindCommand = new BaseCommand(() => Find());
				}
				return _FindCommand;
			}
		}

		#endregion

		#region Collection
		// tu bêdziemy przechowywaæ itemy pobrane z bazy
		private ObservableCollection<T> _List;//gdy jest pole z podkreœlnikiem, to znaczy, ¿e trzba zrobiæ do niego properties (zrobione poni¿ej)
		public ObservableCollection<T> List
		{
			get
			{
				//jeœli lista jest pusta, to j¹ wype³niamy
				if (_List == null)//jeœli lista jest pusta
				{
					Load();//wywo³aj funkcjê Load
				}
				return _List; //zwróæ _List
			}
			set
			{
				_List = value;//kod pobierania listy
				OnPropertyChanged(() => List);//¿eby siê okno odœwie¿y³o
			}
		}

        #endregion

        #region Konstruktor
        public AllViewModel(string displayName)
        {
            DisplayName = displayName;//tu ustawiamy nazwê zak³adki
            //tworzenie obiektu dostêpowego do bazy danych
            sharedData_Entities = new SharedData_Entities();

        }

		#endregion

		#region Helpers

		//ta metoda jest abstrakcyjna, bo nie wiemy jak j¹ napisaæ w tej klasie, a wiemy dopiero w klasach dziedzicz¹cych
		public abstract void Load();

        //ta metoda wyœle przy pomocy Messengera z MVVM Light komunikat do MainWindowViewModel
        // o koniecznoœci otworzenia okna

        public virtual void Add()
		{
            Messenger.Default.Send("Add " + DisplayName);
        }

        public abstract void Sort();
		public abstract List<string> getComboboxSortList();

		public virtual void Find()
		{
            Load();//na wypadek wielokrtotnego wyszukiwania na tej samej liœcie - bez ponownego ³adowania, trzba rêcznie odswiêzyæ widok, by ponownie przeszukaæ listê
        }
		public abstract List<string> getComboboxFindList();

        public virtual void Cleanup() 
        { 
            Messenger.Default.Unregister(this); 
        }
        #endregion
    }
}



