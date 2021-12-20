using System;

namespace BlApi
{
    namespace BO
    {
        /// <summary>
        /// exeptions for BL - captured in ConsuleUI_BL
        /// </summary>
        [Serializable]
        public class WrongIdException : Exception
        {
            public int ID;
            public WrongIdException(int id) : base() => ID = id;
            public WrongIdException(int id, string message) : base(message) => ID = id;
            public WrongIdException(int id, string message, Exception innerException) :
                base(message, innerException) => ID = id;
            public override string ToString() => base.ToString() + $", wrong id: {ID}";
        }
        [Serializable]
        public class ExistingIdException : Exception
        {
            public int ID;
            public ExistingIdException(int id) : base() => ID = id;
            public ExistingIdException(int id, string message) : base(message) => ID = id;
            public ExistingIdException(int id, string message, Exception innerException) :
                base(message, innerException) => ID = id;
            public override string ToString() => base.ToString() + $", id already exists: {ID}";
        }

        /// <summary>
        /// occupied drone exception
        /// </summary>
        [Serializable]
        public class OccupiedDroneException : Exception
        {
            public int ID;
            public OccupiedDroneException(int id) : base() => ID = id;
            public OccupiedDroneException(int id, string message) : base(message) => ID = id;
            public OccupiedDroneException(int id, string message, Exception innerException) :
                base(message, innerException) => ID = id;
            public override string ToString() => base.ToString() + $", drone id occupied, try another: {ID}";
        } 
    }
}