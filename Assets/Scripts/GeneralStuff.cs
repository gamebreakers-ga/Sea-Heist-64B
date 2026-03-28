using System;
using System.Threading.Tasks;
using System.Diagnostics;

public class GF
{
    public static async Task Until(Func<bool> condition)
    {
        while (true)
        {
            if (condition()) return;
            await Task.Delay(100);
        }
    }

    public static async Task<bool> Until(Func<bool> condition, long timeout)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        while (true)
        {
            if (condition()) return true;
            if (stopwatch.ElapsedMilliseconds > timeout) return false;
            await Task.Delay(100);
        }

    }
}