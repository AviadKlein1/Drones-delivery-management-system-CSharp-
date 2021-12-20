using System;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// entity parcel - fields, constructors and ToString function
        /// </summary>
        public class Parcel
        {
            public int id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Reciever { get; set; }
            public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
            public DalApi.DO.MyEnums.PriorityLevel Priority { get; set; }
            public DroneInParcel DroneInParcel { get; set; }
            public DateTime? Requested { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }

            /// <summary>
            /// default constructor
            /// </summary>
            public Parcel()
            {
                Sender = new CustomerInParcel();
                Reciever = new CustomerInParcel();
                DroneInParcel = new DroneInParcel();
                Requested = DateTime.Now;
                Scheduled = null;
                PickedUp = null;
                Delivered = null;
                DroneInParcel = null;
            }

            /// <summary>
            /// copy constructor
            /// </summary>
            /// <param name="temp"></param>
            public Parcel(DalApi.DO.Parcel temp)
            {
                id = temp.Id;
                Sender = new CustomerInParcel();
                Reciever = new CustomerInParcel();
                DroneInParcel = new DroneInParcel();
                Weight = temp.Weight;
                Priority = temp.Priority;
                Requested = temp.Requested;
                Scheduled = temp.Scheduled;
                PickedUp = temp.PickedUp;
                Delivered = temp.Delivered;
            }
            public override string ToString()
            {
                return $"ID: {id}\nWeight Category: {Weight}\nPriority: {Priority}" +
                    $"\nrequested: {Requested}\nscheduled: {Scheduled}" +
                    $"\npicked up: {PickedUp}\ndelivered: {Delivered}\n";
            }
        }

        /// <summary>
        /// entity parcel at costumer - fields, ctors and ToString function
        /// </summary>
        public class ParcelAtCustomer
        {
            public int Id { get; set; }
            public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
            public DalApi.DO.MyEnums.PriorityLevel Priority { get; set; }
            public DalApi.DO.MyEnums.ParcelStatus ParcelStatus { get; set; }
            public CustomerInParcel TheSecondSide { get; set; }

            /// <summary>
            /// default ctor
            /// </summary>
            public ParcelAtCustomer()
            {
                TheSecondSide = new CustomerInParcel();
            }

            public override string ToString()
            {
                return $"ID: {Id}\n the custoner in the second side: {TheSecondSide}\n" +
                    $" Weight Category: {Weight}\n Priority: {Priority}\n Parcel Status: {ParcelStatus}";
            }
        }
        public class ParcelInDelivery
        {
            public int Id { get; set; }
            public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
            public DalApi.DO.MyEnums.PriorityLevel Priority { get; set; }
            public bool BoolParcelStatus { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Reciever { get; set; }
            public Location PickUpLocation { get; set; }
            public Location TargetLocation { get; set; }
            public double Distance { get; set; }

            public override string ToString()
            {
                return $"ID: {Id}\n sender: {Sender}\n reciever: {Reciever}\n bool Parcel Status: {BoolParcelStatus}" +
                    $" Weight Category: {Weight}\n Priority: {Priority}\n distance: {Distance}\n" +
                    $" pick Up Location: {PickUpLocation}\n target Location: {TargetLocation}\n";
            }
        }
        public class ParcelToList
        {
            public int Id { get; set; }
            public string SenderName { get; set; }
            public string RecieverName { get; set; }
            public DalApi.DO.MyEnums.WeightCategory Weight { get; set; }
            public DalApi.DO.MyEnums.PriorityLevel Priority { get; set; }
            public DalApi.DO.MyEnums.ParcelStatus ParcelStatus { get; set; }

            public override string ToString()
            {
                return $"ID: {Id}\n Sender name: {SenderName}\n Receiver name: {RecieverName}\n" +
                    $" Weight Category: {Weight}\n Priority: {Priority}\n Parcel Status: {ParcelStatus}";
            }
        }
    }
}