using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels.Mysql;

[Keyless]
[Table("orderdetails")]
public partial class Orderdetail
{
    [Column("OrderID")]
    public int? OrderId { get; set; }

    [Column("ProductID")]
    public int? ProductId { get; set; }

    [Precision(19)]
    public decimal? UnitPrice { get; set; }

    public short? Quantity { get; set; }

    public float? Discount { get; set; }
}
