using System.Collections.Generic;

namespace PortalWWW.Models
{
    public class OfferPageViewModel
    {
        public string? Header { get; set; }
        public string? HeroImagePath { get; set; }
        public string? HeroImageAlt { get; set; }

        public List<OfferViewModel> Categories { get; set; } = new();
    }
}
