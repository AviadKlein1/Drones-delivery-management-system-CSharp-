using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            public partial class DalObject : IDal
            {
                /// <summary>
                /// add costumer to list of costimers
                /// </summary>
                /// <param name="myCustomer"></param>
                public void addcustomer(Customer myCustomer)
                {
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.customers.Count; i++)
                        if (IDAL.DO.DalObject.DataSource.customers[i].id == myCustomer.id)
                            throw new ExcistingIdException(myCustomer.id, $"customer already exist: {myCustomer.id}");
                    //insert customer to list
                    IDAL.DO.DalObject.DataSource.customers.Add(myCustomer);
                }

                /// <summary>
                /// returns a costumer by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public IDAL.DO.Customer getCustomer(int myId)
                {
                    bool isDouble = false;
                    Customer temp = new Customer();
                    //search costumer
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.customers.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.customers[i].id == myId)
                        {
                            isDouble = true;
                            temp = IDAL.DO.DalObject.DataSource.customers[i];
                        }
                    }
                    if (isDouble == true)
                        return temp;
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }

                /// <summary>
                /// print all customers
                /// </summary>
                public IEnumerable<Customer> getCustomers()
                {
                    List<IDAL.DO.Customer> temp = new List<IDAL.DO.Customer>();
                    temp = IDAL.DO.DalObject.DataSource.customers;
                    return temp;
                }
            }
        }
    }
}