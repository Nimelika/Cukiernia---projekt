using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace PortalWWW.Models
{
    public class QuoteRequestViewModel
    {
        public QuoteRequestViewModel()
        {
            DesiredDeliveryDate = DateOnly.FromDateTime(
                DateTime.Today.AddDays(7)
            );
        }

        [Required(ErrorMessage = "Full name is required")]
        public string GuestName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string GuestEmail { get; set; }

        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please select a pickup location")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "Please select desired delivery date")]
        public DateOnly DesiredDeliveryDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000)]
        public string Description { get; set; }

        public IFormFile? UploadedImage { get; set; }

        [Range(typeof(bool), "true", "true",
            ErrorMessage = "Privacy Policy consent is required")]
        public bool AcceptRodo { get; set; }

        [Range(typeof(bool), "true", "true",
            ErrorMessage = "Terms and Conditions consent is required")]
        public bool AcceptTerms { get; set; }
    }
}
