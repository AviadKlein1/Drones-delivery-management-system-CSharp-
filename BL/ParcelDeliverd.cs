using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelDeliverd : Parcel
        {
            public bool parcelStatus{ get; set; }
            public Location pickUpLocation { get; set; }
            public Location targetLocation { get; set; }
            public double destination { get; set; }



        }
    }
}
