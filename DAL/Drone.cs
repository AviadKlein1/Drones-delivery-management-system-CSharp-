namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// entity drone - fields
        /// </summary>
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
        }
    }
}