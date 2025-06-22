using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel
{
    // 可枚举的Suppliers集合的属性
    public IEnumerable<string>? Suppliers { get; set; }
    
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";
        // 硬编码集合（一个get的http请求生命周期内生效）
        Suppliers =
        [
            "Alpha Co", "Beta Limited", "Gamma Corp"
        ];
    }
}