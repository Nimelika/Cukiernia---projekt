using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Keyless]
public partial class ViewCurrentStockShortage
{
    [Column("ShortageID")]
    public int ShortageId { get; set; }

    [Column("IngredientID")]
    public int IngredientId { get; set; }

    [Required]
    [StringLength(100)]
    public string? IngredientName { get; set; }

    [Column("OrderItemID")]
    public int OrderItemId { get; set; }

    [StringLength(255)]
    public string? CakeName { get; set; }

    [StringLength(20)]
    public string? Name { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    public int? Customer { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CollectionDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal QuantityMissing { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecordedDate { get; set; }
}
