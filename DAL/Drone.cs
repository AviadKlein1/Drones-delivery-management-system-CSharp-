using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        //item's fields
        public struct Drone
        {
            public int id { get; set; }
            public string model { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
        }
    }
}