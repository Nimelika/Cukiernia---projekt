using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class CelebrationCakeSize
{
    [Key]
    public int CelebrationCakeSizeId { get; set; }

    [StringLength(20)]
    public string? Name { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("CelebrationCakeSize")]
    public virtual ICollection<CakeRecipe> CakeRecipes { get; set; } = new List<CakeRecipe>();

    [InverseProperty("CelebrationCakeSize")]
    public virtual ICollection<IngredientReservation> IngredientReservations { get; set; } = new List<IngredientReservation>();
    
    [InverseProperty("CelebrationCakeSizeNavigation")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
