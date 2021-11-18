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
            public double longitude { get; set; }
            public double lattitude { get; set; }

        }
        //public struct CustomerInParcel
        //{
        //    public int id { get; set; }
        //    public string name { get; set; }
        //}

        //public struct CustomerToList
        //{
        //    public int id { get; set; }
        //    public string name { get; set; }
        //    public string phoneNumber { get; set; }
        //    public int parcelsSendAndDeliverd { get; set; }
        //    public int parcelsSendAndNotDeliverd { get; set; }
        //    public int parcelsRecived { get; set; }
        //    public int parcelsInTheWayToMe { get; set; }
        //}
    }
}