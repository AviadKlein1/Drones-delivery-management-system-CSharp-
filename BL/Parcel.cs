using System;

namespace IBL
{
    namespace BO
    {
        //parcel's fields
        public class Parcel
        {
            public int id { get; set; }
            public int senderId { get; set; }
            public int targetId { get; set; }
            public MyEnums.WeightCategory weight { get; set; }
            public MyEnums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public int droneId { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

            /// <summary>
            /// prints an item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nsender ID: " + senderId + "\ntarget ID: " + targetId + "\ndrone ID: " +
                    droneId + "\nWeight Category: " + weight + "\nPriority: " + priority + "\nrequested" +
                    requested + "\nscheduled: " + scheduled + "\npicked up: " + pickedUp + "\ndelivered: " +
                    delivered + '\n';
            }
        }
    }
}
