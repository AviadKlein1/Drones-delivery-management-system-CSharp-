using System.Collections.Generic;
using System;
using DalApi;

namespace DalApi
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// entity parcel
            /// </summary>
            internal partial class DalObject : IDal
            {
                /// <summary>
                /// add parcel to list
                /// </summary>
                /// returns new parcel's id
                public void AddParcel(Parcel myParcel)
                {
                    //insert parcel to array
                    //IDAL.DO.DalObject.DataSource.parcels[IDAL.DO.DalObject.DataSource.Config.parcelIndex] = myParcel;
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                        //if already exist
                        if (DataSource.parcels[i].Id == myParcel.Id)
                            throw new ExistingIdException(myParcel.Id, $"parcel already exist: { myParcel.Id }");
                    DataSource.parcels.Add(myParcel);
                }

                /// <summary>
                /// promote parcel serial number
                /// </summary>
                /// <returns></returns>
                public int ParcelRunId()
                {
                    return DataSource.Config.ParcelRunId++;
                }

                /// <summary>
                /// return a parcel by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public Parcel GetParcel(int myId)
                {
                    bool isDouble = false;
                    Parcel temp = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (DataSource.parcels[i].Id == myId)
                        {
                            isDouble = true;
                            temp = DataSource.parcels[i];
                        }
                    }
                    if (isDouble)
                        return temp;
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }

                /// <summary>
                /// return list of parcels by conditions
                /// </summary>
                public IEnumerable<Parcel> GetParcelsList(System.Predicate<Parcel> match)
                {
                    List<Parcel> newList = new();
                    newList = DataSource.parcels.FindAll(match);
                    return newList;
                }
                
                /// <summary>
                /// Shedule Parcel To Drone in dal
                /// </summary>
                /// <param name="newParcelId"></param>
                /// <param name="droneId"></param>
                /// <returns></returns>
                public void SheduleParcelToDrone(int newParcelId, int droneId)
                {
                    Parcel temp = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (DataSource.parcels[i].Id == newParcelId)
                        {
                            temp = DataSource.parcels[i];
                            temp.Scheduled = DateTime.Now;
                            temp.DroneId = droneId;
                            DataSource.parcels[i] = temp;
                        }
                    }
                }
                /// <summary>
                /// Pick Up Parcel By Drone in dal
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="parcelId"></param>
                /// <returns></returns>
                public void PickUpParcelByDrone(int droneId, int parcelId)
                {
                    Parcel temp = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (DataSource.parcels[i].Id == parcelId)
                        {
                            temp = DataSource.parcels[i];
                            temp.PickedUp = DateTime.Now;
                            DataSource.parcels[i] = temp;
                        }
                    }
                }
                /// <summary>
                /// Deliver Parcel By Drone in dal
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="parcelId"></param>
                /// <returns></returns>
                public void DeliverParcelByDrone(int droneId, int parcelId)
                {
                    Parcel temp = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (DataSource.parcels[i].Id == parcelId)
                        {
                            temp = DataSource.parcels[i];
                            temp.Delivered = DateTime.Now;
                            DataSource.parcels[i] = temp;
                        }
                    }
                }
            }
        }
    }
}