using System;

namespace IBL
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
    }
}