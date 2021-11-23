namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// entity customer = fields
        /// </summary>
        public struct Customer
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phoneNumber { get; set; }
            public Location location { get; set; }
        }
    }
}