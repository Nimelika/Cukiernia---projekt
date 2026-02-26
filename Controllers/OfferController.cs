using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeAWishDB.Context;
using PortalWWW.Models;
using PortalWWW.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PortalWWW.Controllers
{
    public class OfferController : Controller
    {
        private readonly SharedData_Entities _context;
        private readonly PageHeroProvider _hero;

        public OfferController(
            SharedData_Entities context,
            PageHeroProvider hero)
        {
            _context = context;
            _hero = hero;
        }

        // =========================
        // OFFER PAGE
        // =========================

        public async Task<IActionResult> Offer()
        {
            // HERO (PageHeaderId = 3 → OFFER)
            var hero = _hero.GetHero(3);

            var categories = await _context.ProductCategories
                .Where(c => c.IsActive == true)
                .Select(c => new OfferViewModel
                {
                    CategoryId = c.ProductCategoryId,
                    CategoryName = c.Name,
                    ImagePath = c.ImagePath,
                    ImageAlt = c.ImageAlt,
                    Products = c.Products
                        .Where(p => p.IsActive == true)
                        .Select(p => p.Name!)
                        .ToList()
                })
                .ToListAsync();

            // Wrapper view model (hero + content)
            var model = new OfferPageViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt,
                Categories = categories
            };

            return View(model);
        }
    }
}
