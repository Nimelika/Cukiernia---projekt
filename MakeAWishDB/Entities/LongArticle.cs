using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class LongArticle
{
    [Key]
    [Column("ArticleID")]
    public int ArticleId { get; set; }

    [Column("PageHeaderID")]
    public int PageHeaderId { get; set; }

    [StringLength(255)]
    public string? Section1Title { get; set; }

    public string? Section1Content { get; set; }

    [StringLength(255)]
    public string? Section2Title { get; set; }

    public string? Section2Content { get; set; }

    [StringLength(255)]
    public string? Section3Title { get; set; }

    public string? Section3Content { get; set; }

    [StringLength(255)]
    public string? Section4Title { get; set; }

    public string? Section4Content { get; set; }

    [StringLength(255)]
    public string? Section5Title { get; set; }

    public string? Section5Content { get; set; }

    [StringLength(255)]
    public string? Section6Title { get; set; }

    public string? Section6Content { get; set; }

    [StringLength(255)]
    public string? Section7Title { get; set; }

    public string? Section7Content { get; set; }

    public bool? IsPublished { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("PageHeaderId")]
    [InverseProperty("LongArticles")]
    public virtual PageHeader? PageHeader { get; set; }
}
