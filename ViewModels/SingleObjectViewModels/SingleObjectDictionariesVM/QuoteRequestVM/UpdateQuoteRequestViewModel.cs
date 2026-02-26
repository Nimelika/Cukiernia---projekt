using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
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

        public override void Load(int id)
        {
            item = sharedData_Entities.QuoteRequests
                .Include(q => q.CustomerNavigation)
                .Include(q => q.StatusNavigation)
                .FirstOrDefault(q => q.QuoteId == id);

            OnPropertyChanged(() => Status);
        }

        public int QuoteId => item.QuoteId;

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

        public string Phone
        {
            get => item.Phone;
            set { item.Phone = value; OnPropertyChanged(() => Phone); }
        }

        public string Description
        {
            get => item.Description;
            set { item.Description = value; OnPropertyChanged(() => Description); }
        }

        public DateOnly? DesiredDeliveryDate
        {
            get => item.DesiredDeliveryDate;
            set { item.DesiredDeliveryDate = value; OnPropertyChanged(() => DesiredDeliveryDate); }
        }

        // ID STATUSU — bindowany z ComboBox
        public int? Status
        {
            get => item.Status;
            set
            {
                item.Status = value;
                OnPropertyChanged(() => Status);
            }
        }

        public bool? IsConvertedToOrder
        {
            get => item.IsConvertedToOrder;
            set { item.IsConvertedToOrder = value; OnPropertyChanged(() => IsConvertedToOrder); }
        }

       
        public ObservableCollection<Status> StatusItems =>
            new(sharedData_Entities.Statuses
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.Name)
                .ToList());

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
