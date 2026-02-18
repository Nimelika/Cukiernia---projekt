using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class CelebrationCake
{
    [Key]
    [Column("CelebrationCakeID")]
    public int CelebrationCakeId { get; set; }

    [Column("ImageURL")]
    public string? ImageUrl { get; set; }

    [StringLength(255)]
    public string? ImageAlt { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int CakeFilling { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PriceSmall { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PriceMedium { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PriceLarge { get; set; }

    public bool? WithNuts { get; set; }

    public bool? WithFruits { get; set; }

    public bool? WithAlcohol { get; set; }

    public bool? Vegan { get; set; }

    public bool? LowSugar { get; set; }

    public bool? NoSugar { get; set; }

    public bool? WeddingOffer { get; set; }

    public bool? ChildrenOffer { get; set; }

    public bool? IsActive { get; set; }

    [InverseProperty("CelebrationCake")]
    public virtual ICollection<CakeAllergen> CakeAllergens { get; set; } = new List<CakeAllergen>();

    [ForeignKey("CakeFilling")]
    [InverseProperty("CelebrationCakes")]
    public virtual CakeFilling? CakeFillingNavigation { get; set; }

    [InverseProperty("CelebrationCake")]
    public virtual ICollection<CakeRecipe> CakeRecipes { get; set; } = new List<CakeRecipe>();

    [InverseProperty("CelebrationCakeNavigation")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("CelebrationCake")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("CelebrationCakeNavigation")]
    public virtual Recomendation? Recomendation { get; set; }

    [InverseProperty("CelebrationCake")]
    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
}
