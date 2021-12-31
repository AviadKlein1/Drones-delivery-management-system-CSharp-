namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity drone charge - fields and ToString function
        /// </summary>
        public struct DroneCharge
        {
            public DroneCharge(int droneId, int stationId)
            {
                this.DroneId = droneId;
                this.StationId = stationId;
                IsActive = true;
        }
            /// <summary>
            /// 
            /// </summary>
            public bool IsActive { get; set; }
            public int DroneId { get; set; }
            
            /// <summary>
            /// 
            /// </summary>
            public int StationId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "Drone ID: " + DroneId + "\nsStation ID: " + StationId + "\n";
            }
        }
    }
}