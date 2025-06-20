using static System.Environment;

namespace Northwind.DataContext.MySql;

/// <summary>
/// 日志写入类
/// </summary>
public class NorthwindContextLogger
{
    private static readonly object Lock = new object();  // 锁对象
    
    public static void WriteLineToLog(string message)
    {
        // 线程锁避免线程冲突
        lock (Lock)
        {
            // string path = Path.Combine(GetFolderPath(
            //     SpecialFolder.DesktopDirectory),"northwind.log");
            // 获取当前目录
            string currentDirectory = Directory.GetCurrentDirectory();
            string logDir = Path.Combine(currentDirectory, "logs");
            // 自动创建日志目录
            if (!Directory.Exists(logDir)) 
            {
                Directory.CreateDirectory(logDir);
            }
            // 拼接文件的路径
            string path = Path.Combine(logDir, $"northwind_{DateTime.Now:yyyyMMdd}.log");
            // try-catch语句块封装信息写入语句
            try
            {
                using (StreamWriter textFile = File.AppendText(path))
                {
                    textFile.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [INFO] {message}");
                }
            }
            catch (Exception ex)
            {
                // 备用方案：输出到控制台或系统事件日志
                Console.WriteLine($"日志写入失败: {ex.Message}");
            }
        }
    }
}