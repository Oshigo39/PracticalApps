var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 为所有的get请求都指向Hello World
app.MapGet("/", () => "Hello World!");

// 启动网络服务器，托管网站并等待请求
app.Run();          // 这是一个线程阻塞调用
Console.WriteLine("这是在Web服务器停止后执行的。");