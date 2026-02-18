using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("IngredientId", Name = "index_reservations_ingredient")]
[Index("OrderItemId", Name = "index_reservations_orderitem")]
[Index("IsCancelled", Name = "index_reservations_status")]
[Index("IsUsed", Name = "index_reservations_usage")]
public partial class IngredientReservation
{
    [Key]
    [Column("ReservationID")]
    public int ReservationId { get; set; }

    [Column("OrderItemID")]
    public int OrderItemId { get; set; }

    [Column("CelebrationCakeSizeID")]
    public int CelebrationCakeSizeId { get; set; }

    [Column("IngredientID")]
    public int IngredientId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal QuantityReserved { get; set; }

    public bool IsUsed { get; set; }

    public bool IsCancelled { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CelebrationCakeSizeId")]
    [InverseProperty("IngredientReservations")]
    public virtual CelebrationCakeSize? CelebrationCakeSize { get; set; }

    [ForeignKey("IngredientId")]
    [InverseProperty("IngredientReservations")]
    public virtual Ingredient? Ingredient { get; set; }

    [ForeignKey("OrderItemId")]
    [InverseProperty("IngredientReservations")]
    public virtual OrderItem? OrderItem { get; set; }
}
