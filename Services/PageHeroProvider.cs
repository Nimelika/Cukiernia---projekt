using MakeAWishDB.Context;
using PortalWWW.Models;
using System.Linq;

namespace PortalWWW.Services
{
    public class PageHeroProvider
    {
        private readonly SharedData_Entities _db;

        public PageHeroProvider(SharedData_Entities db)
        {
            _db = db;
        }

        public PageHeroViewModel GetHero(int pageHeaderId)
        {
            var header = _db.PageHeaders
                .Where(p =>
                    p.PageHeaderId == pageHeaderId &&
                    p.IsActive == true &&
                    p.IsVisible == true)
                .Select(p => p.DisplayedHeader)
                .FirstOrDefault();

            var hero = _db.PageHeroImages
                .Where(h =>
                    h.PageHeaderId == pageHeaderId &&
                    h.IsActive == true &&
                    h.IsVisible == true)
                .OrderBy(h => h.DisplayOrder)
                .Select(h => new
                {
                    h.ImagePath,
                    h.ImageAlt
                })
                .FirstOrDefault();

            return new PageHeroViewModel
            {
                Header = header,
                HeroImagePath = hero?.ImagePath,
                HeroImageAlt = hero?.ImageAlt
            };
        }
    }
}

