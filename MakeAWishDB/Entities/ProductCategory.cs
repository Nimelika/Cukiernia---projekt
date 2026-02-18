using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class ProductCategory
{
    [Key]
    [Column("ProductCategoryID")]
    public int ProductCategoryId { get; set; }

    [StringLength(255)]
    public string? ImageAlt { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    public byte[]? ImageData { get; set; }

    public bool? IsActive { get; set; }

    [InverseProperty("ProductCategoryNavigation")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
