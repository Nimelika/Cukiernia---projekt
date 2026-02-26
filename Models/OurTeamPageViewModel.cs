using System.Collections.Generic;

namespace PortalWWW.Models
{
    public class OurTeamPageViewModel
    {
        // HERO
        public string? Header { get; set; }
        public string? HeroImagePath { get; set; }
        public string? HeroImageAlt { get; set; }

        // TEAM
        public List<TeamMemberViewModel> Members { get; set; } = new();
    }

    public class TeamMemberViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageAlt { get; set; }
    }
}

