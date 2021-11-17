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
            public CustomerInParcel sender { get; set; }
            public CustomerInParcel reciever { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DroneInParcel DroneInParcel { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
          
        }
        public struct ParcelAtCustomer 
        {
            public int id { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public MyEnums.ParcelStatus parcelStatus { get; set; }
            public CustomerInParcel theSecondSide { get; set; }
        }
        public struct ParcelInDelivery 
        {
            public int id { get; set; }
            public MyEnums.WeightCategory weight { get; set; }
            public MyEnums.PriorityLevel priority { get; set; }
            public bool boolParcelStatus { get; set; }
            public CustomerInParcel sender { get; set; }
            public CustomerInParcel reciever { get; set; }
            public Location pickUpLocation { get; set; }
            public Location targetLocation { get; set; }
            public double distance { get; set; }
        }
        public struct ParcelToList 
        {
            public int id { get; set; }
            public string senderName { get; set; }
            public string recieverName { get; set; }
            public MyEnums.WeightCategory weight { get; set; }
            public MyEnums.PriorityLevel priority { get; set; }
            public MyEnums.ParcelStatus parcelStatus{ get; set; }
        }
    }
}