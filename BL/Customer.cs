using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    namespace BO
    {
        //item's fields
        public class Customer
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phoneNumber { get; set; }
            public Location location { get; set; }

            public List<Parcel> parcelsFromCustomer;
            public List<Parcel> parcelsToCustomer;


            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nPhone: " + phoneNumber + "\nLongitude: " +
                    location.longitude + "\nLattitude: " + location.lattitude + "\n";
            }
        }
    }
}
