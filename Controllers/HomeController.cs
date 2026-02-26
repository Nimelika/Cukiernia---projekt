using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalWWW.Models;
using PortalWWW.Services;
using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using System;
using System.Diagnostics;
using System.Linq;

namespace PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SharedData_Entities _db;
        private readonly PageHeroProvider _hero;

        public HomeController(
            ILogger<HomeController> logger,
            SharedData_Entities db,
            PageHeroProvider hero)
        {
            _logger = logger;
            _db = db;
            _hero = hero;
        }

        // =========================
        // HOME PAGE
        // =========================

        public IActionResult Index()
        {
            return View(BuildPageContent(1));
        }

        // =========================
        // WHO WE ARE (DYNAMIC)
        // PageHeaderId = 2
        // =========================

        public IActionResult WhoWeAre()
        {
            var hero = _hero.GetHero(2);

            var sections = _db.MainPageArticles
                .Where(a => a.IsActive == true && a.IsPublished == true)
                .OrderBy(a => a.PublishDate)
                .Select(a => new WhoWeAreSectionViewModel
                {
                    Title = a.Title,
                    Body = a.Body,
                    ImageUrl = a.ImageUrl,
                    ImageAlt = a.ImageAlt
                })
                .ToList();

            var model = new WhoWeArePageViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt,
                Sections = sections
            };

            return View(model);
        }

        // =========================
        // STATIC / LAYOUT PAGES
        // =========================

        public IActionResult Offer() => View();
        public IActionResult OurTeam()
        {
            var hero = _hero.GetHero(4);

            var members = _db.TeamMembers
                .Where(m => m.IsActive == true)
                .Select(m => new TeamMemberViewModel
                {
                    Name = m.Name,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    ImageAlt = m.ImageAlt
                })
                .ToList();

            var model = new OurTeamPageViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt,
                Members = members
            };

            return View(model);
        }

        public IActionResult JobOffer() => View();

        // =========================
        // OUR SHOPS (DYNAMIC)
        // =========================

        public IActionResult OurShops()
        {
            var hero = _hero.GetHero(5);

            var shops = _db.Shops
                .Where(s => s.IsActive == true)
                .Select(s => new ShopItemViewModel
                {
                    StreetAddress = s.StreetAddress,
                    City = s.City,
                    OpeningHoursMdFr = s.OpeningHoursMdFr,
                    OpeningHoursSt = s.OpeningHoursSt,
                    PhoneNumber = s.PhoneNumber,
                    GoogleMapsUrl = s.GoogleMapsUrl
                })
                .ToList();

            var model = new OurShopsPageViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt,
                Shops = shops
            };

            return View(model);
        }

        public IActionResult CakeOrder() => View();

        // =========================
        // CONTACT PAGE (DYNAMIC)
        // =========================

        [HttpGet]
        public IActionResult Contact()
        {
            var hero = _hero.GetHero(6);

            var shop = _db.Shops
                .FirstOrDefault(s => s.ShopId == 1 && s.IsActive == true);

            var model = new ContactPageViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt,

                StreetAddress = shop?.StreetAddress,
                PostalCode = shop?.PostalCode,
                City = shop?.City,
                PhoneNumber = shop?.PhoneNumber,
                Mail = shop?.Mail,
                GoogleMapsUrl = shop?.GoogleMapsUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var hero = _hero.GetHero(6);
                var shop = _db.Shops.FirstOrDefault(s => s.ShopId == 1 && s.IsActive == true);

                model.Header = hero.Header;
                model.HeroImagePath = hero.HeroImagePath;
                model.HeroImageAlt = hero.HeroImageAlt;

                model.StreetAddress = shop?.StreetAddress;
                model.PostalCode = shop?.PostalCode;
                model.City = shop?.City;
                model.PhoneNumber = shop?.PhoneNumber;
                model.Mail = shop?.Mail;
                model.GoogleMapsUrl = shop?.GoogleMapsUrl;

                return View(model);
            }

            var message = new ContactMessage
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                Subject = model.Subject,
                MessageText = model.MessageText,
                IsReplied = false,
                IsActive = true,
                SubmittedAt = DateTime.Now
            };

            _db.ContactMessages.Add(message);
            _db.SaveChanges();

            TempData["ContactSuccess"] =
                "Thank you for your message. We will get back to you as soon as possible.";

            return RedirectToAction(nameof(Contact));
        }

        // =========================
        // ARTICLE-STYLE PAGES
        // =========================

        public IActionResult LegalNotice()
        {
            return View("Article", BuildPageContent(8));
        }

        public IActionResult Privacy()
        {
            return View("Article", BuildPageContent(9));
        }

        public IActionResult CookiesPolicy()
        {
            return View("Article", BuildPageContent(10));
        }

        // =========================
        // CAKE CATALOG
        // =========================

        public IActionResult CakeCatalog()
        {
            var hero = _hero.GetHero(11);

            var cakes = _db.CelebrationCakes
                .Where(c => c.IsActive == true)
                .Select(c => new CakeCatalogItemViewModel
                {
                    ImageUrl = c.ImageUrl,
                    ImageAlt = c.ImageAlt,
                    Name = c.Name,
                    Description = c.Description,
                    FillingName = c.CakeFillingNavigation.Name,

                    PriceSmall = c.PriceSmall,
                    PriceMedium = c.PriceMedium,
                    PriceLarge = c.PriceLarge,

                    Vegan = c.Vegan,
                    WithNuts = c.WithNuts,
                    WithFruits = c.WithFruits,
                    WithAlcohol = c.WithAlcohol,
                    LowSugar = c.LowSugar,
                    NoSugar = c.NoSugar,
                    WeddingOffer = c.WeddingOffer,
                    ChildrenOffer = c.ChildrenOffer
                })
                .ToList();

            var model = new CakeCatalogPageViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt,
                Cakes = cakes
            };

            return View(model);
        }

        // =========================
        // CONTENT BUILDER (ARTICLES)
        // =========================

        private PageContentViewModel BuildPageContent(int pageHeaderId)
        {
            var hero = _hero.GetHero(pageHeaderId);

            var model = new PageContentViewModel
            {
                Header = hero.Header,
                HeroImagePath = hero.HeroImagePath,
                HeroImageAlt = hero.HeroImageAlt
            };

            var article = _db.LongArticles
                .FirstOrDefault(a =>
                    a.PageHeaderId == pageHeaderId &&
                    a.IsActive == true &&
                    a.IsPublished == true);

            if (article != null)
            {
                void AddSection(string title, string content)
                {
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        model.Sections.Add(new PageSectionViewModel
                        {
                            Title = title,
                            Content = content
                        });
                    }
                }

                AddSection(article.Section1Title, article.Section1Content);
                AddSection(article.Section2Title, article.Section2Content);
                AddSection(article.Section3Title, article.Section3Content);
                AddSection(article.Section4Title, article.Section4Content);
                AddSection(article.Section5Title, article.Section5Content);
                AddSection(article.Section6Title, article.Section6Content);
                AddSection(article.Section7Title, article.Section7Content);
            }

            return model;
        }

        // =========================
        // ERROR HANDLING
        // =========================

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

