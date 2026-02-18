using System;
using System.ComponentModel.DataAnnotations;

namespace PortalWWW.Models
{
    public class CatalogOrderViewModel
    {
        public CatalogOrderViewModel()
        {
            CollectionDate = DateTime.Today.AddDays(3);
        }

        
        // GUEST DETAILS
        

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        public string GuestName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100)]
        public string GuestEmail { get; set; }

        [StringLength(20)]
        public string GuestPhone { get; set; }

        
        // COLLECTION DETAILS
      

        [Required(ErrorMessage = "Please select a pickup location")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "Please select a pickup date")]
        [DataType(DataType.Date)]
        public DateTime CollectionDate { get; set; }

       
        // CAKE FROM CATALOG
       

        [Required(ErrorMessage = "Please select a cake")]
        public int CelebrationCakeId { get; set; }

        [Required(ErrorMessage = "Please select a cake size")]
        public int CelebrationCakeSizeId { get; set; }

        [Range(1, 100, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; } = 1;

        
        // PERSONALIZATION
      
        [StringLength(255, ErrorMessage = "Text is too long")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string? PersonalizedText { get; set; }

        [StringLength(500, ErrorMessage = "Notes are too long")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string? Notes { get; set; }


        
        // CONSENTS
        

        [Range(typeof(bool), "true", "true",
    ErrorMessage = "Privacy Policy consent is required")]
        public bool AcceptRodo { get; set; }

        [Range(typeof(bool), "true", "true",
            ErrorMessage = "Terms and Conditions consent is required")]
        public bool AcceptTerms { get; set; }


    }
}
