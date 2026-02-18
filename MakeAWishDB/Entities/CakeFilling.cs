using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class CakeFilling
{
    [Key]
    [Column("CakeFillingID")]
    public int CakeFillingId { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("CakeFillingNavigation")]
    public virtual ICollection<CelebrationCake> CelebrationCakes { get; set; } = new List<CelebrationCake>();
}
