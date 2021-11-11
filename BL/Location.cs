using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Location
        {
            public Location(){}
            public Location(double longitude, double lattitude)
            {
                this.longitude = longitude;
                this.lattitude = lattitude;

            }
            public Location(IDAL.DO.Location l)
            {
                this.longitude = l.longitude;
                this.lattitude = l.lattitude;
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
