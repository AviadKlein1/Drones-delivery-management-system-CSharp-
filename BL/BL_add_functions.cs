using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// add item to stock
        /// </summary>
        public partial class BL
        {
            //add station
            public void addStation(Station myStation)
            {
                //create new station
                IDAL.DO.Station temp = new IDAL.DO.Station();

                temp.id = myStation.id;
                temp.location = new IDAL.DO.Location(myStation.location.longitude, myStation.location.lattitude);
                temp.name = myStation.name;
                temp.numOfAvailableChargeSlots = myStation.numOfAvailableChargeSlots;
                temp.numOfChargeSlots = myStation.numOfChargeSlots;

                //add new station to list of stations
                try
                {
                    dal.addStation(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //add drone
            public void addDrone(Drone myDrone)
            {
                //create new drone
                IDAL.DO.Drone temp = new IDAL.DO.Drone();

                temp.id = myDrone.id;
                temp.model = myDrone.model;
                temp.weight = myDrone.weight;

                //add new drone to list of drones
                try
                {
                    dal.addDrone(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //add new drone to list of drones (BL) 
                dronesList.Add(addDroneToBLList(myDrone));
            }

            //add drone (BL)
            public DroneToList addDroneToBLList(Drone myDrone)
            {
                //add drone to list (BL)
                DroneToList myDronesToList = new DroneToList();

                myDronesToList.id = myDrone.id;
                myDronesToList.model = myDrone.model;
                myDronesToList.weight = myDrone.weight;
                myDronesToList.battery = rd.Next(20, 41);
                myDronesToList.status = MyEnums.DroneStatus.maintenance;
                myDronesToList.location = new Location(dal.stationLocate(myDrone.firstChargeStationId));

                return myDronesToList;
            }

            //add customer
            public void addcustomer(Customer myCustomer)
            {
                //create new customer
                IDAL.DO.Customer temp = new IDAL.DO.Customer();

                temp.id = myCustomer.id;
                temp.name = myCustomer.name;
                temp.phoneNumber = myCustomer.phoneNumber;
                temp.location = new IDAL.DO.Location(myCustomer.location.longitude, myCustomer.location.lattitude);

                //add customer to list of customers
                try
                {
                    dal.addcustomer(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //add parcel
            public void addParcel(Parcel myParcel)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();

                temp.id = dal.ParcelRunId();
                temp.weight = myParcel.weight;
                temp.priority = myParcel.priority;
                temp.senderId = myParcel.sender.id;
                temp.reciverId = myParcel.reciever.id;
                temp.droneId = (myParcel.DroneInParcel == null ? 0 : myParcel.DroneInParcel.id);
                temp.scheduled = DateTime.Now;
                temp.requested = new DateTime();
                temp.pickedUp = new DateTime();
                temp.delivered = new DateTime();
                Console.WriteLine("your parcel ID is: " + temp.id + "\n");

                //add parcel to list of parcels
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