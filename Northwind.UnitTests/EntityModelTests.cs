using Northwind.DataContext.MySql;
using Northwind.EntityModels.Mysql;
using Xunit;
using Xunit.Abstractions;
using Assert = NUnit.Framework.Assert;

namespace Northwind.UnitTests;

public class EntityModelTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void DatabaseConnectTest()
    {
        using NorthwindContext db = new();
        testOutputHelper.WriteLine(db.Database.CanConnect().ToString());
    }

    [Fact]
    public void CategoryCountTest()
    {
        using NorthwindContext db = new();
        int expected = 8;
        int actual = db.Categories.Count();
        testOutputHelper.WriteLine((expected == actual).ToString());
    }
    
    [Fact]
    public void ProductId1IsChaiTest()
    {
        using NorthwindContext db = new();
        string expected = "Chai";
        Product? product = db.Products.Find(1);
        string actual = product?.ProductName ?? string.Empty;
        testOutputHelper.WriteLine((expected == actual).ToString());
    }
}