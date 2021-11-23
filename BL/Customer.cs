using System.Collections.Generic;


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
                location = new Location(customer.location);
                parcelsFromCustomer = new List<ParcelAtCustomer>();
                parcelsToCustomer = new List<ParcelAtCustomer>();
            }
            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                var listFromCustomerOut = parcelsFromCustomer == null ? "" : string.Join(", ", parcelsFromCustomer);
                var listToCustomerOut = parcelsToCustomer == null ? "" : string.Join(", ", parcelsToCustomer);

                return $"ID: {id}\nName:  {name}\nLongitude: {location.longitude }\nLattitude: {location.lattitude}" +
                    $"\nphone number: {phoneNumber}\nparcels To Customer: { listToCustomerOut}" +
                    $"\nparcels from Customer: { listFromCustomerOut}\n";

                   
                    

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
