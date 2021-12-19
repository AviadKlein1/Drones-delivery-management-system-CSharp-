using System;
using System.Collections.Generic;

namespace ConsoleUI_BL
{
    /// <summary>
    /// recieves details of items from user, and returns item
    /// </summary>
    public class Input
    {
        public Random rd = new();

        //station details
        public static BlApi.BO.Station AddStation()
        {
            BlApi.BO.Station myStation = new();

            //id
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out int id);
            myStation.Id = id;

            //name
            Console.WriteLine("enter name");
            myStation.Name = Console.ReadLine();

            //location - longitude and latitude
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out double locat1);
            Console.WriteLine("enter latitude");
            double.TryParse(Console.ReadLine(), out double locat2);
            myStation.Location = new BlApi.BO.Location(locat1, locat2);

            //number of charge slots
            Console.WriteLine("enter number of charge slots");
            int.TryParse(Console.ReadLine(), out int numOfChargeSlots);
            myStation.NumOfAvailableChargeSlots = numOfChargeSlots;
            myStation.NumOfChargeSlots = numOfChargeSlots;
            myStation.DronesInCharge = new List<BlApi.BO.DroneInCharge>();

            return myStation;
        }

        //drone details
        public static BlApi.BO.Drone AddDrone()
        {
            BlApi.BO.Drone myDrone = new();

            //id
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out int id);
            myDrone.Id = id;

            //model
            Console.WriteLine("enter model");
            myDrone.Model = (Console.ReadLine());

            //weight categoty
            Console.WriteLine("enter max weight (light = 1, medium = 2, heavy = 3)");
            int.TryParse(Console.ReadLine(), out int choice);
            if (choice == 1) myDrone.Weight = DalApi.DO.MyEnums.WeightCategory.light;
            if (choice == 2) myDrone.Weight = DalApi.DO.MyEnums.WeightCategory.medium;
            if (choice == 3) myDrone.Weight = DalApi.DO.MyEnums.WeightCategory.heavy;

            //id of station for first charge
            Console.WriteLine("enter id of station for first charge");
            int.TryParse(Console.ReadLine(), out choice);
            myDrone.FirstChargeStationId = choice;

            return myDrone;
        }

        //customer details
        public static BlApi.BO.Customer AddCustomer()
        {
            BlApi.BO.Customer myCustomer = new();

            //id
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out int id);
            myCustomer.Id = id;

            //name
            Console.WriteLine("enter name");
            myCustomer.Name = Console.ReadLine(
                );

            //phone
            Console.WriteLine("enter phone number");
            myCustomer.PhoneNumber = Console.ReadLine();

            //location
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out double locat);
            myCustomer.Location.Longitude = locat;
            Console.WriteLine("enter latitude");
            double.TryParse(Console.ReadLine(), out locat);
            myCustomer.Location.Latitude = locat;

            return myCustomer;
        }

        //parcel details
        public BlApi.BO.Parcel AddParcel()
        {
            BlApi.BO.Parcel myParcel = new();

            //sender id
            Console.WriteLine("enter sender id");
            int.TryParse(Console.ReadLine(), out int senderId);
            myParcel.Sender.Id = senderId;

            //reciever id
            Console.WriteLine("enter reciever id");
            int.TryParse(Console.ReadLine(), out int targetId);
            myParcel.Reciever.Id = targetId;

            //parcel weight
            Console.WriteLine("enter weight (light = 1, medium = 2, heavy = 3)");
            int.TryParse(Console.ReadLine(), out int choice);
            if (choice == 1) myParcel.Weight = DalApi.DO.MyEnums.WeightCategory.light;
            if (choice == 2) myParcel.Weight = DalApi.DO.MyEnums.WeightCategory.medium;
            if (choice == 3) myParcel.Weight = DalApi.DO.MyEnums.WeightCategory.heavy;

            //priority
            Console.WriteLine("enter priority (regular = 1, quickly = 2, urgent = 3)");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myParcel.Priority = DalApi.DO.MyEnums.PriorityLevel.regular;
            if (choice == 2) myParcel.Priority = DalApi.DO.MyEnums.PriorityLevel.quickly;
            if (choice == 3) myParcel.Priority = DalApi.DO.MyEnums.PriorityLevel.urgent;
            
            return myParcel;
        }
    }
}