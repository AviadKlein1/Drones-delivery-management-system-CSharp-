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
                deliverdParcel = new ParcelInDelivery(temp.deliverdParcel);
                location = new Location(0, temp.location.lattitude);
                location.longitude = temp.location.longitude;
            }

            public int id { get; set; }
            public string model { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.DroneStatus status { get; set; }
            public int battery { get; set; }
            public ParcelInDelivery deliverdParcel { get; set; }
            public Location location { get; set; }
            public int firstChargeStationId { get; set; }
            public override string ToString()
            {
                var delPar = deliverdParcel.id < 100 ? "" : string.Join(", ", deliverdParcel);
                var del = deliverdParcel.id < 100 ? "" : "deliverd Parcel: ";
                return $"ID: {id}\nModel: {model}\nWeight Category: {weight}\nStatus: {status}\nBattery: " +
                    $" {battery}\nLongitude: {location.longitude}\nLattitude: {location.lattitude}\n " +
                    $"{del} \n{delPar}\n";
            }
        }
        public class DroneInCharge
        {
            public DroneInCharge(IDAL.DO.DroneInCharge droneInCharge)
            {
                id = droneInCharge.id;
                battery = droneInCharge.battery;
            }

            public int id { get; set; }
            public int battery { get; set; }
            public override string ToString()
            {
                return $"ID: {id}\n battery: {battery}\n";
            }
        }
        public class DroneInParcel
        {
            public DroneInParcel () { }
            public DroneInParcel(IDAL.DO.DroneInParcel droneInParcel)
            {
                this.id = droneInParcel.id;
                this.battery = droneInParcel.battery;
                this.location = new Location (droneInParcel.location);
            }

            public int id { get; set; }
            public int battery { get; set; }
            public Location location { get; set; }
            public override string ToString()
            {
                return $"ID: {id}\n battery: {battery}\n location: {location} ";
            }

        }
        public class DronesToList
        {
            public int id { get; set; }
            public string model { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.DroneStatus status { get; set; }
            public int battery { get; set; }
            public Location location { get; set; }
            public int deliveredParcelId { get; set; }
            public override string ToString()
            {
                return "ID: " + id + "\nModel: " + model + "\nWeight Category: " + weight + "\nStatus: " +
                    status + "\nBattery: " + battery + "\nLongitude: " + location.longitude +
                    "\nLattitude: " + location.lattitude +"\ndeliverd Parcel id: " + deliveredParcelId + "\n";
            }

        }
    }
}
