namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// location - fields, ctor and ToString function
        /// </summary>
        public struct Location
        {
            /// <summary>
            /// params ctor
            /// </summary>
            /// <param name="longitude"></param>
            /// <param name="latitude"></param>
            public Location(double longitude, double latitude)
            {
                Longitude = longitude;
                Latitude = latitude;
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