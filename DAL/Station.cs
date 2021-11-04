using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IDAL
{
    namespace DO
    {
        //station's fields
        public struct Station
        {
            public int id { get; set; }
            public string name { get; set; }
            public double longitude { get; set; }
            public double lattitude { get; set; }
            public int numOfChargeSlots { get; set; }
            public int numOfAvailableChargeSlots { get; set; }

            public Location location;

            public List<Station> DronesInCharge;
            /// <summary>
            /// prints an item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nLongitude: " + longitude + "\nLattitude: " +
                    lattitude + "\nCharge Slots: " + numOfChargeSlots + "\n";
            }
        }
    }
}    