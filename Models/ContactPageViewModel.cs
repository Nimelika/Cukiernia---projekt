using System.ComponentModel.DataAnnotations;

namespace PortalWWW.Models
{
    public class ContactPageViewModel
    {
        // HERO
        public string? Header { get; set; }
        public string? HeroImagePath { get; set; }
        public string? HeroImageAlt { get; set; }

        // SHOP INFO
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Mail { get; set; }
        public string? GoogleMapsUrl { get; set; }

        // FORM
        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(150)]
        public string? Subject { get; set; }

        [Required]
        public string? MessageText { get; set; }

        // CONSENTS
        [Range(typeof(bool), "true", "true",
            ErrorMessage = "Privacy policy consent is required")]
        public bool AcceptRodo { get; set; }

        [Range(typeof(bool), "true", "true",
            ErrorMessage = "Terms and conditions consent is required")]
        public bool AcceptTerms { get; set; }
    }
}
