using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("IngredientId", "IsValid", Name = "index_shortages_ingredient_valid")]
[Index("OrderItemId", Name = "index_shortages_orderitem")]
public partial class StockShortage
{
    [Key]
    [Column("ShortageID")]
    public int ShortageId { get; set; }

    [Column("IngredientID")]
    public int IngredientId { get; set; }

    [Column("OrderItemID")]
    public int OrderItemId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal QuantityMissing { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RecordedDate { get; set; }

    public bool IsValid { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("IngredientId")]
    [InverseProperty("StockShortages")]
    public virtual Ingredient? Ingredient { get; set; }

    [ForeignKey("OrderItemId")]
    [InverseProperty("StockShortages")]
    public virtual OrderItem? OrderItem { get; set; }
}
