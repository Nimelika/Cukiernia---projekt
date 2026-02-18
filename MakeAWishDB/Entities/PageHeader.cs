using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("InternalName", Name = "UQ__PageHead__CAD7CE323DE0811F", IsUnique = true)]
public partial class PageHeader
{
    [Key]
    [Column("PageHeaderID")]
    public int PageHeaderId { get; set; }

    [StringLength(100)]
    public string? InternalName { get; set; }

    [StringLength(255)]
    public string? DisplayedHeader { get; set; }

    public bool? IsVisible { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("PageHeader")]
    public virtual ICollection<LongArticle>? LongArticles { get; set; } = new List<LongArticle>();
}
