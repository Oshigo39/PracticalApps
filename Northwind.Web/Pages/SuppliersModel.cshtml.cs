using Microsoft.AspNetCore.Mvc.RazorPages;
// 导入数据库上下文名称空间
using Northwind.DataContext.MySql;
// 导入Entity实体类名称空间
using Northwind.EntityModels.Mysql;

namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel
{
    // 数据库上下文私有字段
    private NorthwindContext _db;

    // 构造函数
    public SuppliersModel(NorthwindContext db)
    {
        _db = db;
    }
    
    // 可枚举的Suppliers集合的属性
    // 声明为Supplier对象序列，而不是string字符串
    public IEnumerable<Supplier>? Suppliers { get; set; }
    
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";
        // Linq从数据库中获取数据再过滤排序
        Suppliers = _db.Suppliers
            .OrderBy(c => c.Country)
            .ThenBy(c => c.CompanyName);
    }
}