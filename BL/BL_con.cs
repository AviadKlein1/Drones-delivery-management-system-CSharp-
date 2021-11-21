using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public Random rd = new Random();

            public List<DronesToList> dronesList;

            public IDAL.IDal dal = new IDAL.DO.DalObject.DalObjectStation();

            public BL()
            {
                //insert all the drones to this list
                var dalDrones = dal.getDrones();
                foreach (var element in dalDrones)
                {
                    DronesToList temp = new DronesToList();
                    temp.id = element.id;
                    temp.model = element.model;
                    temp.weight = element.weight;
                    dronesList.Add(temp);
                    
                }

                foreach (var element in dronesList)
                {
                    if (dal.thisDroneIsAssociated(element.id) && dal.thereAreParcelsThatNotDeliverdYet())
                    {
                        element.status = MyEnums.DroneStatus.maintenance;
                        element.battery = rd.Next(25, 101);

                        if (dal.myParcelScheduledButNotPickedUp(element.deliveredParcelId)) element.location = dal.theNearestToSenderStationLocation(element.deliveredParcelId);
                        if (dal.myParcelPickedUpButNotDeliverd(element.deliveredParcelId)) element.location = dal.theLocationOfThisParcelSender(element.deliveredParcelId);

                    }
                }
            }
        }
    }
}