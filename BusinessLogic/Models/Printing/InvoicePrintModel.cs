using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.Printing
{ 
        public class InvoicePrintModel
        {
            public InvoiceHeader Header { get; set; } = new();
            public PartyPrintModel Seller { get; set; } = new();
            public PartyPrintModel Buyer { get; set; } = new();
            public List<InvoiceItemPrintModel> Items { get; set; } = new();
            public InvoiceTotals Totals { get; set; } = new();
            public string? Notes { get; set; }
        }

        public class InvoiceHeader
        {
            public string? InvoiceNumber { get; set; }
            public DateTime? InvoiceDate { get; set; }
            public DateTime? DueDate { get; set; }
            public string? PaymentMethod { get; set; }
            public bool? IsPaid { get; set; }
            public DateTime? PaidAt { get; set; }
        }

        public class PartyPrintModel
        {
            public string? Name { get; set; }
            public string? Address { get; set; }
            public string? PostalCode { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public string? TaxId { get; set; }   // NIP
        }

        public class InvoiceItemPrintModel
        {
            public int Index { get; set; }
            public string? ProductName { get; set; }
            public string? Variant { get; set; }
            public int? Quantity { get; set; }
            public decimal UnitNetPrice { get; set; }
            public decimal VatRate { get; set; }
            public decimal LineNet { get; set; }
            public decimal LineVat { get; set; }
            public decimal LineGross { get; set; }
        }

        public class InvoiceTotals
        {
            public decimal TotalNet { get; set; }
            public decimal TotalVat { get; set; }
            public decimal TotalGross { get; set; }
        }
    }
