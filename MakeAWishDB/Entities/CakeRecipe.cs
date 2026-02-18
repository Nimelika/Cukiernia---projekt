using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("CelebrationCakeId", "CelebrationCakeSizeId", Name = "index_recipes_cake_size")]
public partial class CakeRecipe
{
    [Key]
    [Column("RecipeID")]
    public int RecipeId { get; set; }

    [Column("CelebrationCakeID")]
    public int CelebrationCakeId { get; set; }

    [Column("CelebrationCakeSizeID")]
    public int CelebrationCakeSizeId { get; set; }

    [Column("IngredientID")]
    public int IngredientId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Quantity { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CelebrationCakeId")]
    [InverseProperty("CakeRecipes")]
    public virtual CelebrationCake? CelebrationCake { get; set; }

    [ForeignKey("CelebrationCakeSizeId")]
    [InverseProperty("CakeRecipes")]
    public virtual CelebrationCakeSize? CelebrationCakeSize { get; set; }

    [ForeignKey("IngredientId")]
    [InverseProperty("CakeRecipes")]
    public virtual Ingredient? Ingredient { get; set; }
}
