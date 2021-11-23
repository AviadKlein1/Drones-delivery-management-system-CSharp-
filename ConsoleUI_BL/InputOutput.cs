using System;
using System.Collections.Generic;

namespace ConsoleUI_BL
{
    /// <summary>
    /// recieves details of items from user, and returns item
    /// </summary>
    public class InputOutput
    {
        public Random rd = new Random();

        //station details
        public IBL.BO.Station addStation()
        {
            IBL.BO.Station myStation = new IBL.BO.Station();
            
            //id
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myStation.id = id;

            //name
            Console.WriteLine("enter name");
            myStation.name = (Console.ReadLine());

            //location - longitude and lattitude
            double locat;
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out locat);
            myStation.location = new IBL.BO.Location(0, locat);
            Console.WriteLine("enter lattitude");
            double.TryParse(Console.ReadLine(), out locat);
            myStation.location.lattitude = locat;

            //number of charge slots
            int numOfChargeSlots;
            Console.WriteLine("enter number of charge slots");
            int.TryParse(Console.ReadLine(), out numOfChargeSlots);
            myStation.numOfAvailableChargeSlots = numOfChargeSlots;
            myStation.numOfChargeSlots = numOfChargeSlots;
            myStation.dronesInCharge = new List<IBL.BO.DroneInCharge>();

            return myStation;
        }

        //drone details
        public IBL.BO.Drone addDrone()
        {
            IBL.BO.Drone myDrone = new IBL.BO.Drone();

            //id
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myDrone.id = id;

            //model
            Console.WriteLine("enter model");
            myDrone.model = (Console.ReadLine());

            //weight categoty
            int choice = 0;
            Console.WriteLine("enter max weight (light = 1, medium = 2, heavy = 3)");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myDrone.weight = IDAL.DO.MyEnums.WeightCategory.light;
            if (choice == 2) myDrone.weight = IDAL.DO.MyEnums.WeightCategory.medium;
            if (choice == 3) myDrone.weight = IDAL.DO.MyEnums.WeightCategory.heavy;

            //id of station for first charge
            Console.WriteLine("enter id of station for first charge");
            int.TryParse(Console.ReadLine(), out choice);
            myDrone.firstChargeStationId = choice;

            return myDrone;
        }

        //customer details
        public IBL.BO.Customer addCustomer()
        {
            IBL.BO.Customer myCustomer = new IBL.BO.Customer();

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
        public IBL.BO.Parcel addParcel()
        {
            IBL.BO.Parcel myParcel = new IBL.BO.Parcel();

            //sender id
            int senderId;
            Console.WriteLine("enter sender id");
            int.TryParse(Console.ReadLine(), out senderId);
            myParcel.sender.id = senderId;

            //reciever id
            int targetId;
            Console.WriteLine("enter reciever id");
            int.TryParse(Console.ReadLine(), out targetId);
            myParcel.reciever.id = targetId;

            //parcel weight
            int choice;
            Console.WriteLine("enter weight (light = 1, medium = 2, heavy = 3)");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myParcel.weight = IDAL.DO.MyEnums.WeightCategory.light;
            if (choice == 2) myParcel.weight = IDAL.DO.MyEnums.WeightCategory.medium;
            if (choice == 3) myParcel.weight = IDAL.DO.MyEnums.WeightCategory.heavy;

            //priority
            Console.WriteLine("enter priority (regular = 1, quickly = 2, ergent = 3)");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myParcel.priority = IDAL.DO.MyEnums.PriorityLevel.regular;
            if (choice == 2) myParcel.priority = IDAL.DO.MyEnums.PriorityLevel.quickly;
            if (choice == 3) myParcel.priority = IDAL.DO.MyEnums.PriorityLevel.ergent;
            
            return myParcel;
        }
    }
}