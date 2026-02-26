namespace PortalWWW.Models
{
    public class CakeCatalogPageViewModel
    {
        public string? Header { get; set; }
        public string? HeroImagePath { get; set; }
        public string? HeroImageAlt { get; set; }

        public List<CakeCatalogItemViewModel> Cakes { get; set; } = new();
    }
}
