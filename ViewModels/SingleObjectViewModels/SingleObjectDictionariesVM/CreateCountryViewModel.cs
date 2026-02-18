using DesktopApp.Helpers;
using MakeAWishDB.Entities;
using MakeAWishDB.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic.Models.Validators;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM
{
    public class CreateCountryViewModel : SingleViewModel<Country>, IDataErrorInfo //bo wszystkie zak³adki dziedzicz¹ po WVM
    {




        #region Konstruktor
        public CreateCountryViewModel()
            : base("Country")
        {
            item = new Country();
        }
        #endregion

        #region Data
        //Dla ka¿dego elemetu na interfejsie, który bêdzie dodawany tworzymy w³aœciwoœæ (properties)
        //to jest standardowy interfejs, trzeba siê go nauczyæ
        public string CountryAbbreviation
        {
            get
            {
                return item.CountryAbbreviation; //to jest standardowy get
            }
            set
            {
                if (item.CountryAbbreviation != value) //jeœli obecna wartoœæ jest ró¿na od nowej
                {
                    item.CountryAbbreviation = value;//ustawianie nowej wartoœci w miejsce poprzedniej
                    OnPropertyChanged(() => CountryAbbreviation);//odœwie¿enie okna po zmianie
                }
            }
        }
        public string CountryName
        {
            get
            {
                return item.CountryName; //to jest standardowy get
            }
            set
            {
                if (item.CountryName != value) //jeœli obecna wartoœæ jest ró¿na od nowej
                {
                    item.CountryName = value;//ustawianie nowej wartoœci w miejsce poprzedniej
                    OnPropertyChanged(() => CountryName);//odœwie¿enie okna po zmianie
                }
            }
        }
        #endregion



        #region Validation
        public string Error
        {
            get
            {
                return null;
            }
        }
        public string this[string name]
        {
            get
            {
                string notification = null;

                if (name == "CountryName")
                {

                    notification = StringValidator.StartsWithCapitalLetter(this.CountryName);
                }
                if (name == "CountryAbbreviation")


                {
                    notification = StringValidator.IsTwoChar(this.CountryAbbreviation);
                }

                return notification;

            }

        }
        public override bool IsValid()
        {
            if (this["CountryAbbreviation"] == null && this["CountryName"] == null) return true;
            return false;
        }

        #endregion
    }
}



