using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            public Random rd = new Random();
            public List<Drone> dronelist = new List<Drone>();
            public IDAL.IDal dal = new IDAL.DO.DalObject.DalObject();
            
            public void addStation(IDAL.DO.Station myStation)
            {
                //checks
                myStation.DronesInCharge.Clear();
                    
                //insert station to array
                dal.addStation(myStation);
                
            }
            public void addDrone(IDAL.DO.Drone myDrone)
            {
                //checks
                myDrone.battery = rd.Next(20, 41);
                myDrone.status = IDAL.DO.MyEnums.DroneStatus.maintenance;
                myDrone.location = IDAL.DO.DalObject. (myDrone.chargeStationId);

               


                dal.addDrone(myDrone);
            }

            public void addcustomer(IDAL.DO.Customer myCustomer)
            {
                //checks


                dal.addcustomer(myCustomer);
            }
            public void addParcel(IDAL.DO.Parcel myParcel)
            {
                //checks


                dal.addParcel(myParcel);
            }
        }
    }
}