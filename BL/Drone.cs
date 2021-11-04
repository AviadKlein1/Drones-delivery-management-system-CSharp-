using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        //item's fields
        public class Drone
        {
            public int id { get; set; }
            public string model { get; set; }
            public MyEnums.WeightCategory weight { get; set; }
            public MyEnums.DroneStatus status { get; set; }
            public int battery { get; set; }
            public int chargeStationId { get; set; }
            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nModel: " + model + "\nWeight Category: " + weight + "\nStatus: " + 
                    status + "\nBattery: " + battery + "\ncharge Station Id: " + chargeStationId + "\n";
            }
        }
    }
}
