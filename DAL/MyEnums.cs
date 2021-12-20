namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// defines enums for items' fields - weight, priority, status.
        /// </summary>
        public class MyEnums
        {
            public enum PriorityLevel { regular, quickly, urgent };
            public enum WeightCategory { light, medium, heavy };
            public enum ParcelStatus { requested, scheduled, pickedUp, delivered };
            public static int[] ForRd = { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 };

            public static string[] NamesOfCustomers =
                { "Yael Katz",
                  "Yossi Mizrahi",
                  "Ronit Peretz",
                  "Moti Klein",
                  "Sigal Shtein",
                  "Eli Ankory",
                  "Tamar Tabib",
                  "Avi Gold",
                  "Shira Lasker",
                  "Yoni Biton" };

            public class StationToInit
            {
                public string Name { get; set; }
                public Location Location { get; set; }
                public StationToInit() { }
                public StationToInit(string myName, double lati, double longi)
                {
                    Name = myName;
                    Location = new Location(longi, lati);
                }
            }
            public static StationToInit[] stationTos =
                { new StationToInit("Jerusalem", 31.78029702774437, 35.22149040877181),
                  new StationToInit("Tel Aviv", 32.082937755612186, 34.7908251395941),
                  new StationToInit("Haifa", 32.805995463493694, 34.99025372436736),
                  new StationToInit("Ashdod", 31.794795027439164, 34.651403839531135),
                  new StationToInit("Ariel", 32.105139, 35.189928),
                  new StationToInit("Beer Sheva", 31.253107, 34.786712),
                  new StationToInit("Netanya", 32.332, 34.855),
                  new StationToInit("Nahariya", 33.008629, 35.097535),
                  new StationToInit("Ra'anana", 32.185769, 34.870202),
                  new StationToInit("Ramat Gan", 32.069038, 34.824135) };
        }
    }
}