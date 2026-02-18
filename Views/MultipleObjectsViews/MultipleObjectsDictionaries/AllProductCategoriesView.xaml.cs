using DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM;
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

namespace DesktopApp.Views.MulitipleObjectsViews.MulitipleObjectsDictionaries	
{
	/// <summary>
	/// Interaction logic for AllProductCategoriesView.xaml
	/// </summary>
	public partial class AllProductCategoriesView : AllViewBase
	{
		public AllProductCategoriesView()
		{
			InitializeComponent();
            DataContext = new AllProductCategoriesViewModel();
        }
	}
}



