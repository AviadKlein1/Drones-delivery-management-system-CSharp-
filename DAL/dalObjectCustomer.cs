using System.Collections.Generic;
using DalApi;
namespace DalApi
{
    namespace DO
    {
        namespace DalObject
        {
            partial class DalObject : IDal
            {
                /// <summary>
                /// add costumer to list of costimers
                /// </summary>
                /// <param name="myCustomer"></param>
                public void Addcustomer(Customer myCustomer)
                {
                    for (int i = 0; i < DalApi.DO.DalObject.DataSource.customers.Count; i++)
                        if (DalApi.DO.DalObject.DataSource.customers[i].Id == myCustomer.Id)
                            throw new ExcistingIdException(myCustomer.Id, $"customer already exist: {myCustomer.Id}");
                    //insert customer to list
                    DalApi.DO.DalObject.DataSource.customers.Add(myCustomer);
                }

                /// <summary>
                /// returns a costumer by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public DalApi.DO.Customer GetCustomer(int myId)
                {
                    bool isDouble = false;
                    Customer temp = new Customer();
                    //search costumer
                    for (int i = 0; i < DalApi.DO.DalObject.DataSource.customers.Count; i++)
                    {
                        if (DalApi.DO.DalObject.DataSource.customers[i].Id == myId)
                        {
                            isDouble = true;
                            temp = DalApi.DO.DalObject.DataSource.customers[i];
                        }
                    }
                    if (isDouble == true)
                        return temp;
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }

                /// <summary>
                /// return customers by conditions
                /// </summary>
                public IEnumerable<DalApi.DO.Customer> GetCustomersList(System.Predicate<DalApi.DO.Customer> match)
                {
                    List<DalApi.DO.Customer> newList = new();
                    newList = DalApi.DO.DalObject.DataSource.customers.FindAll(match);
                    return newList;
                }
               
                /// <summary>
                /// update model in drone
                /// <param name="customerId"></param>
                /// <param name="newName"></param>
                /// <param name="newPhone"></param>
                /// </summary>
                public void UpdateCustomer(int customerId, string newName, string newPhone)
                {
                    Customer temp = new Customer();
                    for (int i = 0; i < DataSource.customers.Count; i++)
                    {
                        Customer item = DalApi.DO.DalObject.DataSource.customers[i];
                        if (item.Id == customerId)
                        {
                            temp.Id = customerId;
                            temp.Location = item.Location;
                            if (newName != null) temp.Name = newName;
                            else temp.Name = item.Name;
                            if (newPhone != null) temp.PhoneNumber = newPhone;
                            else temp.PhoneNumber = item.PhoneNumber;
                            temp.Location = item.Location;
                            DalApi.DO.DalObject.DataSource.customers[i] = temp;
                        }
                    }

                }

            }
        }
    }
}