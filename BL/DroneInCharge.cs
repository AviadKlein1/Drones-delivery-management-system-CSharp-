using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneInCharge : Drone
        {
            public DroneInCharge(IDAL.DO.Drone temp)
            {
                base.id = temp.id;
                base.battery = temp.battery;
                base.chargeStationId = temp.chargeStationId;
                base.location = new Location(temp.location);
                base.model = temp.model;
                base.status = temp.status;
            }
        }
    }
}
