using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// display item individually
        /// </summary>
        public partial class BL : IBl
        {
            //display station
            public Station DisplayStation(int stationId)
            {
                IDAL.DO.Station temp = new IDAL.DO.Station();
                try
                {
                    temp = dal.GetStation(stationId);
                }
                catch (Exception)
                {
                    throw;
                }
                //return new Station(temp);
                Station retTemp = new Station(temp);

                var dronesInCharge = new List<DroneInCharge>();
                foreach (var element in dronesList)
                {
                    if (element.Status == MyEnums.DroneStatus.maintenance && element.Location == retTemp.Location)
                    {
                        DroneInCharge droneTemp = new DroneInCharge();
                        droneTemp.Id = element.Id;
                        droneTemp.Battery = element.Battery;
                        dronesInCharge.Add(droneTemp);
                    }
                }
                retTemp.DronesInCharge = dronesInCharge;
                return retTemp;
            }

            //display drone
            public Drone DisplayDrone(int droneId)
            {
                bool exist = false;
                Drone retDrone = new Drone();
                foreach (var element in dronesList)
                {
                    if (element.Id == droneId)
                    {
                        exist = true;
                        retDrone.Id = element.Id;
                        retDrone.Location = element.Location;
                        retDrone.Weight = element.Weight;
                        retDrone.Status = element.Status;
                        retDrone.Model = element.Model;
                        retDrone.Battery = element.Battery;
                        retDrone.Location = element.Location;
                    }
                }
                if (exist == false)
                    throw new WrongIdException(droneId, $"Wrong ID: {droneId}");
                return retDrone;
            }

            //display customer
            public Customer DisplayCustomer(int customerId)
            {
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                try
                {
                    temp = dal.GetCustomer(customerId);
                }
                catch (Exception)
                {
                    throw;
                }

                Customer retTemp = new Customer(temp);
                // maybe duiplay parcels at this customer? 
                var parcelsList = dal.GetParcelsList(allParcels);
                foreach (var item in parcelsList)
                {
                    ParcelAtCustomer myParcel = new ParcelAtCustomer();
                    DateTime? empty = null;
                    if (item.SenderId == retTemp.id)
                    {
                        myParcel.Id = item.Id;
                        myParcel.Weight = item.Weight;
                        myParcel.Priority = item.Priority;

                        if (item.Requested != empty && item.Scheduled == empty) myParcel.ParcelStatus = IDAL.DO.MyEnums.ParcelStatus.requested;
                        if (item.Scheduled != empty && item.PickedUp == empty) myParcel.ParcelStatus = IDAL.DO.MyEnums.ParcelStatus.scheduled;
                        if (item.PickedUp != empty && item.Delivered == empty) myParcel.ParcelStatus = IDAL.DO.MyEnums.ParcelStatus.pickedUp;
                        if (item.Delivered != empty) myParcel.ParcelStatus = IDAL.DO.MyEnums.ParcelStatus.delivered;
                       
                        myParcel.TheSecondSide = TheOtherSide(myParcel.Id, retTemp.id);
                        retTemp.parcelsSent.Add(myParcel);
                    }
                    if (item.ReciverId == retTemp.id)
                    {
                        myParcel.Id = item.Id;
                        myParcel.Weight = item.Weight;
                        myParcel.Priority = item.Priority;

                        if (item.Requested != empty && item.Scheduled == empty) myParcel.ParcelStatus = IDAL.DO.MyEnums.ParcelStatus.requested;
                        if (item.Scheduled != empty && item.PickedUp == empty) myParcel.ParcelStatus = IDAL.DO.MyEnums.ParcelStatus.scheduled;
                        if (item.PickedUp != empty && item.Delivered == empty) myParcel.ParcelStatus = IDAL.DO.MyEnums.ParcelStatus.pickedUp;
                        if (item.Delivered != empty) myParcel.ParcelStatus = IDAL.DO.MyEnums.ParcelStatus.delivered;

                        myParcel.TheSecondSide = TheOtherSide(myParcel.Id, retTemp.id);
                        retTemp.parcelsRecieved.Add(myParcel);
                    }
                }
                return retTemp;
            }

            //display parcel
            public Parcel DisplayParcel(int parcelId)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();
                try
                {
                    temp = dal.GetParcel(parcelId);
                }
                catch (Exception)
                {
                    throw;
                }
                Parcel retTemp = new Parcel(temp);
                return retTemp;
            }
        }
    }
}