using BusinessLogic.Models.Printing;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BusinessLogic.Services.Printing

{
    public class InvoicePrintModelBuilder
    {
        private readonly SharedData_Entities _context;

        public InvoicePrintModelBuilder(SharedData_Entities context)
        {
            _context = context;
        }

        public InvoicePrintModel Build(int invoiceId)
        {
            var invoice = _context.Invoices
                .Include(i => i.InvoiceItems)
                    .ThenInclude(ii => ii.Product)
                .Include(i => i.Order)
                    .ThenInclude(o => o.CustomerNavigation)
                .Include(i => i.PaymentMethodNavigation)
                .FirstOrDefault(i => i.InvoiceId == invoiceId);

            if (invoice == null)
                throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");

            var model = new InvoicePrintModel
            {
                Header = BuildHeader(invoice),
                Buyer = BuildBuyer(invoice),
                Items = BuildItems(invoice),
                Totals = BuildTotals(invoice),
                Notes = invoice.Notes
            };

            return model;
        }

        private InvoiceHeader BuildHeader(Invoice invoice)
        {
            return new InvoiceHeader
            {
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                DueDate = invoice.DueDate,
                PaymentMethod = invoice.PaymentMethodNavigation?.Name,
                IsPaid = invoice.IsPaid,
                PaidAt = invoice.PaidAt
            };
        }

        private PartyPrintModel BuildBuyer(Invoice invoice)
        {
            var customer = invoice.Order?.CustomerNavigation;

            // obsluga zamowien bez klienta
            if (customer == null)
                return new PartyPrintModel
                {
                    Name = invoice.Order?.GuestName ?? "Guest",
                    Email = invoice.Order?.GuestEmail,
                    Phone = invoice.Order?.GuestPhone,

                };

            // standardowa sciezka dla klientow z bazy
            return new PartyPrintModel
            {
                Name = customer.IsCompany == true
                    ? customer.CompanyName
                    : customer.FullName,

                Address = $"{customer.Street} {customer.StreetNumber}",
                PostalCode = customer.PostalCode,
                City = customer.City,
                Country = customer.CountryNavigation?.CountryName,
                Phone = customer.Phone,
                Email = customer.Email,
                TaxId = customer.Nip
            };
        }


        private List<InvoiceItemPrintModel> BuildItems(Invoice invoice)
        {
            var items = new List<InvoiceItemPrintModel>();
            var index = 1;

            foreach (var item in invoice.InvoiceItems.Where(i => i.IsActive))
            {
                items.Add(new InvoiceItemPrintModel
                {
                    Index = index++,
                    ProductName = item.Product?.Name,
                    //Variant = item.Product?.Variant,
                    Quantity = item.Quantity,
                    UnitNetPrice = item.UnitPrice,
                    VatRate = item.VatRate ?? 0,
                    LineNet = item.LineNet,
                    LineVat = item.LineVat,
                    LineGross = item.LineGross
                });
            }

            return items;
        }

        private InvoiceTotals BuildTotals(Invoice invoice)
        {
            return new InvoiceTotals
            {
                TotalNet = invoice.TotalNet,
                TotalVat = invoice.TotalVat ?? 0,
                TotalGross = invoice.TotalGross ?? 0
            };
        }
    }
}
