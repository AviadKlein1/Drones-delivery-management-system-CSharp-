namespace IDAL
{
    namespace DO
    {
        //station's fields
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