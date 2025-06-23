using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.DataContext.MySql;
using Northwind.EntityModels.Mysql;
using System.Linq;

namespace Northwind.Web.Pages
{
    public class CustomerOrdersModel : PageModel
    {
        private NorthwindContext _db;

        public CustomerOrdersModel(NorthwindContext context)
        {
            _db = context;
        }

        public Customer? Customer { get; set; }
        public IQueryable<Order>? Orders { get; set; }

        public IActionResult OnGet(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest("Customer ID is required.");
            }
            
            Customer = _db.Customers.Find(customerId);

            if (Customer == null)
            {
                return NotFound();
            }

            Orders = _db.Orders.Where(o => o.CustomerId == customerId);

            return Page();
        }
    }
}