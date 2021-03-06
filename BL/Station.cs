using System.Collections.Generic;

namespace BlApi
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
            public Station(){}

            /// <summary>
            /// copy ctor
            /// </summary>
            /// <param name="temp"></param>
            public Station(DalApi.DO.Station temp)
            {
                Id = temp.Id;
                Name = temp.Name;
                NumOfAvailableChargeSlots = temp.NumOfAvailableChargeSlots;
                Location = new Location(temp.Location);
            }

            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Name { get; set; }
            public int NumOfAvailableChargeSlots { get; set; }
            public int NumOfChargeSlots { get; set; }
            public List<DroneInCharge> DronesInCharge { get; set; }
            public Location Location { get; set; }
            public override string ToString()
            {
                var listOut = DronesInCharge == null ? "" : string.Join(", ", DronesInCharge);
                return $"ID: { Id }\nName: { Name }\nLongitude: { Location.Longitude }\nlatitude: " +
                    $"{ Location.Latitude }\n" +
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