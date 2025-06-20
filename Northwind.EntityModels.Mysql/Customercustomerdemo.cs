using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels.Mysql;

[Keyless]
[Table("customercustomerdemo")]
public partial class Customercustomerdemo
{
    [Column("CustomerID")]
    [StringLength(5)]
    public string? CustomerId { get; set; }

    [Column("CustomerTypeID")]
    [StringLength(10)]
    public string? CustomerTypeId { get; set; }
}
