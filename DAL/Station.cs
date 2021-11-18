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
            //public override string ToString()
            //{
            //    return "ID: " + id + "\nName: " + name + "\nLongitude: " + /*location.*/longitude + "\nLattitude: " +
            //        /*location.*/lattitude + "\nAvailable Charge Slots: " + numOfAvailableChargeSlots + "\n";
            //}
        }
        //public struct StationToList
        //{
        //    public int id { get; set; }
        //    public string name { get; set; }
        //    public int numOfAvailableChargeSlots { get; set; }
        //    public int numOfOccupiedChargeSlots { get; set; }
        //}
    }
}