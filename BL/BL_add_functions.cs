
using System.Runtime.CompilerServices;
using System;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// add item to stock
        /// </summary>
        public partial class BL : IBl
        {
            //add station
            [MethodImpl(MethodImplOptions.Synchronized)]

            public void AddStation(Station myStation)
            {
                //create a new station
                DalApi.DO.Station temp = new();

                temp.Id = myStation.Id;
                temp.Location = new DalApi.DO.Location(myStation.Location.Latitude, myStation.Location.Longitude);
                temp.Name = myStation.Name;
                temp.NumOfAvailableChargeSlots = myStation.NumOfAvailableChargeSlots;
                temp.NumOfChargeSlots = myStation.NumOfChargeSlots;
                temp.IsActive = true;


                //add new station to list of stations
                try
                {
                    lock (dal)
                    {
                        dal.AddStation(temp);
                    }
                }
                catch (DalApi.DO.ExistingIdException ex)
                {
                    throw new ExistingIdException(myStation.Id, ex.Message);
                }
            }
        

            //add drone

            [MethodImpl(MethodImplOptions.Synchronized)]
            public void AddDrone(Drone myDrone)
            {
                //create new drone
                DalApi.DO.Drone temp = new();

                temp.Id = myDrone.Id;
                temp.Model = myDrone.Model;
                temp.Weight = myDrone.Weight;
                temp.IsActive = true;

                //add new drone to list of drones
                try
                {
                    lock (dal)
                    {
                        dal.AddDrone(temp, myDrone.FirstChargeStationId);
                    }
                }
                catch (DalApi.DO.ExistingIdException ex)
                {
                    throw new ExistingIdException(myDrone.Id, ex.Message);
                }
                catch (DalApi.DO.WrongIdException ex)
                {
                    throw new WrongIdException(myDrone.Id, ex.Message);
                }
               

                //add new drone to list of drones (BL) 
                dronesList.Add(AddDroneToBLList(myDrone));
            }

            //add drone (BL)
            [MethodImpl(MethodImplOptions.Synchronized)]
            public DroneToList AddDroneToBLList(Drone myDrone)
            {
                //add drone to list (BL)
                DroneToList myDronesToList = new();
                lock (dal)
                {
                    myDronesToList.Id = myDrone.Id;
                    myDronesToList.Model = myDrone.Model;
                    myDronesToList.Weight = myDrone.Weight;
                    myDronesToList.Battery = rd.Next(20, 41);
                    myDronesToList.Status = MyEnums.DroneStatus.maintenance;
                    myDronesToList.Location = new Location(dal.StationLocate(myDrone.FirstChargeStationId));
                }
                return myDronesToList;
            }

            //add customer
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void Addcustomer(Customer myCustomer)
            {
                //create new customer
                DalApi.DO.Customer temp = new();

                temp.Id = myCustomer.Id;
                temp.Name = myCustomer.Name;
                temp.PhoneNumber = myCustomer.PhoneNumber;
                temp.IsActive = true;
                temp.Location = new DalApi.DO.Location(myCustomer.Location.Latitude, myCustomer.Location.Longitude);

                //add customer to list of customers
                try
                {
                    lock (dal)
                    {
                        dal.AddCustomer(temp);
                    }
                }
                catch (DalApi.DO.ExistingIdException ex)
                {

                    throw new ExistingIdException(myCustomer.Id, ex.Message);
                }
            }

            //add parcel
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void AddParcel(Parcel myParcel)
            {
                DalApi.DO.Parcel temp = new();
                lock (dal)
                {
                    temp.Id = dal.ParcelRunId();
                    temp.Weight = myParcel.Weight;
                    temp.Priority = myParcel.Priority;
                    temp.SenderId = myParcel.Sender.Id;
                    temp.ReceiverId = myParcel.Receiver.Id;
                    temp.DroneId = (myParcel.DroneInParcel == null ? 0 : myParcel.DroneInParcel.Id);
                    temp.Requested = DateTime.Now;
                    temp.IsActive = true;
                    temp.Scheduled = DateTime.MinValue;
                    temp.PickedUp = DateTime.MinValue;
                    temp.Delivered = DateTime.MinValue;
                    Console.WriteLine("your parcel ID is: " + temp.Id + "\n");

                    //add parcel to list of parcels
                    try
                    {
                        dal.AddParcel(temp);
                    }
                    catch (DalApi.DO.ExistingIdException ex)
                    {
                        throw new ExistingIdException(temp.Id, ex.Message);
                    }
                }
            }
        }
    }
}