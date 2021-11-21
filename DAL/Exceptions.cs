﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        //wrong id exeption
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
        public class ExcistingIdException : Exception
        {
            public int ID;
            public ExcistingIdException(int id) : base() => ID = id;
            public ExcistingIdException(int id, string message) : base(message) => ID = id;
            public ExcistingIdException(int id, string message, Exception innerException) :
                base(message, innerException) => ID = id;
            public override string ToString() => base.ToString() + $", id already exists: {ID}";
        }
    }
}