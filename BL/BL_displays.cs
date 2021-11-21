using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                catch(IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Station retTemp = new Station(temp);
                var dalDrones = dronesList;
                var dronesInCharge = new List<DroneInCharge>();
                foreach(var element in dronesList)
                {
                    if (element.status == MyEnums.DroneStatus.maintenance && element.location == retTemp.location)
                    {
                        DroneInCharge droneTemp = new DroneInCharge();
                        droneTemp.id = element.id;
                        droneTemp.battery = element.battery;
                        dronesInCharge.Add(droneTemp);
                    }
                }
                retTemp.dronesInCharge = dronesInCharge;
                return retTemp;
            }
            public Drone droneDisplay(int droneId)
            {
                Drone retDrone = new Drone();
                foreach(var element in dronesList)
                {
                    if(element.id == droneId)
                    {
                        retDrone.id = element.id;
                        retDrone.location = element.location;
                        retDrone.weight = element.weight;
                        retDrone.status = element.status;
                        retDrone.model = element.model;
                        retDrone.battery = element.battery;
                        var dalParcels = dal.getParcels();
                        foreach(var pElement in dalParcels)
                        {
                            if (pElement.id == element.deliveredParcelId)
                            {
                                retDrone.deliverdParcel.id = pElement.id;
                                retDrone.deliverdParcel.boolParcelStatus =
                            }
                        }
                        retDrone.deliverdParcel = element.deliveredParcelId;
                        retDrone.firstChargeStationId = element.
                    }

                }
                

            }
            public Customer customerDisplay(int customerId)
            {
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                try
                {
                    temp = dal.getCustomer(customerId);
                }
                catch(IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Customer retTemp = new Customer(temp);

                return retTemp;
            }
            public Parcel parcelDisplay(int parcelId)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();
                try
                {
                    temp = dal.getParcel(parcelId);
                }
                catch(IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Parcel retTemp = new Parcel(temp);
                return retTemp;
            }
        }
    }
}