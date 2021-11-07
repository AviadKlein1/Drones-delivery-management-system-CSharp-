using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerToList : Customer
        {
            public int parcelsSendAndDeliverd;
            public int parcelsSendAndNotDeliverd;
            public int parcelsRecived;
            public int parcelsInTheWayToMe;

            public override string ToString()
            {
                base.ToString();
                return "parcelsSendAndDeliverd: " + parcelsSendAndDeliverd +
                    "\nparcelsSendAndNotDeliverd: " + parcelsSendAndNotDeliverd +
                    "\nparcelsRecived: " + parcelsRecived;
            }
        }
    }
}
