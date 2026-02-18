using DesktopApp.Helpers;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace DesktopApp.ViewModels.SingleObjectViewModels
{
    //to jest klasa z której bêd¹ dziedziczyæ wszystkie ViewModele oferuj¹ce dzia³ania na jednym obiekcie
    public class SingleViewModel<T> : WorkspaceViewModel
        where T : class
    {
        #region DB
        //ca³a baza danych
        protected SharedData_Entities sharedData_Entities;// jak siê coœ deklaruje, to trzeba póŸniej zainicjowaæ

        
        internal T item;
        #endregion

        #region Konstruktor
        public SingleViewModel(string displayName)
        {
            DisplayName = displayName;//tu ustawiamy nazwê zak³adki
            sharedData_Entities = new SharedData_Entities();
            

        }
       // protected string RefreshMessageTag { get; }
        #endregion

        #region Command

        private BaseCommand _SaveAndCloseCommand;
        //ta komenda bêdzie podpiêta pod przycisk Zapisz i Zamknij		
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null)
                    //komenda SaveAndCloseCommand uruchamia metodê SaveAndClose
                    _SaveAndCloseCommand = new BaseCommand(SaveAndClose);
                return _SaveAndCloseCommand;
            }
        }

        private BaseCommand _SaveCommand;
        //ta komenda bêdzie podpiêta pod przycisk Zapisz	
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    //komenda SaveAndCloseCommand uruchamia metodê SaveAndClose
                    _SaveCommand = new BaseCommand(Save);
                return _SaveCommand;
            }
        }

        #endregion

        #region  Validation
        public virtual bool IsValid()
        {
            return true;
        }
        #endregion

        #region Helpers

        //funkcja zapisuj¹ca nowy kraj do bazy danych
        public virtual void Save()
        {
            if (IsValid())
            {
                string question = "Do you want to save the data?";
                string boxtitle = "Data saving";

                var result = MessageBox.Show(question,boxtitle,MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var entry = sharedData_Entities.Entry(item);
                    if (entry.State == EntityState.Detached)
                    {
                        sharedData_Entities.Add(item);
                    }
                    else
                    {
                        sharedData_Entities.Update(item);
                    }
                    //dodawanie rekordu do lokalnej kolekcji
                   
                    //zapisanie rekordu (jego zmiany) do bazy danych
                    sharedData_Entities.SaveChanges();
                    Messenger.Default.Send("refresh", DisplayName);
                    
                    MessageBox.Show("Data has been saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        

                }
                else if (result == MessageBoxResult.No)
                {
                    //this.OnRequestClose();
                    OnRequestClose();
                }
                else
                { }

                
            }
            else
               // ShowMessageBoxError("Data must be corrected before saving");
                 MessageBox.Show("Data must be corrected before saving", "Correction needed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void SaveAndClose()
        {
            //wywo³anie funkcji zapisuj¹cej nowy towar
           // Save();
            Save();
            //wywo³anie fukcji zamykaj¹cej zak³adkê (dziedziczona z WorkspaceViewModel)
            OnRequestClose();
        }
        #endregion
    }
}



