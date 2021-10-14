using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DAL.DO.Station myStation = new DAL.DO.Station();
            Console.WriteLine(myStation);
            DAL.DO.Drone myDrone = new DAL.DO.Drone();
            Console.WriteLine(myDrone);
            DAL.DO.Customer myCustomer = new DAL.DO.Customer();
            Console.WriteLine(myCustomer);
            DAL.DO.DroneCharge myDroneCharge = new DAL.DO.DroneCharge();
            Console.WriteLine(myDroneCharge);
            DAL.DO.Parcel myParcel = new DAL.DO.Parcel();
            Console.WriteLine(myParcel);
        }
    }
}
