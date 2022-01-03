using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    sealed partial class DalXml : IDal
    {
        XElement Config;
        string configPath = @"ConfigXml.xml";

        XElement StationsRoot;
        string stationsPath = @"StationsXml.xml";

        XElement ParcelsRoot;
        string parcelsPath = @"ParcelsXml.xml";

        XElement DronesRoot;
        string dronesPath = @"DronesXml.xml";

        XElement CustomersRoot;
        string customersPath = @"CustomersXml.xml";

        XElement DroneChargesRoot;
        string droneChargesPath = @"DroneChargesXml.xml";
        

        public DalXml()
        {
            if (!File.Exists(configPath))
                CreateFiles(Config, configPath);
            else
                LoadData(Config, configPath);

            if (!File.Exists(stationsPath))
                CreateFiles(StationsRoot,stationsPath);
            else
                LoadData(StationsRoot, stationsPath);

            if (!File.Exists(parcelsPath))
                CreateFiles(ParcelsRoot, parcelsPath);
            else
                LoadData(ParcelsRoot, parcelsPath);

            if (!File.Exists(customersPath))
                CreateFiles(CustomersRoot, customersPath);
            else
                LoadData(CustomersRoot, customersPath);

            if (!File.Exists(dronesPath))
                CreateFiles(DronesRoot, dronesPath);
            else
                LoadData(DronesRoot, dronesPath);

            if (!File.Exists(droneChargesPath))
                CreateFiles(DroneChargesRoot, droneChargesPath);
            else
                LoadData(DroneChargesRoot, droneChargesPath);
        }

        private void CreateFiles(XElement root, string path)
        {
            root = new XElement(root.Name);
            root.Save(path);
        }

        public static void LoadData(XElement root, string path)
        {
            try
            {
                root = XElement.Load(path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        DalApi.DO.MyEnums.WeightCategory WeightCategory(string str)
        {
            if (str == "light") return DalApi.DO.MyEnums.WeightCategory.light;
            if (str == "medium") return DalApi.DO.MyEnums.WeightCategory.medium;
            else return DalApi.DO.MyEnums.WeightCategory.heavy;
        }
        DalApi.DO.MyEnums.PriorityLevel PriorityLevel(string str)
        {
            if (str == "quickly") return DalApi.DO.MyEnums.PriorityLevel.quickly;
            if (str == "regular") return DalApi.DO.MyEnums.PriorityLevel.regular;
            else return DalApi.DO.MyEnums.PriorityLevel.urgent;
        }
    }
}