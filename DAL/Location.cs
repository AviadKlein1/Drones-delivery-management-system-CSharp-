using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Location
        {
            //public Location()
            //{}
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
