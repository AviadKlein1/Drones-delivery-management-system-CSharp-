using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// display item individually
        /// </summary>
        public partial class BL
        {
            //display station
            public Station DisplayStation(int stationId)
            {
                IDAL.DO.Station temp = new IDAL.DO.Station();
                try
                {
                    temp = dal.GetStation(stationId);
                }
                catch (IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //return new Station(temp);
                Station retTemp = new Station(temp);

                var dronesInCharge = new List<DroneInCharge>();
                foreach (var element in dronesList)
                {
                    if (element.status == MyEnums.DroneStatus.maintenance && element.location == retTemp.Location)
                    {
                        DroneInCharge droneTemp = new DroneInCharge();
                        droneTemp.id = element.id;
                        droneTemp.battery = element.battery;
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
                    if (element.id == droneId)
                    {
                        exist = true;
                        retDrone.id = element.id;
                        retDrone.location = element.location;
                        retDrone.weight = element.weight;
                        retDrone.status = element.status;
                        retDrone.model = element.model;
                        retDrone.battery = element.battery;
                        retDrone.location = element.location;
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
                catch (IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Customer retTemp = new Customer(temp);
                // maybe duiplay parcels at this customer? 
                var parcelsList = dal.GetParcels();
                foreach (var item in parcelsList)
                {
                    ParcelAtCustomer myParcel = new ParcelAtCustomer();
                    DateTime empty = new DateTime();
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
                catch (IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Parcel retTemp = new Parcel(temp);
                return retTemp;
            }
        }
    }
}