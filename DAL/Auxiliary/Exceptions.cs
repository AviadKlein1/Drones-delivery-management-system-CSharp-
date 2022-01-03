using System;

namespace DalApi
{
    namespace DO
    {
        /// <summary>
        /// wrong id exeption
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

        /// <summary>
        /// item already exist exception
        /// </summary>
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
    }
}