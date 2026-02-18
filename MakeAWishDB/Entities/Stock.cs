using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Table("Stock")]
[Index("IngredientId", Name = "index_stock_ingredient")]
public partial class Stock
{
    [Key]
    [Column("StockID")]
    public int StockId { get; set; }

    [Column("IngredientID")]
    public int IngredientId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal QuantityAvailable { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdated { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("IngredientId")]
    [InverseProperty("Stocks")]
    public virtual Ingredient? Ingredient { get; set; }
}
