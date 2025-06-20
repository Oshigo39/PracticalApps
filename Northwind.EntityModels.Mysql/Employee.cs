using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
[Table("employees")]
public partial class Employee
{
    [Column("EmployeeID")]
    public int? EmployeeId { get; set; }

    [StringLength(20)]
    public string? LastName { get; set; }

    [StringLength(10)]
    public string? FirstName { get; set; }

    [StringLength(30)]
    public string? Title { get; set; }

    [StringLength(25)]
    public string? TitleOfCourtesy { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? BirthDate { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? HireDate { get; set; }

    [StringLength(60)]
    public string? Address { get; set; }

    [StringLength(15)]
    public string? City { get; set; }

    [StringLength(15)]
    public string? Region { get; set; }

    [StringLength(10)]
    public string? PostalCode { get; set; }

    [StringLength(15)]
    public string? Country { get; set; }

    [StringLength(24)]
    public string? HomePhone { get; set; }

    [StringLength(4)]
    public string? Extension { get; set; }

    public byte[]? Photo { get; set; }

    public string? Notes { get; set; }

    public int? ReportsTo { get; set; }

    [StringLength(255)]
    public string? PhotoPath { get; set; }
}
