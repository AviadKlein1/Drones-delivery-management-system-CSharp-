using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        //item's fields
        public class Drone
        {
            public Drone() { }

            public Drone(IDAL.DO.Drone temp)
            {
                id = temp.id;
                model = temp.model;
                status = temp.status;
                weight = temp.weight;
                battery = temp.battery;
                chargeStationId = temp.chargeStationId;
                weight = temp.weight;
                status = temp.status;
                location.lattitude = temp.location.lattitude;
                location.longitude = temp.location.longitude;
            }

            public int id { get; set; }
            public string model { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.DroneStatus status { get; set; }
            public int battery { get; set; }
            public int chargeStationId { get; set; }
            public virtual Location location { get; set; }
            public  ParcelDeliverd deliverdParcel { get; set; }

         
                

            /// <summary>
            /// prints item's details
            /// </summary>
            /// 

            public override string ToString()
            {
                return "ID: " + id + "\nModel: " + model + "\nWeight Category: " + weight + "\nStatus: " +
                    status + "\nBattery: " + battery + "\ncharge Station Id: " + chargeStationId +
                    "\nLongitude: " + location.longitude + "\nLattitude: " + location.lattitude +
                    "\ndeliverdParcel: " + deliverdParcel +"\n";
            }
        }
        public class DroneInCharge : Drone
        {
            public DroneInCharge(IDAL.DO.Drone temp)
            {
                base.id = temp.id;
                base.battery = temp.battery;
                base.chargeStationId = temp.chargeStationId;
                base.location = new Location(temp.location);
                base.model = temp.model;
                base.status = temp.status;
            }
        }
        public class DroneInParcel : Drone
        {
        }
        public class DronesToList : Drone
        {
            public DronesToList()
            {
                this.deliverdParcelId = base.deliverdParcel.id;
            }
            public int deliverdParcelId { get; set; }
        }
    }
}
