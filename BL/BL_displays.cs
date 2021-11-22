using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public Station stationDisplay(int stationId)
            {
                IDAL.DO.Station temp = new IDAL.DO.Station();
                try
                {
                    temp = dal.getStation(stationId);
                }
                catch (IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Station retTemp = new Station(temp);

                //var dronesInCharge = new List<DroneInCharge>();
                //foreach (var element in dronesList)
                //{
                //    if (element.status == MyEnums.DroneStatus.maintenance && element.location == retTemp.location)
                //    {
                //        DroneInCharge droneTemp = new DroneInCharge();
                //        droneTemp.id = element.id;
                //        droneTemp.battery = element.battery;
                //        dronesInCharge.Add(droneTemp);
                //    }
                //}
                //retTemp.dronesInCharge = dronesInCharge;
                return retTemp;
            }
            public Drone droneDisplay(int droneId)
            {
                bool isExist = false;
                Drone retDrone = new Drone();
                foreach (var element in dronesList)
                {
                    if (element.id == droneId)
                    {
                        isExist = true;
                        retDrone.id = element.id;
                        retDrone.location = element.location;
                        retDrone.weight = element.weight;
                        retDrone.status = element.status;
                        retDrone.model = element.model;
                        retDrone.battery = element.battery;
                        retDrone.location = element.location;
                    }
                }
                if (isExist == false) throw new WrongIdException(droneId, $"Wrong ID: {droneId}");
                return retDrone;
            }
            public Customer customerDisplay(int customerId)
            {
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                try
                {
                    temp = dal.getCustomer(customerId);
                }
                catch (IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Customer retTemp = new Customer(temp);
                // maybe dusplay parcels at this customer? 
                var parcelsList = dal.getParcels();
                foreach (var item in parcelsList)
                {
                    ParcelAtCustomer myParcel = new ParcelAtCustomer();
                    DateTime empty = new DateTime();
                    if (item.senderId == retTemp.id)
                    {
                        myParcel.id = item.id;
                        myParcel.weight = item.weight;
                        myParcel.priority = item.priority;

                        if (item.requested != empty && item.scheduled == empty) myParcel.parcelStatus = IDAL.DO.MyEnums.ParcelStatus.requested;
                        if (item.scheduled != empty && item.pickedUp == empty) myParcel.parcelStatus = IDAL.DO.MyEnums.ParcelStatus.scheduled;
                        if (item.pickedUp != empty && item.delivered == empty) myParcel.parcelStatus = IDAL.DO.MyEnums.ParcelStatus.pickedUp;
                        if (item.delivered != empty) myParcel.parcelStatus = IDAL.DO.MyEnums.ParcelStatus.delivered;
                       
                        myParcel.theSecondSide = theOtherSide(myParcel.id, retTemp.id);
                        
                    }
                    retTemp.parcelsFromCustomer.Add(myParcel);
                }
                return retTemp;
            }
            public Parcel parcelDisplay(int parcelId)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();
                try
                {
                    temp = dal.getParcel(parcelId);
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