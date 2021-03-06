using System;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// class drone - fields, constructors and ToString function
        /// </summary>
        public class Drone
        {
            /// <summary>
            /// default constructor
            /// </summary>
            public Drone()
            {

            }

            /// <summary>
            /// params constructor - initialize fields
            /// </summary>
            /// <param name="myDrone"></param>
            public Drone(DalApi.DO.Drone myDrone)
            {
                Id = myDrone.Id;
                Model = myDrone.Model;
                Weight = myDrone.Weight;
            }

            public int Id { get; set; }
            public string Model { get; set; }
            public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
            public MyEnums.DroneStatus Status { get; set; }
            public double Battery { get; set; }
            public ParcelInDelivery DeliveredParcel { get; set; }
            public Location Location { get; set; }
            public int FirstChargeStationId { get; set; }
            public override string ToString()
            {
                return $"ID: {Id}\nModel: {Model}\nWeight Category: {Weight}\nStatus:" +
                    $"{Status}\nBattery: {Battery}\nLocation: {Location}\n";
            }
        }

        /// <summary>
        /// entity drone in charge - fields ant ToString function
        /// </summary>
        public class DroneInCharge
        {
            public DroneInCharge(int Id, double Battery, DateTime time)
            {
                this.Id = Id;
                this.Battery = Battery;
                this.StartChargeTime = time;
            }
            public int Id { get; set; }
            public double Battery { get; set; }
            public DateTime StartChargeTime { get; set; }

            public override string ToString()
            {
                return $"ID: {Id}\n Battery: {Battery}\n";
            }
        }
       

        /// <summary>
        /// entity drone in parcel - constructor and fields
        /// </summary>
        public class DroneInParcel
        {
            /// <summary>
            /// default constructor
            /// </summary>
            public DroneInParcel()
            {
                Id = 0;
                Battery = 0;
                Location = null;
            }
            public DroneInParcel(int Id)
            {
                this.Id = Id;
                Battery = 0;
                Location = null;
            }

            public int Id { get; set; }
            public double Battery { get; set; }
            public Location Location { get; set; }
            public override string ToString()
            {
                return $"ID: { Id }\n Battery: { Battery }\n Location: { Location }";
            }
        }

        /// <summary>
        /// entity drone to list - fields and ToString function
        /// </summary>
        public class DroneToList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
            public MyEnums.DroneStatus Status { get; set; }
            public double Battery { get; set; }
            public Location Location { get; set; }
            public int DeliveredParcelId { get; set; }
            public override string ToString()
            {
                return $"ID: { Id }\nModel: { Model }\nWeight Category: { Weight }\nStatus: { Status }\nBattery: " +
                   $" { (int)Battery} \n{Location}\nCurrent parcel's id: { DeliveredParcelId }";
            }
        }
    }
}