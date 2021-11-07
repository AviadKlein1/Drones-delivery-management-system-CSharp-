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
        public partial class BL : IBL
        {
            public Random rd = new Random();

            public List<DronesToList> dronelist = new List<DronesToList>();

            public IDAL.IDal dal;

            public BL()
            {
                this.dal = new IDAL.DO.DalObject.DalObject();
            }
        
        }
    }
}