using System.Collections.Generic;

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



        /// <summary>
        /// display stations list in condition
        /// </summary>
        /// <returns></returns> return list of stations accured to conditions
        public IEnumerable<IBL.BO.StationToList> GetStationsList(System.Predicate<IDAL.DO.Station> match);
        public IEnumerable<IBL.BO.DroneToList> GetDronesList(System.Predicate<IBL.BO.DroneToList> match);
        public IEnumerable<IBL.BO.CustomerToList> GetCustomersList(System.Predicate<IDAL.DO.Customer> match);
        public IEnumerable<IBL.BO.ParcelToList> GetParcelsList(System.Predicate<IDAL.DO.Parcel> match);


    }
}