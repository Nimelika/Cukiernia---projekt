using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeAWishDB.Context;
using System.Linq;
using System.Threading.Tasks;

namespace PortalWWW.ViewComponents
{
    public class SpecialCakesCarouselViewComponent : ViewComponent
    {
        private readonly SharedData_Entities _db;

        public SpecialCakesCarouselViewComponent(SharedData_Entities db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cakes = await _db.Recomendations
                .Where(r => r.IsActive)
                .Include(r => r.CelebrationCakeNavigation)
                .Where(r => r.CelebrationCakeNavigation != null &&
                            r.CelebrationCakeNavigation.IsActive == true)
                .Select(r => r.CelebrationCakeNavigation!)
                .ToListAsync();

            return View(cakes);
        }
    }
}
