using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class Customer
{
    [Key]
    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string? FullName { get; set; }

    [Required]
    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? Fax { get; set; }

    [StringLength(255)]
    public string? Website { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(20)]
    public string? StreetNumber { get; set; }

    [StringLength(20)]
    public string? PostalCode { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    public int? Region { get; set; }

    public int Country { get; set; }

    [StringLength(100)]
    public string? DeliveryStreet { get; set; }

    [StringLength(20)]
    public string? DeliveryStreetNumber { get; set; }

    [StringLength(20)]
    public string? DeliveryPostalCode { get; set; }

    [StringLength(100)]
    public string? DeliveryCity { get; set; }

    public int? DeliveryRegion { get; set; }

    public int? DeliveryCountry { get; set; }

    public bool? IsCompany { get; set; }

    [StringLength(150)]
    public string? CompanyName { get; set; }

    [Column("NIP")]
    [StringLength(20)]
    public string? Nip { get; set; }

    [Column("REGON")]
    [StringLength(20)]
    public string? Regon { get; set; }

    [StringLength(100)]
    public string SearchTerm { get; set; }

    [StringLength(255)]
    public string Notes { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RegisteredAt { get; set; }

    [InverseProperty("CustomerNavigation")]
    public virtual ICollection<ContactMessage> ContactMessages { get; set; } = new List<ContactMessage>();

    [ForeignKey("Country")]
    [InverseProperty("CustomerCountryNavigations")]
    public virtual Country CountryNavigation { get; set; }

    [ForeignKey("DeliveryCountry")]
    [InverseProperty("CustomerDeliveryCountryNavigations")]
    public virtual Country DeliveryCountryNavigation { get; set; }

    [ForeignKey("DeliveryRegion")]
    [InverseProperty("CustomerDeliveryRegionNavigations")]
    public virtual Region DeliveryRegionNavigation { get; set; }

    [InverseProperty("CustomerNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("CustomerNavigation")]
    public virtual ICollection<QuoteRequest> QuoteRequests { get; set; } = new List<QuoteRequest>();

    [ForeignKey("Region")]
    [InverseProperty("CustomerRegionNavigations")]
    public virtual Region RegionNavigation { get; set; }
}
