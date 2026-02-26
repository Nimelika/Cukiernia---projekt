using System.Collections.Generic;

namespace PortalWWW.Models
{
    public class WhoWeArePageViewModel
    {
        // HERO
        public string? Header { get; set; }
        public string? HeroImagePath { get; set; }
        public string? HeroImageAlt { get; set; }

        // CONTENT
        public List<WhoWeAreSectionViewModel> Sections { get; set; } = new();
    }

    public class WhoWeAreSectionViewModel
    {
        public string? Title { get; set; }
        public string? Body { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageAlt { get; set; }
    }
}
