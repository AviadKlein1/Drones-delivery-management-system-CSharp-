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
                Longitude = longitude;
                Latitude = latitude;
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

            /// <summary>
            /// display geographic coordinate by DMS display
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                double minLat = ((double)(Latitude - (int)Latitude) * 60);
                double minLon = ((double)(Longitude - (int)Longitude) * 60);
                double secLat = ((double)(minLat - (int)minLat) * 60);
                double secLon = ((double)(minLon - (int)minLon) * 60);


                return $"{ (int)Latitude }° { (int)minLat }' { (int)secLat}\" N  " +
                    $"{ (int)Longitude }° {(int)minLon}' {(int)secLon}\" S";
            }
        }
    }
}