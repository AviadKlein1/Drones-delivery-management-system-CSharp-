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
            public Location location = new Location();
            public List<ParcelAtCustomer> parcelsFromCustomer { get; set; }
            public List<ParcelAtCustomer> parcelsToCustomer{ get; set; }

            public Customer()
            {
            }
            public Customer(IDAL.DO.Customer temp)
            {
                id = temp.id;
                name = temp.name;
                phoneNumber = temp.phoneNumber;
                location = new Location(temp.longitude, temp.lattitude);

                foreach (IDAL.DO.ParcelAtCustomer element in temp.parcelsFromCustomer)
                {
                    ParcelAtCustomer parcel = new ParcelAtCustomer(element);
                    parcelsFromCustomer.Add(parcel);
                }

                foreach (IDAL.DO.ParcelAtCustomer element in temp.parcelsToCustomer)
                {
                    ParcelAtCustomer parcel = new ParcelAtCustomer(element);
                    parcelsToCustomer.Add(parcel);
                }
            }

            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nPhone: " + phoneNumber + "\nLongitude: " +
                    location.longitude + "\nLattitude: " + location.lattitude + "\nparcels From Customer: " +
                   parcelsFromCustomer + "\nnparcels To Customer :" + parcelsToCustomer + "\n";
               
            }

            //public class CustomerInParcel : Customer
            //{

            //}
            public class CustomerToList : Customer
            {
                public int parcelsSendAndDeliverd;
                public int parcelsSendAndNotDeliverd;
                public int parcelsRecived;
                public int parcelsInTheWayToMe;

                public override string ToString()
                {
                    base.ToString();
                    return "parcelsSendAndDeliverd: " + parcelsSendAndDeliverd +
                        "\nparcelsSendAndNotDeliverd: " + parcelsSendAndNotDeliverd +
                        "\nparcelsRecived: " + parcelsRecived;
                }
            }
        }
    }
}
