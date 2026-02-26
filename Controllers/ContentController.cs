using MakeAWishDB.Context;
using Microsoft.AspNetCore.Mvc;
using PortalWWW.Models;
using PortalWWW.Services;
using System.Linq;

namespace PortalWWW.Controllers
{
    public class ContentController : Controller
    {
        private readonly SharedData_Entities _db;
        private readonly PageHeroProvider _hero;

        public ContentController(
            SharedData_Entities db,
            PageHeroProvider hero)
        {
            _db = db;
            _hero = hero;
        }

        // =========================
        // ARTICLE PAGES
        // =========================

        public IActionResult Legal()
        {
            return View("Article", GetPageContent(8));
        }

        public IActionResult Privacy()
        {
            return View("Article", GetPageContent(9));
        }

        public IActionResult Cookies()
        {
            return View("Article", GetPageContent(10));
        }

        // =========================
        // CONTENT BUILDER
        // =========================

        private PageContentViewModel GetPageContent(int pageHeaderId)
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
    }
}

