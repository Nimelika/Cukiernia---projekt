using System.Collections.Generic;

namespace PortalWWW.Models
{
    public class OurShopsPageViewModel
    {
        // HERO
        public string? Header { get; set; }
        public string? HeroImagePath { get; set; }
        public string? HeroImageAlt { get; set; }

        // SHOPS
        public List<ShopItemViewModel> Shops { get; set; } = new();
    }

    public class ShopItemViewModel
    {
        public string? StreetAddress { get; set; }
        public string? City { get; set; }

        public string? OpeningHoursMdFr { get; set; }
        public string? OpeningHoursSt { get; set; }

        public string? PhoneNumber { get; set; }
        public string? GoogleMapsUrl { get; set; }
    }
}
