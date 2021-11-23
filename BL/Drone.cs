namespace IBL
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
            public Drone(IDAL.DO.Drone myDrone)
            {
                id = myDrone.id;
                model = myDrone.model;
                weight = myDrone.weight;
            }

            public int id { get; set; }
            public string model { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public MyEnums.DroneStatus status { get; set; }
            public int battery { get; set; }
            public ParcelInDelivery deliveredParcel { get; set; }
            public Location location { get; set; }
            public int firstChargeStationId { get; set; }
            public override string ToString()
            {
                //var delPar = deliverdParcel.id < 100 ? "" : string.Join(", ", deliverdParcel);
                //var del = deliverdParcel.id < 100 ? "" : "deliverd Parcel: ";
                return $"ID: {id}\nModel: {model}\nWeight Category: {weight}\nStatus: {status}\nBattery: " +
                    $" {battery}\nLongitude: {location.longitude}\nLattitude: {location.lattitude}\n "/*+$"{del} \n{delPar}\n"*/;
            }
        }

        /// <summary>
        /// entity drone in charge - fields ant ToString function
        /// </summary>
        public class DroneInCharge
        {
            public int id { get; set; }
            public int battery { get; set; }
            public override string ToString()
            {
                return $"ID: {id}\n battery: {battery}\n";
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

            public int id { get; set; }
            public int battery { get; set; }
            public Location location { get; set; }
            public override string ToString()
            {
                return $"ID: { id }\n battery: { battery }\n location: { location }";
            }
        }

        /// <summary>
        /// entity drone to list - fields and ToString function
        /// </summary>
        public class DroneToList
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
                return $"ID: { id }\nModel: { model }\nWeight Category: { weight }\nStatus: { status }\nBattery: " +
                   $" { battery} \nLongitude: { location.longitude }\nLattitude: " +
                   $"{ location.lattitude }\nDeliverd parcel id: { deliveredParcelId }";
            }
        }
    }
}