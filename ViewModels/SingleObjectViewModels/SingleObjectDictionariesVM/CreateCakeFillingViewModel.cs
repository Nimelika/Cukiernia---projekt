using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM
{
    public class CreateCakeFillingViewModel : SingleViewModel<CakeFilling> //bo wszystkie zak³adki dziedzicz¹ po WVM
	{




		#region Konstruktor
		public CreateCakeFillingViewModel()
			: base("Cake Filling")
		{
			item = new CakeFilling();
		}
		#endregion

		#region Data
		//Dla ka¿dego elemetu na interfejsie, który bêdzie dodawany tworzymy w³aœciwoœæ (properties)
		//to jest standardowy interfejs, trzeba siê go nauczyæ
		public string Name
		{
			get
			{
				return item.Name; //to jest standardowy get
			}
			set
			{
				if (item.Name != value) //jeœli obecna wartoœæ jest ró¿na od nowej
				{
					item.Name = value;//ustawianie nowej wartoœci w miejsce poprzedniej
					OnPropertyChanged(() => Name);//odœwie¿enie okna po zmianie
				}
			}
		}		
		#endregion


	}
}



