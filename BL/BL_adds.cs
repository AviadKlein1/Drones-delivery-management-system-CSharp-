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
                temp.name = myStation.name;
                temp.numOfAvailableChargeSlots = myStation.numOfAvailableChargeSlots;
                temp.numOfChargeSlots = myStation.numOfChargeSlots;
                temp.DronesInCharge = new List<IDAL.DO.Drone>();
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
                temp.chargeStationId = myDrone.chargeStationId;
                temp.battery =  rd.Next(20, 41);
                temp.status = IDAL.DO.MyEnums.DroneStatus.maintenance;
                temp.location = dal.stationLocate(temp.chargeStationId);


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
                temp.droneId = myParcel.drone.id;
                temp.senderId = myParcel.sender.id;
                temp.targetId = myParcel.reciver.id;

                temp.priority = myParcel.priority;
                temp.weight = myParcel.weight;


                temp.scheduled = DateTime.Now;
                temp.requested = new DateTime();
                temp.pickedUp = new DateTime();
                temp.delivered = new DateTime();
                Console.WriteLine("your parcel ID is: " + temp.id + "\n");
                //checks


                dal.addParcel(temp);
            }
        }
    }
}