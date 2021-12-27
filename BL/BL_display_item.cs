using System;
using System.Collections.Generic;

namespace BlApi
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
                DalApi.DO.Station temp = new();
                try
                {
                    temp = dal.GetStation(stationId);
                }
                catch (Exception)
                {
                    throw;
                }
                //return new Station(temp);
                Station retTemp = new(temp);
                retTemp.NumOfChargeSlots = temp.NumOfChargeSlots;
                retTemp.NumOfAvailableChargeSlots = temp.NumOfAvailableChargeSlots;
                retTemp.Id = temp.Id;
                retTemp.Name = temp.Name;
                retTemp.Location = new Location(temp.Location);

                var dronesInCharge = new List<DroneInCharge>();
                foreach (var element in dronesList)
                {
                    if (element.Status == MyEnums.DroneStatus.maintenance &&
                        element.Location == retTemp.Location)
                    {
                        DroneInCharge droneTemp = new()
                        {
                            Id = element.Id,
                            Battery = element.Battery
                        };
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
                Drone retDrone = new();
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
                DalApi.DO.Customer temp = new();
                try
                {
                    temp = dal.GetCustomer(customerId);
                }
                catch (Exception)
                {
                    throw new WrongIdException(customerId);
                }

                Customer retTemp = new(temp);
                // maybe duiplay parcels at this customer? 
                var parcelsList = dal.GetParcelsList(allParcels);
                foreach (var item in parcelsList)
                {
                    ParcelAtCustomer myParcel = new();
                    DateTime? empty = null;
                    if (item.SenderId == retTemp.Id)
                    {
                        myParcel.Id = item.Id;
                        myParcel.Weight = item.Weight;
                        myParcel.Priority = item.Priority;

                        if (item.Requested != empty && item.Scheduled == empty)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.requested;
                        if (item.Scheduled != empty && item.PickedUp == empty)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.scheduled;
                        if (item.PickedUp != empty && item.Delivered == empty)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.pickedUp;
                        if (item.Delivered != empty)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.delivered;
                       
                        myParcel.TheSecondSide = TheOtherSide(myParcel.Id, retTemp.Id);
                        retTemp.ParcelsSent.Add(myParcel);
                    }
                    if (item.ReceiverId == retTemp.Id)
                    {
                        myParcel.Id = item.Id;
                        myParcel.Weight = item.Weight;
                        myParcel.Priority = item.Priority;

                        if (item.Requested != null && item.Scheduled == null)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.requested;
                        if (item.Scheduled != empty && item.PickedUp == empty)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.scheduled;
                        if (item.PickedUp != empty && item.Delivered == empty)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.pickedUp;
                        if (item.Delivered != empty)
                            myParcel.ParcelStatus = DalApi.DO.MyEnums.ParcelStatus.delivered;

                        myParcel.TheSecondSide = TheOtherSide(myParcel.Id, retTemp.Id);
                        retTemp.ParcelsRecieved.Add(myParcel);
                    }
                }
                return retTemp;
            }

            //display parcel
            public Parcel DisplayParcel(int parcelId)
            {
                BlApi.BO.Parcel temp = new();
                var v = dal.GetParcel(parcelId);
                try
                {
                    temp.Id = v.Id;
                    if (v.DroneId == 0) temp.DroneInParcel = new DroneInParcel(0);
                    if (v.DroneId != 0) temp.DroneInParcel = new DroneInParcel(v.DroneId);
                    temp.DroneInParcel = new DroneInParcel(v.DroneId == 0 ? 0 : v.DroneId);

                    temp.Priority = v.Priority;
                    temp.Sender = new CustomerInParcel(v.SenderId != 0? v.SenderId : 0);
                    temp.Receiver = new CustomerInParcel(v.ReceiverId != 0 ? v.ReceiverId : 0); ;
                    temp.Requested = v.Requested;
                    temp.Scheduled = v.Scheduled;
                    temp.PickedUp = v.PickedUp;
                    temp.Delivered = v.Delivered;
                    temp.Weight = v.Weight;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return temp;
            }
        }
    }
}