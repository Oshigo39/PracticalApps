#region using

using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Caching.Memory;          // 用于使用内存中缓存的名称空间
using Microsoft.AspNetCore.Mvc.Formatters;
using Northwind.DataContext.MySql;
using Northwind.WebApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI; // 导入使用用户存储库的名称空间

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

// 注册CRUD存储库的"作用域生命周期服务"，实现接口-实现绑定，任何依赖该接口的组件都会获得对应实现的实例
// 同一个 HTTP 请求范围内共享同一个实例、不同请求会创建新实例、请求结束时自动释放
builder.Services.AddScoped<ICustomerRepositories,CustomerRepositories>();

// 添加swagger服务
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 自动生成机器可读的 OpenAPI 文档的服务
builder.Services.AddOpenApi();

// 添加日志服务，并配置日志记录
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096;
    options.ResponseBodyLogLimit = 4092;
});

var app = builder.Build();

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // 配置Swagger的Http请求管道，指定其url范围
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Service API Version 1");

        c.SupportedSubmitMethods(new[]
        {
            SubmitMethod.Get, SubmitMethod.Post,
            SubmitMethod.Put, SubmitMethod.Delete
        });
    });
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();