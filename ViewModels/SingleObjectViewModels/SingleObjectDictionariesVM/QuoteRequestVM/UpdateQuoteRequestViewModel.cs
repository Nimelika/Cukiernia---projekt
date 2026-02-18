using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.QuoteRequestVM
{
    public class UpdateQuoteRequestViewModel
        : UpdateViewModel<QuoteRequest>
    {
        public UpdateQuoteRequestViewModel()
            : base("Update Quote Request")
        {
        }

        public int QuoteId => item.QuoteId;

        public string GuestName
        {
            get => item.GuestName;
            set
            {
                if (item.GuestName != value)
                {
                    item.GuestName = value;
                    OnPropertyChanged(() => GuestName);
                }
            }
        }

        public string GuestEmail
        {
            get => item.GuestEmail;
            set
            {
                if (item.GuestEmail != value)
                {
                    item.GuestEmail = value;
                    OnPropertyChanged(() => GuestEmail);
                }
            }
        }

        public string Phone
        {
            get => item.Phone;
            set
            {
                if (item.Phone != value)
                {
                    item.Phone = value;
                    OnPropertyChanged(() => Phone);
                }
            }
        }

        public string Description
        {
            get => item.Description;
            set
            {
                if (item.Description != value)
                {
                    item.Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public DateOnly? DesiredDeliveryDate
        {
            get => item.DesiredDeliveryDate;
            set
            {
                if (item.DesiredDeliveryDate != value)
                {
                    item.DesiredDeliveryDate = value;
                    OnPropertyChanged(() => DesiredDeliveryDate);
                }
            }
        }

        public int? Status
        {
            get => item.Status;
            set
            {
                if (item.Status != value)
                {
                    item.Status = value;
                    OnPropertyChanged(() => Status);
                }
            }
        }

        public bool? IsConvertedToOrder
        {
            get => item.IsConvertedToOrder;
            set
            {
                if (item.IsConvertedToOrder != value)
                {
                    item.IsConvertedToOrder = value;
                    OnPropertyChanged(() => IsConvertedToOrder);
                }
            }
        }

        public override void Load(int id)
        {
            item = sharedData_Entities.QuoteRequests
                .Include(q => q.CustomerNavigation)
                .Include(q => q.StatusNavigation)
                .FirstOrDefault(q => q.QuoteId == id);
        }

        protected override string ValidateProperty(string propertyName)
        {
            if (propertyName == nameof(GuestEmail) &&
                string.IsNullOrWhiteSpace(GuestEmail))
                return "Guest email cannot be empty";

            if (propertyName == nameof(Description) &&
                string.IsNullOrWhiteSpace(Description))
                return "Description cannot be empty";

            return null;
        }
    }
}

