using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("OrderId", Name = "index_invoices_order")]
public partial class Invoice
{
    [Key]
    [Column("InvoiceID")]
    public int InvoiceId { get; set; }

    [StringLength(67)]
    public string? InvoiceNumber { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    public DateTime? InvoiceDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalNet { get; set; }

    
    [Column("TotalVAT", TypeName = "decimal(19, 8)")]
    public decimal? TotalVat { get; set; }

    [Column(TypeName = "decimal(20, 8)")]
    public decimal? TotalGross { get; set; }

    public int PaymentMethod { get; set; }

    public bool? IsPaid { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaidAt { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    [ForeignKey("OrderId")]
    [InverseProperty("Invoices")]
    public virtual Order? Order { get; set; }

    [ForeignKey("PaymentMethod")]
    [InverseProperty("Invoices")]
    public virtual PaymentMethod? PaymentMethodNavigation { get; set; }
}
