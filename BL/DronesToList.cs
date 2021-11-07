using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DronesToList : Drone
        {
            public DronesToList()
            {
                this.deliverdParcelId = base.deliverdParcel.id;
            }
            public int deliverdParcelId { get; set;}
        }
    }
}
