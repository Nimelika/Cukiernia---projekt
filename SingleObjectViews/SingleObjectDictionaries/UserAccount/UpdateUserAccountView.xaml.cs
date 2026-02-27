using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.UserAccountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApp.Views.SingleObjectViews.SingleObjectDictionaries.UserAccount
{
    /// <summary>
    /// Interaction logic for UpdateUserAccountView.xaml
    /// </summary>
    public partial class UpdateUserAccountView : SingleViewBase
    {
        public UpdateUserAccountView()
        {
            InitializeComponent();
        }
        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UpdateUserAccountViewModel vm)
            {
                vm.NewPassword = ((PasswordBox)sender).Password;
            }
        }

    }
}
