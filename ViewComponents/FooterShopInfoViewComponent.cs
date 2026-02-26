using Microsoft.AspNetCore.Mvc;
using MakeAWishDB.Context;
using System.Linq;

namespace PortalWWW.ViewComponents
{
    public class FooterShopInfoViewComponent : ViewComponent
    {
        private readonly SharedData_Entities _db;

        public FooterShopInfoViewComponent(SharedData_Entities db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var shop = _db.Shops
                .FirstOrDefault(s => s.ShopId == 1 && s.IsActive);

            return View(shop);
        }
    }
}

