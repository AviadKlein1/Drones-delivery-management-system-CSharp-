using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

            [MethodImpl(MethodImplOptions.Synchronized)]
            public Station DisplayStation(int stationId)
            {
                lock (dal)
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


                    var dronesInCharge = (from item in dronesList
                                          where item.Status == MyEnums.DroneStatus.maintenance
                                          where item.Location.Latitude == retTemp.Location.Latitude &&
                                                item.Location.Longitude == retTemp.Location.Longitude
                                          select new DroneInCharge(item.Id, item.Battery,
                                          dal.GetDroneCharges().First(itemDC => itemDC.DroneId == item.Id).StartChargeTime)).ToList();
                    retTemp.DronesInCharge = dronesInCharge;
                    return retTemp;
                }
            }

            //display drone
            [MethodImpl(MethodImplOptions.Synchronized)]
            public Drone DisplayDrone(int droneId)
            {
                var DelPar = new ParcelInDelivery();
                bool exist = false;
                Drone retDrone = new();
                foreach (var element in from element in dronesList
                                        where element.Id == droneId
                                        select element)
                {
                    exist = true;
                    retDrone.Id = element.Id;
                    retDrone.Location = element.Location;
                    retDrone.Weight = element.Weight;
                    retDrone.Status = element.Status;
                    retDrone.Model = element.Model;
                    retDrone.Battery = element.Battery;
                    retDrone.Location = element.Location;
                    DelPar.Id =AssociatedParcelId(element.Id);
                    retDrone.DeliveredParcel = DelPar;

                }

                if (exist == false)
                    throw new WrongIdException(droneId, $"Wrong ID: {droneId}");
                return retDrone;
            }

            //display customer
            [MethodImpl(MethodImplOptions.Synchronized)]
            public Customer DisplayCustomer(int customerId)
            {
                lock (dal)
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
                    var parcelsList = dal.GetParcelsList();
                    foreach (var item in parcelsList)
                    {
                        ParcelAtCustomer myParcel = new();
                        DateTime? empty = DateTime.MinValue;
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

                            if (item.Requested != DateTime.MinValue && item.Scheduled == DateTime.MinValue)
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
            }

            //display parcel
            [MethodImpl(MethodImplOptions.Synchronized)]
            public Parcel DisplayParcel(int parcelId)
            {
                lock (dal)
                {
                    Parcel temp = new();
                    var v = dal.GetParcel(parcelId);
                    try
                    {
                        temp.Id = v.Id;
                        if (v.DroneId == 0) temp.DroneInParcel = new DroneInParcel(0);
                        else temp.DroneInParcel = new DroneInParcel(v.DroneId);
                        temp.DroneInParcel = new DroneInParcel(v.DroneId == 0 ? 0 : v.DroneId);

                        temp.Priority = v.Priority;
                        temp.Sender = new CustomerInParcel(v.SenderId != 0 ? v.SenderId : 0);
                        temp.Sender.Name = NameOfCustomer(v.SenderId);
                        temp.Receiver = new CustomerInParcel(v.ReceiverId != 0 ? v.ReceiverId : 0);
                        temp.Receiver.Name = NameOfCustomer(v.ReceiverId);
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
            [MethodImpl(MethodImplOptions.Synchronized)]
            public ParcelInDelivery DisplayDeliveredParcel(int droneId)
            {
                var drone = DisplayDrone(droneId);
                var droneLocation = new DalApi.DO.Location(drone.Location.Latitude, drone.Location.Longitude);
                if (drone.DeliveredParcel.Id == 0) throw new WrongIdException(droneId, "no parcel delivered at this drone");
                var parcel = DisplayParcel(drone.DeliveredParcel.Id);
                ParcelInDelivery temp = new();
                temp.Id = drone.DeliveredParcel.Id;
                temp.Priority = parcel.Priority;
                temp.Sender = parcel.Sender;
                temp.Receiver = parcel.Receiver;
                temp.Weight = parcel.Weight;
                temp.IsPickedUp = parcel.PickedUp > DateTime.MinValue ? false : true;
                temp.PickUpLocation = new Location(SenderLocation(temp.Id));
                temp.TargetLocation = new Location(ReceiverLocation(temp.Id));
                temp.Distance = dal.GetDistance(droneLocation, SenderLocation(temp.Id))
                    + dal.GetDistance(SenderLocation(temp.Id), ReceiverLocation(temp.Id))
                    + dal.GetDistance(ReceiverLocation(temp.Id), NearestAvailableChargeSlot(ReceiverLocation(temp.Id)).Location);

                if (parcel.Scheduled != DateTime.MinValue && parcel.PickedUp == DateTime.MinValue)
                    temp.Distance = dal.GetDistance(droneLocation, SenderLocation(temp.Id));
                if (parcel.PickedUp != DateTime.MinValue && parcel.Delivered == DateTime.MinValue)
                    temp.Distance = dal.GetDistance(SenderLocation(temp.Id), ReceiverLocation(temp.Id));

                return temp;
            }
        }
    }
}