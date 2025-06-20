using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
[Table("employeeterritories")]
public partial class Employeeterritory
{
    [Column("EmployeeID")]
    public int? EmployeeId { get; set; }

    [Column("TerritoryID")]
    [StringLength(20)]
    public string? TerritoryId { get; set; }
}
