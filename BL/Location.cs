namespace IBL
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
            /// <param name="lattitude"></param>
            public Location(double longitude, double lattitude)
            {
                this.longitude = longitude;
                this.lattitude = lattitude;
            }

            /// <summary>
            /// copy constructor
            /// </summary>
            /// <param name="l"></param>
            public Location(IDAL.DO.Location l)
            {
                this.longitude = l.Longitude;
                this.lattitude = l.Lattitude;
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