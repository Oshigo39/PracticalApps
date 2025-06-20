using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
[Table("shippers")]
public partial class Shipper
{
    [Column("ShipperID")]
    public int? ShipperId { get; set; }

    [StringLength(40)]
    public string? CompanyName { get; set; }

    [StringLength(24)]
    public string? Phone { get; set; }
}
