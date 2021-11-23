namespace IDAL
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
            /// <param name="lattitude"></param>
            public Location(double longitude, double lattitude)
            {
                this.longitude = longitude;
                this.lattitude = lattitude;
            }

            public double longitude { get; set; }
            public double lattitude { get; set; }

            public override string ToString()
            {
                return "longitude: " + longitude + "lattitude: " + lattitude + "\n";
            }
        }
    }
}