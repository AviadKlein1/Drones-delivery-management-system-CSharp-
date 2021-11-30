using System.Collections.Generic;

namespace IDAL
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
        public void AddStation(IDAL.DO.Station station);
        /// <summary>
        /// add drone to list of drones
        /// </summary>
        public void AddDrone(IDAL.DO.Drone drone , int firstChargeStationId);
        /// <summary>
        /// add costumer to list of costimers
        /// </summary>
        /// <param name="myCustomer"></param>
        public void Addcustomer(IDAL.DO.Customer customer);
        /// <summary>
        /// add parcel to list
        /// </summary>
        /// returns new parcel's id
        public void AddParcel(IDAL.DO.Parcel parcel);
        /// <summary>
        /// return station by its id
        /// </summary>
        /// <param name="myId"></param>
        public IDAL.DO.Station GetStation(int id);
        /// <summary>
        /// returns a drone by its id
        /// </summary>
        /// <param name="myId"></param>
        /// <returns></returns>
        public IDAL.DO.Drone GetDrone(int id);
        /// <summary>
        /// returns a costumer by its id
        /// </summary>
        /// <param name="myId"></param>
        /// <returns></returns>
        public IDAL.DO.Customer GetCustomer(int id);
        /// <summary>
        /// return a parcel by its id
        /// </summary>
        /// <param name="myId"></param>
        /// <returns></returns>
        public IDAL.DO.Parcel GetParcel(int id);
        /// <summary>
        /// return all stations
        /// </summary>
        public IEnumerable<IDAL.DO.Station> GetStations();
        /// <summary>
        /// return all drones
        /// </summary>
        public IEnumerable<IDAL.DO.Drone> GetDrones();
        /// <summary>
        /// return all customers
        /// </summary>
        public IEnumerable<IDAL.DO.Customer> GetCustomers();
        /// <summary>
        /// return all parcels
        /// </summary>
        public IEnumerable<IDAL.DO.Parcel> GetParcels();
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
        public IDAL.DO.Location StationLocate(int StationId);
        /// <summary>
        /// return the distance between two coordinates
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Distance(IDAL.DO.Location a, IDAL.DO.Location b);
        /// <summary>
        /// update name and number of charge slots in station
        /// <param name="stationId"></param>
        /// <param name="newName"></param>
        /// <param name="numOfChargeSlots"></param>
        /// </summary>
        public void UpdateStation(int stationId, string newName, int numOfChargeSlots);
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
        public void DecriseChargeSlot(int stationId);
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
        public void SheduleParcelToDrone(int newParcelId,int droneId);
        /// <summary>
        /// Pick Up Parcel By Drone in dal
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public void PickUpParcelByDrone(int droneId, int parcelId);
        /// <summary>
        /// Deliver Parcel By Drone in dal
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public void DeliverParcelByDrone(int droneId, int parcelId);
    }
}


            