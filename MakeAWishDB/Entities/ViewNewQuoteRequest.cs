using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Keyless]
public partial class ViewNewQuoteRequest
{
    [Column("QuoteID")]
    public int QuoteId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(100)]
    public string? RequesterName { get; set; }

    [StringLength(100)]
    public string? RequesterEmail { get; set; }

    public string? Description { get; set; }

    public DateOnly? DesiredDeliveryDate { get; set; }

    [StringLength(20)]
    public string? StatusName { get; set; }

    public bool IsConvertedToOrder { get; set; }

    [StringLength(255)]
    public string? UploadedImagePath { get; set; }
}
