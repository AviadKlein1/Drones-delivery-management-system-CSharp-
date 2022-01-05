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
            /// level of urgency of delivery
            /// </summary>
            public enum PriorityLevel { regular, quickly, urgent };

            /// <summary>
            /// categories for parcel's weight and drone's max cargo weight
            /// </summary>
            public enum WeightCategory { light, medium, heavy };

            /// <summary>
            /// parcelws status: requested - delivery ordered by sender
            ///                  scheduled - parcel associated to a specific drone
            ///                  pickedup - parcel collected by drone
            ///                  delivered - parcel dropped off at receiver's location
            /// </summary>
            public enum ParcelStatus { requested, scheduled, pickedUp, delivered };
        }
    }
}