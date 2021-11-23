using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// entity station - fields, ctors and ToString function
        /// </summary>
        public class Station
        {
            /// <summary>
            /// default ctor
            /// </summary>
            public Station()
            {
                
            }

            /// <summary>
            /// copy ctor
            /// </summary>
            /// <param name="temp"></param>
            public Station(IDAL.DO.Station temp)
            {
                id = temp.id;
                name = temp.name;
                numOfAvailableChargeSlots = temp.numOfAvailableChargeSlots;
                location = new Location(temp.location);
            }
            public int id { get; set; }
            public string name { get; set; }
            public int numOfAvailableChargeSlots { get; set; }
            public int numOfChargeSlots { get; set; }
            public List<DroneInCharge> dronesInCharge { get; set; }
            public Location location { get; set; }
            public override string ToString()
            {
                var listOut = dronesInCharge == null ? "" : string.Join(", ", dronesInCharge);
                return $"ID: { id }\nName: { name }\nLongitude: { location.longitude }\nLattitude: " +
                    $"{ location.lattitude }\n" +
                    $"Available Charge Slots: {numOfAvailableChargeSlots}\n" +
                    $"\nDrones in charge: {listOut}\n";
            }
        }

        /// <summary>
        /// entity station to list - fields and ToString function
        /// </summary>
        public class StationToList
        {
            public int id { get; set; }
            public string name { get; set; }
            public int numOfAvailableChargeSlots { get; set; }
            public int numOfOccupiedChargeSlots { get; set; }
            public override string ToString()
            {
                return $"ID: {id}\nName: {name}\nAvailable Charge Slots: {numOfAvailableChargeSlots}\n" +
                    $"Occupied Charge Slots: {numOfOccupiedChargeSlots}\n";
            }
        }
    }
}