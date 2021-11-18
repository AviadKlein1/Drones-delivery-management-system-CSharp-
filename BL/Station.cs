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
                Name = temp.name;
                foreach (IDAL.DO.DroneInCharge element in temp.DronesInCharge)
                {
                    DroneInCharge droneIn = new DroneInCharge(element);
                    dronesInCharge.Add(droneIn);
                }
                numOfAvailableChargeSlots = temp.numOfAvailableChargeSlots;
                location = new Location(0,temp.lattitude);
                location.longitude = temp.longitude;
            }
            public int id { get; set; }
            public string Name { get; set; }
            public int numOfAvailableChargeSlots { get; set; }
            public int numOfChargeSlots { get; set; }
            
            public List<DroneInCharge> dronesInCharge { get; set; }
            public Location location { get; set; }
            /// <summary>
            /// prints an item's details
            /// </summary>
            public override string ToString()
            {
                var listOut = dronesInCharge == null ? "" : string.Join(", ", dronesInCharge);
                return $"ID: {id}\nName:  {Name}\nLongitude: { location.longitude }\nLattitude: { location.lattitude}\n" +
                    $"Available Charge Slots: {numOfAvailableChargeSlots}\n" +
                    $"\nDrones in charge: {listOut}\n";
            }
        }
        class StationToList
        {
            public int id { get; set; }
            public string Name { get; set; }
            public int numOfAvailableChargeSlots { get; set; }
            public int numOfOccupiedChargeSlots { get; set; }
            public override string ToString()
            {
                return $"ID: {id}\n Name: {Name}\n Available Charge Slots: {numOfAvailableChargeSlots}\n" +
                    $"Occupied Charge Slots: {numOfOccupiedChargeSlots}\n";
            }
        }
    }
}

