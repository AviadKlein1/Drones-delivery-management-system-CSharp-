namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// defines enums for items' fields - weight, priority, status.
        /// </summary>
        public class MyEnums
        {
            /// <summary>
            /// 
            /// </summary>
            public enum PriorityLevel { regular, quickly, urgent };

            /// <summary>
            /// 
            /// </summary>
            public enum WeightCategory { lite, medium, heavy };

            /// <summary>
            /// 
            /// </summary>
            public enum DroneStatus { available, maintenance, delivery };

            /// <summary>
            /// 
            /// </summary>
            public enum ParcelStatus { requested, scheduled, pickedUp, delivered };
        }
    }
}