using BusinessLogic.Models.EntitiesForView;
using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderItemVM;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BusinessLogic.Models.Validators;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.OrderVM

    {
        public class CreateOrderViewModel : CreateViewModel<Order>
        {
            public CreateOrderViewModel()
                : base("Add Order")
            {
                AddOrderItemCommand = new RelayCommand(AddOrderItem);
                RemoveOrderItemCommand = new RelayCommand<CreateOrderItemViewModel>(RemoveOrderItem);

                OrderItems.CollectionChanged += OrderItems_CollectionChanged;
            }

            protected override void InitializeNewItem()
            {
                item.OrderDate = DateTime.Now;
                item.IsActive = true;
            }

        #region Properties

        public DateTime? OrderDate
        {
            get => item.OrderDate;
            set
            {
                if (item.OrderDate == value)
                    return;

                var error = DateValidator.DoDatesMatch(
                    value,
                    CollectionDate,
                    "Order Date",
                    "Collection Date"
                );

                if (error != null)
                {
                    System.Windows.MessageBox.Show(error);
                    return;
                }

                item.OrderDate = value;
                OnPropertyChanged(() => OrderDate);
            }
        }


        public DateTime? CollectionDate
        {
            get => item.CollectionDate;
            set
            {
                if (item.CollectionDate == value)
                    return;

                var error = DateValidator.DoDatesMatch(
                    OrderDate,
                    value,
                    "Order Date",
                    "Collection Date"
                );

                if (error != null)
                {
                    System.Windows.MessageBox.Show(error);
                    return;
                }

                item.CollectionDate = value;
                OnPropertyChanged(() => CollectionDate);
            }
        }


        public bool? IsPaid
            {
                get => item.IsPaid;
                set
                {
                    if (item.IsPaid == value)
                        return;

                    item.IsPaid = value;
                    OnPropertyChanged(() => IsPaid);
                }
            }

            public string? GuestName
            {
                get => item.GuestName;
                set
                {
                    if (item.GuestName == value)
                        return;

                    item.GuestName = value;
                    OnPropertyChanged(() => GuestName);
                }
            }

            public string? GuestEmail
            {
                get => item.GuestEmail;
                set
                {
                    if (item.GuestEmail == value)
                        return;

                    item.GuestEmail = value;
                    OnPropertyChanged(() => GuestEmail);
                }
            }

            public string? GuestPhone
            {
                get => item.GuestPhone;
                set
                {
                    if (item.GuestPhone == value)
                        return;

                    item.GuestPhone = value;
                    OnPropertyChanged(() => GuestPhone);
                }
            }

            #endregion

            #region ComboBox Items

            public IQueryable<KeyAndValue> CustomerComboBoxItems =>
                sharedData_Entities.Customers
                    .Select(c => new KeyAndValue
                    {
                        Key = c.CustomerId,
                        Value = c.FullName
                    })
                    .ToList()
                    .AsQueryable();

            public IQueryable<KeyAndValue> ShopComboBoxItems =>
                sharedData_Entities.Shops
                    .Select(s => new KeyAndValue
                    {
                        Key = s.ShopId,
                        Value = s.InternalName
                    })
                    .ToList()
                    .AsQueryable();

            public IQueryable<KeyAndValue> StatusComboBoxItems =>
                sharedData_Entities.Statuses
                    .Select(s => new KeyAndValue
                    {
                        Key = s.StatusId,
                        Value = s.Name
                    })
                    .ToList()
                    .AsQueryable();

        public IQueryable<KeyAndValue> CelebrationCakeComboBoxItems =>
    sharedData_Entities.CelebrationCakes
            .Where(cc => cc.IsActive == true)
        .Select(cc => new KeyAndValue

        {
            Key = cc.CelebrationCakeId,
            Value = cc.Name
        })
        .ToList()
        .AsQueryable();

        #endregion

        #region Order Items

        public ObservableCollection<CreateOrderItemViewModel> OrderItems { get; }
                = new ObservableCollection<CreateOrderItemViewModel>();

            private void OrderItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.NewItems != null)
                {
                    foreach (CreateOrderItemViewModel vm in e.NewItems)
                        vm.PropertyChanged += OrderItem_PropertyChanged;
                }

                if (e.OldItems != null)
                {
                    foreach (CreateOrderItemViewModel vm in e.OldItems)
                        vm.PropertyChanged -= OrderItem_PropertyChanged;
                }
            }

            private void OrderItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                // miejsce na przyszłe Total / Quantity / Price
            }

            #endregion

            #region Commands

            public ICommand AddOrderItemCommand { get; }
            public ICommand RemoveOrderItemCommand { get; }

            private void AddOrderItem()
            {
                OrderItems.Add(new CreateOrderItemViewModel(new OrderItem()));
            }

            private void RemoveOrderItem(CreateOrderItemViewModel vm)
            {
                if (vm != null)
                    OrderItems.Remove(vm);
            }

            #endregion

            #region Validation & Save

            public override bool IsValid()
            {
                if (!base.IsValid())
                    return false;

                return OrderItems.All(i => i.IsValid());
            }

            public override void Save()
            {
                sharedData_Entities.Add(item);
                sharedData_Entities.SaveChanges();

                foreach (var vm in OrderItems)
                {
                    vm.item.OrderId = item.OrderId;
                    item.OrderItems.Add(vm.item);
                }

                sharedData_Entities.SaveChanges();
                base.Save();
            }

            #endregion
        }
    }
