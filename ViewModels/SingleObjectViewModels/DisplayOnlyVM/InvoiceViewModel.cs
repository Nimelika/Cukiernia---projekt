using MakeAWishDB.Entities;
using System;

namespace DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM
{
    public class InvoiceViewModel
    {
        public Invoice Item { get; }

        public InvoiceViewModel(Invoice item)
        {
            Item = item;
        }

        public int Position { get; set; }

        public int InvoiceId => Item.InvoiceId;

        public string InvoiceNumber => Item.InvoiceNumber;

        public int OrderId => Item.OrderId;

        public DateTime? InvoiceDate => Item.InvoiceDate;

        public decimal TotalNet => Item.TotalNet;

        public decimal? TotalVat => Item.TotalVat;

        public decimal? TotalGross => Item.TotalGross;

        public string PaymentMethod =>
            Item.PaymentMethodNavigation != null
                ? Item.PaymentMethodNavigation.Name
                : string.Empty;

        public bool? IsPaid => Item.IsPaid;

        public DateTime? CreatedAt => Item.CreatedAt;

        public DateTime? DueDate => Item.DueDate;

        public DateTime? PaidAt => Item.PaidAt;
    }
}
