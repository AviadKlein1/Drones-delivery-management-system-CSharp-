using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// add item to stock
        /// </summary>
        public partial class BL : IBl
        {
            //add station
            public void AddStation(Station myStation)
            {
                //create new station
                IDAL.DO.Station temp = new IDAL.DO.Station();

                temp.Id = myStation.Id;
                temp.Location = new IDAL.DO.Location(myStation.Location.longitude, myStation.Location.lattitude);
                temp.Name = myStation.Name;
                temp.NumOfAvailableChargeSlots = myStation.NumOfAvailableChargeSlots;
                temp.NumOfChargeSlots = myStation.NumOfChargeSlots;

                //add new station to list of stations
                try
                {
                    dal.AddStation(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //add drone
            public void AddDrone(Drone myDrone)
            {
                //create new drone
                IDAL.DO.Drone temp = new IDAL.DO.Drone();

                temp.Id = myDrone.id;
                temp.Model = myDrone.model;
                temp.weight = myDrone.weight;

                //add new drone to list of drones
                try
                {
                    dal.AddDrone(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //add new drone to list of drones (BL) 
                dronesList.Add(AddDroneToBLList(myDrone));
            }

            //add drone (BL)
            public DroneToList AddDroneToBLList(Drone myDrone)
            {
                //add drone to list (BL)
                DroneToList myDronesToList = new DroneToList();

                myDronesToList.id = myDrone.id;
                myDronesToList.model = myDrone.model;
                myDronesToList.weight = myDrone.weight;
                myDronesToList.battery = rd.Next(20, 41);
                myDronesToList.status = MyEnums.DroneStatus.maintenance;
                myDronesToList.location = new Location(dal.StationLocate(myDrone.firstChargeStationId));

                return myDronesToList;
            }

            //add customer
            public void Addcustomer(Customer myCustomer)
            {
                //create new customer
                IDAL.DO.Customer temp = new IDAL.DO.Customer();

                temp.Id = myCustomer.id;
                temp.Name = myCustomer.name;
                temp.PhoneNumber = myCustomer.phoneNumber;
                temp.Location = new IDAL.DO.Location(myCustomer.location.longitude, myCustomer.location.lattitude);

                //add customer to list of customers
                try
                {
                    dal.Addcustomer(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //add parcel
            public void AddParcel(Parcel myParcel)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();

                temp.Id = dal.ParcelRunId();
                temp.Weight = myParcel.Weight;
                temp.Priority = myParcel.Priority;
                temp.SenderId = myParcel.Sender.id;
                temp.ReciverId = myParcel.Reciever.id;
                temp.DroneId = (myParcel.DroneInParcel == null ? 0 : myParcel.DroneInParcel.id);
                temp.Scheduled = DateTime.Now;
                temp.Requested = new DateTime();
                temp.PickedUp = new DateTime();
                temp.Delivered = new DateTime();
                Console.WriteLine("your parcel ID is: " + temp.Id + "\n");

                //add parcel to list of parcels
                try
                {
                    dal.AddParcel(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}