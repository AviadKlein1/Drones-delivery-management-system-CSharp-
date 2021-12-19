using System;
namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// location - fields, ctor and ToString function
        /// </summary>
        public struct  Location
        {
            /// <summary>
            /// params ctor
            /// </summary>
            /// <param name="longitude"></param>
            /// <param name="latitude"></param>
            public Location(double longitude, double latitude)
            {
                this.Longitude = longitude;
                this.latitude = latitude;
            }

            public double Longitude { get; set; }
            public double latitude { get; set; }

            public override string ToString()
            {
                return "longitude: " + Longitude + "latitude: " + latitude + "\n";
            }
        }
    }
}