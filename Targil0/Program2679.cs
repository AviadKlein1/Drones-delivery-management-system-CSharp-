using System;


namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome3080();
            welcome2679();
            Console.ReadKey();
        }

        static partial void welcome3080();
        private static void welcome2679()
        {
            Console.Write("Enter your name: ");
            string user = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", user);
        }
    }
}
