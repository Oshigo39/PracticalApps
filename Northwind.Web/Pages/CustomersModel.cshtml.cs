using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.DataContext.MySql;
using Northwind.EntityModels.Mysql;

namespace Northwind.Web.Pages;

public class CustomersModel : PageModel
{
    private NorthwindContext _db;

    public CustomersModel(NorthwindContext db)
    {
        _db = db;
    }

    // ILookup: 键映射到值集合的方式
    public ILookup<string?, Customer>? CustomersByCountry { get; set; }
    
    public void OnGet()
    {
        CustomersByCountry = _db.Customers.ToLookup(c => c.Country);
    }
}