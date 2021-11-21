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
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DateTime requested { get; set; }
            public int droneId { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

        }
    }
}