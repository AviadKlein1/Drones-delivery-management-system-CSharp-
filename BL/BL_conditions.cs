using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
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
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static bool AllStations(DalApi.DO.Station s) { return true; }
            public readonly System.Predicate<DalApi.DO.Station> allStations = AllStations;
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static bool AvailableForCharge(DalApi.DO.Station s) { return s.NumOfAvailableChargeSlots > 0; }
            public System.Predicate<DalApi.DO.Station> availableForCharge = AvailableForCharge;


            //parcels
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllParcels(DalApi.DO.Parcel p) { return true; }
            public readonly System.Predicate<DalApi.DO.Parcel> allParcels = AllParcels;
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool UnassociatedParcels(DalApi.DO.Parcel p) { return p.Scheduled == DateTime.MinValue; }
            public System.Predicate<DalApi.DO.Parcel> unassociatedParcels = UnassociatedParcels;
            [MethodImpl(MethodImplOptions.Synchronized)]

            static bool ScheduledButNotPickedUp(DalApi.DO.Parcel p) { return p.Scheduled != DateTime.MinValue && p.PickedUp == DateTime.MinValue; }
            public System.Predicate<DalApi.DO.Parcel> scheduledButNotPickedUp = ScheduledButNotPickedUp;
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool PickedUpButNotDeliverd(DalApi.DO.Parcel p) { return p.Delivered == DateTime.MinValue && p.PickedUp != DateTime.MinValue; }
            public System.Predicate<DalApi.DO.Parcel> pickedUpButNotDeliverd = PickedUpButNotDeliverd;
            //customers
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllCustomers(DalApi.DO.Customer c) { return true; }
            public static readonly System.Predicate<DalApi.DO.Customer> allCustomers = AllCustomers;

            //drones
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllDrones(DalApi.DO.Drone d) { return true; }
            public static readonly System.Predicate<DalApi.DO.Drone> allDrones = AllDrones;
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllDronesInMaintenance(DroneToList d) { return d.Status == MyEnums.DroneStatus.maintenance; }
            public System.Predicate<BlApi.BO.DroneToList> allDronesInMaintenance = AllDronesInMaintenance;
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllDronesInDelivery(DroneToList d) { return d.Status == MyEnums.DroneStatus.delivery; }
            public System.Predicate<DroneToList> allDronesInDelivery = AllDronesInDelivery;
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllDronesInAvailable(DroneToList d) { return d.Status == MyEnums.DroneStatus.available; }
            public System.Predicate<DroneToList> allDronesInAvailable = AllDronesInAvailable;
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllDronesInHeavy(DroneToList d) { return d.Weight == DalApi.DO.MyEnums.WeightCategory.heavy; }
            public System.Predicate<DroneToList> allDronesInHeavy = AllDronesInHeavy;
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllDronesInMedium(DroneToList d) { return d.Weight == DalApi.DO.MyEnums.WeightCategory.medium; }
            public System.Predicate<DroneToList> allDronesInMedium = AllDronesInMedium;
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static bool AllDronesInLight(DroneToList d) { return d.Weight == DalApi.DO.MyEnums.WeightCategory.light; }
            public System.Predicate<DroneToList> allDronesInLight = AllDronesInLight;
        }
    }
}