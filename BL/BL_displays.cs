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
                return retTemp;
            }
            public Drone droneDisplay(int droneId)
            {
                IDAL.DO.Drone temp = new IDAL.DO.Drone();
                try
                {
                    temp = dal.getDrone(droneId);
                }
                catch(IDAL.DO.WrongIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Drone retTemp = new Drone(temp);
                return retTemp;
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