using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        //parcel's fields
        public struct Parcel
        {
            public int id { get; set; }
            public int senderId { get; set; }
            public int targetId { get; set; }
            public int droneId { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

            /// <summary>
            /// prints an item's details
            /// </summary>
            //public override string ToString()
            //{
            //    return "ID: " + id + "\nsender ID: " + senderId + "\ntarget ID: " + targetId + "\ndrone ID: " +
            //        droneId + "\nWeight Category: " + weight + "\nPriority: " + priority + "\nrequested" + 
            //        requested + "\nscheduled: " + scheduled + "\npicked up: " + pickedUp + "\ndelivered: " +
            //        delivered + '\n';
            //}
        }
        public struct ParcelAtCustomer 
        {
            public int id { get; set; }
            public int senderId { get; set; }
            public int targetId { get; set; }
            public int droneId { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
            public MyEnums.ParcelStatus parcelStatus;
            public Customer theSecondSide;
        }
        public struct ParcelDeliverd 
        {
            public int id { get; set; }
            public int senderId { get; set; }
            public int targetId { get; set; }
            public int droneId { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
            public bool boolParcelStatus { get; set; }
            public Location pickUpLocation { get; set; }
            public Location targetLocation { get; set; }
            public double destination { get; set; }
        }
        public struct ParcelToList 
        {
            public int id { get; set; }
            public int senderId { get; set; }
            public int targetId { get; set; }
            public int droneId { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
            public string senderName { get; set; }

            public MyEnums.ParcelStatus parcelStatus;
            public bool boolParcelStatus { get; set; }
            public Location pickUpLocation { get; set; }
            public Location targetLocation { get; set; }
            public double destination { get; set; }

        }
    }
}