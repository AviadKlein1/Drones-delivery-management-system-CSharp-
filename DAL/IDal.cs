using System.Collections.Generic;

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
        public IEnumerable<IDAL.DO.Customer> getCustomers();
        public IEnumerable<IDAL.DO.Parcel> getParcels();

        //public IEnumerable<IDAL.DO.Parcel> getNotAssociatedParcels();
        //public IEnumerable<IDAL.DO.Station> getAvailableToChargeStations();
        public double[] droneElectricityConsumption();
        public IDAL.DO.Location stationLocate(int StationId);

        //public bool thisDroneIsAssociated(int droneId);
        //public bool thereAreParcelsThatNotDeliverdYet();
        //public bool myParcelScheduledButNotPickedUp(int parcelId);
        //public bool myParcelPickedUpButNotDeliverd(int parcelId);
        ////public int idOfAssociatedParcel(int droneId);
        //public IDAL.DO.Location theNearestToSenderStationLocation(int parcelId);
        //public IDAL.DO.Station theNearestStation(IDAL.DO.Location l);
        public int distance(IDAL.DO.Location a, IDAL.DO.Location b);
        public void updatStation(int stationId, string newName, int numOfChargeSlots);
        public void updateDrone(int droneId, string newModel);
        public void updateCustomer(int customerId, string newName, string newPhone);
        public void decriseChargeSlot(int stationId);
        public void increaseChargeSlot(int stationId);
        public void PickUpParcelByDrone(int droneId, int parcelId);
        public void DeliverParcelByDrone(int droneId, int parcelId);

        





    }
}


            