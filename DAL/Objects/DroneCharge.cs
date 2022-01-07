using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using DalApi.DO;

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
            public DroneCharge(int myDroneId, int myStationId, DateTime time)
            {
                DroneId = myDroneId;
                StationId = myStationId;
                StartChargeTime = time;
                IsActive = true;
            }
            public DroneCharge(bool flag)
            {
                DroneId = 0;
                StationId = 0;
                StartChargeTime = DateTime.MinValue;
                IsActive = flag;
            }

            /// <summary>
            /// is deleted
            /// </summary>
            public bool IsActive { get; set; }
            
            public DateTime StartChargeTime { get; set; }
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