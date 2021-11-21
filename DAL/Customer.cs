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
        public struct Customer
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phoneNumber { get; set; }
            public Location location { get; set; }
        }
    }
}