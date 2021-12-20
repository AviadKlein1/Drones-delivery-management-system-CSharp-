using System.Collections.Generic;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// class Customer - fields, ctors and ToString function
        /// </summary>
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public Location Location { get; set; }
            //list of parcels sent by customer
            public List<ParcelAtCustomer> ParcelsSent { get; set; }
            //list of parcels sent to customer
            public List<ParcelAtCustomer> ParcelsRecieved { get; set; }

            /// <summary>
            /// default constructor
            /// </summary>
            public Customer()
            {
                Location = new Location();
                ParcelsSent = new List<ParcelAtCustomer>();
                ParcelsRecieved = new List<ParcelAtCustomer>();
            }

            /// <summary>
            /// param constructor (initialize fields)
            /// </summary>
            /// <param name="customer"></param>
            public Customer(DalApi.DO.Customer customer)
            {
                Id = customer.Id;
                Name = customer.Name;
                PhoneNumber = customer.PhoneNumber;
                Location = new Location(customer.Location);
                ParcelsSent = new List<ParcelAtCustomer>();
                ParcelsRecieved = new List<ParcelAtCustomer>();
            }

            /// <summary>
            /// prints item's details
            /// </summary>
            public override string ToString()
            {
                var parcelsSent = ParcelsSent == null ? "" : string.Join(", ", this.ParcelsSent);
                var parcelsDelivered = ParcelsRecieved == null ? "" : string.Join(", ", ParcelsRecieved);

                return $"ID: { Id }\nName: { Name }\nLongitude: { Location.Longitude }\nlatitude: { Location.Latitude }" +
                    $"\nphone number: { PhoneNumber }\nparcels To Customer: { parcelsDelivered }" +
                    $"\nparcels from Customer: { parcelsSent }\n";
            }
        }

        /// <summary>
        /// class customer in parcel - fields, constructor and ToString functiom
        /// </summary>
        public class CustomerInParcel
        {
            public CustomerInParcel() { }
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                return "ID: " + Id + "\nName: " + Name + "\n";
            }
        }

        /// <summary>
        /// class customer to list - fields and ToString function
        /// </summary>
        public class CustomerToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public int ParcelsDelivered { get; set; }
            public int ParcelsSentButNotDelivered { get; set; }
            public int ReceivedParcels { get; set; }
            public int ScheduledParcels { get; set; }

            public override string ToString()
            {
                return $"ID: {Id}\nName: {Name}\nPhone number: {PhoneNumber}\n" +
                $"Parcels sent and delivered: {ParcelsDelivered}\nParcels sent but not delivered: " +
                $"{ ParcelsSentButNotDelivered}\nParcels recieved: { ReceivedParcels}\n" +
                $"Parcels to be arrived: {ScheduledParcels}";
            }
        }
    }
}