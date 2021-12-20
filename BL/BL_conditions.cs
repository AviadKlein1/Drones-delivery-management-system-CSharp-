using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// add item to stock
        /// </summary>
        public partial class BL : IBl
        {
            //stations
            public static bool AllStations(DalApi.DO.Station s) { return true; }
            public readonly System.Predicate<DalApi.DO.Station> allStations = AllStations;
            public static bool AvailableForCharge(DalApi.DO.Station s) { return s.NumOfAvailableChargeSlots > 0; }
            public System.Predicate<DalApi.DO.Station> availableForCharge = AvailableForCharge;


            //parcels
            public static bool AllParcels(DalApi.DO.Parcel p) { return true; }
            public readonly System.Predicate<DalApi.DO.Parcel> allParcels = AllParcels;
            public static bool UnassociatedParcels(DalApi.DO.Parcel p) { return p.Scheduled == null; }
            public System.Predicate<DalApi.DO.Parcel> unassociatedParcels = UnassociatedParcels;
            static bool ScheduledButNotPickedUp(DalApi.DO.Parcel p) { return p.Scheduled != null && p.PickedUp == null; }
            public System.Predicate<DalApi.DO.Parcel> scheduledButNotPickedUp = ScheduledButNotPickedUp;
            public static bool PickedUpButNotDeliverd(DalApi.DO.Parcel p) { return p.Delivered == null && p.PickedUp != null; }
            public System.Predicate<DalApi.DO.Parcel> pickedUpButNotDeliverd = PickedUpButNotDeliverd;
            //customers
            public static bool AllCustomers(DalApi.DO.Customer c) { return true; }
            public readonly System.Predicate<DalApi.DO.Customer> allCustomers = AllCustomers;

            //drones
            public static bool AllDrones(DalApi.DO.Drone d) { return true; }
            public static readonly System.Predicate<DalApi.DO.Drone> allDrones = AllDrones;
            public static bool AllDronesInMaintenance(DroneToList d) { return d.Status == MyEnums.DroneStatus.maintenance; }
            public System.Predicate<BlApi.BO.DroneToList> allDronesInMaintenance = AllDronesInMaintenance;
            public static bool AllDronesInDelivery(DroneToList d) { return d.Status == MyEnums.DroneStatus.delivery; }
            public System.Predicate<DroneToList> allDronesInDelivery = AllDronesInDelivery;
            public static bool AllDronesInAvailable(DroneToList d) { return d.Status == MyEnums.DroneStatus.available; }
            public System.Predicate<DroneToList> allDronesInAvailable = AllDronesInAvailable;
            public static bool AllDronesInHeavy(DroneToList d) { return d.Weight == DalApi.DO.MyEnums.WeightCategory.heavy; }
            public System.Predicate<DroneToList> allDronesInHeavy = AllDronesInHeavy;
            public static bool AllDronesInMedium(DroneToList d) { return d.Weight == DalApi.DO.MyEnums.WeightCategory.medium; }
            public System.Predicate<DroneToList> allDronesInMedium = AllDronesInMedium;
            public static bool AllDronesInLight(DroneToList d) { return d.Weight == DalApi.DO.MyEnums.WeightCategory.light; }
            public System.Predicate<DroneToList> allDronesInLight = AllDronesInLight;
        }
    }
}