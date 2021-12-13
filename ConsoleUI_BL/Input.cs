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
        public BlApi.BO.Station AddStation()
        {
            BlApi.BO.Station myStation = new();

            //id
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myStation.Id = id;

            //name
            Console.WriteLine("enter name");
            myStation.Name = (Console.ReadLine());

            //location - longitude and lattitude
            double locat1;
            double locat2;
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out locat1);
            Console.WriteLine("enter lattitude");
            double.TryParse(Console.ReadLine(), out locat2);
            myStation.Location = new BlApi.BO.Location(locat1, locat2);

            //number of charge slots
            int numOfChargeSlots;
            Console.WriteLine("enter number of charge slots");
            int.TryParse(Console.ReadLine(), out numOfChargeSlots);
            myStation.NumOfAvailableChargeSlots = numOfChargeSlots;
            myStation.NumOfChargeSlots = numOfChargeSlots;
            myStation.DronesInCharge = new List<BlApi.BO.DroneInCharge>();

            return myStation;
        }

        //drone details
        public BlApi.BO.Drone AddDrone()
        {
            BlApi.BO.Drone myDrone = new();

            //id
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myDrone.Id = id;

            //model
            Console.WriteLine("enter model");
            myDrone.Model = (Console.ReadLine());

            //weight categoty
            int choice = 0;
            Console.WriteLine("enter max weight (light = 1, medium = 2, heavy = 3)");
            int.TryParse(Console.ReadLine(), out choice);
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
        public BlApi.BO.Customer AddCustomer()
        {
            BlApi.BO.Customer myCustomer = new();

            //id
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myCustomer.id = id;

            //name
            Console.WriteLine("enter name");
            myCustomer.name = (Console.ReadLine());

            //phone
            Console.WriteLine("enter phone number");
            myCustomer.phoneNumber = (Console.ReadLine());

            //location
            double locat;
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out locat);
            myCustomer.location.longitude = locat;
            Console.WriteLine("enter lattitude");
            double.TryParse(Console.ReadLine(), out locat);
            myCustomer.location.lattitude = locat;

            return myCustomer;
        }

        //parcel details
        public BlApi.BO.Parcel AddParcel()
        {
            BlApi.BO.Parcel myParcel = new();

            //sender id
            int senderId;
            Console.WriteLine("enter sender id");
            int.TryParse(Console.ReadLine(), out senderId);
            myParcel.Sender.id = senderId;

            //reciever id
            int targetId;
            Console.WriteLine("enter reciever id");
            int.TryParse(Console.ReadLine(), out targetId);
            myParcel.Reciever.id = targetId;

            //parcel weight
            int choice;
            Console.WriteLine("enter weight (light = 1, medium = 2, heavy = 3)");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myParcel.Weight = DalApi.DO.MyEnums.WeightCategory.light;
            if (choice == 2) myParcel.Weight = DalApi.DO.MyEnums.WeightCategory.medium;
            if (choice == 3) myParcel.Weight = DalApi.DO.MyEnums.WeightCategory.heavy;

            //priority
            Console.WriteLine("enter priority (regular = 1, quickly = 2, ergent = 3)");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myParcel.Priority = DalApi.DO.MyEnums.PriorityLevel.regular;
            if (choice == 2) myParcel.Priority = DalApi.DO.MyEnums.PriorityLevel.quickly;
            if (choice == 3) myParcel.Priority = DalApi.DO.MyEnums.PriorityLevel.ergent;
            
            return myParcel;
        }
    }
}