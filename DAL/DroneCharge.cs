namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// entity drone charge - fields and ToString function
        /// </summary>
        public struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public override string ToString()
            {
                return "Drone ID: " + DroneId + "\nsStation ID: " + StationId + "\n";
            }
        }
    }
}