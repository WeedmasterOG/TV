using System;
using System.Diagnostics;
using System.Threading;

namespace TVPers
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                // Check if process is running, if not, start it
                if (Process.GetProcessesByName("TV").Length == 0)
                {
                    Process.Start("TV.exe");
                }

                Thread.Sleep(100);
            }
        }
    }
}
