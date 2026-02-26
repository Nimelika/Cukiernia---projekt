using MakeAWishDB.Entities;
using System.Collections.Generic;

namespace PortalWWW.Models
{
    public class OrderEntryViewModel
    {
        // =========================
        // HERO IMAGE
        // =========================

        public string? Header { get; set; }
        public string? HeroImagePath { get; set; }
        public string? HeroImageAlt { get; set; }

        // =========================
        // ORDER STEPS
        // =========================

        public List<OrderStep> Steps { get; set; } = new();
    }
}

