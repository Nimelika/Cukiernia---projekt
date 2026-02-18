using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[PrimaryKey("RoleId", "ModuleId")]
[Table("ModuleAccess")]
public partial class ModuleAccess
{
    [Key]
    [Column("RoleID")]
    public int RoleId { get; set; }

    [Key]
    [Column("ModuleID")]
    public int ModuleId { get; set; }

    public bool HasAccess { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("ModuleAccesses")]
    public virtual Module? Module { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("ModuleAccesses")]
    public virtual UserRole? Role { get; set; }
}
