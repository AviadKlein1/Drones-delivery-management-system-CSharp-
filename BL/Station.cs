using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        //station's fields
        public class Station
        {
            public Station(){ }

            public Station(IDAL.DO.Station temp)
            {
                id = temp.id;
                name = temp.name;

                foreach (IDAL.DO.Drone element in temp.DronesInCharge)
                {
                    DroneInCharge droneIn = new DroneInCharge(element);
                    dronesInCharge.Add(droneIn);
                }

                numOfChargeSlots = temp.numOfChargeSlots;
                numOfAvailableChargeSlots = temp.numOfAvailableChargeSlots;
                location.lattitude = temp.lattitude;
                location.longitude = temp.longitude;
            }
            public int id { get; set; }
            public string name { get; set; }
            //public double longitude { get; set; }
            //public double lattitude { get; set; }
            public int numOfChargeSlots { get; set; }
            public int numOfAvailableChargeSlots { get; set; }
            public List<DroneInCharge> dronesInCharge = new List<DroneInCharge>();
            public Location location = new Location();
            /// <summary>
            /// prints an item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nLongitude: " + location.longitude + "\nLattitude: " +
                    location.lattitude + "\nCharge Slots: " + numOfChargeSlots + "\n";
            }
        }
    }
}

