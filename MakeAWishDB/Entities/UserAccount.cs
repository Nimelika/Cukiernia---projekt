using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("Email", Name = "UQ_UserAccounts_Email", IsUnique = true)]
public partial class UserAccount
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [Required]
    [StringLength(255)]
    public string? PasswordHash { get; set; }

    [Required]
    [StringLength(150)]
    public string? Email { get; set; }

    public int UserRole { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(100)]
    public string? DisplayName { get; set; }


    [InverseProperty("User")]
    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    [ForeignKey("UserRole")]
    [InverseProperty("UserAccounts")]
    public virtual UserRole? UserRoleNavigation { get; set; }
}
