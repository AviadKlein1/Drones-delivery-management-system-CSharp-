namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// defines enums for items' fields - weight, priority, status.
        /// </summary>
        public class MyEnums
        {
            public enum PriorityLevel { regular, quickly, ergent };
            public enum WeightCategory { light, medium, heavy };
            public enum ParcelStatus { requested, scheduled, pickedUp, delivered };
            public static int[] ForRd = { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0 ,0};

            public static string[] NamesForCustomers =  { "Abu Abdallah Mohammed Amghar",
                "Abdul Fatah Younis",
                "tomer Choen",
                "Elad Katch",
                "Avi",
                "Nati",
                "Meilech",
                "Moishi",
                "Nachum",
                "Mendi" };


            public class StationToInit
            {
                public string Name { get; set; }
                public Location Location { get; set; }
                public StationToInit() { }
                public StationToInit(string name, double latti, double longi)
                {
                    Name = name;
                    Location = new Location(longi, latti);
                }
            }
            public static StationToInit[] stationTos = { new StationToInit("Jerusalem",31.78029702774437, 35.22149040877181),
                new StationToInit("Tel aviv",32.082937755612186, 34.7908251395941),
                new StationToInit("Haifa", 32.805995463493694, 34.99025372436736),
                new StationToInit("Yericho",31.86156317014965, 35.4572864958303),
                new StationToInit("Adei ad", 32.039796772622864, 35.33520602618077),
                new StationToInit("Hebron", 31.530519828134118, 35.10261554352295),
                new StationToInit("Shchem", 32.227648338488386, 35.255747566592454),
                new StationToInit("Beit lechem",31.704102486343732, 35.207172992582464),
                new StationToInit("Ashdod", 31.794795027439164, 34.651403839531135),
                new StationToInit("Kfar saba",32.180465537327684, 34.923075961099656)};

        }
    }
}