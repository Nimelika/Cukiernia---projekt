using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class Shop
{
    [Key]
    [Column("ShopID")]
    public int ShopId { get; set; }

    [Required]
    [StringLength(255)]
    public string? InternalName { get; set; }

    [Column("ImageURL")]
    public string? ImageUrl { get; set; }

    [StringLength(255)]
    public string? ImageAlt { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(255)]
    public string? StreetAddress { get; set; }

    [StringLength(20)]
    public string? PostalCode { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    public string? Mail { get; set; }

    [StringLength(100)]
    public string? OpeningHoursMdFr { get; set; }

    [StringLength(100)]
    public string? OpeningHoursSt { get; set; }

    [Column("GoogleMapsURL")]
    [StringLength(500)]
    public string? GoogleMapsUrl { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("ShopNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
