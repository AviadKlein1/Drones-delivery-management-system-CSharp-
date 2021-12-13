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
            /// <param name="lattitude"></param>
            public Location(double longitude, double lattitude)
            {
                this.Longitude = longitude;
                this.Lattitude = lattitude;
            }

            public double Longitude { get; set; }
            public double Lattitude { get; set; }

            public override string ToString()
            {
                return "longitude: " + Longitude + "lattitude: " + Lattitude + "\n";
            }
        }
    }
}