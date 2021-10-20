using System;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int id { get; set; }
            public string name { get; set; }
            public double longitude { get; set; }
            public double lattitude { get; set; }
            public int numOfChargeSlots { get; set; }
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nLongitude: " + longitude + "\nLattitude " +
                    lattitude + "\nCharge Slots " + numOfChargeSlots + "\n";
            }
        }
    }

}    