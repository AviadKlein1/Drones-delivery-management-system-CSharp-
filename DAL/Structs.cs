using System;

namespace DAL
{
    namespace DO
    {
        public class enums
        {
            public enum PriorityLevel { regular, quickly, ergent };
            public enum WeightCategory { light, medium, heavy };
            public enum DroneStatus { available, maintenance, delivery };
        }
        public struct Station
        {
            public int id { get; set; }
            public string name { get; set; }
            public double longitude { get; set; }
            public double lattitude { get; set; }
            public int numOfChargeSlots { get; set; }
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nLongitude: " + longitude + "\nLattitude " +
                    lattitude + "\nCharge Slots " + numOfChargeSlots + "\n";
            }
        }

        public struct Drone
        {
            public int id { get; set; }
            public string model { get; set; }
            public enums.WeightCategory weight { get; set; }
            public enums.DroneStatus status { get; set; }
            public int battery { get; set; }
            public override string ToString()
            {
                return "ID: " + id + "\nModel: " + model + "\nWeight Category: " + weight +  "\nStatus: " + 
                    status + "\nBattery: " + battery + "\n";
            }
        }

        public struct Customer
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phoneNumber { get; set; }
            public double longitude { get; set; }
            public double lattitude { get; set; }
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nPhone: " + phoneNumber + "\nLongitude: " +
                    longitude + "\nLattitude: " + lattitude + "\n";
            }
        }

        public struct DroneCharge
        {
            public int droneId { get; set; }
            public int stationId { get; set; }
            public override string ToString()
            {
                return "Drone ID: " + droneId + "\nsStation ID: " + stationId + "\n";
            }
        }

        public struct Parcel
        {
            public int id { get; set; }
            public int senderId { get; set; }
            public int targetId { get; set; }
            public enums.WeightCategory weight { get; set; }
            public enums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public int droneId { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
            public override string ToString()
            {
                return  "ID: " + id + "\nsender ID: " + senderId + "\ntarget ID: " + targetId + "\nWeight Category: " +
                     weight + "\nrequested: " + "\nPriority: " + priority + "\nrequested" + requested + "\ndrone ID: " +
                     droneId + "\nscheduled: " + scheduled + "\npicked up: " + pickedUp + "\ndelivered: " +
                     delivered + '\n';
        }
    }
}
namespace DalObject
{

}