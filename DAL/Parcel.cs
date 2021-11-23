using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// entity parcel - fields
        /// </summary>
        public struct Parcel
        {
            public int id { get; set; }
            public int senderId { get; set; }
            public int reciverId { get; set; }
            public int droneId { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }
        }
    }
}