using DesktopApp.Helpers;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM
{
    public class CreateModuleViewModel: SingleViewModel<MakeAWishDB.Entities.Module> //bo wszystkie zak³adki dziedzicz¹ po WVM
    {




        #region Konstruktor
        public CreateModuleViewModel()
			: base("Module")
		{
			item = new MakeAWishDB.Entities.Module();
		}
		#endregion

		#region Data
		//Dla ka¿dego elemetu na interfejsie, który bêdzie dodawany tworzymy w³aœciwoœæ (properties)
		//to jest standardowy interfejs, trzeba siê go nauczyæ
		public string ModuleName
        {
			get
			{
				return item.ModuleName; //to jest standardowy get
			}
			set
			{
				if (item.ModuleName != value) //jeœli obecna wartoœæ jest ró¿na od nowej
				{
					item.ModuleName = value;//ustawianie nowej wartoœci w miejsce poprzedniej
					OnPropertyChanged(() => ModuleName);//odœwie¿enie okna po zmianie
				}
			}
		}
		public string Description
        {
			get
			{
				return item.Description; //to jest standardowy get
			}
			set
			{
				if (item.Description != value) //jeœli obecna wartoœæ jest ró¿na od nowej
				{
					item.Description = value;//ustawianie nowej wartoœci w miejsce poprzedniej
					OnPropertyChanged(() => Description);//odœwie¿enie okna po zmianie
				}
			}
		}
        #endregion



    }
}



