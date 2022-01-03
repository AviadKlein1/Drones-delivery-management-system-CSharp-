using System;
using System.Reflection;

namespace DalApi
{
    public class DalFactory
    {
        public static IDal GetDal()
        {
            IDal dal = DalXml.Instance;
            return dal;
        }
    }
}
