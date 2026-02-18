using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class Status
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(20)]
    public string? Name { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<QuoteRequest> QuoteRequests { get; set; } = new List<QuoteRequest>();
}
