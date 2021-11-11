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
                temp = dal.getStation(stationId);
                Station retTemp = new Station(temp);
                return retTemp;
            }
            public Drone droneDisplay(int droneId)
            {
                IDAL.DO.Drone temp = new IDAL.DO.Drone();
                temp = dal.getDrone(droneId);
                Drone retTemp = new Drone(temp);
                return retTemp;
            }
            public Customer customerDisplay(int customerId)
            {
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                temp = dal.getCustomer(customerId);
                Customer retTemp = new Customer(temp);
                return retTemp;
            }
            public Parcel parcelDisplay(int parcelId)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();
                temp = dal.getParcel(parcelId);
                Parcel retTemp = new Parcel(temp);
                return retTemp;
            }

        }
    }
}



