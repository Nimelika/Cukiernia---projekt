using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("CountryAbbreviation", Name = "UQ__Countrie__0F71A1ED6680DA07", IsUnique = true)]
[Index("CountryName", Name = "UQ__Countrie__E056F201E44B3207", IsUnique = true)]
public partial class Country
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string? CountryAbbreviation { get; set; }

    [Required]
    [StringLength(50)]
    public string? CountryName { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("CountryNavigation")]
    public virtual ICollection<Customer>? CustomerCountryNavigations { get; set; } = new List<Customer>();

    [InverseProperty("DeliveryCountryNavigation")]
    public virtual ICollection<Customer>? CustomerDeliveryCountryNavigations { get; set; } = new List<Customer>();

    [InverseProperty("Country")]
    public virtual ICollection<Region>? Regions { get; set; } = new List<Region>();
}
