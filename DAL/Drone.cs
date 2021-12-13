namespace DalApi
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
            public DalApi.DO.MyEnums.WeightCategory weight { get; set; }
        }
    }
}