using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    public class InputOutput
    {
        public IDAL.DO.Station Station()
        {
            IDAL.DO.Station myStation = new IDAL.DO.Station();
            //recieve details from user
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myStation.id = id;

            Console.WriteLine("enter name");
            myStation.name = (Console.ReadLine());

            double longitude;
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out longitude);
            myStation.longitude = longitude;

            double lattitude;
            Console.WriteLine("enter lattitude");
            double.TryParse(Console.ReadLine(), out lattitude);
            myStation.lattitude = lattitude;

            int numOfChargeSlots;
            Console.WriteLine("enter number of charge slots");
            int.TryParse(Console.ReadLine(), out numOfChargeSlots);
            myStation.numOfChargeSlots = numOfChargeSlots;
            myStation.numOfAvailableChargeSlots = numOfChargeSlots;
            return myStation;
        }
        public IDAL.DO.Drone Drone()
        {
            IDAL.DO.Drone myDrone = new IDAL.DO.Drone();
            //recieve details from user
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myDrone.id = id;

            Console.WriteLine("enter model");
            myDrone.model = (Console.ReadLine());

            int choice = 0;
            //Console.WriteLine("enter status (available = 1, maintenance = 2, delivery = 3)");
            //int.TryParse(Console.ReadLine(), out choice);
            //if (choice == 1) myDrone.status = MyEnums.DroneStatus.available;
            //if (choice == 2) myDrone.status = MyEnums.DroneStatus.maintenance;
            //if (choice == 3) myDrone.status = MyEnums.DroneStatus.delivery;

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
        public IDAL.DO.Customer Customer()
        {
            IDAL.DO.Customer myCustomer = new IDAL.DO.Customer();
            //recieve details from user
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            myCustomer.id = id;

            Console.WriteLine("enter name");
            myCustomer.name = (Console.ReadLine());

            Console.WriteLine("enter phone number");
            myCustomer.phoneNumber = (Console.ReadLine());

            double longitude;
            Console.WriteLine("enter longitude");
            double.TryParse(Console.ReadLine(), out longitude);
            myCustomer.longitude = longitude;

            double lattitude;
            Console.WriteLine("enter lattitude");
            double.TryParse(Console.ReadLine(), out lattitude);
            myCustomer.lattitude = lattitude;

            return myCustomer;
        }
        public IDAL.DO.Parcel Parcel(int ParcelRunId)
        {
            IDAL.DO.Parcel myParcel = new IDAL.DO.Parcel();

            
            myParcel.id = ParcelRunId;

            int senderId;
            Console.WriteLine("enter sender id");
            int.TryParse(Console.ReadLine(), out senderId);
            myParcel.senderId = senderId;

            int targetId;
            Console.WriteLine("enter target id");
            int.TryParse(Console.ReadLine(), out targetId);
            myParcel.targetId = targetId;

            int droneId;
            Console.WriteLine("enter drone id");
            int.TryParse(Console.ReadLine(), out droneId);
            myParcel.droneId = droneId;

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
            myParcel.requested = DateTime.Now;

            return myParcel;
        }

    }
}
