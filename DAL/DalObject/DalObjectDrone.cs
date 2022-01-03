using System.Collections.Generic;
using System.Linq;

namespace DalApi
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// contains all functions regards entity drone
            /// </summary>
            internal partial class DalObject : IDal
            {
                /// <summary>
                /// add drone to list of drones
                /// </summary>
                public void AddDrone(Drone myDrone, int firstChargeStationId)
                {
                    bool found = false;
                    for (int i = 0; i < DataSource.stations.Count; i++)
                        if (DataSource.stations[i].Id == firstChargeStationId)
                            found = true;
                    //if not such station exist
                    if (found == false)
                        throw new WrongIdException(firstChargeStationId, $"no such station: {firstChargeStationId}");

                    for (int i = 0; i < DataSource.drones.Count; i++)
                        //if drone already exist
                        if (DataSource.drones[i].Id == myDrone.Id)
                            throw new ExistingIdException(myDrone.Id, $"drone already exist: {myDrone.Id}");
                    //insert drone to list
                    DataSource.drones.Add(myDrone);
                    //find charge slot
                    DecreaseChargeSlot(firstChargeStationId);
                    AddDroneCharge(myDrone.Id, firstChargeStationId);
                }

                public void DeleteDrone(int myId)
                {
                    Drone temp = new();
                    for (int i = 0; i < DataSource.drones.Count; i++)
                    {
                        Drone item = DataSource.drones[i];
                        //search drones
                        if (item.Id == myId)
                        {
                            temp.Id = item.Id;
                            temp.IsActive = false;
                            temp.Model = item.Model;
                            DataSource.drones[i] = temp;
                            return;
                        }
                    }
                    throw new WrongIdException(myId, $"wrong id: {myId}");
                }
                /// <summary>
                /// add drone to list of drone Charge
                /// </summary>
                public void AddDroneCharge(int droneId, int StationId)
                {
                    bool found = false;
                    for (int i = 0; i < DataSource.stations.Count; i++)
                        if (DataSource.stations[i].Id == StationId)
                            found = true;
                    //if not such station exist
                    if (found == false)
                        throw new WrongIdException(StationId, $"no such station: {StationId}");
                    var temp = new DroneCharge(droneId, StationId);
                    DataSource.droneCharges.Add(temp);
                }
                public void EndDroneCharge(int droneId)
                {
                    var temp = new DroneCharge();
                    for (int i = 0; i < DataSource.droneCharges.Count; i++)
                        if (DataSource.droneCharges[i].DroneId == droneId)
                        {
                            temp.DroneId = droneId;
                            temp.StationId = DataSource.droneCharges[i].StationId;
                            temp.IsActive = false;
                        }
                }

                /// <summary>
                /// returns a drone by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public Drone GetDrone(int myId)
                {
                    bool found = false;
                    Drone temp = new();
                    for (int i = 0; i < DataSource.drones.Count; i++)
                        if (DataSource.drones[i].Id == myId && DataSource.drones[i].IsActive)
                        {
                            found = true;
                            temp = DataSource.drones[i];
                        }
                    if (found == false)
                        return temp;
                    //if not found
                    else
                        throw new WrongIdException(myId, $"wrong id: {myId}");
                }

                /// <summary>
                /// return all drones
                /// </summary>
                public IEnumerable<Drone> GetDronesList()
                {
                    List<Drone> temp = new();
                    temp.AddRange(from item in DataSource.drones
                                  where item.IsActive
                                  select item);
                    return temp;
                }
                public IEnumerable<DO.DroneCharge> GetDroneCharges()
                {
                    List<DroneCharge> temp = new();
                    temp.AddRange(from item in DataSource.droneCharges
                                  where item.IsActive
                                  select item);
                    return temp;
                }

                /// <summary>
                /// update model in drone
                /// <param name="droneId"></param>
                /// <param name="newModel"></param>
                /// </summary>
                public void UpdateDrone(int droneId, string newModel)
                {
                    Drone temp = new();
                    for (int i = 0; i < DataSource.drones.Count; i++)
                    {
                        Drone item = DataSource.drones[i];
                        if (item.Id == droneId)
                        {
                            temp.Id = droneId;
                            temp.Model = newModel;
                            temp.Weight = item.Weight;
                            DataSource.drones[i] = temp;
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
                    droneElectricityConsumption[0] = DataSource.Config.free;
                    droneElectricityConsumption[1] = DataSource.Config.lightWeight;
                    droneElectricityConsumption[2] = DataSource.Config.mediumWeight;
                    droneElectricityConsumption[3] = DataSource.Config.heavyWeight;
                    droneElectricityConsumption[4] = DataSource.Config.DroneLoadRate;

                    return droneElectricityConsumption;
                }
            }
        }
    }
}