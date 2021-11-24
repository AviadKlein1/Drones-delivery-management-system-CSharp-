using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        public int ParcelRunId();
        public void AddStation(IDAL.DO.Station station);
        public void AddDrone(IDAL.DO.Drone drone);
        public void Addcustomer(IDAL.DO.Customer customer);
        public void AddParcel(IDAL.DO.Parcel parcel);

        public IDAL.DO.Station GetStation(int id);
        public IDAL.DO.Drone GetDrone(int id);
        public IDAL.DO.Customer GetCustomer(int id);
        public IDAL.DO.Parcel GetParcel(int id);

        public IEnumerable<IDAL.DO.Station> GetStations();
        public IEnumerable<IDAL.DO.Drone> GetDrones();
        public IEnumerable<IDAL.DO.Customer> GetCustomers();
        public IEnumerable<IDAL.DO.Parcel> GetParcels();

        //public IEnumerable<IDAL.DO.Parcel> getNotAssociatedParcels();
        //public IEnumerable<IDAL.DO.Station> getAvailableToChargeStations();
        public double[] DroneElectricityConsumption();
        public IDAL.DO.Location StationLocate(int StationId);

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
        public void SheduleParcelToDrone(int newParcelId,int droneId);
        public void PickUpParcelByDrone(int droneId, int parcelId);
        public void DeliverParcelByDrone(int droneId, int parcelId);

        





    }
}


            