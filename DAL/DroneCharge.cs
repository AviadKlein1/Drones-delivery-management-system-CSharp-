namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// entity drone charge - fields and ToString function
        /// </summary>
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