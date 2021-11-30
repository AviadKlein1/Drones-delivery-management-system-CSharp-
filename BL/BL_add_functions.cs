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
                IDAL.DO.Station temp = new();

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
                    throw ex;
                }
            }

            //add drone
            public void AddDrone(Drone myDrone)
            {
                //create new drone
                IDAL.DO.Drone temp = new();

                temp.Id = myDrone.Id;
                temp.Model = myDrone.Model;
                temp.weight = myDrone.Weight;

                //add new drone to list of drones
                try
                {
                    dal.AddDrone(temp, myDrone.FirstChargeStationId);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    throw ex;

                }

                //add new drone to list of drones (BL) 
                dronesList.Add(AddDroneToBLList(myDrone));
            }

            //add drone (BL)
            public DroneToList AddDroneToBLList(Drone myDrone)
            {
                //add drone to list (BL)
                DroneToList myDronesToList = new();

                myDronesToList.Id = myDrone.Id;
                myDronesToList.Model = myDrone.Model;
                myDronesToList.Weight = myDrone.Weight;
                myDronesToList.Battery = rd.Next(20, 41);
                myDronesToList.Status = MyEnums.DroneStatus.maintenance;
                myDronesToList.Location = new Location(dal.StationLocate(myDrone.FirstChargeStationId));

                return myDronesToList;
            }

            //add customer
            public void Addcustomer(Customer myCustomer)
            {
                //create new customer
                IDAL.DO.Customer temp = new();

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

                    throw ex;
                }
            }

            //add parcel
            public void AddParcel(Parcel myParcel)
            {
                IDAL.DO.Parcel temp = new();

                temp.Id = dal.ParcelRunId();
                temp.Weight = myParcel.Weight;
                temp.Priority = myParcel.Priority;
                temp.SenderId = myParcel.Sender.id;
                temp.ReciverId = myParcel.Reciever.id;
                temp.DroneId = (myParcel.DroneInParcel == null ? 0 : myParcel.DroneInParcel.Id);
                temp.Requested = DateTime.Now;
                temp.Scheduled = null;
                temp.PickedUp = null;
                temp.Delivered = null;
                Console.WriteLine("your parcel ID is: " + temp.Id + "\n");

                //add parcel to list of parcels
                try
                {
                    dal.AddParcel(temp);
                }
                catch (IDAL.DO.ExcistingIdException ex)
                {
                    throw ex;

                }
            }
        }
    }
}