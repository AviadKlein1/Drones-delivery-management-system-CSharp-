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
            public int id { get; set; }
            public string model { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public MyEnums.DroneStatus status { get; set; }
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
            public MyEnums.DroneStatus status { get; set; }
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
