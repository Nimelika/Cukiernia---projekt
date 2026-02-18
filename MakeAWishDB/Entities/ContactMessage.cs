using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class ContactMessage
{
    [Key]
    [Column("MessageID")]
    public int MessageId { get; set; }

    public int? Customer { get; set; }

    [Required]
    [StringLength(100)]
    public string? FullName { get; set; }

    [Required]
    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [Required]
    [StringLength(150)]
    public string? Subject { get; set; }

    [Required]
    public string? MessageText { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SubmittedAt { get; set; }

    public bool? IsReplied { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("Customer")]
    [InverseProperty("ContactMessages")]
    public virtual Customer? CustomerNavigation { get; set; }
}
