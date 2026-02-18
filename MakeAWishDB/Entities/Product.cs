using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    public int ProductCategory { get; set; }

    [Column("CelebrationCakeID")]
    public int? CelebrationCakeId { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("CelebrationCakeId")]
    [InverseProperty("Products")]
    public virtual CelebrationCake? CelebrationCake { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<InvoiceItem>? InvoiceItems { get; set; } = new List<InvoiceItem>();

    [ForeignKey("ProductCategory")]
    [InverseProperty("Products")]
    public virtual ProductCategory? ProductCategoryNavigation { get; set; }
}
