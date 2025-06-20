using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels.Mysql;

[Keyless]
[Table("territories")]
public partial class Territory
{
    [Column("TerritoryID")]
    [StringLength(20)]
    public string? TerritoryId { get; set; }

    [StringLength(50)]
    public string? TerritoryDescription { get; set; }

    [Column("RegionID")]
    public int? RegionId { get; set; }
}
