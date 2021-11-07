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

            public CustomerInParcel sender { get; set; }
            public CustomerInParcel reciver { get; set; }
            public DroneInParcel drone { get; set; }
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
    }
}
