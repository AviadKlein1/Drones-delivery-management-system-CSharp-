namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity customer = fields
        /// </summary>
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public Location Location { get; set; }
        }
    }
}