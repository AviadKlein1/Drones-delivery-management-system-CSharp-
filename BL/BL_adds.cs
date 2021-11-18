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
            public void addStation(Station myStation)
            {
                IDAL.DO.Station temp = new IDAL.DO.Station();

                temp.id = myStation.id;
                temp/*.location*/.lattitude = myStation.location.lattitude;
                temp/*.location*/.longitude = myStation.location.longitude;
                temp.name = myStation.Name;
                temp.numOfAvailableChargeSlots = myStation.numOfAvailableChargeSlots;
                temp.numOfChargeSlots = myStation.numOfChargeSlots;

                temp.DronesInCharge = new List<IDAL.DO.DroneInCharge>();
                //checks
           
                

                //insert station to array
                dal.addStation(temp);
                
            }
            public void addDrone(Drone myDrone)
            {
                IDAL.DO.Drone temp = new IDAL.DO.Drone();
                temp.id = myDrone.id;
                temp.model = myDrone.model;
                temp.weight = myDrone.weight;
                temp.firstChargeStationId = myDrone.firstChargeStationId;
                temp.battery =  rd.Next(20, 41);
                temp.status = IDAL.DO.MyEnums.DroneStatus.maintenance;
                temp.location = dal.stationLocate(temp.firstChargeStationId);


                //checks



                dal.addDrone(temp);
            }
            public void addcustomer(Customer myCustomer)
            {
                IDAL.DO.Customer temp = new IDAL.DO.Customer();
                temp.id = myCustomer.id;
                temp.name = myCustomer.name;
                temp.phoneNumber = myCustomer.phoneNumber;
                temp.longitude = myCustomer.location.longitude;
                temp.lattitude = myCustomer.location.lattitude;



                //checks


                dal.addcustomer(temp);
            }
            public void addParcel(Parcel myParcel)
            {
                IDAL.DO.Parcel temp = new IDAL.DO.Parcel();

                temp.id = dal.ParcelRunId();

                IDAL.DO.CustomerInParcel CusSender = new IDAL.DO.CustomerInParcel();
                CusSender.id = myParcel.sender.id;
                CusSender.name = myParcel.sender.name;
                temp.sender = CusSender;

                IDAL.DO.CustomerInParcel cusReciever = new IDAL.DO.CustomerInParcel();
                cusReciever.id = myParcel.reciever.id;
                cusReciever.name = myParcel.reciever.name;
                temp.reciever = cusReciever;

                temp.priority = myParcel.priority;
                temp.weight = myParcel.weight;

                temp.scheduled = DateTime.Now;
                temp.requested = new DateTime();
                temp.pickedUp = new DateTime();
                temp.delivered = new DateTime();

                IDAL.DO.DroneInParcel droneInParcel = new IDAL.DO.DroneInParcel();
                temp.DroneInParcel = droneInParcel;

                Console.WriteLine("your parcel ID is: " + temp.id + "\n");
                //checks


                dal.addParcel(temp);
            }
        }
    }
}