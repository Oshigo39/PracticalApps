using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels.Mysql;

[Keyless]
[Table("region")]
public partial class Region
{
    [Column("RegionID")]
    public int? RegionId { get; set; }

    [StringLength(50)]
    public string? RegionDescription { get; set; }
}
