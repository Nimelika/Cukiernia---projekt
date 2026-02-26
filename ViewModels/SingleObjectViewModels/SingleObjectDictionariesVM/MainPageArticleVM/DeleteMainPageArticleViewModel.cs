using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using System.Linq;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.MainPageArticleVM
{
    public class DeleteMainPageArticleViewModel
        : DeleteViewModel<MainPageArticle>
    {
        public DeleteMainPageArticleViewModel()
            : base("Delete Main Page Article")
        {
        }

        // Nazwa komunikatu do odświeżenia listy artykułów
        protected override string RefreshMessageTag => "AllMainPageArticles";

        public void Load(int id)
        {
            item = sharedData_Entities.MainPageArticles
                .FirstOrDefault(a => a.ArticleId == id);
        }

        // Pola tylko do wyświetlenia w widoku
        public string Title => item?.Title;
        public string ImageAlt => item?.ImageAlt;
    }
}

