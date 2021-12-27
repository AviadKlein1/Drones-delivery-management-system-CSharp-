﻿using System;

namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// entity parcel - fields
        /// </summary>
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int ReceiverId { get; set; }
            public int DroneId { get; set; }
            public MyEnums.WeightCategory Weight { get; set; }
            public MyEnums.PriorityLevel Priority { get; set; }
            public DateTime? Requested { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }
            //public bool IsActive { get; set; }

        }
    }
}