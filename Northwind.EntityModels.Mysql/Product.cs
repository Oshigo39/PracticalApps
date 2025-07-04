﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels.Mysql;

// byd这个特性导致测试一直不通过（提示entity无主键）
// [Keyless]
[Table("products")]
public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int? ProductId { get; set; }

    [StringLength(40)]
    public string? ProductName { get; set; }

    [Column("SupplierID")]
    public int? SupplierId { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    [StringLength(20)]
    public string? QuantityPerUnit { get; set; }

    [Precision(19)]
    public decimal? Price { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool? Discontinued { get; set; }
}
