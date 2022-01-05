namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity drone - fields
        /// </summary>
        public struct Drone
        {
            /// <summary>
            /// drone's identification
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// drone's model
            /// </summary>
            public string Model { get; set; }

            /// <summary>
            /// drone's weight
            /// </summary>
            public MyEnums.WeightCategory Weight { get; set; }

            /// <summary>
            /// is deleted
            /// </summary>
            public bool IsActive { get; set; }
        }
    }
}