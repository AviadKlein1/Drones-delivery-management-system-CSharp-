using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    public class InputOutput
    {
        public IBL.BO.Station addStation()
        {
            IBL.BO.Station myStation = new IBL.BO.Station();
            //recieve details from user
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myStation.id = id;

            Console.WriteLine("enter name");
            myStation.name = (Console.ReadLine());

            double locat;
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out locat);
            myStation.location.longitude = locat;
            Console.WriteLine("enter lattitude");
            double.TryParse(Console.ReadLine(), out locat);
            myStation.location.lattitude = locat;

            int numOfChargeSlots;
            Console.WriteLine("enter number of charge slots");
            int.TryParse(Console.ReadLine(), out numOfChargeSlots);
            myStation.numOfChargeSlots = numOfChargeSlots;
            myStation.numOfAvailableChargeSlots = numOfChargeSlots;
            return myStation;
        }
        public IBL.BO.Drone addDrone()
        {
            IBL.BO.Drone myDrone = new IBL.BO.Drone();
            //recieve details from user
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myDrone.id = id;

            Console.WriteLine("enter model");
            myDrone.model = (Console.ReadLine());

            int choice = 0;

            Console.WriteLine("enter max weight (lite = 1, medium = 2, heavy = 3)");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myDrone.weight = IDAL.DO.MyEnums.WeightCategory.lite;
            if (choice == 2) myDrone.weight = IDAL.DO.MyEnums.WeightCategory.medium;
            if (choice == 3) myDrone.weight = IDAL.DO.MyEnums.WeightCategory.heavy;

            Console.WriteLine("enter number of station for first charge");
            int.TryParse(Console.ReadLine(), out choice);
            myDrone.chargeStationId = choice;

            return myDrone;
        }
        public IBL.BO.Customer addCustomer()
        {
            IBL.BO.Customer myCustomer = new IBL.BO.Customer();
            //recieve details from user
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myCustomer.id = id;

            Console.WriteLine("enter name");
            myCustomer.name = (Console.ReadLine());

            Console.WriteLine("enter phone number");
            myCustomer.phoneNumber = (Console.ReadLine());

            double locat;
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out locat);
            myCustomer.location.longitude = locat;
            Console.WriteLine("enter lattitude");
            double.TryParse(Console.ReadLine(), out locat);
            myCustomer.location.lattitude = locat;

            return myCustomer;
        }
        public IBL.BO.Parcel addParcel()
        {
            IBL.BO.Parcel myParcel = new IBL.BO.Parcel();

            int senderId;
            Console.WriteLine("enter sender id");
            int.TryParse(Console.ReadLine(), out senderId);
            myParcel.sender.id = senderId;

            int targetId;
            Console.WriteLine("enter target id");
            int.TryParse(Console.ReadLine(), out targetId);
            myParcel.reciver.id = targetId;

            //int droneId;
            //Console.WriteLine("enter drone id");
            //int.TryParse(Console.ReadLine(), out droneId);
            //myParcel.drone.id = droneId;

            int choice;
            Console.WriteLine("enter weight (lite = 1, medium = 2, heavy = 3)");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myParcel.weight = IDAL.DO.MyEnums.WeightCategory.lite;
            if (choice == 2) myParcel.weight = IDAL.DO.MyEnums.WeightCategory.medium;
            if (choice == 3) myParcel.weight = IDAL.DO.MyEnums.WeightCategory.heavy;

            Console.WriteLine("enter priority (regular = 1, quickly = 2, ergent = 3 )");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1) myParcel.priority = IDAL.DO.MyEnums.PriorityLevel.regular;
            if (choice == 2) myParcel.priority = IDAL.DO.MyEnums.PriorityLevel.quickly;
            if (choice == 3) myParcel.priority = IDAL.DO.MyEnums.PriorityLevel.ergent;


           

            return myParcel;
        }

        
    }
}
