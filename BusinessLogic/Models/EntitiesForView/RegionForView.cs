using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.EntitiesForView
{
    public class RegionForView
    {
        #region Data

        public int Position { get; set; }


        public int RegionId { get; set; }

    
        public string RegionName { get; set; } = null!;

    
        public string? CountryName { get; set; }

        public bool IsActive { get; set; }

        #endregion
    }
}



