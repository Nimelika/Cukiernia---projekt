using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("CelebrationCake", Name = "index_orderitems_cake")]
[Index("OrderId", Name = "index_orderitems_order")]
public partial class OrderItem
{
    [Key]
    [Column("OrderItemID")]
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int CelebrationCake { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal UnitPrice { get; set; }

    public int? CelebrationCakeSize { get; set; }

    [StringLength(255)]
    public string? PersonalizedText { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CelebrationCake")]
    [InverseProperty("OrderItems")]
    public virtual CelebrationCake? CelebrationCakeNavigation { get; set; }

    [ForeignKey("CelebrationCakeSize")]
    [InverseProperty("OrderItems")]
    public virtual CelebrationCakeSize? CelebrationCakeSizeNavigation { get; set; }

    [InverseProperty("OrderItem")]
    public virtual ICollection<IngredientReservation>? IngredientReservations { get; set; } = new List<IngredientReservation>();

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order? Order { get; set; }

    [InverseProperty("OrderItem")]
    public virtual ICollection<StockShortage> StockShortages { get; set; } = new List<StockShortage>();
}
