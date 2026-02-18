using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Keyless]
public partial class ViewNewOrder
{
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    [StringLength(100)]
    public string? CustomerName { get; set; }

    [StringLength(100)]
    public string? CustomerEmail { get; set; }

    public int Shop { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CollectionDate { get; set; }

    [StringLength(20)]
    public string? StatusName { get; set; }

    public bool? IsPaid { get; set; }
}
