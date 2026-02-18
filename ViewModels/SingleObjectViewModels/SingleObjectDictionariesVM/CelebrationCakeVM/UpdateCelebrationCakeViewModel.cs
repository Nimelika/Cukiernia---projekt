using CommunityToolkit.Mvvm.Input;
using DesktopApp.ViewModels.AbstractViewModels;
using MakeAWishDB.Entities;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace DesktopApp.ViewModels.SingleObjectViewModels.SingleObjectDictionariesVM.CelebrationCakeVM
{
    public class UpdateCelebrationCakeViewModel : UpdateViewModel<CelebrationCake>
    {
        public UpdateCelebrationCakeViewModel()
            : base("Update Celebration Cake")
        {
            UploadImageCommand = new RelayCommand(UploadImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);
        }

        public int CelebrationCakeId => item.CelebrationCakeId;

        public string Name { get => item.Name; set { item.Name = value; OnPropertyChanged(() => Name); } }
        public string Description { get => item.Description; set { item.Description = value; OnPropertyChanged(() => Description); } }
        public string ImageAlt { get => item.ImageAlt; set { item.ImageAlt = value; OnPropertyChanged(() => ImageAlt); } }

        public int CakeFilling
        {
            get => item.CakeFilling;
            set { item.CakeFilling = value; OnPropertyChanged(() => CakeFilling); }
        }

        public decimal? PriceSmall { get => item.PriceSmall; set { item.PriceSmall = value; OnPropertyChanged(() => PriceSmall); } }
        public decimal? PriceMedium { get => item.PriceMedium; set { item.PriceMedium = value; OnPropertyChanged(() => PriceMedium); } }
        public decimal? PriceLarge { get => item.PriceLarge; set { item.PriceLarge = value; OnPropertyChanged(() => PriceLarge); } }

        public bool? WithNuts { get => item.WithNuts; set { item.WithNuts = value; OnPropertyChanged(() => WithNuts); } }
        public bool? WithFruits { get => item.WithFruits; set { item.WithFruits = value; OnPropertyChanged(() => WithFruits); } }
        public bool? WithAlcohol { get => item.WithAlcohol; set { item.WithAlcohol = value; OnPropertyChanged(() => WithAlcohol); } }
        public bool? Vegan { get => item.Vegan; set { item.Vegan = value; OnPropertyChanged(() => Vegan); } }
        public bool? LowSugar { get => item.LowSugar; set { item.LowSugar = value; OnPropertyChanged(() => LowSugar); } }
        public bool? NoSugar { get => item.NoSugar; set { item.NoSugar = value; OnPropertyChanged(() => NoSugar); } }
        public bool? WeddingOffer { get => item.WeddingOffer; set { item.WeddingOffer = value; OnPropertyChanged(() => WeddingOffer); } }
        public bool? ChildrenOffer { get => item.ChildrenOffer; set { item.ChildrenOffer = value; OnPropertyChanged(() => ChildrenOffer); } }

        public ObservableCollection<CakeFilling> CakeFillingItems =>
            new(sharedData_Entities.CakeFillings.Where(f => f.IsActive).ToList());

        public ICommand UploadImageCommand { get; }
        public ICommand DeleteImageCommand { get; }

        private void UploadImage()
        {
            var dlg = new OpenFileDialog { Filter = "Images|*.jpg;*.png" };
            if (dlg.ShowDialog() == true)
                item.ImageUrl = dlg.FileName;
        }

        private void DeleteImage() => item.ImageUrl = null;

        public override void Load(int id)
        {
            item = sharedData_Entities.CelebrationCakes.First(c => c.CelebrationCakeId == id);
        }

        protected override string ValidateProperty(string propertyName) => null;
    }
}

