using System;
using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            public partial class DalObject : IDal
            {

                public DalObject()
                {
                    IDAL.DO.DalObject.DataSource.Initialize();
                    
                }
                /// <summary>
                /// add to array
                /// </summary>
                public void addStation(Station myStation)
                {
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                        if (IDAL.DO.DalObject.DataSource.stations[i].id == myStation.id)
                            throw new ExcistingIdException(myStation.id, $"station already exist: {myStation.id}");
                    //insert station to list
                    IDAL.DO.DalObject.DataSource.stations.Add(myStation);
                }
                /// <summary>
                /// belong parcel to specific drone
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
                /// print stations details
                /// </summary>
                /// <param name="myId"></param>
                public Station getStation(int myId)
                {
                    bool isDouble = false;
                    Station temp = new Station();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.stations[i].id == myId)
                        {
                            isDouble = true;
                            temp = IDAL.DO.DalObject.DataSource.stations[i];
                        }
                    }
                    if (isDouble == true)
                    {
                        return temp;
                    }
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }
                /// <summary>
                /// print all stations
                /// </summary>
                public IEnumerable<Station> getStations()
                {
                    List<IDAL.DO.Station> temp = new List<IDAL.DO.Station>();
                    temp = IDAL.DO.DalObject.DataSource.stations;
                    return temp;
                }
                public void updatStation(int stationId, string newName, int numOfChargeSlots)
                {
                    Station temp = new Station();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                    {
                        Station item = IDAL.DO.DalObject.DataSource.stations[i];
                        if (item.id == stationId)
                        {
                            temp.id = stationId;
                            temp.location = item.location;
                            if (newName != null) temp.name = newName;
                            else temp.name = item.name;
                            if (numOfChargeSlots != 0) temp.numOfChargeSlots = numOfChargeSlots;
                            else temp.numOfChargeSlots = item.numOfChargeSlots;
                            IDAL.DO.DalObject.DataSource.stations[i] = temp;
                        }
                    }
                }
                /// <summary>
                /// print all available to charge stations
                /// </summary>
                public IEnumerable<Station> getAvailableToChargeStations()
                {
                    int size = IDAL.DO.DalObject.DataSource.stations.Count;
                    List<IDAL.DO.Station> temp = new List<IDAL.DO.Station>();
                    temp = IDAL.DO.DalObject.DataSource.stations;
                    for (int i = 0; i < size; i++)
                        if (IDAL.DO.DalObject.DataSource.stations[i].numOfAvailableChargeSlots > 0)
                            temp.Add(IDAL.DO.DalObject.DataSource.stations[i]);
                    return temp;
                }
                public IDAL.DO.Location stationLocate(int StationId)
                {
                    Location temp = new Location();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.stations[i].id == StationId)
                        {
                            temp.lattitude = IDAL.DO.DalObject.DataSource.stations[i].location.lattitude;
                            temp.longitude = IDAL.DO.DalObject.DataSource.stations[i].location.longitude;

                        }
                    }
                    return temp;
                }
                public void decriseChargeSlot(int stationId)
                {
                    Station temp = new Station();
                    for (int i = 0; i < DataSource.stations.Count; i++)
                    {
                        Station item = IDAL.DO.DalObject.DataSource.stations[i];
                        if (item.id == stationId)
                        {
                            temp.id = stationId;
                            temp.location = item.location;
                            temp.name = item.name;
                            temp.numOfChargeSlots = item.numOfChargeSlots - 1;
                            IDAL.DO.DalObject.DataSource.stations[i] = temp;
                        }
                    }
                }










                public int distance(Location a, Location b)
                {
                    return (int)Math.Sqrt(Math.Pow((b.longitude - a.longitude), 2) -
                        Math.Pow((b.lattitude - a.lattitude), 2));
                }
            }
        }
    }
}