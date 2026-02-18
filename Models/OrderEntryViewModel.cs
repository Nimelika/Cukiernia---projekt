using MakeAWishDB.Entities;
using System.Collections.Generic;

namespace PortalWWW.Models
{
    public class OrderEntryViewModel
    {
        public string Header { get; set; }
        public List<OrderStep> Steps { get; set; }
    }
}
