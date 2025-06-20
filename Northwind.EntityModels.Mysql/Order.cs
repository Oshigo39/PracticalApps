using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels.Mysql;

[Keyless]
[Table("orders")]
public partial class Order
{
    [Column("OrderID")]
    public int? OrderId { get; set; }

    [Column("CustomerID")]
    [StringLength(5)]
    public string? CustomerId { get; set; }

    [Column("EmployeeID")]
    public int? EmployeeId { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? OrderDate { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? RequiredDate { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? ShippedDate { get; set; }

    public int? ShipVia { get; set; }

    [Precision(19)]
    public decimal? Freight { get; set; }

    [StringLength(40)]
    public string? ShipName { get; set; }

    [StringLength(60)]
    public string? ShipAddress { get; set; }

    [StringLength(15)]
    public string? ShipCity { get; set; }

    [StringLength(15)]
    public string? ShipRegion { get; set; }

    [StringLength(10)]
    public string? ShipPostalCode { get; set; }

    [StringLength(15)]
    public string? ShipCountry { get; set; }
}
