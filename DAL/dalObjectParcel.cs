using System.Collections.Generic;
using System;


namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// entity parcel
            /// </summary>
            public partial class DalObject : IDal
            {
                /// <summary>
                /// add parcel to list
                /// </summary>
                /// returns new parcel's id
                public void AddParcel(Parcel myParcel)
                {
                    //insert parcel to array
                    //IDAL.DO.DalObject.DataSource.parcels[IDAL.DO.DalObject.DataSource.Config.parcelIndex] = myParcel;
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                        //if already exist
                        if (IDAL.DO.DalObject.DataSource.parcels[i].Id == myParcel.Id)
                            throw new ExcistingIdException(myParcel.Id, $"parcel already exist: {myParcel.Id}");
                    IDAL.DO.DalObject.DataSource.parcels.Add(myParcel);
                }

                /// <summary>
                /// promote parcel serial number
                /// </summary>
                /// <returns></returns>
                public int ParcelRunId()
                {
                    return IDAL.DO.DalObject.DataSource.Config.ParcelRunId++;
                }

                /// <summary>
                /// return a parcel by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public IDAL.DO.Parcel GetParcel(int myId)
                {
                    bool isDouble = false;
                    Parcel temp = new Parcel();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (IDAL.DO.DalObject.DataSource.parcels[i].Id == myId)
                        {
                            isDouble = true;
                            temp = IDAL.DO.DalObject.DataSource.parcels[i];
                        }
                    }
                    if (isDouble == true)
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
                    List<IDAL.DO.Parcel> newList = new();
                    newList = IDAL.DO.DalObject.DataSource.parcels.FindAll(match);
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
                    Parcel temp = new Parcel();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (IDAL.DO.DalObject.DataSource.parcels[i].Id == newParcelId)
                        {
                            temp = IDAL.DO.DalObject.DataSource.parcels[i];
                            temp.Scheduled = DateTime.Now;
                            temp.DroneId = droneId;
                            IDAL.DO.DalObject.DataSource.parcels[i] = temp;
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
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (IDAL.DO.DalObject.DataSource.parcels[i].Id == parcelId)
                        {
                            temp = IDAL.DO.DalObject.DataSource.parcels[i];
                            temp.PickedUp = DateTime.Now;
                            IDAL.DO.DalObject.DataSource.parcels[i] = temp;
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
                    Parcel temp = new Parcel();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.parcels.Count; i++)
                    {
                        //search parcel
                        if (IDAL.DO.DalObject.DataSource.parcels[i].Id == parcelId)
                        {
                            temp = IDAL.DO.DalObject.DataSource.parcels[i];
                            temp.Delivered = DateTime.Now;
                            IDAL.DO.DalObject.DataSource.parcels[i] = temp;
                        }
                    }
                }
            }
        }
    }
}