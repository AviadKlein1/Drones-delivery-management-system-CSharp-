using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("To add station enter 1 \nTo add drone enter 2\n To add customer enter 3\nTo add paracel enter 4");

            IDAL.DO.DalObject.DalObject station1 = new IDAL.DO.DalObject.DalObject();
            station1.addStation(0);
            

            


        }
    }
}
