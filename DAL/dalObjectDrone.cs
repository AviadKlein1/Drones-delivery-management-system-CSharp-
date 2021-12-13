using System.Collections.Generic;
using DalApi;
namespace DalApi
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// entity drone
            /// </summary>
            partial class DalObject : IDal
            {
                /// <summary>
                /// add drone to list of drones
                /// </summary>
                public void AddDrone(Drone myDrone, int firstChargeStationId)
                {
                    bool flag = false;
                    for (int i = 0; i < DalApi.DO.DalObject.DataSource.stations.Count; i++)
                        //if not such station exist
                        if (DalApi.DO.DalObject.DataSource.stations[i].Id == firstChargeStationId)
                        {
                            flag = true;
                        }
                    if(flag == false) throw new WrongIdException(firstChargeStationId, $"no such station: {firstChargeStationId}");

                    for (int i = 0; i < DalApi.DO.DalObject.DataSource.drones.Count; i++)
                        //if drone already exist
                        if(DalApi.DO.DalObject.DataSource.drones[i].Id == myDrone.Id)
                            throw new ExcistingIdException(myDrone.Id, $"drone already exist: {myDrone.Id}");
                    //insert drone to list
                    DalApi.DO.DalObject.DataSource.drones.Add(myDrone);
                    // find charge slot
                    DecriseChargeSlot(firstChargeStationId);
                }

                /// <summary>
                /// returns a drone by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public DalApi.DO.Drone GetDrone(int myId)
                {
                    bool isDouble = false;
                    Drone temp = new Drone();
                    for (int i = 0; i < DalApi.DO.DalObject.DataSource.drones.Count; i++)
                    {
                        if (DalApi.DO.DalObject.DataSource.drones[i].Id == myId)
                        {
                            isDouble = true;
                            temp = DalApi.DO.DalObject.DataSource.drones[i];
                        }
                    }
                    if (isDouble == false)
                        return temp;
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }

                /// <summary>
                /// return all drones
                /// </summary>
                public IEnumerable<Drone> GetDrones()
                {
                    List<DalApi.DO.Drone> temp = new List<DalApi.DO.Drone>();
                    temp = DalApi.DO.DalObject.DataSource.drones;
                    return temp;
                }
                /// <summary>
                /// update model in drone
                /// <param name="droneId"></param>
                /// <param name="newModel"></param>
                /// </summary>
                public void UpdateDrone(int droneId, string newModel)
                {
                    Drone temp = new Drone();
                    for (int i = 0; i < DataSource.drones.Count; i++)
                    {
                        Drone item = DalApi.DO.DalObject.DataSource.drones[i];
                        if (item.Id == droneId)
                        {
                            temp.Id = droneId;
                            temp.Model = newModel;
                            temp.weight = item.weight;
                            DalApi.DO.DalObject.DataSource.drones[i] = temp;
                        }
                    }
                }
              
                /// <summary>
                /// returns an array contains electricity consumption data
                /// </summary>
                /// <returns></returns> array of double nums (battery % per distance)
                public double[] DroneElectricityConsumption()
                {
                    double[] droneElectricityConsumption = new double[5];
                    droneElectricityConsumption[0] = DalApi.DO.DalObject.DataSource.Config.free;
                    droneElectricityConsumption[1] = DalApi.DO.DalObject.DataSource.Config.lightWeight;
                    droneElectricityConsumption[2] = DalApi.DO.DalObject.DataSource.Config.mediumWeight;
                    droneElectricityConsumption[3] = DalApi.DO.DalObject.DataSource.Config.heavyWeight;
                    droneElectricityConsumption[4] = DalApi.DO.DalObject.DataSource.Config.DroneLoadRate;

                    return droneElectricityConsumption;
                }
            }
        }
    }
}