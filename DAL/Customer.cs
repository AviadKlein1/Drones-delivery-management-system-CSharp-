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
            /// customer idenification
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PhoneNumber { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Location Location { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public bool IsActive { get; set; }

        }
    }
}