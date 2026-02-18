using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

[Index("CelebrationCake", Name = "UQ__Recomend__46385E408BDB0A0C", IsUnique = true)]
public partial class Recomendation
{
    [Key]
    [Column("RecomendationID")]
    public int RecomendationId { get; set; }

    public int CelebrationCake { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CelebrationCake")]
    [InverseProperty("Recomendation")]
    public virtual CelebrationCake? CelebrationCakeNavigation { get; set; }
}
