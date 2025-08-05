using System;
namespace Binary_ASM__CPP_CSConsoleApplicationExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("请键入：");
            do
            {
                Console.WriteLine("您键入的是：" + Console.ReadLine());
            }
            while (Console.ReadLine() == "a");           
        }
    }
}
