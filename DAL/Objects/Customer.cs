namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity customer - fields
        /// </summary>
        public struct Customer
        {
            /// <summary>
            /// customer's identification (9 figuers)
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// customer's name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// customer's phine number: "05" + "0/2/4/8" + 7 figuers
            /// </summary>
            public string PhoneNumber { get; set; }

            /// <summary>
            /// customer's location - longitude and latitude
            /// </summary>
            public Location Location { get; set; }

            /// <summary>
            /// is deleted
            /// </summary>
            public bool IsActive { get; set; }
        }
    }
}