using System;

namespace IBL
{
    namespace BO
    {
        //item's sields 
        public class DroneCharge
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