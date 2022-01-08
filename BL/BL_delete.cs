using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// contains item updating functions
        /// </summary>
        public partial class BL : IBl
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void DeleteStation(BO.Station myStation)
            {
                lock (dal)
                {
                    dal.DeleteStation(myStation.Id);
                }
            }

            /// <summary>
            /// Delete drone
            /// </summary>
            /// <returns></returns>

            [MethodImpl(MethodImplOptions.Synchronized)]
            public void DeleteDrone(Drone myDrone)
            {
                lock (dal)
                {
                    var v = (List<DalApi.DO.DroneCharge>)dal.GetDroneCharges();
                    var tempDC = new DalApi.DO.DroneCharge();//if it was in charge
                    for (int i = 0; i < v.Count(); i++)
                    {
                        if (v[i].DroneId == myDrone.Id && v[i].IsActive)
                        {
                            tempDC = v[i];
                            tempDC.IsActive = false;
                            dal.IncreaseChargeSlot(v[i].StationId);
                            v[i] = tempDC;
                        }
                    }

                    DroneToList temp = new();
                    dal.DeleteDrone(myDrone.Id);
                    foreach (var item in from item in dronesList
                                         where item.Id == myDrone.Id
                                         select item)
                    {
                        temp = item;
                    }

                    dronesList.Remove(temp);
                }
            }

            /// <summary>
            /// add customer
            /// </summary>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void DeleteCustomer(BO.Customer myCustomer)
            {
                lock (dal)
                {
                    dal.DeleteCustomer(myCustomer.Id);
                }
            }

            /// <summary>
            /// Delete parcel
            /// </summary>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void DeleteParcel(BO.Parcel myParcel)
            {
                lock (dal)
                {
                    var tempP = DisplayParcel(myParcel.Id);
                    dronesList.Find(item => item.Id == tempP.DroneInParcel.Id).DeliveredParcelId = 0;
                    dronesList.Find(item => item.Id == tempP.DroneInParcel.Id).Status = MyEnums.DroneStatus.available;
                    dal.DeleteParcel(myParcel.Id);
                }
            }
        }
    }
}