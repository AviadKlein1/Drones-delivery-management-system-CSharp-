using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// add item to stock
        /// </summary>
        public partial class BL : IBl
        {
            //stations
            static bool AllStations(IDAL.DO.Station s) { return true; }
            readonly System.Predicate<IDAL.DO.Station> allStations = AllStations;
            static bool AvailableForCharge(IDAL.DO.Station s) { return (s.NumOfAvailableChargeSlots > 0); }
            System.Predicate<IDAL.DO.Station> availableForCharge = AvailableForCharge;


            //parcels
            static bool AllParcels(IDAL.DO.Parcel p) { return true; }
            readonly System.Predicate<IDAL.DO.Parcel> allParcels = AllParcels;
            static bool UnassociatedParcels(IDAL.DO.Parcel p) { return (p.Scheduled == null); }
            System.Predicate<IDAL.DO.Parcel> unassociatedParcels = UnassociatedParcels;
            static bool ScheduledButNotPickedUp(IDAL.DO.Parcel p) { return (p.Scheduled != null && p.PickedUp == null); }
            System.Predicate<IDAL.DO.Parcel> scheduledButNotPickedUp = ScheduledButNotPickedUp;
            static bool PickedUpButNotDeliverd(IDAL.DO.Parcel p) { return (p.Delivered == null && p.PickedUp != null); }
            System.Predicate<IDAL.DO.Parcel> pickedUpButNotDeliverd = PickedUpButNotDeliverd;

            static bool AllCustomers(IDAL.DO.Customer c) { return true; }
            readonly System.Predicate<IDAL.DO.Customer> allCustomers = AllCustomers;
            static bool AllDrones(IDAL.DO.Drone d) { return true; }
            readonly System.Predicate<IDAL.DO.Drone> allDrones = AllDrones;

        }
    }
}