using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("IsAvailable", Name = "index_ingredients_isavailable")]
public partial class Ingredient
{
    [Key]
    [Column("IngredientID")]
    public int IngredientId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("UnitID")]
    public int UnitId { get; set; }

    public bool IsAllergenic { get; set; }

    public bool IsVegan { get; set; }

    public bool IsAvailable { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Ingredient")]
    public virtual ICollection<CakeAllergen> CakeAllergens { get; set; } = new List<CakeAllergen>();

    [InverseProperty("Ingredient")]
    public virtual ICollection<CakeRecipe> CakeRecipes { get; set; } = new List<CakeRecipe>();

    [InverseProperty("Ingredient")]
    public virtual ICollection<IngredientReservation> IngredientReservations { get; set; } = new List<IngredientReservation>();

    [InverseProperty("Ingredient")]
    public virtual ICollection<StockShortage> StockShortages { get; set; } = new List<StockShortage>();

    [InverseProperty("Ingredient")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    [ForeignKey("UnitId")]
    [InverseProperty("Ingredients")]
    public virtual Unit? Unit { get; set; }
}
