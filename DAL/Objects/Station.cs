namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity station - fields
        /// </summary>
        public struct Station
        {
            /// <summary>
            /// station'd id (3 digits)
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// station's name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// station's location - longitude and latitude
            /// </summary>
            public Location Location { get; set; }

            /// <summary>
            /// number of charging slots at station
            /// </summary>
            public int NumOfChargeSlots { get; set; }

            /// <summary>
            /// number of available charging slots at station
            /// </summary>
            public int NumOfAvailableChargeSlots { get; set; }

            /// <summary>
            /// is deleted
            /// </summary>
            public bool IsActive { get; set; }
        }
    }
}