using BusinessLogic.Models.EntitiesForView;
using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderVM
{
    public class UpdateOrderViewModel : UpdateViewModel<Order>
    {
        public UpdateOrderViewModel()
            : base("Update Order")
        {
        }

        public override void Load(int id)
        {
            item = sharedData_Entities.Orders
                .Include(o => o.CustomerNavigation)
                .Include(o => o.ShopNavigation)
                .Include(o => o.StatusNavigation)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CelebrationCakeNavigation)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.CelebrationCakeSizeNavigation)
                .FirstOrDefault(o => o.OrderId == id);

            LoadOrderItems();
        }

        #region Editable fields

        public DateTime? OrderDate
        {
            get => item.OrderDate;
            set { item.OrderDate = value; OnPropertyChanged(() => OrderDate); }
        }

        public int? Customer
        {
            get => item.Customer;
            set { item.Customer = value; OnPropertyChanged(() => Customer); }
        }

        public string GuestName
        {
            get => item.GuestName;
            set { item.GuestName = value; OnPropertyChanged(() => GuestName); }
        }

        public string GuestEmail
        {
            get => item.GuestEmail;
            set { item.GuestEmail = value; OnPropertyChanged(() => GuestEmail); }
        }

        public string GuestPhone
        {
            get => item.GuestPhone;
            set { item.GuestPhone = value; OnPropertyChanged(() => GuestPhone); }
        }

        public int Shop
        {
            get => item.Shop;
            set { item.Shop = value; OnPropertyChanged(() => Shop); }
        }

        public DateTime? CollectionDate
        {
            get => item.CollectionDate;
            set { item.CollectionDate = value; OnPropertyChanged(() => CollectionDate); }
        }

        public bool? IsPaid
        {
            get => item.IsPaid;
            set { item.IsPaid = value; OnPropertyChanged(() => IsPaid); }
        }

        public int Status
        {
            get => item.Status;
            set { item.Status = value; OnPropertyChanged(() => Status); }
        }

        #endregion

        #region ComboBox sources

        public ObservableCollection<KeyAndValue> CustomerComboBoxItems =>
    new ObservableCollection<KeyAndValue>(
        sharedData_Entities.Customers
            .Where(c => c.IsActive == true)
            .Select(c => new KeyAndValue
            {
                Key = c.CustomerId,
                Value = c.FullName
            })
            .ToList()
    );

        public ObservableCollection<KeyAndValue> ShopComboBoxItems =>
      new ObservableCollection<KeyAndValue>(
          sharedData_Entities.Shops
              .Where(s => s.IsActive)
              .Select(s => new KeyAndValue
              {
                  Key = s.ShopId,
                  Value = s.InternalName
              })
              .ToList()
      );

        public ObservableCollection<KeyAndValue> StatusComboBoxItems =>
            new ObservableCollection<KeyAndValue>(
                sharedData_Entities.Statuses
                    .Where(s => s.IsActive)
                    .Select(s => new KeyAndValue
                    {
                        Key = s.StatusId,
                        Value = s.Name
                    })
                    .ToList()
            );


        #endregion

        #region OrderItems (READ‑ONLY)

        public ObservableCollection<OrderItemPreviewViewModel> OrderItems { get; }
            = new();

        private void LoadOrderItems()
        {
            OrderItems.Clear();
            foreach (var oi in item.OrderItems)
                OrderItems.Add(new OrderItemPreviewViewModel(oi));
        }

        #endregion

        protected override string ValidateProperty(string propertyName) => null;
    }
}
