
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeAWishDB.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PortalWWW.ViewComponents
    {
        public class BestSellersViewComponent : ViewComponent
        {
            private readonly SharedData_Entities _db;

            public BestSellersViewComponent(SharedData_Entities db)
            {
                _db = db;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var dateFrom = DateTime.Now.AddDays(-30);

                var cakes = await _db.OrderItems
                    .Where(oi =>
                        oi.IsActive &&
                        oi.Order != null &&
                        oi.Order.OrderDate >= dateFrom)
                    .Include(oi => oi.CelebrationCakeNavigation)
                    .GroupBy(oi => oi.CelebrationCakeNavigation)
                    .Select(g => new
                    {
                        Cake = g.Key,
                        TotalQuantity = g.Sum(x => x.Quantity ?? 1)
                    })
                    .Where(x => x.Cake != null && x.Cake.IsActive == true)
                    .OrderByDescending(x => x.TotalQuantity)
                    .ThenBy(x => x.Cake!.Name)
                    .Take(4)
                    .Select(x => x.Cake!)
                    .ToListAsync();

                return View(cakes);
            }
        }
    }
