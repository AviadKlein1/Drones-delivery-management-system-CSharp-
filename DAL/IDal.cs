using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDal
    {
        public int ParcelRunId();
        /// <summary>
        /// create a new station and add to array
        /// </summary>
        public void addStation(IDAL.DO.Station station);
        /// <summary>
        /// create a new drone, and add it to array
        /// </summary>
        public void addDrone(IDAL.DO.Drone drone);

        /// <summary>
        /// create a new customer and add it to array
        /// </summary>
        public void addcustomer(IDAL.DO.Customer customer);
        /// <summary>
        /// create a new parcel and add it to array
        /// </summary>
        /// returns new parcel's id
        public void addParcel(IDAL.DO.Parcel parcel);
        public IDAL.DO.Station getStation(int id);
        public IDAL.DO.Drone getDrone(int id);
        public IDAL.DO.Customer getCustomer(int id);
        public IDAL.DO.Parcel getParcel(int id);


        ///// <summary>
        ///// belong parcel to specific drone
        ///// </summary>
        //public void paracelToDrone(int parcelId);
        ///// <summary>
        ///// report pacel picked up
        ///// </summary>
        ///// <param name="myId"></param>
        //public void pickUp(int myId);
        ///// <summary>
        ///// report parcel delivered
        ///// </summary>
        ///// <param name="myId"></param>
        //public void delivered(int myId);
        ///// <summary>
        ///// send drone to charge slot
        ///// </summary>
        ///// <param name="droneId"></param>
        ///// <param name="stationId"></param>
        //public void sendToCharge(int droneId, int stationId);
        ///// <summary>
        ///// report drone ended charging
        ///// </summary>
        ///// <param name="droneId"></param>
        ///// <param name="stationId"></param>
        //public void endCharge(int droneId, int stationId);

        public IEnumerable<IDAL.DO.Station> getStations();
        /// <summary>
        /// print all drones
        /// </summary>
        public IEnumerable<IDAL.DO.Drone> getDrones();
        /// <summary>
        /// print all customers
        /// </summary>
        public IEnumerable<IDAL.DO.Customer> getCustomers();
        /// <summary>
        /// print all parcels
        /// </summary>
        public IEnumerable<IDAL.DO.Parcel> getParcels();
        /// <summary>
        /// print all parcels which are not associated with a drone
        /// </summary>
        public IEnumerable<IDAL.DO.Parcel> getNotAssociatedParcels();
        /// print all available to charge stations
        /// </summary>
        public IEnumerable<IDAL.DO.Station> getAvailableToChargeStations();

        //new method electric consum
        public double[] droneElectricityConsumption();
        public IDAL.DO.Location stationLocate(int StationId);


    }
}
