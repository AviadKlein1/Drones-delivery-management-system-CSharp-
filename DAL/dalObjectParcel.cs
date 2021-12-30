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
                        if (DataSource.parcels[i].Id == myId)
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

                /// <summary>
                /// return filtered list of parcels by conditions
                /// </summary>
                public IEnumerable<Parcel> GetParcelsList(Predicate<Parcel> match)
                {
                    List<Parcel> newList = new();
                    newList = DataSource.parcels.FindAll(match);
                    return newList;
                }
                
                /// <summary>
                /// Schedule Parcel To Drone in dal
                /// </summary>
                /// <param name="newParcelId"></param>
                /// <param name="droneId"></param>
                /// <returns></returns>
                public void ScheduleParcelToDrone(int newParcelId, int droneId)
                {
                    Parcel myParcel = new();
                    for (int i = 0; i < DataSource.parcels.Count; i++)
                        //search parcel
                        if (DataSource.parcels[i].Id == newParcelId)
                        {
                            myParcel = DataSource.parcels[i];
                            myParcel.Scheduled = DateTime.Now;
                            myParcel.DroneId = droneId;
                            DataSource.parcels[i] = myParcel;
                            break;
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