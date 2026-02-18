using MakeAWishDB.Context;
using Microsoft.AspNetCore.Mvc;
using PortalWWW.Models;

namespace PortalWWW.Controllers
{
    public class ContentController : Controller
    {
        private readonly SharedData_Entities _db;

        public ContentController(SharedData_Entities db)
        {
            _db = db;
        }

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

        private PageContentViewModel GetPageContent(int pageHeaderId)
        {
            var header = _db.PageHeaders
                .Where(p => p.PageHeaderId == pageHeaderId && p.IsActive && p.IsVisible == true)
                .Select(p => p.DisplayedHeader)
                .FirstOrDefault();

            var article = _db.LongArticles
                .FirstOrDefault(a => a.PageHeaderId == pageHeaderId && a.IsActive && a.IsPublished == true);

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

    }

}
