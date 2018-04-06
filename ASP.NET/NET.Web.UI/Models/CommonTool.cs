using System.Diagnostics;
using System.Web.Script.Serialization;

public class CommonTool
{
    public delegate string Delgate();
    public delegate string DelgatePare(string name);
    public static string stopwatch(Delgate delgate)
    {
        System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        stopwatch.Stop(); //  停止监视
        delgate();
        return stopwatch.Elapsed.ToString();
    }

    public static string stopwatch(DelgatePare delgate, string name)
    {
        System.Diagnostics.Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        stopwatch.Stop(); //  停止监视
        delgate(name);
        return stopwatch.Elapsed.ToString();
    }
   
}