using System;

namespace FileWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystemWatcher = new FileWatcher("D:\\invoice");
            fileSystemWatcher.Start();
            Console.ReadLine();
        }
    }
}
