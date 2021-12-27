using System;
using System.Collections.Generic;

namespace DalApi
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// contains all functions regards entity STATION
            /// </summary>
            internal sealed partial class DalObject : IDal
            {
                #region singleton
                /// <summary>
                /// create a single instance of dal object
                /// </summary>
                private static readonly IDal instance = new DalObject();
                public static IDal GetInstance() { return instance; }
                private DalObject() { DataSource.Initialize(); }
                #endregion

                /// <summary>
                /// add to list
                /// </summary>
                public void AddStation(Station myStation)
                {
                    for (int i = 0; i < DataSource.stations.Count; i++)
                        //if already exist
                        if (DataSource.stations[i].Id == myStation.Id)
                            throw new ExistingIdException(myStation.Id, $"station already exist: {myStation.Id}");
                    //insert station to list
                    DataSource.stations.Add(myStation);
                }

                /// <summary>
                /// return station by its id
                /// </summary>
                /// <param name="myId"></param>
                public Station GetStation(int myId)
                {
                    bool found = false;
                    Station temp = new();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                        //search station
                        if (DataSource.stations[i].Id == myId)
                        {
                            found = true;
                            temp = DataSource.stations[i];
                        }
                    if (found == true)
                        return temp;
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }

                /// <summary>
                /// delete station
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public void DeleteStation(int myId)
                {
                    Station temp = new();
                    foreach (Station item in DataSource.stations)
                    {
                        //search station
                        if (item.Id == myId)
                        {
                            temp = item;
                            temp.IsActive = false;

                            return;
                        }
                    }
                   throw new WrongIdException(myId, $"wrong id: {myId}");
                }


                /// <summary>
                /// return stations by conditions
                /// </summary>
                public IEnumerable<Station> GetStationsList(Predicate<Station> match)
                {
                    List<Station> newList = new();
                    newList = DataSource.stations.FindAll(match);
                    return newList;
                }
                
                /// <summary>
                /// update name and number of charge slots in station
                /// <param name="stationId"></param>
                /// <param name="newName"></param>
                /// <param name="numOfChargeSlots"></param>
                /// </summary>
                public void UpdateStation(int stationId, string newName, int numOfChargeSlots, int avialble)
                {
                    Station temp = new();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                    {
                        Station item = DataSource.stations[i];
                        if (item.Id == stationId)
                        {
                            temp.Id = stationId;
                            temp.Location = item.Location;
                            if (newName != null)
                                temp.Name = newName;
                            else
                                temp.Name = item.Name;
                            if (numOfChargeSlots != 0)
                            {
                                temp.NumOfChargeSlots = numOfChargeSlots;
                                temp.NumOfAvailableChargeSlots = avialble;
                            }
                            else
                                temp.NumOfChargeSlots = item.NumOfChargeSlots;
                            DataSource.stations[i] = temp;
                        }
                    }
                }
                /// <summary>
                /// return all available to charge stations
                /// </summary>
                public IEnumerable<Station> GetAvailableToChargeStations()
                {
                    int size = DataSource.stations.Count;
                    List<Station> temp = DataSource.stations;
                    for (int i = 0; i < size; i++)
                        //if available for charge
                        if (DataSource.stations[i].NumOfAvailableChargeSlots > 0)
                            temp.Add(DataSource.stations[i]);
                    return temp;
                }

                /// <summary>
                /// return station location by its id
                /// </summary>
                /// <param name="StationId"></param>
                /// <returns></returns>
                public Location StationLocate(int StationId)
                {
                    Location temp = new();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                        if (DataSource.stations[i].Id == StationId)
                        {
                            temp.Latitude = DataSource.stations[i].Location.Latitude;
                            temp.Longitude = DataSource.stations[i].Location.Longitude;
                        }
                    return temp;
                }

                /// <summary>
                /// decrease num of available charge slots
                /// <param name="stationId"></param>
                /// </summary>
                public void DecreaseChargeSlot(int stationId)
                {
                    Station temp = new();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                    {
                        Station item = DataSource.stations[i];
                        if (item.Id == stationId)
                        {
                            temp.Id = stationId;
                            temp.Location = item.Location;
                            temp.Name = item.Name;
                            temp.NumOfChargeSlots = item.NumOfChargeSlots;
                            temp.NumOfAvailableChargeSlots = item.NumOfAvailableChargeSlots - 1;
                            DataSource.stations[i] = temp;
                        }
                    }
                }

                /// <summary>
                /// increase num of available charge slots
                /// <param name="stationId"></param>
                /// </summary>
                public void IncreaseChargeSlot(int stationId)
                {
                    Station temp = new();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                    {
                        Station item = DataSource.stations[i];
                        if (item.Id == stationId)
                        {
                            temp.Id = stationId;
                            temp.Location = item.Location;
                            temp.Name = item.Name;
                            temp.NumOfChargeSlots = item.NumOfChargeSlots;
                            temp.NumOfAvailableChargeSlots = item.NumOfAvailableChargeSlots + 1;
                            DataSource.stations[i] = temp;
                        }
                    }
                }
                /// <summary>
                /// return the distance between two geographic coordinates
                /// </summary>
                /// <param name="a"></param>
                /// <param name="b"></param>
                /// <returns></returns>
                public double GetDistance(Location a, Location b)
                {
                    var d1 = a.Latitude * (Math.PI / 180.0);
                    var num1 = a.Longitude * (Math.PI / 180.0);
                    var d2 = b.Latitude * (Math.PI / 180.0);
                    var num2 = b.Longitude * (Math.PI / 180.0) - num1;
                    var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                        (Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0));

                    return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))) / 1000;
                }
            }
        }
    }
}