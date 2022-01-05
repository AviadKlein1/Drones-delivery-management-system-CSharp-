using System;

namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity parcel - fields
        /// </summary>
        public struct Parcel
        {
            /// <summary>
            /// parcel's identification
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// sender's identification
            /// </summary>
            public int SenderId { get; set; }

            /// <summary>
            /// receiver's identification
            /// </summary>
            public int ReceiverId { get; set; }

            /// <summary>
            /// host drone's identification
            /// </summary>
            public int DroneId { get; set; }

            /// <summary>
            /// parcel's weight (categories: heavy, medium, light)
            /// </summary>
            public MyEnums.WeightCategory Weight { get; set; }

            /// <summary>
            /// level of delivery urgency (categories: regular, urgent, quickly)
            /// </summary>
            public MyEnums.PriorityLevel Priority { get; set; }

            /// <summary>
            /// time of delivery order
            /// </summary>
            public DateTime? Requested { get; set; }

            /// <summary>
            /// time of parcel association to drone
            /// </summary>
            public DateTime? Scheduled { get; set; }

            /// <summary>
            /// time of picking up parcel from sender
            /// </summary>
            public DateTime? PickedUp { get; set; }

            /// <summary>
            /// time of dropping by parcel at receiver's
            /// </summary>
            public DateTime? Delivered { get; set; }

            /// <summary>
            /// is deleted
            /// </summary>
            public bool IsActive { get; set; }
        }
    }
}