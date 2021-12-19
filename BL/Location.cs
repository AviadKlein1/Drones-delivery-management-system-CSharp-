namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// location - fields, constructors and ToString function
        /// </summary>
        public class Location
        {
            /// <summary>
            /// default constructor
            /// </summary>
            public Location()
            {

            }

            /// <summary>
            /// params constructor (initialize fields)
            /// </summary>
            /// <param name="longitude"></param>
            /// <param name="latitude"></param>
            public Location(double longitude, double latitude)
            {
                this.Longitude = longitude;
                this.Latitude = latitude;
            }

            /// <summary>
            /// copy constructor
            /// </summary>
            /// <param name="l"></param>
            public Location(DalApi.DO.Location l)
            {
                Longitude = l.Longitude;
                Latitude = l.Latitude;
            }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public override string ToString()
            {
                return $"latitude: { (int)Latitude } ° { (int)(Latitude - (int)Latitude) * 60 }'" +
                    $" {Latitude - ((int)Latitude * 60) - ((int)(Latitude - (int)Latitude) * 60)} \" N" +
                    $"longitude:  { (int)Longitude } ° { (int)(Longitude - (int)Longitude) * 60 }'" +
                    $" {Longitude - ((int)Longitude * 60) - ((int)(Longitude - (int)Longitude) * 60)} \" S\n";
            }
        }
    }
}