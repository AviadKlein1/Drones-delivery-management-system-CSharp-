namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// defines enums for items' fields - weight, priority, status.
        /// </summary>
        public class MyEnums
        {
            public enum PriorityLevel { regular, quickly, urgent };
            public enum WeightCategory { lite, medium, heavy };
            public enum DroneStatus { available, maintenance, delivery };
            public enum ParcelStatus { requested, scheduled, pickedUp, delivered };
        }
    }
}