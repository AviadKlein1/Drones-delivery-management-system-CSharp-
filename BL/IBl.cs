using System;
using System.Collections.Generic;

namespace BlApi
{
    public interface IBl
    {
        /// <summary>
        /// add station to dal
        /// </summary>
        /// <returns></returns>
        public void AddStation(BO.Station myStation);

        /// <summary>
        /// add drone to dal
        /// </summary>
        /// <returns></returns>
        public void AddDrone(BO.Drone myDrone);

        /// <summary>
        /// add drone to bl list
        /// </summary>
        /// <returns></returns>
        public BO.DroneToList AddDroneToBLList(BO.Drone myDrone);

        /// <summary>
        /// add customer
        /// </summary>
        /// <returns></returns>
        public void Addcustomer(BO.Customer myCustomer);

        /// <summary>
        /// add parcel
        /// </summary>
        /// <returns></returns>
        public void AddParcel(BO.Parcel myParcel);

        /// <summary>
        /// display station
        /// </summary>
        /// <returns></returns>
        /// /// <summary>
        /// Delete station 
        /// </summary>
        /// <returns></returns>
        public void DeleteStation(BO.Station myStation);

        /// <summary>
        /// Delete drone
        /// </summary>
        /// <returns></returns>
        public void DeleteDrone(BO.Drone myDrone);

        /// <summary>
        /// add customer
        /// </summary>
        /// <returns></returns>
        public void DeleteCustomer(BO.Customer myCustomer);

        /// <summary>
        /// Delete parcel
        /// </summary>
        /// <returns></returns>
        public void DeleteParcel(BO.Parcel myParcel);
        public BO.Station DisplayStation(int stationId);

        /// <summary>
        /// display drone
        /// </summary>
        /// <returns></returns>
        public BO.Drone DisplayDrone(int droneId);

        /// <summary>
        /// display customer
        /// </summary>
        /// <returns></returns>
        public BO.Customer DisplayCustomer(int customerId);

        /// <summary>
        /// display parcel
        /// </summary>
        /// <returns></returns>
        public BO.Parcel DisplayParcel(int parcelId);
        
        /// <summary>
        /// updates drone data
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="newModel"></param>
        /// <returns></returns>
        public bool UpdateDrone(int droneId, string newModel);

        /// <summary>
        /// update station details
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="newName"></param>
        /// <param name="numOfChargeSlots"></param>
        /// <returns></returns>
        public bool UpdateStation(int stationId, string newName, int numOfChargeSlots, int avielable);

        /// <summary>
        /// update customer details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="newName"></param>
        /// <param name="newPhone"></param>
        /// <returns></returns>
        public bool UpdateCustomer(int customerId, string newName, string newPhone);

        /// <summary>
        /// get drone and ssend it to charge. if succsses return true
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public bool ChargeDrone(int droneId);

        /// <summary>
        /// end drone charge. if succsses return true
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="chargeTime"></param>
        /// <returns></returns>
        public bool ReleaseDroneFromCharge(int droneId);

        /// <summary>
        /// associate drone with parcel. if succsses return true
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public bool ScheduleParcelToDrone(int droneId);

        /// <summary>
        /// pick up parcel from sender. if succsses return true
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public bool PickUpParcel(int droneId);

        /// <summary>
        /// deliver parcel to reciever. if succsses return true
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public bool DeliverParcel(int droneId);

        /// <summary>
        /// get stations list in condition
        /// </summary>
        /// <returns></returns> return list of stations accured to conditions
        public IEnumerable<BO.StationToList> GetStationsList(System.Predicate<DalApi.DO.Station> match);


        /// <summary>
        /// get drones list in condition
        /// </summary>
        /// <returns></returns> return list of drones accured to conditions
        public List<BO.DroneToList> GetDronesList(System.Predicate<BO.DroneToList> match);

        public IEnumerable<BO.DroneInCharge> GetDroneChargesList(int stationId);



        /// <summary>
        /// get customers list in condition
        /// </summary>
        /// <returns></returns> return list of customers accured to conditions
        public IEnumerable<BO.CustomerToList> GetCustomersList(System.Predicate<DalApi.DO.Customer> match);


        /// <summary>
        /// get parcels list in condition
        /// </summary>
        /// <returns></returns> return list of parcels accured to conditions
        public IEnumerable<BO.ParcelToList> GetParcelsList(System.Predicate<DalApi.DO.Parcel> match);
        public void PlaySimulator(int droneId, Action ui_Update, Func<bool> stopCheck);


    }
}