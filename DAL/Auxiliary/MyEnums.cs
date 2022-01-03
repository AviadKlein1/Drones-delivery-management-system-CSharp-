namespace DalApi
{
    namespace DO
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
            public enum WeightCategory { light, medium, heavy };

            /// <summary>
            /// 
            /// </summary>
            public enum ParcelStatus { requested, scheduled, pickedUp, delivered };
        }
    }
}