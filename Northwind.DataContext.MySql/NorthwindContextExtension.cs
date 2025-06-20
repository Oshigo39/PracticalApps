using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace Northwind.DataContext.MySql;

/// <summary>
/// 封装 NorthwindContext 的依赖注入配置，集成 MySQL 驱动、日志和安全检查
/// </summary>
public class NorthwindContextExtension
{
    public static IServiceCollection AddNorthwindContext(
        IServiceCollection services, 
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Northwind")!;
        
        // 安全记录日志（隐藏密码）
        var csb = new MySqlConnectionStringBuilder(connectionString);
        NorthwindContextLogger.WriteLineToLog(
            $"MySQL连接配置: 主机={csb.Server}, 数据库={csb.Database}, 端口={csb.Port}"
        );
        
        services.AddDbContext<NorthwindContext>(options =>
            {
                // 使用官方 MySQL 提供程序
                options.UseMySQL(connectionString); // 注意方法名是 UseMySQL（大写 SQL）
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);
        
        return services;
    }
}