using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("StepNo", Name = "UQ__OrderSte__2434686271605109", IsUnique = true)]
public partial class OrderStep
{
    [Key]
    [Column("OrderStepID")]
    public int OrderStepId { get; set; }

    [Column("ImageURL")]
    public string? ImageUrl { get; set; }

    public int StepNo { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}
