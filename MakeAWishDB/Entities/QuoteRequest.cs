using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("CreatedAt", Name = "index_quotes_created")]
[Index("Status", Name = "index_quotes_status")]
public partial class QuoteRequest
{
    [Key]
    [Column("QuoteID")]
    public int QuoteId { get; set; }

    public int? Customer { get; set; }

    [StringLength(100)]
    public string? GuestName { get; set; }

    [StringLength(100)]
    public string? GuestEmail { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    public string? Description { get; set; }

    public DateOnly? DesiredDeliveryDate { get; set; }

    [StringLength(255)]
    public string? UploadedImagePath { get; set; }

    public int? Status { get; set; }

    public bool? IsConvertedToOrder { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("Customer")]
    [InverseProperty("QuoteRequests")]
    public virtual Customer? CustomerNavigation { get; set; }

    [ForeignKey("Status")]
    [InverseProperty("QuoteRequests")]
    public virtual Status? StatusNavigation { get; set; }
}
