using System;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public void addStation(Station myStation)
            {
                IDAL.DO.Station temp = new IDAL.DO.Station();

                temp.id = myStation.id;
                temp.location = new IDAL.DO.Location(myStation.location.longitude, myStation.location.lattitude);
                temp.name = myStation.Name;
                temp.numOfAvailableChargeSlots = myStation.numOfAvailableChargeSlots;
                temp.numOfChargeSlots = myStation.numOfChargeSlots;

                try
                {
                    //insert station to array
                    dal.addStation(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            public void addDrone(Drone myDrone)
            {
                IDAL.DO.Drone temp = new IDAL.DO.Drone();

                temp.id = myDrone.id;
                temp.model = myDrone.model;
                temp.weight = myDrone.weight;
                try
                {
                    dal.addDrone(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                dronesList.Add(addDroneToBLList(myDrone));
            }
            public DroneToList addDroneToBLList(Drone myDrone)
            {
                //add to the list in BL
                DroneToList myDronesToList = new DroneToList();
                myDronesToList.id = myDrone.id;
                myDronesToList.model = myDrone.model;
                myDronesToList.weight = myDrone.weight;
                myDronesToList.battery = rd.Next(20, 41);
                myDronesToList.status = MyEnums.DroneStatus.maintenance;
                myDronesToList.location = new Location(dal.stationLocate(myDrone.firstChargeStationId));
                return myDronesToList;
            }
            public void addcustomer(Customer myCustomer)
            {
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                temp.id = myCustomer.id;
                temp.name = myCustomer.name;
                temp.phoneNumber = myCustomer.phoneNumber;
                temp.location = new IDAL.DO.Location(myCustomer.location.longitude, myCustomer.location.lattitude);
                try
                {
                    dal.addcustomer(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            public void addParcel(Parcel myParcel)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();

                temp.id = dal.ParcelRunId();
                temp.weight = myParcel.weight;
                temp.priority = myParcel.priority;
                temp.senderId = myParcel.sender.id;
                temp.reciverId = myParcel.reciever.id;
                temp.droneId = myParcel.DroneInParcel.id;
                temp.scheduled = DateTime.Now;
                temp.requested = new DateTime();
                temp.pickedUp = new DateTime();
                temp.delivered = new DateTime();
                Console.WriteLine("your parcel ID is: " + temp.id + "\n");

                try
                {
                    dal.addParcel(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}