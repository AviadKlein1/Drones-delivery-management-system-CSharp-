using System;
using System.Reflection;

namespace DalApi
{
    public class DalFactory
    {
        public static DO.IDal GetDal()
        {
            DO.IDal dal = DalXml.Instance;
            return dal;
        }
    }
}
