using BusinessLogic.Models.EntitiesForView;
using BusinessLogic.Models.Validators;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderItemVM

{
    public class CreateOrderItemViewModel : CreateViewModel<OrderItem>
    {
        public CreateOrderItemViewModel()
            : base("Add Order Item")
        {
            InitializeNewItem();
        }

        public CreateOrderItemViewModel(OrderItem existingItem)
            : base("Add Order Item")
        {
            item = existingItem;
            InitializeNewItem();
        }

        protected override void InitializeNewItem()
        {
            item.IsActive = true;
            item.Quantity ??= 1;
        }

        #region Properties

        public int CelebrationCake
        {
            get => item.CelebrationCake;
            set
            {
                if (item.CelebrationCake == value)
                    return;

                item.CelebrationCake = value;
                OnPropertyChanged(() => CelebrationCake);
            }
        }

        public int? Quantity
        {
            get => item.Quantity;
            set
            {
                var error = BusinessValidator.IsGreaterThanZero(value, "Quantity");
                if (error != null)
                {
                    System.Windows.MessageBox.Show(error);
                    return;
                }

                item.Quantity = value;
                OnPropertyChanged(() => Quantity);
                OnPropertyChanged(() => LineTotal);
            }
        }

        public decimal UnitPrice
        {
            get => item.UnitPrice;
            set
            {
                if (item.UnitPrice == value)
                    return;

                item.UnitPrice = value;
                OnPropertyChanged(() => UnitPrice);
                OnPropertyChanged(() => LineTotal);
            }
        }

        public int? CelebrationCakeSize
        {
            get => item.CelebrationCakeSize;
            set
            {
                if (item.CelebrationCakeSize == value)
                    return;

                item.CelebrationCakeSize = value;
                OnPropertyChanged(() => CelebrationCakeSize);
            }
        }

        public string? PersonalizedText
        {
            get => item.PersonalizedText;
            set
            {
                if (item.PersonalizedText == value)
                    return;

                item.PersonalizedText = value;
                OnPropertyChanged(() => PersonalizedText);
            }
        }

        public string? Notes
        {
            get => item.Notes;
            set
            {
                if (item.Notes == value)
                    return;

                item.Notes = value;
                OnPropertyChanged(() => Notes);
            }
        }

        #endregion

        #region Calculated

        public decimal LineTotal =>
            (item.Quantity ?? 0) * item.UnitPrice;

        #endregion

        #region ComboBox Items

        public List<KeyAndValue> CakeComboBoxItems =>
            sharedData_Entities.CelebrationCakes
                .Select(c => new KeyAndValue
                {
                    Key = c.CelebrationCakeId,
                    Value = c.Name
                })
                .ToList();

        public List<KeyAndValue> CakeSizeComboBoxItems =>
            sharedData_Entities.CelebrationCakeSizes
                .Select(s => new KeyAndValue
                {
                    Key = s.CelebrationCakeSizeId,
                    Value = s.Name
                })
                .ToList();

        #endregion

       
    }
}
