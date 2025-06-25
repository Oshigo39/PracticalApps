#region using

using Microsoft.Extensions.Caching.Memory;          // 用于使用内存中缓存的名称空间
using Microsoft.AspNetCore.Mvc.Formatters;
using Northwind.DataContext.MySql;
using Northwind.EntityModels.Mysql;

#endregion

#region 配置Web服务器主机和服务

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1. 注册Northwind数据库上下文到依赖容器当中
NorthwindContextExtension
    .AddNorthwindContext(builder.Services, builder.Configuration);
// 2. 注册controller服务，开启路由、模型绑定、验证等功能
builder.Services.AddControllers(option =>
    {
        // 并将Controllers的默认输出格式化器的名称和支持的媒体类型写入控制台
        Console.WriteLine("Default output formatters:");
        foreach (var formatter in option.OutputFormatters)
        {
            OutputFormatter? mediaFormatter = formatter as OutputFormatter;
            if (mediaFormatter is null)
            {
                Console.WriteLine(
                    $"{formatter.GetType().Name}"
                );
            }
            else
            {
                Console.WriteLine(
                    $"{0},Media types: {1},",
                    arg0: mediaFormatter.GetType().Name,
                    arg1: string.Join(",", mediaFormatter.SupportedMediaTypes)
                );
            }
        }
    })      // 添加XML序列化格式化程序
    .AddXmlDataContractSerializerFormatters()
    .AddXmlSerializerFormatters();
// 3. 内存缓存服务
builder.Services.AddSingleton<IMemoryCache>(
    new MemoryCache(new MemoryCacheOptions()));
// 自动生成机器可读的 OpenAPI 文档的服务
builder.Services.AddOpenApi();

var app = builder.Build();

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();