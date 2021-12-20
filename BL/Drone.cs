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
                Weight = myDrone.weight;
            }

            public int Id { get; set; }
            public string Model { get; set; }
            public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
            public MyEnums.DroneStatus Status { get; set; }
            public int Battery { get; set; }
            public ParcelInDelivery DeliveredParcel { get; set; }
            public Location Location { get; set; }
            public int FirstChargeStationId { get; set; }
            public override string ToString()
            {
                //var delPar = deliverdParcel.id < 100 ? "" : string.Join(", ", deliverdParcel);
                //var del = deliverdParcel.id < 100 ? "" : "deliverd Parcel: ";
                return $"ID: {Id}\nModel: {Model}\nWeight Category: {Weight}\nStatus: {Status}\nBattery: " +
                    $" {Battery}\nLongitude: {Location.Longitude}\nLatitude: {Location.Latitude}\n "/*+$"{del} \n{delPar}\n"*/;
            }
        }

        /// <summary>
        /// entity drone in charge - fields ant ToString function
        /// </summary>
        public class DroneInCharge
        {
            public int Id { get; set; }
            public int Battery { get; set; }
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

            }

            public int Id { get; set; }
            public int Battery { get; set; }
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
            public int Battery { get; set; }
            public Location Location { get; set; }
            public int DeliveredParcelId { get; set; }
            public override string ToString()
            {
                return $"ID: { Id }\nModel: { Model }\nWeight Category: { Weight }\nStatus: { Status }\nBattery: " +
                   $" { Battery} \n{Location}" +
                   $"\nCurrent parcel's id: { DeliveredParcelId }";
            }
        }
    }
}