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
        public void addStation(IDAL.DO.Station station);
        public void addDrone(IDAL.DO.Drone drone);
        public void addcustomer(IDAL.DO.Customer customer);
        public void addParcel(IDAL.DO.Parcel parcel);
        public IDAL.DO.Station getStation(int id);
        public IDAL.DO.Drone getDrone(int id);
        public IDAL.DO.Customer getCustomer(int id);
        public IDAL.DO.Parcel getParcel(int id);

        public IEnumerable<IDAL.DO.Station> getStations();
        
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
        public bool thisDroneIsAssociated(int droneId);
        public bool thereAreParcelsThatNotDeliverdYet();
        public bool myParcelScheduledButNotPickedUp(int parcelId);
        public bool myParcelPickedUpButNotDeliverd(int parcelId);
        //public int idOfAssociatedParcel(int droneId);
        public IDAL.DO.Location theNearestToSenderStationLocation(int parcelId);
        public IDAL.DO.Station theNearestStation(IDAL.DO.Location l);
        public int distance(IDAL.DO.Location a, IDAL.DO.Location b);






    }
}
