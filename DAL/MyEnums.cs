using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// defines enums for items' fields - weight, priority.
        /// </summary>
        public class MyEnums
        {
            public enum PriorityLevel { regular, quickly, ergent };
            public enum WeightCategory { lite, medium, heavy };
            public enum DroneStatus { available, maintenance, delivery };
            public enum ParcelStatus { requested, scheduled, pickedUp, delivered };

        }
    }
}