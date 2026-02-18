using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Shopping
{
    public class CatalogOrderService
    {
        private readonly SharedData_Entities _db;

        public CatalogOrderService(SharedData_Entities db)
        {
            _db = db;
        }
        // Metoda do tworzenia zamowienia z katalogu
        public async Task CreateOrderAsync(
            string guestName,
            string guestEmail,
            string guestPhone,
            int shopId,
            DateTime collectionDate,
            int celebrationCakeId,
            int celebrationCakeSizeId,
            int quantity,
            string personalizedText,
            string notes)
        {
            // Tworzenie nowego zamowienia
            var order = new Order
            {
                OrderDate = DateTime.Now,
                GuestName = guestName,
                GuestEmail = guestEmail,
                GuestPhone = guestPhone,
                Shop = shopId,
                CollectionDate = collectionDate,
                Status = 1,
                IsPaid = false,
                IsActive = true
            };
            // Pobranie ceny tortu z bazy
            var cake = await _db.CelebrationCakes
                .FirstAsync(c => c.CelebrationCakeId == celebrationCakeId);

            decimal unitPrice = celebrationCakeSizeId switch
            {
                1 => cake.PriceSmall ?? 0,
                2 => cake.PriceMedium ?? 0,
                3 => cake.PriceLarge ?? 0,
                _ => 0
            };
            // Tworzenie OrderItem
            var orderItem = new OrderItem
            {
                CelebrationCake = celebrationCakeId,
                CelebrationCakeSize = celebrationCakeSizeId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                PersonalizedText = personalizedText,
                Notes = notes,
                IsActive = true
            };
            // Powiazanie OrderItem z Order
            order.OrderItems.Add(orderItem);
            //Zapis do bazy
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }
    }
}
