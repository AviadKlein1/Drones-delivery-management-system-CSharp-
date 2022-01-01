﻿using System.Collections.Generic;

namespace DalApi
{
    public interface IDal
    {
        /// <summary>
        /// promote parcel serial number
        /// </summary>
        /// <returns></returns>
        public int ParcelRunId();

        /// <summary>
        /// add to list
        /// </summary>
        public void AddStation(DO.Station station);

        /// <summary>
        /// add drone to list of drones
        /// </summary>
        public void AddDrone(DO.Drone drone, int firstChargeStationId);

        /// <summary>
        /// add costumer to list of costimers
        /// </summary>
        /// <param name="myCustomer"></param>
        public void Addcustomer(DO.Customer customer);

        /// <summary>
        /// add parcel to list
        /// </summary>
        /// returns new parcel's id
        public void AddParcel(DO.Parcel parcel);
        /// <summary>
        /// add drone to drone charge list
        /// </summary>
        ///
        public void AddDroneCharge(int droneId,int StationId);
        public void EndDroneCharge(int droneId);

        public void DeleteStation(int myId);
        public void DeleteParcel(int myId);
        public void DeleteDrone(int myId);
        public void DeleteCustomer(int myId);

        /// <summary>
        /// return station by its id
        /// </summary>
        /// <param name="myId"></param>
        public DO.Station GetStation(int id);

        /// <summary>
        /// returns a drone by its id
        /// </summary>
        /// <param name="myId"></param>
        /// <returns></returns>
        public DO.Drone GetDrone(int id);

        /// <summary>
        /// returns a costumer by its id
        /// </summary>
        /// <param name="myId"></param>
        /// <returns></returns>
        public DO.Customer GetCustomer(int id);

        /// <summary>
        /// return a parcel by its id
        /// </summary>
        /// <param name="myId"></param>
        /// <returns></returns>
        public DO.Parcel GetParcel(int id);

        /// <summary>
        /// return all drones
        /// </summary>
        public IEnumerable<DO.Drone> GetDrones();
       
        /// <summary>
        /// returns an array contains electricity consumption data
        /// </summary>
        /// <returns></returns> array of double nums (battery % per distance)
        public double[] DroneElectricityConsumption();

        /// <summary>
        /// return stations location by its id
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns></returns>
        public DO.Location StationLocate(int StationId);

        /// <summary>
        /// return the distance between two coordinates
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double GetDistance(DO.Location a, DO.Location b);

        /// <summary>
        /// update name and number of charge slots in station
        /// <param name="stationId"></param>
        /// <param name="newName"></param>
        /// <param name="numOfChargeSlots"></param>
        /// </summary>
        public void UpdateStation(int stationId, string newName, int numOfChargeSlots,int avialble);
        /// <summary>
        /// update model in drone
        /// <param name="droneId"></param>
        /// <param name="newModel"></param>
        /// </summary>
        public void UpdateDrone(int droneId, string newModel);
        /// <summary>
        /// update model in drone
        /// <param name="customerId"></param>
        /// <param name="newName"></param>
        /// <param name="newPhone"></param>
        /// </summary>
        public void UpdateCustomer(int customerId, string newName, string newPhone);

        /// <summary>
        /// decrise num of available charge slots
        /// <param name="stationId"></param>
        /// </summary>
        public void DecreaseChargeSlot(int stationId);

        /// <summary>
        /// increase num of available charge slots
        /// <param name="stationId"></param>
        /// </summary>
        public void IncreaseChargeSlot(int stationId);

        /// <summary>
        /// Shedule Parcel To Drone in dal
        /// </summary>
        /// <param name="newParcelId"></param>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public void ScheduleParcelToDrone(int newParcelId,int droneId);
        /// <summary>
        /// Pick Up Parcel By Drone in dal
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public void PickUpParcel(int droneId, int parcelId);
        /// <summary>
        /// Deliver Parcel By Drone in dal
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public void DeliverParcel(int droneId, int parcelId);
        
        /// <summary>
        /// get filtered stations list (by condition)
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetStationsList();

        public IEnumerable<DO.DroneCharge> GetDroneCharges();

        /// <summary>
        /// get filtered customers list (by condition)
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public IEnumerable<DO.Customer> GetCustomersList();

        /// <summary>
        /// get filtered parcels list (by condition)
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public IEnumerable<DO.Parcel> GetParcelsList();
        public IEnumerable<DO.Parcel> GetParcelsList(System.Predicate<DalApi.DO.Parcel> match);
        public void UpdatedroneIdInParcel(int ParcelId, int droneId);


    }
}