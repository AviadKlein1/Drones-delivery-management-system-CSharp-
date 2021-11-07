using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelAtCustomer : Parcel
        {
            public MyEnums.ParcelStatus parcelStatus;
            public CustomerInParcel theSecondSide;
        }


    }

}
