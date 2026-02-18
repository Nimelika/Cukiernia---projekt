using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalWWW.Models;
using MakeAWishDB.Context;
using System.Diagnostics;
using System.Linq;

namespace PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SharedData_Entities _db;

        public HomeController(
            ILogger<HomeController> logger,
            SharedData_Entities db)
        {
            _logger = logger;
            _db = db;
        }

        
        // HOME PAGE
       
        public IActionResult Index()
        {
            // Index ma w³asny widok, ale treœæ z bazy
            var model = GetPageContent(1);
            return View(model);
        }

        
        // INDIVIDUAL LAYOUT PAGES
        
        public IActionResult WhoWeAre()
        {
            return View();
        }

        public IActionResult Offer()
        {
            return View();
        }

        public IActionResult OurTeam()
        {
            return View();
        }

        public IActionResult JobOffer()
        {
            return View();
        }

        public IActionResult OurShops()
        {
            return View();
        }

        public IActionResult CakeOrder()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        
        // ARTICLE-STYLE PAGES
        // (shared Article view)
        
        public IActionResult LegalNotice()
        {
            return View("Article", GetPageContent(8));
        }

        public IActionResult Privacy()
        {
            return View("Article", GetPageContent(9));
        }

        public IActionResult CookiesPolicy()
        {
            return View("Article", GetPageContent(10));
        }

        
        // CONTENT BUILDER
       
        private PageContentViewModel GetPageContent(int pageHeaderId)
        {
            var header = _db.PageHeaders
    .Where(p =>
        p.PageHeaderId == pageHeaderId &&
        p.IsActive &&
        p.IsVisible == true)
    .Select(p => p.DisplayedHeader)
    .FirstOrDefault();


            var article = _db.LongArticles
     .FirstOrDefault(a =>
         a.PageHeaderId == pageHeaderId &&
         a.IsActive &&
         a.IsPublished == true);

            var model = new PageContentViewModel
            {
                Header = header
            };

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

        
        // ERROR HANDLING
       
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
