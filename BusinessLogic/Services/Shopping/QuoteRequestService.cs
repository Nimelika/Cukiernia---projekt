using MakeAWishDB.Context;
using MakeAWishDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Shopping
{
    
        public class QuoteRequestService
        {
            private readonly SharedData_Entities _db;

            public QuoteRequestService(SharedData_Entities db)
            {
                _db = db;
            }

            public async Task CreateAsync(QuoteRequest quote)
            {
                _db.QuoteRequests.Add(quote);
                await _db.SaveChangesAsync();
            }
        }
    }

