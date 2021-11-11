using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    interface IBL
    {
        public void addStation();
        /// <summary>
        /// create a new drone, and add it to array
        /// </summary>
        public void addDrone();
        /// <summary>
        /// create a new customer and add it to array
        /// </summary>
        public void addcustomer();
        /// <summary>
        /// create a new parcel and add it to array
        /// </summary>
        /// returns new parcel's id
        public int addParcel();
        /// <summary>
        /// belong parcel to specific drone
        /// </summary>
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
        ///// <summary>
        ///// print stations details
        ///// </summary>
        ///// <param name="myId"></param>
        //public void stationDisplay(int myId);
        ///// <summary>
        ///// print drone details
        ///// </summary>
        ///// <param name="myId"></param>
        //public void droneDisplay(int myId);
        ///// <summary>
        ///// print customer details
        ///// </summary>
        ///// <param name="myId"></param>
        //public void customerDisplay(int myId);
        ///// <summary>
        ///// print parcel details
        ///// </summary>
        ///// <param name="myId"></param>
        //public void parcelDisplay(int myId);
        ///// <summary>
        ///// print all stations
        ///// </summary>
        //public IEnumerable<IDAL.DO.Station> stationsDisplay();
        ///// <summary>
        ///// print all drones
        ///// </summary>
        //public IEnumerable<IDAL.DO.Drone> dronesDisplay();
        ///// <summary>
        ///// print all customers
        ///// </summary>
        //public IEnumerable<IDAL.DO.Customer> customersDisplay();
        ///// <summary>
        ///// print all parcels
        ///// </summary>
        //public IEnumerable<IDAL.DO.Parcel> parcelsDisplay();
        ///// <summary>
        ///// print all parcels which are not associated with a drone
        ///// </summary>
        //public IEnumerable<IDAL.DO.Parcel> notAssociatedParcelsDisplay();
        ///// <summary>
        ///// print all available to charge stations
        ///// </summary>
        //public IEnumerable<IDAL.DO.Station> availableToChargeStattions();

        ////new method electric consum
        //public double[] droneElectricityConsumption();




    }
}
