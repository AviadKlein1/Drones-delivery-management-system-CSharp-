namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity station - fields
        /// </summary>
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int NumOfChargeSlots { get; set; }
            public int NumOfAvailableChargeSlots { get; set; }
            //public bool IsActive { get; set; }
        }
    }
}