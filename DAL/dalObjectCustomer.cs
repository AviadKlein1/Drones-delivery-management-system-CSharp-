﻿using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            public partial class DalObject : IDal
            {
                /// <summary>
                ///  add it to array
                /// </summary>
                public void addcustomer(Customer myCustomer)
                {
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.customers.Count; i++)
                        if (IDAL.DO.DalObject.DataSource.customers[i].id == myCustomer.id)
                            throw new ExcistingIdException(myCustomer.id, $"customer already exist: {myCustomer.id}");
                    //insert customer to array
                    IDAL.DO.DalObject.DataSource.customers.Add(myCustomer);
                }
                public IDAL.DO.Customer getCustomer(int myId)
                {
                    bool isDouble = false;
                    Customer temp = new Customer();
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