﻿using System.Collections.Generic;

namespace IBL
{
    public interface IBl
    {
        /// <summary>
        /// add station to dal
        /// </summary>
        /// <returns></returns>
        public void AddStation(IBL.BO.Station myStation);
        /// <summary>
        /// add drone to dal
        /// </summary>
        /// <returns></returns>
        public void AddDrone(IBL.BO.Drone myDrone);
        /// <summary>
        /// add drone to bl list
        /// </summary>
        /// <returns></returns>
        public IBL.BO.DroneToList AddDroneToBLList(IBL.BO.Drone myDrone);
        /// <summary>
        /// add customer
        /// </summary>
        /// <returns></returns>
        public void Addcustomer(IBL.BO.Customer myCustomer);
        /// <summary>
        /// add parcel
        /// </summary>
        /// <returns></returns>
        public void AddParcel(IBL.BO.Parcel myParcel);
        /// <summary>
        /// display station
        /// </summary>
        /// <returns></returns>
        public IBL.BO.Station DisplayStation(int stationId);
        /// <summary>
        /// display drone
        /// </summary>
        /// <returns></returns>
        public IBL.BO.Drone DisplayDrone(int droneId);
        /// <summary>
        /// display customer
        /// </summary>
        /// <returns></returns>
        public IBL.BO.Customer DisplayCustomer(int customerId);
        /// <summary>
        /// display parcel
        /// </summary>
        /// <returns></returns>
        public IBL.BO.Parcel DisplayParcel(int parcelId);
        /// <summary>
        /// display stations list
        /// </summary>
        /// <returns></returns> return list of stations
        public List<IBL.BO.StationToList> DisplayStations();
        /// <summary>
        /// display drones list
        /// </summary>
        /// <returns></returns> return list of drones
        public List<IBL.BO.DroneToList> DisplayDrones();
        /// <summary>
        /// display customers list
        /// </summary>
        /// <returns></returns> return list of customers
        public List<IBL.BO.CustomerToList> DisplayCustomers();
        /// <summary>
        /// display parcels list
        /// </summary>
        /// <returns></returns> return list of parcels
        public List<IBL.BO.ParcelToList> DisplayParcels();
        /// <summary>
        /// display unassociated parcels list
        /// </summary>
        /// <returns></returns> return list of parcels
        public List<IBL.BO.ParcelToList> DisplayUnassociatedParcels();
        /// <summary>
        /// display available for charge stations list
        /// </summary>
        /// <returns></returns> return list of stations
        public List<IBL.BO.StationToList> DisplayAvailableStations();
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
        public bool UpdateStation(int stationId, string newName, int numOfChargeSlots);
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
        public bool ReleaseDroneFromCharge(int droneId, int chargeTime);
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
        public bool PickUpParcelByDrone(int droneId);
        /// <summary>
        /// deliver parcel to reciever. if succsses return true
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public bool DeliverParcelByDrone(int droneId);
    }
}