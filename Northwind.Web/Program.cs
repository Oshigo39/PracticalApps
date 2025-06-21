var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region 配置 HTTP 管道和路由
// 如果当前环境不是开发环境，则强制使用https
if (!app.Environment.IsDevelopment())
{
    // 添加使用 HSTS 的中间件，该中间件会添加 Strict-Transport-Security 标头
    app.UseHsts();
}
// 添加将 HTTP 请求重定向到 HTTPS 的中间件。
app.UseHttpsRedirection();

// 为所有的get请求都指向Hello World
app.MapGet("/", () => $"Environment is: {app.Environment.EnvironmentName}");

#endregion

// 启动网络服务器，托管网站并等待请求
// 这是一个线程阻塞调用
app.Run();
Console.WriteLine("这是在Web服务器停止后执行的。");