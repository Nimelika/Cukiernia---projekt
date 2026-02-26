using BusinessLogic.Services.Shopping;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalWWW.Models;
using PortalWWW.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortalWWW.Controllers
{
    public class OrderController : Controller
    {
        private readonly CatalogOrderService _catalogOrderService;
        private readonly QuoteRequestService _quoteRequestService;
        private readonly SharedData_Entities _db;
        private readonly PageHeroProvider _hero;

        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public OrderController(
            CatalogOrderService catalogOrderService,
            QuoteRequestService quoteRequestService,
            SharedData_Entities db,
            PageHeroProvider hero)
        {
            _catalogOrderService = catalogOrderService;
            _quoteRequestService = quoteRequestService;
            _db = db;
            _hero = hero;
        }

        // =========================
        // ORDER ENTRY (PageHeaderId = 7)
        // =========================

        [HttpGet]
        public IActionResult OrderEntry()
        {
            var hero = _hero.GetHero(7);

            var steps = _db.OrderSteps
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.StepNo)
                .ToList();

            var model = new OrderEntryViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt,
                Steps = steps
            };

            return View(model);
        }

        // =========================
        // CATALOG ORDER (PageHeaderId = 13)
        // =========================

        [HttpGet]
        public IActionResult CatalogOrder()
        {
            PrepareCatalogDropdowns();

            var hero = _hero.GetHero(13);

            return View(new CatalogOrderViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCatalog(CatalogOrderViewModel model)
        {
            if (model.CollectionDate < DateTime.Today.AddDays(3))
            {
                ModelState.AddModelError(
                    nameof(model.CollectionDate),
                    "Pickup date must be at least 3 days from today."
                );
            }

            if (!ModelState.IsValid)
            {
                PrepareCatalogDropdowns();

                var hero = _hero.GetHero(13);
                model.Header = hero.Header;
                model.HeroImagePath = hero.HeroImagePath;
                model.HeroImageAlt = hero.HeroImageAlt;

                return View("CatalogOrder", model);
            }

            await _catalogOrderService.CreateOrderAsync(
                model.GuestName,
                model.GuestEmail,
                model.GuestPhone,
                model.ShopId,
                model.CollectionDate,
                model.CelebrationCakeId,
                model.CelebrationCakeSizeId,
                model.Quantity,
                model.PersonalizedText,
                model.Notes
            );

            return RedirectToAction(nameof(OrderConfirmation));
        }

        // =========================
        // QUOTE REQUEST (PageHeaderId = 12)
        // =========================

        [HttpGet]
        public IActionResult QuoteRequest()
        {
            PrepareShopDropdown();

            var hero = _hero.GetHero(12);

            return View(new QuoteRequestViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuoteRequest(QuoteRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PrepareShopDropdown();

                var hero = _hero.GetHero(12);
                model.Header = hero.Header;
                model.HeroImagePath = hero.HeroImagePath;
                model.HeroImageAlt = hero.HeroImageAlt;

                return View("QuoteRequest", model);
            }

            string? imagePath = null;

            if (model.UploadedImage != null && model.UploadedImage.Length > 0)
            {
                Directory.CreateDirectory(SharedUploadsRoot);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.UploadedImage.FileName);
                var fullPath = Path.Combine(SharedUploadsRoot, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await model.UploadedImage.CopyToAsync(stream);

                imagePath = "/uploads/" + fileName;
            }

            var quote = new QuoteRequest
            {
                GuestName = model.GuestName,
                GuestEmail = model.GuestEmail,
                Phone = model.Phone,
                Description = model.Description,
                DesiredDeliveryDate = model.DesiredDeliveryDate,
                UploadedImagePath = imagePath,
                Status = 1,
                IsConvertedToOrder = false,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            await _quoteRequestService.CreateAsync(quote);

            return RedirectToAction(nameof(OrderConfirmation));
        }

        // =========================
        // ORDER CONFIRMATION (PageHeaderId = 14)
        // =========================

        [HttpGet]
        public IActionResult OrderConfirmation()
        {
            var hero = _hero.GetHero(14);

            return View(new PageHeroViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt
            });
        }

        // =========================
        // HELPERS
        // =========================

        private void PrepareCatalogDropdowns()
        {
            PrepareShopDropdown();

            ViewBag.Cakes = new SelectList(
                _db.CelebrationCakes.Where(x => x.IsActive == true),
                "CelebrationCakeId",
                "Name"
            );

            ViewBag.CakeSizes = new SelectList(
                _db.CelebrationCakeSizes.Where(s => s.IsActive),
                "CelebrationCakeSizeId",
                "Name"
            );
        }

        private void PrepareShopDropdown()
        {
            ViewBag.Shops = new SelectList(
                _db.Shops
                    .Where(s => s.IsActive)
                    .Select(s => new
                    {
                        s.ShopId,
                        DisplayName = s.City + " " + s.StreetAddress
                    }),
                "ShopId",
                "DisplayName"
            );
        }
    }
}
