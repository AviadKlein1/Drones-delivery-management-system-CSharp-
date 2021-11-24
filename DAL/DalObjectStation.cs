using System;
using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// entity station
            /// </summary>
            public partial class DalObject : IDal
            {
                public DalObject()
                {
                    IDAL.DO.DalObject.DataSource.Initialize();
                }

                /// <summary>
                /// add to list
                /// </summary>
                public void AddStation(Station myStation)
                {
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                        //if already exist
                        if (IDAL.DO.DalObject.DataSource.stations[i].Id == myStation.Id)
                            throw new ExcistingIdException(myStation.Id, $"station already exist: {myStation.Id}");
                    //insert station to list
                    IDAL.DO.DalObject.DataSource.stations.Add(myStation);
                }

                /// <summary>
                /// associate parcel with specific drone
                /// </summary>
                public void paracelToDrone(int parcelId)
                {
                    //search and available drone 
                    //int i;
                    //bool flag = false;
                    //for (i = 0; i < IDAL.DO.DalObject.DataSource.drones.Count; i++)
                    //    if(IDAL.DO.DalObject.DataSource.drones[i].status == MyEnums.DroneStatus.available)
                    //    {
                    //        flag = true;
                    //        break;
                    //    }
                    ////if available
                    //if (flag)
                    //{
                    //    IDAL.DO.Drone temp = IDAL.DO.DalObject.DataSource.drones[i];
                    //    temp.status = MyEnums.DroneStatus.delivery;
                    //    IDAL.DO.DalObject.DataSource.drones[i] = temp;

                    //    //change status to delivery
                    //    //IDAL.DO.DalObject.DataSource.drones[i].status = MyEnums.DroneStatus.delivery;
                    //    //search parcel
                    //    //int j = 0;
                    //    //while (IDAL.DO.DalObject.DataSource.parcels[j].id != parcelId)
                    //    //    j++;
                    //    ////belong parcel
                    //    //IDAL.DO.DalObject.DataSource.parcels[j].droneId = IDAL.DO.DalObject.DataSource.drones[i].id;

                    //    //drones at i
                    //    //parcels at j
                    //    for (int j = 0; j < IDAL.DO.DalObject.DataSource.parcels.Count; j++)
                    //    {
                    //        if (IDAL.DO.DalObject.DataSource.parcels[j].id == parcelId)
                    //        {
                    //            IDAL.DO.Parcel temp2 = IDAL.DO.DalObject.DataSource.parcels[i];
                    //            temp2.droneId = IDAL.DO.DalObject.DataSource.drones[i].id;
                    //            //update scheduled time
                    //            temp2.scheduled = DateTime.Now;
                    //            IDAL.DO.DalObject.DataSource.parcels[j] = temp2;
                    //        }
                    //    }
                    //}
                }

                /// <summary>
                /// return station by its id
                /// </summary>
                /// <param name="myId"></param>
                public Station GetStation(int myId)
                {
                    bool isDouble = false;
                    Station temp = new Station();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    {
                        //search station
                        if (IDAL.DO.DalObject.DataSource.stations[i].Id == myId)
                        {
                            isDouble = true;
                            temp = IDAL.DO.DalObject.DataSource.stations[i];
                        }
                    }
                    if (isDouble == true)
                    {
                        return temp;
                    }
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }

                /// <summary>
                /// return all stations
                /// </summary>
                public IEnumerable<Station> GetStations()
                {
                    List<IDAL.DO.Station> temp = new List<IDAL.DO.Station>();
                    temp = IDAL.DO.DalObject.DataSource.stations;
                    return temp;
                }
                public void UpdateStation(int stationId, string newName, int numOfChargeSlots)
                {
                    Station temp = new Station();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                    {
                        Station item = IDAL.DO.DalObject.DataSource.stations[i];
                        if (item.Id == stationId)
                        {
                            temp.Id = stationId;
                            temp.Location = item.Location;
                            if (newName != null) temp.Name = newName;
                            else temp.Name = item.Name;
                            if (numOfChargeSlots != 0) temp.NumOfChargeSlots = numOfChargeSlots;
                            else temp.NumOfChargeSlots = item.NumOfChargeSlots;
                            IDAL.DO.DalObject.DataSource.stations[i] = temp;
                        }
                    }
                }
                /// <summary>
                /// return all available to charge stations
                /// </summary>
                public IEnumerable<Station> getAvailableToChargeStations()
                {
                    int size = IDAL.DO.DalObject.DataSource.stations.Count;
                    List<IDAL.DO.Station> temp = new List<IDAL.DO.Station>();
                    temp = IDAL.DO.DalObject.DataSource.stations;
                    for (int i = 0; i < size; i++)
                        //if available for charge
                        if (IDAL.DO.DalObject.DataSource.stations[i].NumOfAvailableChargeSlots > 0)
                            temp.Add(IDAL.DO.DalObject.DataSource.stations[i]);
                    return temp;
                }

                /// <summary>
                /// return stations location by its id
                /// </summary>
                /// <param name="StationId"></param>
                /// <returns></returns>
                public IDAL.DO.Location StationLocate(int StationId)
                {
                    Location temp = new Location();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.stations[i].Id == StationId)
                        {
                            temp.Lattitude = IDAL.DO.DalObject.DataSource.stations[i].Location.Lattitude;
                            temp.Longitude = IDAL.DO.DalObject.DataSource.stations[i].Location.Longitude;

                        }
                    }
                    return temp;
                }
                public void DecriseChargeSlot(int stationId)
                {
                    Station temp = new Station();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                    {
                        Station item = IDAL.DO.DalObject.DataSource.stations[i];
                        if (item.Id == stationId)
                        {
                            temp.Id = stationId;
                            temp.Location = item.Location;
                            temp.Name = item.Name;
                            temp.NumOfChargeSlots = item.NumOfChargeSlots - 1;
                            IDAL.DO.DalObject.DataSource.stations[i] = temp;
                        }
                    }
                }
                public void increaseChargeSlot(int stationId)
                {
                    Station temp = new Station();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                    {
                        Station item = IDAL.DO.DalObject.DataSource.stations[i];
                        if (item.Id == stationId)
                        {
                            temp.Id = stationId;
                            temp.Location = item.Location;
                            temp.Name = item.Name;
                            temp.NumOfChargeSlots = item.NumOfChargeSlots + 1;
                            IDAL.DO.DalObject.DataSource.stations[i] = temp;
                        }
                    }
                }
                /// <summary>
                /// return the distance between two coordinates
                /// </summary>
                /// <param name="a"></param>
                /// <param name="b"></param>
                /// <returns></returns>
                public int Distance(Location a, Location b)
                {
                    return (int)Math.Sqrt(Math.Pow((b.Longitude - a.Longitude), 2) -
                        Math.Pow((b.Lattitude - a.Lattitude), 2));
                }
            }
        }
    }
}