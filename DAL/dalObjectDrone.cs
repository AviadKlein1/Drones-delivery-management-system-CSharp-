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
                public void addDrone(Drone myDrone)
                {
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.drones.Count; i++)
                        //if drone already exist
                        if (IDAL.DO.DalObject.DataSource.drones[i].id == myDrone.id)
                            throw new ExcistingIdException(myDrone.id, $"drone already exist: {myDrone.id}");
                    //insert drone to list
                    IDAL.DO.DalObject.DataSource.drones.Add(myDrone);
                }

                /// <summary>
                /// returns a drone by its id
                /// </summary>
                /// <param name="myId"></param>
                /// <returns></returns>
                public IDAL.DO.Drone getDrone(int myId)
                {
                    bool isDouble = false;
                    Drone temp = new Drone();
                    for (int i = 0; i < IDAL.DO.DalObject.DataSource.drones.Count; i++)
                    {
                        if (IDAL.DO.DalObject.DataSource.drones[i].id == myId)
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
                /// print all drones
                /// </summary>
                public IEnumerable<Drone> getDrones()
                {
                    List<IDAL.DO.Drone> temp = new List<IDAL.DO.Drone>();
                    temp = IDAL.DO.DalObject.DataSource.drones;
                    return temp;
                }
                /// <summary>
                /// send drone to charge slot
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="stationId"></param>
                public void sendToCharge(int droneId, int stationId)
                {
                    //    //create a new item "drone charge"
                    //    IDAL.DO.DroneCharge myDroneCharge = new DroneCharge();
                    //    int j = 0;
                    //    while (IDAL.DO.DalObject.DataSource.drones[j].id != droneId)
                    //        j++;
                    //    //update drone status - maintenance
                    //    IDAL.DO.DalObject.DataSource.drones[j].status = MyEnums.DroneStatus.maintenance;
                    //    myDroneCharge.droneId = IDAL.DO.DalObject.DataSource.drones[j].id;

                    //    //int k = 0;
                    //    //while (IDAL.DO.DalObject.DataSource.stations[k].id != stationId)
                    //    //    k++;

                    //    //myDroneCharge.stationId = IDAL.DO.DalObject.DataSource.stations[k].id;
                    //    for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    //    {
                    //        if (IDAL.DO.DalObject.DataSource.stations[i].id == stationId)
                    //        {
                    //            IDAL.DO.Station temp = IDAL.DO.DalObject.DataSource.stations[i];
                    //            temp.id = stationId;
                    //            //update number of available charge slots in station
                    //            temp.numOfAvailableChargeSlots--;
                    //            IDAL.DO.DalObject.DataSource.stations[i] = temp;
                    //        }
                    //    }
                    //    IDAL.DO.DalObject.DataSource.droneCharges.Add( myDroneCharge);
                }
                /// <summary>
                /// report drone ended charging
                /// </summary>
                /// <param name="droneId"></param>
                /// <param name="stationId"></param>
                public void endCharge(int droneId, int stationId)
                {
                    //int j = 0;
                    //while (IDAL.DO.DalObject.DataSource.drones[j].id != droneId)
                    //    j++;
                    ////update drone status to available
                    //IDAL.DO.DalObject.DataSource.drones[j].status = MyEnums.DroneStatus.available;

                    ////int k = 0;
                    ////while (IDAL.DO.DalObject.DataSource.stations[k].id != stationId)
                    ////    k++;
                    //for (int i = 0; i < IDAL.DO.DalObject.DataSource.stations.Count; i++)
                    //{
                    //    if (IDAL.DO.DalObject.DataSource.stations[i].id == stationId)
                    //    {
                    //        IDAL.DO.Station temp = IDAL.DO.DalObject.DataSource.stations[i];
                    //        //update number of available charge slots in station
                    //        temp.numOfAvailableChargeSlots++;
                    //        IDAL.DO.DalObject.DataSource.stations[i] = temp;
                    //    }
                    //}
                }

                /// <summary>
                /// returns an array contains electricity consumption data
                /// </summary>
                /// <returns></returns> array of double nums (battery % per distance)
                public double[] droneElectricityConsumption()
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