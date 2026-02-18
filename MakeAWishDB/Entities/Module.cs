using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("ModuleName", Name = "UQ__Modules__EAC9AEC3AFBB1771", IsUnique = true)]
public partial class Module
{
    [Key]
    [Column("ModuleID")]
    public int ModuleId { get; set; }

    [Required]
    [StringLength(100)]
    public string? ModuleName { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    public bool IsActive { get; set; }

    [Required]
    [StringLength(50)]
    public string Code { get; set; } = null!;


    [InverseProperty("Module")]
    public virtual ICollection<ModuleAccess> ModuleAccesses { get; set; } = new List<ModuleAccess>();
}
