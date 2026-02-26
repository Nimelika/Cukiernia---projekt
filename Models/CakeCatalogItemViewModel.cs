namespace PortalWWW.Models
{
    public class CakeCatalogItemViewModel
    {
        public string? ImageUrl { get; set; }
        public string? ImageAlt { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? FillingName { get; set; }

        public decimal? PriceSmall { get; set; }
        public decimal? PriceMedium { get; set; }
        public decimal? PriceLarge { get; set; }

        public bool? Vegan { get; set; }
        public bool? WithNuts { get; set; }
        public bool? WithFruits { get; set; }
        public bool? WithAlcohol { get; set; }
        public bool? LowSugar { get; set; }
        public bool? NoSugar { get; set; }
        public bool? WeddingOffer { get; set; }
        public bool? ChildrenOffer { get; set; }
    }
}
