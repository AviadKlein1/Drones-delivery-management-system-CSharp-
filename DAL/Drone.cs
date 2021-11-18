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
        //public struct DroneInCharge
        //{
        //    public int id { get; set; }
        //    public int battery { get; set; }
        //}
        //public struct DroneInParcel
        //{
        //    public int id { get; set; }
        //    public int battery { get; set; }
        //    public Location location { get; set; }
        //}
        //public struct DronesToList 
        //{
        //    public int id { get; set; }
        //    public string model { get; set; }
        //    public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
        //    public IDAL.DO.MyEnums.DroneStatus status { get; set; }
        //    public int battery { get; set; }
        //    public Location location { get; set; }
        //    public int deliveredParcelId { get; set; }
        //}
    }
}