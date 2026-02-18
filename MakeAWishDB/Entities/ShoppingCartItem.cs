using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class ShoppingCartItem
{
    [Key]
    [Column("CartItemID")]
    public int CartItemId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [StringLength(100)]
    public string? SessionToken { get; set; }

    [Column("CelebrationCakeID")]
    public int CelebrationCakeId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AddedAt { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CelebrationCakeId")]
    [InverseProperty("ShoppingCartItems")]
    public virtual CelebrationCake? CelebrationCake { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ShoppingCartItems")]
    public virtual UserAccount? User { get; set; }
}
