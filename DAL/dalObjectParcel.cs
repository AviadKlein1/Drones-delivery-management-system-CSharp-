using System.Collections.Generic;
using System;
using DalApi;
using System.Linq;

namespace DalApi
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// contains all functions regards entity PARCEL
            /// </summary>
            internal partial class DalObject : IDal
            {
                /// <summary>
                /// add parcel to list
                /// </summary>
                /// returns new parcel's id
                public void AddParcel(Parcel myParcel)
                {
                    //if already exist
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                        if (DataSource.parcels[i].Id == myParcel.Id)
                            throw new ExistingIdException(myParcel.Id, $"parcel already exist: { myParcel.Id }");
                    //insert parcel to array
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
                    bool found = false;
                    Parcel myParcel = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (DataSource.parcels[i].Id == myId && DataSource.parcels[i].IsActive)
                        {
                            found = true;
                            myParcel = DataSource.parcels[i];
                        }
                    }
                    if (found == true)
                        return myParcel;
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: { myId }");
                }

                public void DeleteParcel(int myId)
                {
                    Parcel temp = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                    {
                        Parcel item = DataSource.parcels[i];
                        //search customers
                        if (item.Id == myId)
                        {
                            temp.Id = item.Id;
                            temp.IsActive = false;
                            temp.DroneId = item.DroneId;
                            temp.Priority = item.Priority;
                            temp.Weight = item.Weight;
                            temp.SenderId = item.SenderId;
                            temp.ReceiverId = item.ReceiverId;
                            temp.Requested = item.Requested;
                            temp.Scheduled = item.Scheduled;
                            temp.PickedUp = item.PickedUp;
                            temp.Delivered = item.Delivered;
                            DataSource.parcels[i] = temp;
                            return;
                        }
                    }
                    throw new WrongIdException(myId, $"wrong id: {myId}");
                }
                /// <summary>
                /// return filtered list of parcels by conditions
                /// </summary>
                /// 
                /// 
                public IEnumerable<DO.Parcel> GetParcelsList(System.Predicate<DalApi.DO.Parcel> match)
                {
                    List<Parcel> temp1 = new();
                    List<Parcel> temp2 = new();
                    temp1.AddRange(from item in DataSource.parcels
                                   where item.IsActive
                                   select item);
                    ;
                    temp2 = temp1.FindAll(match);
                    return temp2 ;
                }
                /// <summary>
                /// return filtered list of parcels by conditions
                /// </summary>
                /// 
                /// 
                public IEnumerable<Parcel> GetParcelsList()
                {
                    List<Parcel> temp = new();
                    temp.AddRange(from item in DataSource.parcels
                                  where item.IsActive
                                  select item);
                    ;
                    return temp;
                }
                public void UpdatedroneIdInParcel(int ParcelId, int droneId)
                {
                    Parcel temp = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                    {
                        Parcel item = DataSource.parcels[i];
                        //search customers
                        if (item.Id == ParcelId)
                        {
                            temp.Id = item.Id;
                            temp.IsActive = true;
                            temp.DroneId = droneId;
                            temp.Priority = item.Priority;
                            temp.Weight = item.Weight;
                            temp.SenderId = item.SenderId;
                            temp.ReceiverId = item.ReceiverId;
                            temp.Requested = item.Requested;
                            temp.Scheduled = item.Scheduled;
                            temp.PickedUp = item.PickedUp;
                            temp.Delivered = item.Delivered;
                            DataSource.parcels[i] = temp;
                            return;
                        }
                    }
                }


                /// <summary>
                /// Schedule Parcel To Drone in dal
                /// </summary>
                /// <param name="newParcelId"></param>
                /// <param name="droneId"></param>
                /// <returns></returns>
                public void ScheduleParcelToDrone(int newParcelId, int droneId)
                {
                    Parcel temp = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                        //search parcel
                        if (DataSource.parcels[i].Id == newParcelId && DataSource.parcels[i].IsActive)
                        {
                            temp.Id = DataSource.parcels[i].Id;
                            temp.Requested = DataSource.parcels[i].Requested;
                            temp.PickedUp = null;
                            temp.Delivered = null;
                            temp.Scheduled = DateTime.Now;
                            temp.DroneId = droneId;
                            temp.IsActive = true;
                            temp.Priority = DataSource.parcels[i].Priority;
                            temp.ReceiverId = DataSource.parcels[i].ReceiverId;
                            temp.SenderId = DataSource.parcels[i].SenderId;
                            temp.Weight = DataSource.parcels[i].Weight;
                            DataSource.parcels[i] = temp;
                        }
                }

                /// <summary>
                /// Pick Up Parcel By Drone in dal
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="parcelId"></param>
                /// <returns></returns>
                public void PickUpParcel(int droneId, int parcelId)
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
                /// 
                public void DeliverParcel(int droneId, int parcelId)
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