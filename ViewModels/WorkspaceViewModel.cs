using DesktopApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class WorkspaceViewModel : BaseViewModel
    {
        #region Fields and Commands
        //ka¿da zka³adka ma minim nazwê i zamknij
        public string DisplayName { get; set; } //w tym properties bêdzie przychowywana nazwa zak³adki
        public int TabCode { get; set; } //w tym properties bêdzie przechowywany kod zak³adki
        private BaseCommand _CloseCommand; //to jest komenda do obs³ugi zamykania okna
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(() => this.OnRequestClose()); //ta komenda wyo³a funkcje zamykaj¹c¹ zak³adkê 
                return _CloseCommand;
            }
        }
        #endregion
        #region RequestClose [event]
        public event EventHandler RequestClose;
        protected void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion

        #region Messageboxes
        public void ShowSaveMBQestion()

        {

        }

        #endregion

    }
}



