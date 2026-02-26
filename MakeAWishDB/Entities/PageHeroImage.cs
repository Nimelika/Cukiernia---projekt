using System;

namespace MakeAWishDB.Entities
{
    public class PageHeroImage
    {
        public int PageHeroImageId { get; set; }
        public int PageHeaderId { get; set; }

        public string ImagePath { get; set; } = null!;
        public string? ImageAlt { get; set; }

        public bool? IsVisible { get; set; }
        public bool? IsActive { get; set; }

        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }

        public PageHeader PageHeader { get; set; } = null!;
    }
}

