using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// contains item updating functions
        /// </summary>
        public partial class BL : IBl
        {
            /// <summary>
            /// updates drone data
            /// </summary>
            /// <param name="droneId"></param>
            /// <param name="newModel"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool UpdateDrone(int droneId, string newModel)
            {
                lock (dal)
                {
                    bool found = false;
                    System.Collections.Generic.IEnumerable<DalApi.DO.Drone> dalDronesList = dal.GetDronesList();
                    foreach (var _ in from DalApi.DO.Drone item in dalDronesList
                                      where item.Id == droneId
                                      select new { })
                    {
                        found = true;
                        break;
                    }

                    if (found)
                    {
                        dal.UpdateDrone(droneId, newModel);
                        foreach (var dItem in from DroneToList dItem in dronesList
                                              where dItem.Id == droneId
                                              select dItem)
                        {
                            dItem.Model = newModel;
                            break;
                        }
                    }
                    else
                        throw new WrongIdException(droneId, $"wrong id: {droneId}");
                    return found;
                }
            }

            /// <summary>
            /// update station details
            /// </summary>
            /// <param name="stationId"></param>
            /// <param name="newName"></param>
            /// <param name="numOfChargeSlots"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool UpdateStation(int stationId, string newName, int numOfChargeSlots, int avialble)
            {
                lock (dal)
                {
                    bool found = false;
                    var dalStationsList = dal.GetStationsList();
                    foreach (var _ in
                    //search station
                    from item in dalStationsList
                    where item.Id == stationId
                    select new { })
                    {
                        found = true;
                        break;
                    }

                    if (found)
                        dal.UpdateStation(stationId, newName, numOfChargeSlots, avialble);
                    else
                        throw new WrongIdException(stationId, $"wrong id: {stationId}");
                    return found;
                }
            }

            /// <summary>
            /// update customer details
            /// </summary>
            /// <param name="customerId"></param>
            /// <param name="newName"></param>
            /// <param name="newPhone"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool UpdateCustomer(int customerId, string newName, string newPhone)
            {
                lock (dal)
                {
                    var found = false;
                    var dalCustomersList = dal.GetCustomersList();
                    foreach (var _ in
                    //search customer
                    from item in dalCustomersList
                    where item.Id == customerId
                    select new { })
                    {
                        found = true;
                        break;
                    }

                    if (found)
                        dal.UpdateCustomer(customerId, newName, newPhone);
                    else
                        throw new WrongIdException(customerId, $"wrong id: {customerId}");
                    return found;
                }
            }

            /// <summary>
            /// get drone and ssend it to charge. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool ChargeDrone(int droneId)
            {
                lock (dal)
                {
                    bool found = false;
                    var v = dronesList;
                    foreach (var item in
                    //search drone
                    from DroneToList item in v
                    where item.Id == droneId
                    select item)
                    {
                        if (item.Status != MyEnums.DroneStatus.available)
                            throw new OccupiedDroneException(droneId, $"wrong id: { droneId }");
                        //is available
                        else
                        {
                            DalApi.DO.Location itemLocation = new(item.Location.Longitude, item.Location.Latitude);
                            var tempStation = NearestReachableChargeSlot(itemLocation, droneId);
                            //if all stations are occupied
                            if (tempStation.Id == 0)
                            {
                                Console.WriteLine("not available station to charge drone\n");
                                break;
                            }
                            // send drone to charge
                            dal.DecreaseChargeSlot(tempStation.Id);
                            foreach (var dItem in from dItem in dronesList
                                                  where dItem.Id == droneId
                                                  select dItem)
                            {
                                dItem.Status = MyEnums.DroneStatus.maintenance;
                                dItem.Location = new Location(tempStation.Location);
                                //battery
                                var dis = dal.GetDistance(itemLocation, tempStation.Location);
                                var neededBattery = (int)BatteryRequirementForVoyage(droneId, dis);
                                dItem.Battery -= neededBattery;
                                found = true;
                            }

                            dal.AddDroneCharge(droneId, tempStation.Id, DateTime.Now);
                        }
                    }

                    return found;
                }
            }

            /// <summary>
            /// end drone charge. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <param name="chargeTime"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool ReleaseDroneFromCharge(int droneId)
            {
                lock (dal)
                {
                    bool found = false;
                    var v = dronesList;
                    foreach (var item in
                    //search drone
                    from item in v
                    where item.Id == droneId
                    select item)
                    {
                        if (item.Status != MyEnums.DroneStatus.maintenance)
                        {
                            throw new WrongIdException
                                (droneId, $"wrong id, drone is not charging currently: {droneId}");
                        }
                        // in maintenance
                        else
                        {
                            DalApi.DO.Location itemLocation = new(item.Location.Latitude, item.Location.Longitude);
                            var chargeTime = dal.EndDroneCharge(droneId, DateTime.Now);

                            var tempStation = NearestStation(itemLocation);
                            dal.IncreaseChargeSlot(tempStation.Id);
                            foreach (var dItem in from dItem in dronesList
                                                  where dItem.Id == droneId
                                                  select dItem)
                            {
                                dItem.Status = MyEnums.DroneStatus.available;
                                //battery
                                double newBattery = dItem.Battery += (int)(DroneLoadRate * chargeTime);
                                dItem.Battery = newBattery > 100 ? 100 : newBattery;
                                found = true;
                            }
                        }
                    }

                    return found;
                }
            }

            /// <summary>
            /// associate drone with parcel. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool ScheduleParcelToDrone(int droneId)
            {

                bool found = false;
                MyEnums.DroneStatus droneStatus = new MyEnums.DroneStatus();
                int parcelId = 0;
                var v = dronesList;
                foreach (var drone in
                //search drone
                from DroneToList drone in v
                where drone.Id == droneId
                select drone)
                {
                    //if (drone.Status == MyEnums.DroneStatus.delivery)
                    //    throw new OccupiedDroneException(droneId, $"drone is occupied, try another: {droneId}");
                    ////is available 
                    //else
                    //{
                        int newParcelId = MostSuitableParcel(droneId);
                        if (newParcelId == 0)
                            break;
                        else
                        {
                            found = true;
                            droneStatus = MyEnums.DroneStatus.delivery;
                            parcelId = newParcelId;
                        }

                    //}
                }
                if (found) _ScheduleParcelToDrone(droneId, droneStatus, parcelId);
                return found;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            internal void _ScheduleParcelToDrone(int droneId, MyEnums.DroneStatus droneStatus, int parcelId)
            {
                lock (dal)
                {
                    foreach (var item in dronesList)
                    {
                        if (item.Id == droneId)
                        {
                            item.Status = droneStatus;
                            item.DeliveredParcelId = parcelId;
                            dal.ScheduleParcelToDrone(parcelId, droneId);
                        }
                    }
                }
            }

            /// <summary>
            /// pick up parcel from sender. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool PickUpParcel(int droneId)
            {

                lock (dal)
                {
                    bool found = false;
                    bool droneExistFlag = false;
                    var v = dronesList;
                    int idOfThisParcel = 0;
                    DalApi.DO.Location ourSenderLocation = new();
                    foreach (var item in
                    //search drone
                    from item in v
                    where item.Id == droneId
                    select item)
                    {
                        idOfThisParcel = item.DeliveredParcelId;
                        droneExistFlag = true;
                    }

                    if (droneExistFlag == false)
                        throw new WrongIdException(droneId, $"wrong id: {droneId}");
                    if (ScheduledButNotPickedUp(idOfThisParcel))
                    {
                        var parcelsList = dal.GetParcelsList();
                        foreach (var item in from item in parcelsList//find parcel
                                             where item.Id == idOfThisParcel
                                             select item)
                        {
                            //our parcel belong to our drone
                            if (item.DroneId == droneId)
                            {
                                ourSenderLocation = SenderLocation(item.Id);
                                //update parcel
                                dal.PickUpParcel(droneId, item.Id);
                                found = true;
                                //update drone
                                DroneToList temp = new();
                                for (int i = 0; i < v.Count; i++)
                                {
                                    DroneToList dItem = v[i];
                                    if (dItem.Id == droneId)
                                    {
                                        DalApi.DO.Location earlyDroneLocation =
                                            new(dItem.Location.Longitude, dItem.Location.Latitude);
                                        temp.Id = dItem.Id;
                                        temp.Model = dItem.Model;
                                        temp.Location = new Location(ourSenderLocation);
                                        temp.Status = dItem.Status;
                                        temp.Battery = dItem.Battery;
                                        temp.Weight = dItem.Weight;
                                        temp.DeliveredParcelId = item.Id;

                                        //uodate location
                                        temp.Location.Longitude = ourSenderLocation.Longitude;
                                        temp.Location.Latitude = ourSenderLocation.Latitude;
                                        //update battery
                                        temp.Battery -= (int)BatteryRequirementForVoyage(droneId, dal.GetDistance(earlyDroneLocation, ourSenderLocation));
                                        if (temp.Battery < 0) temp.Battery = 0;
                                        v[i] = temp;
                                    }
                                }
                            }
                            else
                                Console.WriteLine("not our parcel");
                        }
                    }
                    return found;
                }
            }

            /// <summary>
            /// deliver parcel to receiver
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns> returns a boolean type to determine wether
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool DeliverParcel(int droneId)
            {
                lock (dal)
                {
                    bool flag = false;
                    var droneExistFlag = false;
                    var v = dronesList;
                    int idOfThisParcel = 0;
                    DalApi.DO.Location ourReciverLocation = new();
                    foreach (var item in
                    //search drone
                    from item in v
                    where item.Id == droneId
                    select item)
                    {
                        idOfThisParcel = item.DeliveredParcelId;
                        droneExistFlag = true;
                    }

                    if (droneExistFlag == false)
                        throw new WrongIdException(droneId, $"wrong id: { droneId }");
                    if (!PickedUpButNotDelivered(idOfThisParcel))
                        Console.WriteLine("this parcel is not in the right status\n");
                    var parcelsList = dal.GetParcelsList();
                    foreach (var item in from item in parcelsList//find our parcel
                                         where item.Id == idOfThisParcel
                                         select item)
                    {
                        // our parcel belong to our drone
                        if (item.DroneId == droneId)
                        {
                            ourReciverLocation = ReceiverLocation(item.Id);
                            //update parcel
                            dal.DeliverParcel(droneId, item.Id);
                            flag = true;
                            //update drone
                            DroneToList temp = new();
                            for (int i = 0; i < v.Count; i++)
                            {
                                DroneToList dItem = v[i];
                                if (dItem.Id == droneId)
                                {
                                    temp.Id = dItem.Id;
                                    temp.Model = dItem.Model;
                                    temp.Location = new Location(ourReciverLocation);
                                    temp.Status = MyEnums.DroneStatus.available;
                                    temp.Weight = dItem.Weight;
                                    temp.Battery = dItem.Battery;
                                    temp.DeliveredParcelId = 0;
                                    DalApi.DO.Location earlyDroneLocation = new(dItem.Location.Longitude, dItem.Location.Latitude);
                                    // update battery
                                    temp.Battery -= (int)BatteryRequirementForVoyage
                                        (droneId, dal.GetDistance(earlyDroneLocation, ourReciverLocation));
                                    if (temp.Battery < 0) temp.Battery = 0;
                                    v[i] = temp;
                                }
                            }
                        }
                        else Console.WriteLine("not our parcel");
                    }

                    var g = ChargeDrone(droneId);
                    return flag;
                }
            }
            #region |for Simulation|
            /// <summary>
            /// pick up parcel from sender. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool PickUpParcel(int droneId, double _droneSpeed, Action Worker_ProgressChanged)
            {

                lock (dal)
                {
                    BlApi.BO.DroneToList dtl = new();
                    double earlyBattery = 0;
                    double droneSpeed = _droneSpeed;
                    double tick = 0.5;
                    double distance = 0;
                    bool found = false;
                    bool droneExistFlag = false;
                    var v = dronesList;
                    int idOfThisParcel = 0;
                    DalApi.DO.Location ourSenderLocation = new();

                    foreach (var item in
                    //search drone
                    from item in v
                    where item.Id == droneId
                    select item)
                    {
                        idOfThisParcel = item.DeliveredParcelId;
                        droneExistFlag = true;
                    }

                    if (droneExistFlag == false)
                        throw new WrongIdException(droneId, $"wrong id: {droneId}");
                    if (ScheduledButNotPickedUp(idOfThisParcel))
                    {
                        var parcelsList = dal.GetParcelsList();
                        foreach (var item in from item in parcelsList//find parcel
                                             where item.Id == idOfThisParcel
                                             select item)
                        {
                            //our parcel belong to our drone
                            if (item.DroneId == droneId)
                            {
                                ourSenderLocation = SenderLocation(item.Id);
                                //update parcel
                                dal.PickUpParcel(droneId, item.Id);
                                found = true;
                                //update drone
                                DroneToList temp = new();

                                for (int i = 0; i < v.Count; i++)
                                {
                                    DroneToList dItem = v[i];
                                    if (dItem.Id == droneId)
                                    {
                                        earlyBattery = dItem.Battery;
                                        DalApi.DO.Location earlyDroneLocation = new(dItem.Location.Longitude, dItem.Location.Latitude);
                                        var senderLocat = new Location(ourSenderLocation);
                                        distance = (dal.GetDistance(earlyDroneLocation, ourSenderLocation))/100;

                                        double time = (distance / droneSpeed);//time = way/speed represented in seconds
                                        var TicksNum = time / tick;
                                        var DistanceForTick = (droneSpeed*tick);
                                        var BatteryforAllJourny = (int)BatteryRequirementForVoyage(droneId, distance * 100);
                                        var BatteryForTick = BatteryforAllJourny / TicksNum;
                                        for (int k = 0; k < (int)TicksNum; k++)
                                        {
                                            temp.Id = dItem.Id;
                                            temp.Model = dItem.Model;
                                            temp.Status = dItem.Status;
                                            temp.Battery = dItem.Battery;
                                            temp.Weight = dItem.Weight;
                                            temp.DeliveredParcelId = item.Id;
                                            //uodate location
                                            temp.Location = new Location(dItem.Location.Latitude + Math.Abs(dItem.Location.Latitude - ourSenderLocation.Latitude),
                                                dItem.Location.Longitude + Math.Abs(dItem.Location.Longitude - ourSenderLocation.Longitude));
                                            ////update battery
                                            temp.Battery = dItem.Battery - BatteryForTick;
                                            if (temp.Battery < 0) temp.Battery = 0;
                                            v[i] = temp;
                                            Worker_ProgressChanged(/*temp*/);
                                        }
                                        temp.Id = dItem.Id;
                                        temp.Model = dItem.Model;
                                        temp.Battery = dItem.Battery;
                                        temp.Status = dItem.Status;
                                        temp.Weight = dItem.Weight;
                                        temp.DeliveredParcelId = item.Id;
                                        //uodate location
                                        temp.Location = new Location(ourSenderLocation.Latitude, ourSenderLocation.Longitude);

                                        ////update battery
                                        temp.Battery = earlyBattery - BatteryRequirementForVoyage(droneId, distance);
                                        if (temp.Battery < 0) temp.Battery = 0;
                                        v[i] = temp;
                                        dtl = temp;

                                    }
                                }
                            }
                            else
                                Console.WriteLine("not our parcel");
                        }
                    }
                    return found;
                }
            }

            /// <summary>
            /// deliver parcel to receiver
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns> returns a boolean type to determine wether
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool DeliverParcel(int droneId, double _droneSpeed, Action Worker_ProgressChanged)
            {
                lock (dal)
                {
                    double earlyBattery = 0;
                    double droneSpeed = _droneSpeed;
                    double tick = 0.5;
                    double distance = 0;
                    bool flag = false;
                    var droneExistFlag = false;
                    var v = dronesList;
                    int idOfThisParcel = 0;
                    DalApi.DO.Location ourReciverLocation = new();
                    foreach (var item in
                    //search drone
                    from item in v
                    where item.Id == droneId
                    select item)
                    {
                        idOfThisParcel = item.DeliveredParcelId;
                        droneExistFlag = true;
                    }

                    if (droneExistFlag == false)
                        throw new WrongIdException(droneId, $"wrong id: { droneId }");
                    if (!PickedUpButNotDelivered(idOfThisParcel))
                        Console.WriteLine("this parcel is not in the right status\n");
                    var parcelsList = dal.GetParcelsList();
                    foreach (var item in from item in parcelsList//find our parcel
                                         where item.Id == idOfThisParcel
                                         select item)
                    {
                        // our parcel belong to our drone
                        if (item.DroneId == droneId)
                        {
                            ourReciverLocation = ReceiverLocation(item.Id);
                            //update parcel
                            dal.DeliverParcel(droneId, item.Id);
                            flag = true;
                            //update drone
                            DroneToList temp = new();

                            for (int i = 0; i < v.Count; i++)
                            {
                                DroneToList dItem = v[i];
                                if (dItem.Id == droneId)
                                {
                                    earlyBattery = dItem.Battery;
                                    DalApi.DO.Location earlyDroneLocation = new(dItem.Location.Longitude, dItem.Location.Latitude);
                                    var senderLocat = new Location(ourReciverLocation);
                                    distance = dal.GetDistance(earlyDroneLocation, ourReciverLocation);
                                    double time = distance / droneSpeed;//time = way/speed represented in seconds
                                    var TicksNum = time / tick;
                                    var DistanceForTick = droneSpeed * 0.5;
                                    var BatteryforAllJourny = (int)BatteryRequirementForVoyage(droneId, distance);
                                    var BatteryForTick = BatteryforAllJourny / TicksNum;

                                    for (int k = 0; k < (int)TicksNum; k++)
                                    {
                                        temp.Id = dItem.Id;
                                        temp.Model = dItem.Model;
                                        temp.Status = dItem.Status;
                                        temp.Battery = dItem.Battery;

                                        temp.Weight = dItem.Weight;
                                        temp.DeliveredParcelId = item.Id;
                                        //uodate location
                                        temp.Location = new Location(dItem.Location.Latitude + Math.Abs(dItem.Location.Latitude - ourReciverLocation.Latitude),
                                            dItem.Location.Longitude + Math.Abs(dItem.Location.Longitude - ourReciverLocation.Longitude));

                                        ////update battery
                                        temp.Battery = dItem.Battery-BatteryForTick;
                                        if (temp.Battery < 0) temp.Battery = 0;
                                        v[i] = temp;
                                        //Thread.CurrentThread.Join(50);
                                        Worker_ProgressChanged();
                                    }
                                    temp.Id = dItem.Id;
                                    temp.Model = dItem.Model;
                                    temp.Battery = dItem.Battery;
                                    temp.Status = dItem.Status;
                                    temp.Weight = dItem.Weight;
                                    temp.DeliveredParcelId = item.Id;
                                    //uodate location
                                    temp.Location = new Location(ourReciverLocation.Latitude, ourReciverLocation.Longitude);
                                    ////update battery
                                    temp.Battery =  earlyBattery -BatteryRequirementForVoyage(droneId, distance);
                                    if (temp.Battery < 0) temp.Battery = 0;
                                    v[i] = temp;
                                }
                            }
                        }
                        else Console.WriteLine("not our parcel");
                    }

                    var g = ChargeDrone(droneId, droneSpeed, Worker_ProgressChanged);
                    return flag;
                }
            }
            /// <summary>
            /// get drone and ssend it to charge. if succsses return true
            /// </summary>
            /// <param name="droneId"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool ChargeDrone(int droneId, double _droneSpeed, Action Worker_ProgressChanged)
            {
                lock (dal)
                {
                    DroneToList temp = new();
                    bool found = false;
                    double droneSpeed = _droneSpeed;
                    double earlyBattery = 0;
                    double tick = 0.5;
                    double distance = 0;
                    var v = dronesList;
                    DalApi.DO.Location itemLocation = new DalApi.DO.Location();
                    //search drone
                    foreach (var item in v)
                    {
                        if (item.Id == droneId)
                        {
                            itemLocation = new(item.Location.Longitude, item.Location.Latitude);
                            found = true;
                        }
                    }

                    var tempStation = NearestReachableChargeSlot(itemLocation, droneId);
                    //if all stations are occupied
                    if (tempStation.Id == 0)
                    {
                        throw new Exception("not available station to charge drone\n");
                    }
                    // send drone to charge
                    dal.DecreaseChargeSlot(tempStation.Id);
                    //
                    for (int i = 0; i < v.Count; i++)
                    {
                        DroneToList dItem = v[i];
                        if (dItem.Id == droneId)
                        {
                            earlyBattery = dItem.Battery;
                            DalApi.DO.Location earlyDroneLocation = new(dItem.Location.Longitude, dItem.Location.Latitude);
                            var chargeSlotLocation = new DalApi.DO.Location(tempStation.Location.Latitude, tempStation.Location.Longitude);
                            distance = dal.GetDistance(earlyDroneLocation, chargeSlotLocation);

                            var time = distance / droneSpeed;//time = way/speed represented in seconds
                            var TicksNum = time / tick;
                            var DistanceForTick = droneSpeed * 0.5;
                            var BatteryforAllJourny = (int)BatteryRequirementForVoyage(droneId, distance);
                            var BatteryForTick = BatteryforAllJourny / TicksNum;
                            for (int k = 0; k < (int)TicksNum; k++)
                            {
                                temp.Id = dItem.Id;
                                temp.Model = dItem.Model;
                                temp.Status = MyEnums.DroneStatus.maintenance;
                                temp.Battery = dItem.Battery;

                                temp.Weight = dItem.Weight;
                                temp.DeliveredParcelId = dItem.Id;
                                //uodate location

                                temp.Location = new Location(dItem.Location.Latitude + Math.Abs(dItem.Location.Latitude - chargeSlotLocation.Latitude),
                                    dItem.Location.Latitude + Math.Abs(dItem.Location.Longitude - chargeSlotLocation.Longitude));
                                ////update battery
                                temp.Battery = dItem.Battery - BatteryForTick;
                                if (temp.Battery < 0) temp.Battery = 0;
                                v[i] = temp;
                            }
                            temp.Id = dItem.Id;
                            temp.Model = dItem.Model;
                            temp.Status = dItem.Status;
                            temp.Battery = dItem.Battery;
                            temp.Weight = dItem.Weight;
                            temp.DeliveredParcelId = dItem.DeliveredParcelId;
                            //uodate location
                            temp.Location = new Location(tempStation.Location.Latitude, tempStation.Location.Longitude);

                            ////update battery
                            temp.Battery = earlyBattery - BatteryRequirementForVoyage(droneId, distance);
                            if (temp.Battery < 0) temp.Battery = 0;
                            v[i] = temp;
                            Worker_ProgressChanged(/*temp*/);
                        }
                    }
                    dal.AddDroneCharge(droneId, tempStation.Id, DateTime.Now);
                return found;
                }
            }
            #endregion
        }
    }
}