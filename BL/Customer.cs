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
            public List<ParcelAtCustomer> parcelsFromCustomer { get; set; }
            public List<ParcelAtCustomer> parcelsToCustomer { get; set; }

            public Customer()
            {
                location = new Location();
                parcelsFromCustomer = new List<ParcelAtCustomer>();
                parcelsToCustomer = new List<ParcelAtCustomer>();
            }
            public Customer(IDAL.DO.Customer customer)
            {
                id = customer.id;
                name = customer.name;
                phoneNumber = customer.phoneNumber;
                location = new Location();
                parcelsFromCustomer = new List<ParcelAtCustomer>();
                parcelsToCustomer = new List<ParcelAtCustomer>();
            }
            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\nPhone: " + phoneNumber + "\nLongitude: " +
                    location.longitude + "\nLattitude: " + location.lattitude + "\nparcels From Customer: " +
                   parcelsFromCustomer + "\nparcels To Customer :" + parcelsToCustomer + "\n";

            }
        }

        public class CustomerInParcel
        {
            public CustomerInParcel() { }
            public int id { get; set; }
            public string name { get; set; }
            public override string ToString()
            {
                return "ID: " + id + "\nName: " + name + "\n";
            }

        }
        public class CustomerToList
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phoneNumber { get; set; }
            public int parcelsSendAndDeliverd { get; set; }
            public int parcelsSendAndNotDeliverd { get; set; }
            public int parcelsRecived { get; set; }
            public int parcelsInTheWayToMe { get; set; }

            public override string ToString()
            {

                return $"ID: {id}\n name: {name}\n phone number: {phoneNumber}\n" +
                $" parcels Send And Deliverd: {parcelsSendAndDeliverd}\n parcels Send And Not Deliverd: " +
                $"{ parcelsSendAndNotDeliverd}\n parcels Recived: { parcelsRecived}" +
                $"parcels In The Way To Me: {parcelsInTheWayToMe}";
            }

        }       
    }
}
