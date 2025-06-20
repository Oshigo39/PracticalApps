using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
[Table("customerdemographics")]
public partial class Customerdemographic
{
    [Column("CustomerTypeID")]
    [StringLength(10)]
    public string? CustomerTypeId { get; set; }

    public string? CustomerDesc { get; set; }
}
