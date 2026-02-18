using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class UserRole
{
    [Key]
    [Column("RoleID")]
    public int RoleId { get; set; }

    [Required]
    [StringLength(50)]
    public string? RoleName { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<ModuleAccess> ModuleAccesses { get; set; } = new List<ModuleAccess>();

    [InverseProperty("UserRoleNavigation")]
    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
