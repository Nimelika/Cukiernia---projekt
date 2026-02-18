using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.ProductCategoryVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllProductCategoriesViewModel : AllViewModel<ProductCategoryViewModel>
    {
        #region Constructor
        public AllProductCategoriesViewModel()
            : base("Product Categories")
        {
            DetailsCommand = new RelayCommand<ProductCategoryViewModel>(ShowDetails);
            UpdateCommand = new RelayCommand<ProductCategoryViewModel>(ShowUpdate);
            DeleteCommand = new RelayCommand<ProductCategoryViewModel>(ShowDelete);

            Messenger.Default.Register<string>(this, "ProductCategory", msg => { Load(); });
        }
        #endregion

        #region Properties
        private ProductCategoryViewModel _SelectedProductCategory;
        public ProductCategoryViewModel SelectedProductCategory
        {
            get => _SelectedProductCategory;
            set
            {
                if (value != _SelectedProductCategory)
                {
                    _SelectedProductCategory = value;
                    Messenger.Default.Send(_SelectedProductCategory);
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand<ProductCategoryViewModel> DetailsCommand { get; set; }
        public RelayCommand<ProductCategoryViewModel> UpdateCommand { get; set; }
        public RelayCommand<ProductCategoryViewModel> DeleteCommand { get; set; }

        private void ShowDetails(ProductCategoryViewModel category)
        {
            if (category == null) return;

            var entity = SharedData_Entities.ProductCategories
                .FirstOrDefault(pc => pc.ProductCategoryId == category.ProductCategoryId);

            if (entity != null)
            {
                var displayVM = new DisplayProductCategoryViewModel();
                displayVM.item = entity;
                Messenger.Default.Send(displayVM, "ProductCategoryDisplay");
            }
        }

        private void ShowUpdate(ProductCategoryViewModel category)
        {
            if (category == null) return;

            var entity = SharedData_Entities.ProductCategories
                .FirstOrDefault(pc => pc.ProductCategoryId == category.ProductCategoryId);

            if (entity != null)
            {
                var updateVM = new UpdateProductCategoryViewModel();
                updateVM.Load(category.ProductCategoryId);
                Messenger.Default.Send(updateVM, "ProductCategoryUpdate");
            }
        }

        private void ShowDelete(ProductCategoryViewModel category)
        {
            if (category == null) return;

            var entity = SharedData_Entities.ProductCategories
                .FirstOrDefault(pc => pc.ProductCategoryId == category.ProductCategoryId);

            if (entity != null)
            {
                var deleteVM = new DeleteProductCategoryViewModel();
                deleteVM.item = entity;
                Messenger.Default.Send(deleteVM, "ProductCategoryDelete");
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ProductCategoryViewModel>
            (
                SharedData_Entities.ProductCategories
                .Where(pc => pc.IsActive == true)
                .AsEnumerable()
                .Select((pc, index) =>
                {
                    var vm = new ProductCategoryViewModel(pc);
                    vm.Position = index + 1;
                    return vm;
                })
            );
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Name" };
        }

        public override void Sort()
        {
            if (SortField == "Name")
                List = new ObservableCollection<ProductCategoryViewModel>(List.OrderBy(item => item.Name));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Name" };
        }

        public override void Find()
        {
            base.Find();

            if (FindField == "Name")
                List = new ObservableCollection<ProductCategoryViewModel>
                (
                    List.Where(item =>
                        item.Name != null &&
                        item.Name.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase))
                );
        }
        #endregion
    }
}
