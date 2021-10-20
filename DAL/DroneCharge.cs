using System;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int droneId { get; set; }
            public int stationId { get; set; }
            public override string ToString()
            {
                return "Drone ID: " + droneId + "\nsStation ID: " + stationId + "\n";
            }
        }
    }
}