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
        class Exeptions : Exception
        {
            protected Exeptions(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
