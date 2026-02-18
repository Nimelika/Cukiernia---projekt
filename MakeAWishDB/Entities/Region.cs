using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class Region
{
    [Key]
    [Column("RegionID")]
    public int RegionId { get; set; }

    [Required]
    [StringLength(100)]
    public string? RegionName { get; set; }

    [Column("CountryID")]
    public int CountryId { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("Regions")]
    public virtual Country? Country { get; set; }

    [InverseProperty("DeliveryRegionNavigation")]
    public virtual ICollection<Customer> CustomerDeliveryRegionNavigations { get; set; } = new List<Customer>();

    [InverseProperty("RegionNavigation")]
    public virtual ICollection<Customer> CustomerRegionNavigations { get; set; } = new List<Customer>();
}
