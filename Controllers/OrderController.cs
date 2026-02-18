using BusinessLogic.Services.Shopping;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalWWW.Models;
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

        // KATALOG DLA PORTALWWW + DESKTOPAPP
        private const string SharedUploadsRoot = @"C:\MakeAWishShared\uploads";

        public OrderController(
            CatalogOrderService catalogOrderService,
            QuoteRequestService quoteRequestService,
            SharedData_Entities db)
        {
            _catalogOrderService = catalogOrderService;
            _quoteRequestService = quoteRequestService;
            _db = db;
        }

        
        // ORDER ENTRY 
        

        [HttpGet]
        public IActionResult OrderEntry()
        {
            var header = _db.PageHeaders
                .Where(p => p.PageHeaderId == 7 && p.IsActive == true && p.IsVisible == true)

                .Select(p => p.DisplayedHeader)
                .FirstOrDefault();

            var steps = _db.OrderSteps
                .Where(s => s.IsActive == true)

                .OrderBy(s => s.StepNo)
                .ToList();

            var model = new OrderEntryViewModel
            {
                Header = header,
                Steps = steps
            };

            return View(model);
        }

        
        // CATALOG ORDER
        

        [HttpGet]
        public IActionResult CatalogOrder()
        {
            PrepareCatalogDropdowns();
            return View(new CatalogOrderViewModel());
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

            return RedirectToAction("OrderConfirmation");
        }

        
        // QUOTE REQUEST
        

        [HttpGet]
        public IActionResult QuoteRequest()
        {
            PrepareShopDropdown();
            return View(new QuoteRequestViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuoteRequest(QuoteRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PrepareShopDropdown();
                return View("QuoteRequest", model);
            }

            string? imagePath = null;

            if (model.UploadedImage != null && model.UploadedImage.Length > 0)
            {
                // SPRAWDZAMY CZY KATALOG ISTNIEJE, JEŚLI NIE TO GO TWORZYMY (TAK DLA PORTALWWW I DESKTOPAPP)
                Directory.CreateDirectory(SharedUploadsRoot);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.UploadedImage.FileName);
                var fullPath = Path.Combine(SharedUploadsRoot, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.UploadedImage.CopyToAsync(stream);
                }

                //ZAPISUJEMY ŚCIEŻKĘ WEBOWĄ (TAKĄ SAMĄ DLA DESKTOPAPP)
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

            return RedirectToAction("OrderConfirmation");
        }

        // CONFIRMATION
       

        [HttpGet]
        public IActionResult OrderConfirmation()
        {
            return View();
        }

        
        // HELPERS
      

        private void PrepareCatalogDropdowns()
        {
            PrepareShopDropdown();

            ViewBag.Cakes = new SelectList(
                _db.CelebrationCakeSizes.Where(s => s.IsActive == true),

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
