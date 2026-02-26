using System.Collections.Generic;

namespace PortalWWW.Models
{
    public class PageContentViewModel
    {
        public string? Header { get; set; }

        // HERO IMAGE
        public string? HeroImagePath { get; set; }
        public string? HeroImageAlt { get; set; }

        // PAGE CONTENT
        public List<PageSectionViewModel> Sections { get; set; } = new();
    }

    public class PageSectionViewModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}

