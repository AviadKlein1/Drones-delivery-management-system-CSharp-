using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DAL.DO.Station myStation = new DAL.DO.Station();
            Console.WriteLine(myStation.toString());
        }
    }
}
