using System.Collections.Generic;

namespace DalApi
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// contains all functions regards the entity CUSTOMER
            /// </summary>
            internal partial class DalObject : IDal
            {
                /// <summary>
                /// add customer to list of customers
                /// </summary>
                /// <param name="myCustomer"></param>
                public void Addcustomer(Customer myCustomer)
                {
                    for (int i = 0; i < DataSource.customers.Count; i++)
                        if (DataSource.customers[i].Id == myCustomer.Id)
                            throw new ExistingIdException(myCustomer.Id, $"customer already exist: {myCustomer.Id}");
                    //insert customer to list
                    DataSource.customers.Add(myCustomer);
                }

                /// <summary>
                /// returns a costumer by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public Customer GetCustomer(int myId)
                {
                    bool found = false;
                    Customer temp = new();
                    //search costumer
                    for (int i = 0; i < DataSource.customers.Count; i++)
                        if (DataSource.customers[i].Id == myId)
                        {
                            found = true;
                            temp = DataSource.customers[i];
                        }
                    if (found)
                        return temp;
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }

                /// <summary>
                /// return customers by conditions
                /// </summary>
                public IEnumerable<Customer> GetCustomersList(System.Predicate<Customer> match)
                {
                    List<Customer> newList = new();
                    newList = DataSource.customers.FindAll(match);
                    return newList;
                }
               
                /// <summary>
                /// update customer's details
                /// <param name="customerId"></param>
                /// <param name="newName"></param>
                /// <param name="newPhone"></param>
                /// </summary>
                public void UpdateCustomer(int customerId, string newName, string newPhone)
                {
                    Customer temp = new();
                    for (int i = 0; i < DataSource.customers.Count; i++)
                    {
                        Customer item = DataSource.customers[i];
                        if (item.Id == customerId)
                        {
                            temp.Id = customerId;
                            temp.Location = item.Location;
                            if (newName != null)
                                temp.Name = newName;
                            else temp.Name = item.Name;
                            if (newPhone != null)
                                temp.PhoneNumber = newPhone;
                            else
                                temp.PhoneNumber = item.PhoneNumber;
                            temp.Location = item.Location;
                            DataSource.customers[i] = temp;
                        }
                    }
                }
            }
        }
    }
}