using static System.Environment;

namespace Northwind.DataContext.MySql;

public class NorthwindContextLogger
{
    public static void WriteLine(string message)
    {
        // string path = Path.Combine(GetFolderPath(
        //     SpecialFolder.DesktopDirectory),"northwind.log");

        string currentDirectory = Directory.GetCurrentDirectory();
        string path = Path.Combine(currentDirectory, "logs", $"northwind_{DateTime.Now:yyyyMMdd}.log");
        
        StreamWriter textFile = File.AppendText(path);
        // 向数据流中写入字符串，之后是行结束符（自动换行）
        textFile.WriteLine(message);
        // 关闭当前 StreamWriter 对象和底层流。
        textFile.Close();
    }
}