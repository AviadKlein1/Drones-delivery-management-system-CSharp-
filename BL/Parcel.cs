using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        //parcel's fields
        public class Parcel
        {
            public int id { get; set; }
            //public int droneId { get; set; }
            //public int senderId { get; set; }
            //public int targetId { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

            public Customer sender;
            public Customer reciver;
            public DroneInParcel drone = new DroneInParcel();

            public Parcel() { }
            public Parcel(IDAL.DO.Parcel temp) 
            {
                id = temp.id;
                weight = temp.weight;
                priority = temp.priority;
                sender.id = temp.senderId;
                reciver.id = temp.targetId;
                drone.id = temp.droneId;
            }

            /// <summary>
            /// prints an item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nsender ID: " + sender.id + "\ntarget ID: " + reciver.id + "\ndrone ID: " +
                    drone.id + "\nWeight Category: " + weight + "\nPriority: " + priority + "\nrequested" +
                    requested + "\nscheduled: " + scheduled + "\npicked up: " + pickedUp + "\ndelivered: " +
                    delivered + '\n';
            }
        }
        public class ParcelAtCustomer : Parcel
        {
            public IDAL.DO.MyEnums.ParcelStatus parcelStatus;
            public Customer theSecondSide;

            public ParcelAtCustomer(IDAL.DO.ParcelAtCustomer parcel)
            {
                id = parcel.id;
                sender.id = parcel.senderId;
                reciver.id = parcel.targetId;
                drone.id = parcel.droneId;
                weight = parcel.weight;
                priority = parcel.priority;
                requested = parcel.requested;
                scheduled = parcel.scheduled;
                pickedUp = parcel.pickedUp;
                delivered = parcel.delivered;
                parcelStatus = parcel.parcelStatus;
                theSecondSide = new Customer(parcel.theSecondSide);
            }
        }
        public class deliverdParcel : Parcel
        {
            public deliverdParcel(IDAL.DO.ParcelDeliverd temp)
            {
                id = temp.id;
                weight = temp.weight;
                priority = temp.priority;
                sender.id = temp.senderId;
                reciver.id = temp.targetId;
                drone.id = temp.droneId;
                boolParcelStatus = temp.boolParcelStatus;
                pickUpLocation = new Location(temp.pickUpLocation);
                targetLocation = new Location(temp.targetLocation);
                destination = temp.destination;
            }

            public bool boolParcelStatus { get; set; }
            public Location pickUpLocation { get; set; }
            public Location targetLocation { get; set; }
            public double destination { get; set; }

        }
        public class ParcelToList : Parcel
        {

            public string senderName { get; set; }

            public MyEnums.ParcelStatus parcelStatus;
            public ParcelToList()
            {
                senderName = base.sender.name;
            }
        }
    }
}
