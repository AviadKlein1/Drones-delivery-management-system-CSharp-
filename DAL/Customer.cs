using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IDAL
{
    namespace DO
    {
        //item's fields
        public struct Customer
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phoneNumber { get; set; }
            //public Location location { get; set; }
            public double longitude { get; set; }
            public double lattitude { get; set; }

            public List<ParcelAtCustomer> parcelsFromCustomer;
            public List<ParcelAtCustomer> parcelsToCustomer;


            /// <summary>
            /// prints item's details
            /// </summary>
            //public override string ToString()
            //{
            //    return "ID: " + id + "\nName: " + name + "\nPhone: " + phoneNumber + "\nLongitude: " + /*location.*/longitude + "\nLattitude: " +
            //        /*location.*/lattitude + "\n";
            //}
            public struct CustomerInParcel 
            {
                public int id { get; set; }
                public string name { get; set; }
                public string phoneNumber { get; set; }
                public double longitude { get; set; }
                public double lattitude { get; set; }

                public List<Parcel> parcelsFromCustomer;
                public List<Parcel> parcelsToCustomer;

            }

            public struct CustomerToList
            {
                public int id { get; set; }
                public string name { get; set; }
                public string phoneNumber { get; set; }
                public double longitude { get; set; }
                public double lattitude { get; set; }

                public List<Parcel> parcelsFromCustomer;
                public List<Parcel> parcelsToCustomer;
                public int parcelsSendAndDeliverd { get; set; }
                public int parcelsSendAndNotDeliverd { get; set; }
                public int parcelsRecived { get; set; }
                public int parcelsInTheWayToMe { get; set; }

                //public override string ToString()
                //{
                //    base.ToString();
                //    return "parcelsSendAndDeliverd: " + parcelsSendAndDeliverd +
                //        "\nparcelsSendAndNotDeliverd: " + parcelsSendAndNotDeliverd +
                //        "\nparcelsRecived: " + parcelsRecived;
                //}
            }
        }
    }
}