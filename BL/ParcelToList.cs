using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
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
