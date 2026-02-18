using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.ProductCategoryVM

{
    public class DeleteProductCategoryViewModel : DeleteViewModel<ProductCategory>
    {
        public DeleteProductCategoryViewModel()
            : base("Delete Product Category")
        {
        }

        protected override string RefreshMessageTag => "RefreshProductCategories";

        public string CategoryName => item?.Name;

        public string ImageAlt => item?.ImageAlt;
    }
}
