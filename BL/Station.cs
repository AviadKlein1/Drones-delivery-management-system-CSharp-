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
                Id = temp.Id;
                Name = temp.Name;
                NumOfAvailableChargeSlots = temp.NumOfAvailableChargeSlots;
                Location = new Location(temp.Location);
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public int NumOfAvailableChargeSlots { get; set; }
            public int NumOfChargeSlots { get; set; }
            public List<DroneInCharge> DronesInCharge { get; set; }
            public Location Location { get; set; }
            public override string ToString()
            {
                var listOut = DronesInCharge == null ? "" : string.Join(", ", DronesInCharge);
                return $"ID: { Id }\nName: { Name }\nLongitude: { Location.longitude }\nLattitude: " +
                    $"{ Location.lattitude }\n" +
                    $"Available Charge Slots: {NumOfAvailableChargeSlots}\n" +
                    $"\nDrones in charge: {listOut}\n";
            }
        }

        /// <summary>
        /// entity station to list - fields and ToString function
        /// </summary>
        public class StationToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int NumOfAvailableChargeSlots { get; set; }
            public int NumOfOccupiedChargeSlots { get; set; }
            public override string ToString()
            {
                return $"ID: {Id}\nName: {Name}\nAvailable Charge Slots: {NumOfAvailableChargeSlots}\n" +
                    $"Occupied Charge Slots: {NumOfOccupiedChargeSlots}\n";
            }
        }
    }
}