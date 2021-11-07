using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class StationToList : Station
        {
            public StationToList()
            {
                this.numOfNotAvailableChargeSlots = base.numOfChargeSlots - base.numOfAvailableChargeSlots;
            }
            public int numOfNotAvailableChargeSlots { get; set; }

        }
    }
}
