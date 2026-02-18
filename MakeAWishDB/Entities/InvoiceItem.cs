using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Entities;

public partial class InvoiceItem
{
    public int InvoiceItemId { get; set; }

    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? VatRate { get; set; }

    // COMPUTED COLUMNS — READ ONLY
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal LineNet { get; private set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal LineVat { get; private set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal LineGross { get; private set; }


    public bool IsActive { get; set; }



    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceItems")]
    public virtual Invoice? Invoice { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceItems")]
    public virtual Product? Product { get; set; }


}
