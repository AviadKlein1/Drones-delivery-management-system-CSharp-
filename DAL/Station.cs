namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// entity station - fields
        /// </summary>
        public struct Station
        {
            public int id { get; set; }
            public string name { get; set; }
            public Location location { get; set; }
            public int numOfChargeSlots { get; set; }
            public int numOfAvailableChargeSlots { get; set; }

        }
    }
}