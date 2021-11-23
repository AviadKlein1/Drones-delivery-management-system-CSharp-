using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// entity parcel - fields, constructors and ToString function
        /// </summary>
        public class Parcel
        {
            public int id { get; set; }
            public CustomerInParcel sender { get; set; }
            public CustomerInParcel reciever { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DroneInParcel DroneInParcel { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

            /// <summary>
            /// default constructor
            /// </summary>
            public Parcel()
            {
                sender = new CustomerInParcel();
                reciever = new CustomerInParcel();
                DroneInParcel = new DroneInParcel();
                requested = DateTime.Now;
                scheduled = new DateTime();
                pickedUp = new DateTime();
                delivered = new DateTime();
                DroneInParcel = null;
            }

            /// <summary>
            /// copy constructor
            /// </summary>
            /// <param name="temp"></param>
            public Parcel(IDAL.DO.Parcel temp)
            {
                id = temp.id;
                sender = new CustomerInParcel();
                reciever = new CustomerInParcel();
                DroneInParcel = new DroneInParcel();
                weight = temp.weight;
                priority = temp.priority;
                requested = temp.requested;
                scheduled = temp.scheduled;
                pickedUp = temp.pickedUp;
                delivered = temp.delivered;
            }
            public override string ToString()
            {
                //\n sender: {sender}\n reciever: {reciever}\n drone: {DroneInParcel}
                return $"ID: {id}\n" +
                    $"Weight Category: {weight}\nPriority: {priority}\nrequested: {requested}\n" +
                    $"scheduled: {scheduled}\npicked up: {pickedUp}\ndelivered: {delivered}\n";
            }
        }

        /// <summary>
        /// entity parcel at costumer - fields, ctors and ToString function
        /// </summary>
        public class ParcelAtCustomer
        {
            public int id { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public IDAL.DO.MyEnums.ParcelStatus parcelStatus { get; set; }
            public CustomerInParcel theSecondSide { get; set; }

            /// <summary>
            /// default ctor
            /// </summary>
            public ParcelAtCustomer()
            {
                theSecondSide = new CustomerInParcel();
            }

            public override string ToString()
            {
                return $"ID: {id}\n the custoner in the second side: {theSecondSide}\n" +
                    $" Weight Category: {weight}\n Priority: {priority}\n Parcel Status: {parcelStatus}";
            }
        }
        public class ParcelInDelivery
        {
            public int id { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public bool boolParcelStatus { get; set; }
            public CustomerInParcel sender { get; set; }
            public CustomerInParcel reciever { get; set; }
            public Location pickUpLocation { get; set; }
            public Location targetLocation { get; set; }
            public double distance { get; set; }

            public override string ToString()
            {
                return $"ID: {id}\n sender: {sender}\n reciever: {reciever}\n bool Parcel Status: {boolParcelStatus}" +
                    $" Weight Category: {weight}\n Priority: {priority}\n distance: {distance}\n" +
                    $" pick Up Location: {pickUpLocation}\n target Location: {targetLocation}\n";
            }
        }
        public class ParcelToList
        {
            public int id { get; set; }
            public string senderName { get; set; }
            public string recieverName { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public IDAL.DO.MyEnums.ParcelStatus parcelStatus { get; set; }

            public override string ToString()
            {
                return $"ID: {id}\n sender name: {senderName}\n reciever name: {recieverName}\n" +
                    $" Weight Category: {weight}\n Priority: {priority}\n Parcel Status: {parcelStatus}";
            }
        }
    }
}