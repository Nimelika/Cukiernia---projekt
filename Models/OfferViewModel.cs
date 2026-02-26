namespace PortalWWW.Models
{
    public class OfferViewModel
    { public int CategoryId { get; set; } 
        public string? CategoryName { get; set; } 
        public string? ImagePath { get; set; } 
        public string? ImageAlt { get; set; } 
        public List<string> Products { get; set; } = new(); 
    }
}