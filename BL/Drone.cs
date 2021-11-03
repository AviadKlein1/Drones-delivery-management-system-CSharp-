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
        public struct Drone
        {
            public int id { get; set; }
            public string model { get; set; }
            public IBL.BO.MyEnums.WeightCategory weight { get; set; }
            public IBL.BO.MyEnums.DroneStatus status { get; set; }
            public int battery { get; set; }

            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nModel: " + model + "\nWeight Category: " + weight + "\nStatus: " + 
                    status + "\nBattery: " + battery + "\n";
            }
        }
    }
}
