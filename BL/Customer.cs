using System.Collections.Generic;


namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// class Customer - fields, ctors and ToString function
        /// </summary>
        public class Customer
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phoneNumber { get; set; }
            public Location location { get; set; }
            //list of parcels sent by customer
            public List<ParcelAtCustomer> parcelsSent { get; set; }
            //list of parcels sent to customer
            public List<ParcelAtCustomer> parcelsRecieved { get; set; }

            /// <summary>
            /// default constructor
            /// </summary>
            public Customer()
            {
                location = new Location();
                parcelsSent = new List<ParcelAtCustomer>();
                parcelsRecieved = new List<ParcelAtCustomer>();
            }

            /// <summary>
            /// param constructor (initialize fields)
            /// </summary>
            /// <param name="customer"></param>
            public Customer(IDAL.DO.Customer customer)
            {
                id = customer.id;
                name = customer.name;
                phoneNumber = customer.phoneNumber;
                location = new Location(customer.location);
                parcelsSent = new List<ParcelAtCustomer>();
                parcelsRecieved = new List<ParcelAtCustomer>();
            }

            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                var parcelsSent = this.parcelsSent == null ? "" : string.Join(", ", this.parcelsSent);
                var parcelsDelivered = parcelsRecieved == null ? "" : string.Join(", ", parcelsRecieved);

                return $"ID: {id}\nName: { name }\nLongitude: { location.longitude }\nLattitude: { location.lattitude }" +
                    $"\nphone number: { phoneNumber }\nparcels To Customer: { parcelsDelivered }" +
                    $"\nparcels from Customer: { parcelsSent }\n";
            }
        }

        /// <summary>
        /// class customer in parcel - fields, constructor and ToString functiom
        /// </summary>
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

        /// <summary>
        /// class customer to list - fields and ToString function
        /// </summary>
        public class CustomerToList
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phoneNumber { get; set; }
            public int parcelsDelivered { get; set; }
            public int parcelsSentButNotDelivered { get; set; }
            public int recievedParcels { get; set; }
            public int ScheduledParcels { get; set; }

            public override string ToString()
            {
                return $"ID: { id }\n name: { name }\n phone number: { phoneNumber }\n" +
                $" parcels Send And Deliverd: { parcelsDelivered} \n parcels Send And Not Deliverd: " +
                $"{ parcelsSentButNotDelivered }\n parcels Recived: { recievedParcels }" +
                $"parcels In The Way To Me: { ScheduledParcels }";
            }
        }
    }
}