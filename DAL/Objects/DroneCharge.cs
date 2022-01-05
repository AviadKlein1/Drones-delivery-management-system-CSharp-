namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity drone charge - fields and ToString function
        /// </summary>
        public struct DroneCharge
        {
            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="myDroneId"></param>
            /// <param name="myStationId"></param>
            public DroneCharge(int myDroneId, int myStationId)
            {
                DroneId = myDroneId;
                StationId = myStationId;
                IsActive = true;
            }

            /// <summary>
            /// is deleted
            /// </summary>
            public bool IsActive { get; set; }

            /// <summary>
            /// drone's identification
            /// </summary>
            public int DroneId { get; set; }
            
            /// <summary>
            /// host station's identification
            /// </summary>
            public int StationId { get; set; }

            /// <summary>
            /// to string function
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "Drone ID: " + DroneId + "\nsStation ID: " + StationId + "\n";
            }
        }
    }
}