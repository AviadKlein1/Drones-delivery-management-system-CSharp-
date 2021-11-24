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
                /// <summary>
                /// update name and number of charge slots in station
                /// <param name="stationId"></param>
                /// <param name="newName"></param>
                /// <param name="numOfChargeSlots"></param>
                /// </summary>
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
                public IEnumerable<Station> GetAvailableToChargeStations()
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
                /// <summary>
                /// decrise num of available charge slots
                /// <param name="stationId"></param>
                /// </summary>
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
                /// <summary>
                /// increase num of available charge slots
                /// <param name="stationId"></param>
                /// </summary>
                public void IncreaseChargeSlot(int stationId)
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