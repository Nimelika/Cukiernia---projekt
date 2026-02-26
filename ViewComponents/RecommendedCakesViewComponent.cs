using System.Linq;
using System.Threading.Tasks;
using MakeAWishDB.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PortalWWW.ViewComponents
{
    public class RecommendedCakesViewComponent : ViewComponent
    {
        private readonly SharedData_Entities _db;

        public RecommendedCakesViewComponent(SharedData_Entities db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take = 4)
        {
            var cakes = await _db.Recomendations
                .Where(r => r.IsActive)
                .Include(r => r.CelebrationCakeNavigation)
                .Where(r => r.CelebrationCakeNavigation != null && r.CelebrationCakeNavigation.IsActive == true)
                .Select(r => r.CelebrationCakeNavigation!)
                .Take(take)
                .ToListAsync();

            return View(cakes);
        }
    }
}

