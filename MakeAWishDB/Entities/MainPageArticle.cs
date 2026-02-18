using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class MainPageArticle
{
    [Key]
    [Column("ArticleID")]
    public int ArticleId { get; set; }

    [Column("ImageURL")]
    public string? ImageUrl { get; set; }

    [StringLength(255)]
    public string? ImageAlt { get; set; }

    [StringLength(255)]
    public string? Title { get; set; }

    public string? Body { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PublishDate { get; set; }

    public bool? IsPublished { get; set; }

    public bool IsActive { get; set; }
}
