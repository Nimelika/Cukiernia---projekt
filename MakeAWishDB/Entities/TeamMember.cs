using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class TeamMember
{
    [Key]
    [Column("TeamMemberID")]
    public int TeamMemberId { get; set; }

    [Column("ImageURL")]
    public string? ImageUrl { get; set; }

    [StringLength(255)]
    public string? ImageAlt { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}
