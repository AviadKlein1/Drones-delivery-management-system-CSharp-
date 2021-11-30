using System.Collections.Generic;

namespace IDAL
{
    namespace DO
    {
        namespace DalObject
        {
            /// <summary>
            /// entity drone
            /// </summary>
            public partial class DalObject : IDal
            {
                /// <summary>
                /// add drone to list of drones
                /// </summary>
                public void AddDrone(Drone myDrone, int firstChargeStationId)
                {
                    for(int i = 0; i < IDAL.DO.DalObject.DataSource.drones.Count; i++)
                        //if drone already exist
                        if(IDAL.DO.DalObject.DataSource.drones[i].Id == myDrone.Id)
                            throw new ExcistingIdException(myDrone.Id, $"drone already exist: {myDrone.Id}");
                    //insert drone to list
                    IDAL.DO.DalObject.DataSource.drones.Add(myDrone);
                    // find charge slot
                    DecriseChargeSlot(firstChargeStationId);
                }

                /// <summary>
                /// returns a drone by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public IDAL.DO.Drone GetDrone(int myId)
                {
                    bool isDouble = false;
                    Drone temp = new Drone();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.drones.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.drones[i].Id == myId)
                        {
                            isDouble = true;
                            temp = IDAL.DO.DalObject.DataSource.drones[i];
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
                    List<IDAL.DO.Drone> temp = new List<IDAL.DO.Drone>();
                    temp = IDAL.DO.DalObject.DataSource.drones;
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
                        Drone item = IDAL.DO.DalObject.DataSource.drones[i];
                        if (item.Id == droneId)
                        {
                            temp.Id = droneId;
                            temp.Model = newModel;
                            temp.weight = item.weight;
                            IDAL.DO.DalObject.DataSource.drones[i] = temp;
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
                    droneElectricityConsumption[0] = IDAL.DO.DalObject.DataSource.Config.free;
                    droneElectricityConsumption[1] = IDAL.DO.DalObject.DataSource.Config.lightWeight;
                    droneElectricityConsumption[2] = IDAL.DO.DalObject.DataSource.Config.mediumWeight;
                    droneElectricityConsumption[3] = IDAL.DO.DalObject.DataSource.Config.heavyWeight;
                    droneElectricityConsumption[4] = IDAL.DO.DalObject.DataSource.Config.DroneLoadRate;

                    return droneElectricityConsumption;
                }
            }
        }
    }
}