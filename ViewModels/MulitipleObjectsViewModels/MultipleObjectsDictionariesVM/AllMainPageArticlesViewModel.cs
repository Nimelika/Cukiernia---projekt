using DesktopApp.ViewModels.AbstractViewModels;
using DesktopApp.ViewModels.SingleObjectViewModels.DisplayOnlyVM;
using DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.MainPageArticleVM;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MakeAWishDB.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesktopApp.ViewModels.MulitipleObjectsViewModels.MultipleObjectsDictionariesVM
{
    public class AllMainPageArticlesViewModel : AllViewModel<MainPageArticleViewModel>
    {
        #region Constructor
        public AllMainPageArticlesViewModel()
            : base("Main Page Articles")
        {
            DetailsCommand = new RelayCommand<MainPageArticleViewModel>(ShowDetails);
            UpdateCommand = new RelayCommand<MainPageArticleViewModel>(ShowUpdate);
            DeleteCommand = new RelayCommand<MainPageArticleViewModel>(ShowDelete);

            Messenger.Default.Register<string>(this, "MainPageArticle", msg => { Load(); });
        }
        #endregion

        #region Properties
        private MainPageArticleViewModel _SelectedArticle;
        public MainPageArticleViewModel SelectedArticle
        {
            get => _SelectedArticle;
            set
            {
                if (value != _SelectedArticle)
                {
                    _SelectedArticle = value;
                    Messenger.Default.Send(_SelectedArticle);
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand<MainPageArticleViewModel> DetailsCommand { get; set; }
        public RelayCommand<MainPageArticleViewModel> UpdateCommand { get; set; }
        public RelayCommand<MainPageArticleViewModel> DeleteCommand { get; set; }

        private void ShowDetails(MainPageArticleViewModel article)
        {
            if (article == null) return;

            var displayVM = new DisplayMainPageArticleViewModel();
            displayVM.Load(article.ArticleId);
            Messenger.Default.Send(displayVM, "MainPageArticleDisplay");
        }

        private void ShowUpdate(MainPageArticleViewModel article)
        {
            if (article == null) return;

            var updateVM = new UpdateMainPageArticleViewModel();
            updateVM.Load(article.ArticleId);
            Messenger.Default.Send(updateVM, "MainPageArticleUpdate");
        }

        private void ShowDelete(MainPageArticleViewModel article)
        {
            if (article == null) return;

            var deleteVM = new DeleteMainPageArticleViewModel();
            deleteVM.Load(article.ArticleId);
            Messenger.Default.Send(deleteVM, "MainPageArticleDelete");
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<MainPageArticleViewModel>
            (
                SharedData_Entities.MainPageArticles
                    .Where(a => a.IsActive)
                    .AsEnumerable()
                    .Select((a, index) =>
                    {
                        var vm = new MainPageArticleViewModel(a);
                        vm.Position = index + 1;
                        return vm;
                    })
            );
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Title", "PublishDate" };
        }

        public override void Sort()
        {
            if (SortField == "Title")
                List = new ObservableCollection<MainPageArticleViewModel>(List.OrderBy(a => a.Title));

            if (SortField == "PublishDate")
                List = new ObservableCollection<MainPageArticleViewModel>(List.OrderByDescending(a => a.PublishDate));
        }

        public override List<string> getComboboxFindList()
        {
            return new List<string> { "Title" };
        }

        public override void Find()
        {
            base.Find();

            if (FindField == "Title")
                List = new ObservableCollection<MainPageArticleViewModel>
                (
                    List.Where(a =>
                        a.Title != null &&
                        a.Title.StartsWith(FindTextBox, System.StringComparison.CurrentCultureIgnoreCase))
                );
        }
        #endregion
    }
}
