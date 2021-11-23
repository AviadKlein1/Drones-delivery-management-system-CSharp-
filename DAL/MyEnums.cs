namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// defines enums for items' fields - weight, priority, status.
        /// </summary>
        public class MyEnums
        {
            public enum PriorityLevel { regular, quickly, ergent };
            public enum WeightCategory { light, medium, heavy };
            public enum ParcelStatus { requested, scheduled, pickedUp, delivered };

        }
    }
}