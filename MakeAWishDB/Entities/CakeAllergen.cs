using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class CakeAllergen
{
    [Key]
    [Column("CakeAllergenID")]
    public int CakeAllergenId { get; set; }

    [Column("CelebrationCakeID")]
    public int CelebrationCakeId { get; set; }

    [Column("IngredientID")]
    public int IngredientId { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("CelebrationCakeId")]
    [InverseProperty("CakeAllergens")]
    public virtual CelebrationCake? CelebrationCake { get; set; }

    [ForeignKey("IngredientId")]
    [InverseProperty("CakeAllergens")]
    public virtual Ingredient?Ingredient { get; set; }
}
