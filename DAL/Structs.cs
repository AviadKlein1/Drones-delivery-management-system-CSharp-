using System;

namespace DAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int NumOfChargeSlots { get; set; }
            public string toString()
            {
                return ("ID: " + Id + "\nname: " + Name + "\nlongitude: " + Longitude + 
                    "\nlattitude: " + Lattitude + "\nnum of charge slots: " + NumOfChargeSlots);
            }
        }
    }
}

//        public struct Station
//        {
//            public string Name { get; set; }
//        }

//        public struct Station
//        {
//            public string Name { get; set; }
//        }

//        public struct Station
//        {
//            public string Name { get; set; }
//        }

//        public struct Station
//        {
//            public string Name { get; set; }
//        }

//        public struct Station
//        {
//            public string Name { get; set; }
//        }


//    }

//    namespace DalObject
//    { 
    
//    }
//}

