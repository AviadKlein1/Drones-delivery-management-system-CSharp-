using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        //item's sields 
        public struct DroneCharge
        {
            public int droneId { get; set; }
            public int stationId { get; set; }

            /// <summary>
            ///prints item's details 
            /// </summary>
            public override string ToString()
            {
                return "Drone ID: " + droneId + "\nsStation ID: " + stationId + "\n";
            }
        }
    }
}