using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    for(int i=0;i < IDAL.DO.DalObject.DataSource.stations.Count;i++)
                        if(IDAL.DO.DalObject.DataSource.stations[i].id == myStation.id)
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
                        if (IDAL.DO.DalObject.DataSource.stations[i].id == myId)
                        {
                            isDouble = true;
                            temp = IDAL.DO.DalObject.DataSource.stations[i];
                        }
                    if (isDouble == false)
                        return temp;
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
                            temp.lattitude = IDAL.DO.DalObject.DataSource.stations[i].lattitude;
                            temp.longitude = IDAL.DO.DalObject.DataSource.stations[i].longitude;

                        }
                    }
                    return temp;
                }







                public bool thereAreParcelsThatNotDeliverdYet()
                {
                    var dalParcelsList = getParcels();
                    foreach (var element in dalParcelsList)
                    {
                        DateTime emptyDateTime = new DateTime();
                        if (element.delivered == emptyDateTime) return true;
                    }
                    return false;
                }
                public bool thisDroneIsAssociated(int droneId)
                {
                    var dalParcelsList = getParcels();
                    foreach (var element in dalParcelsList)
                    {
                        if (element.droneId == droneId) return true;
                    }
                    return false;
                }
                public bool myParcelScheduledButNotPickedUp(int parcelId)
                {
                    var dalParcelsList = getParcels();
                    foreach (var element in dalParcelsList)
                    {
                        if (element.id == parcelId)
                        {
                            DateTime emptyDateTime = new DateTime();
                            if (element.pickedUp == emptyDateTime && element.scheduled != emptyDateTime) return true;
                        }
                    }
                    return false;
                }

                public bool myParcelPickedUpButNotDeliverd(int parcelId)
                {
                    var dalParcelsList = getParcels();
                    foreach (var element in dalParcelsList)
                    {
                        if (element.id == parcelId)
                        {
                            DateTime emptyDateTime = new DateTime();
                            if (element.delivered == emptyDateTime && element.pickedUp != emptyDateTime) return true;
                        }
                    }
                    return false;
                }
                public IDAL.DO.Location theNearestToSenderStationLocation(int parcelId)
                {
                    Location tempLocation = new Location();
                    var dalParcelsList = getParcels();
                    foreach (var pElement in dalParcelsList)
                    {
                        if (pElement.id == parcelId)
                        {
                            var customersList = getCustomers();

                            foreach (var cElement in customersList)
                            {

                                (cElement.);
                            }
                        }
                    }
                    return tempLocation;
                }
                public Station theNearestStation(Location l)
                {
                    Station tempStation = new Station();
                    var stationList = getStations();
                    double min = 99999999999;
                    foreach (var element in stationList)
                    {
                        distance(l, element.)
                    }
                }
                public double distance(Location a, Location b)
                {
                    return Math.Sqrt(Math.Pow((b.longitude - a.longitude), 2) -
                        Math.Pow((b.lattitude - a.lattitude), 2));
                }
            }
        }
    }
}