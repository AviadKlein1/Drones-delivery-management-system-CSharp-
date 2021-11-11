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
            public List<Parcel> parcelsFromCustomer;
            public List<Parcel> parcelsToCustomer;

            public Customer()
            {
            }
            public Customer(IDAL.DO.Customer temp)
            {
                id = temp.id;
                name = temp.name;
                phoneNumber = temp.phoneNumber;
                location = new Location(temp.longitude, temp.lattitude);
            }

            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nPhone: " + phoneNumber + "\nLongitude: " +
                    location.longitude + "\nLattitude: " + location.lattitude + "\n";
            }
        }

        public class CustomerInParcel : Customer
        {

        }
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
