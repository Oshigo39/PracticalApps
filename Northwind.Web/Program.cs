#region 导入必要的名称空间

using Northwind.EntityModels.Mysql;
using Northwind.DataContext.MySql;

#endregion

#region 配置Web服务器主机和服务

var builder = WebApplication.CreateBuilder(args);
// web主机builder添加RazorPage服务
builder.Services.AddRazorPages();
// 注册Northwind数据库上下文到依赖容器当中
NorthwindContextExtension
    .AddNorthwindContext(builder.Services,builder.Configuration);

var app = builder.Build();

#endregion

#region 配置 HTTP 管道和路由
// 如果当前环境不是开发环境，则强制使用https
if (!app.Environment.IsDevelopment())
{
    // 添加使用 HSTS 的中间件，该中间件会添加 Strict-Transport-Security 标头
    app.UseHsts();
}

// 作为中间件实现匿名内联委托
// 来拦截 HTTP 请求和响应
app.Use(async (context, next) =>
{
    if (context.GetEndpoint() is RouteEndpoint rep)
    {
        Console.WriteLine($"Endpoint name: {rep.DisplayName}");
        Console.WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
    }

    if (context.Request.Path == "/bonjour")
    {
        // 在 URL 路径匹配的情况下，这将成为终止符
        // 返回的委托不会调用下一个委托。
        await context.Response.WriteAsync("Bonjour Monde!");
        return;
    }

    // 我们可以在调用下一个委托之前修改请求。
    await next();

    // 我们可以在调用下一个委托后修改响应。
});

// 添加将 HTTP 请求重定向到 HTTPS 的中间件。
app.UseHttpsRedirection();

// 在当前 pat 上启用默认文件映射（UseDefaultFiles必须在UseStaticFiles之前）
app.UseDefaultFiles();
// 为当前请求路径启用静态文件服务
app.UseStaticFiles();

// 配置RazorPages路由，RazorPages终结点注册到应用程序的请求处理管道中，使RazorPages能够响应 HTTP 请求
app.MapRazorPages();
// 为所有的get请求都指向Hello World
app.MapGet("/hello", () => $"Environment is: {app.Environment.EnvironmentName}");

#endregion

// 启动网络服务器，托管网站并等待请求
// 这是一个线程阻塞调用
app.Run();
Console.WriteLine("由于线程阻塞，这是在Web服务器停止后执行的。");