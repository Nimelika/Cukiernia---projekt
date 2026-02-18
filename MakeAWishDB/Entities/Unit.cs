using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class Unit
{
    [Key]
    [Column("UnitID")]
    public int UnitId { get; set; }

    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(10)]
    public string? Abbreviation { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Unit")]
    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
