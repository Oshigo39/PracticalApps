using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Northwind.DataContext.MySql;

/// <summary>
/// 配置文件管理类，但是不知为何读取的Northwind连接字符串总是为null
/// </summary>
public class ConfigManager
{
    public static IConfiguration Configuration { get; set; }

    static ConfigManager()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
    }
}