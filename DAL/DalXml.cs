using DalApi.DO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DalApi
{
    sealed partial class DalXml : IDal
    {
        #region singleton
        /// <summary>
        /// create a single instance of dal object
        /// </summary>
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        #endregion
        #region add func
        public void AddConfig()
        {
            ConfigRoot = XElement.Load(ConfigPath);
            XElement ParcelRunId = new XElement("ParcelRunId",DataSource.parcelRunId);
            XElement free = new XElement("free", 0.15);
            XElement lightWeight = new XElement("lightWeight", 0.1);
            XElement mediumWeight = new XElement("mediumWeight", 0.2);
            XElement heavyWeight = new XElement("heavyWeight", 0.3);
            XElement DroneLoadRate = new XElement("DroneLoadRate", 0.4);

            ConfigRoot.Add(new XElement("ConfigRoot", ParcelRunId, free, lightWeight, mediumWeight, heavyWeight, DroneLoadRate));
            ConfigRoot.Save(ConfigPath);
        }
        public void AddStation(Station station)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement Id = new XElement("Id", station.Id);
            XElement Name = new XElement("Name", station.Name);
            XElement Latitude = new XElement("Latitude", station.Location.Latitude);
            XElement Longitude = new XElement("Longitude", station.Location.Longitude);
            XElement Location = new XElement("Location", Latitude, Longitude);
            XElement NumOfChargeSlots = new XElement("NumOfChargeSlots", station.NumOfChargeSlots);
            XElement NumOfAvailableChargeSlots = new XElement("NumOfAvailableChargeSlots", station.NumOfAvailableChargeSlots);
            XElement IsActive = new XElement("IsActive", station.IsActive);

            ArrayOfStation.Add(new XElement("Station", Id, Name, Location, NumOfChargeSlots, NumOfAvailableChargeSlots, IsActive));
            ArrayOfStation.Save(stationsPath);
        }

        public void AddDrone(Drone drone, int firstChargeStationId)
        {
            ArrayOfDrone = XElement.Load(dronesPath);
            XElement Id = new XElement("Id", drone.Id);
            XElement Model = new XElement("Model", drone.Model);
            XElement Weight = new XElement("Weight", drone.Weight);
            XElement IsActive = new XElement("IsActive", drone.IsActive);

            ArrayOfDrone.Add(new XElement("Drone", Id, Model, Weight, IsActive));
            ArrayOfDrone.Save(dronesPath);
            AddDroneCharge(drone.Id, firstChargeStationId);
            DecreaseChargeSlot(firstChargeStationId);
        }
        public void AddDrone(Drone drone)
        {
            ArrayOfDrone = XElement.Load(dronesPath);
            XElement Id = new XElement("Id", drone.Id);
            XElement Model = new XElement("Model", drone.Model);
            XElement Weight = new XElement("Weight", drone.Weight);
            XElement IsActive = new XElement("IsActive", drone.IsActive);

            ArrayOfDrone.Add(new XElement("Drone", Id, Model, Weight, IsActive));
            ArrayOfDrone.Save(dronesPath);
        }

        public void AddCustomer(Customer customer)
        {
            ArrayOfCustomer = XElement.Load(customersPath);
            XElement Id = new XElement("Id", customer.Id);
            XElement Name = new XElement("Name", customer.Name);
            XElement PhoneNumber = new XElement("PhoneNumber", customer.PhoneNumber);
            XElement IsActive = new XElement("IsActive", customer.IsActive);
            XElement Latitude = new XElement("Latitude", customer.Location.Latitude);
            XElement Longitude = new XElement("Longitude", customer.Location.Longitude);
            XElement Location = new XElement("Location", Latitude, Longitude);
            ArrayOfCustomer.Add(new XElement("Customer", Id, Name, PhoneNumber, Location, IsActive));
            ArrayOfCustomer.Save(customersPath);
        }

        public void AddParcel(Parcel parcel)
        {
            ArrayOfParcel = XElement.Load(parcelsPath);
            XElement Id = new XElement("Id", parcel.Id);
            XElement SenderId = new XElement("SenderId", parcel.SenderId);
            XElement ReceiverId = new XElement("ReceiverId", parcel.ReceiverId);
            XElement IsActive = new XElement("IsActive", parcel.IsActive);
            XElement DroneId = new XElement("DroneId", parcel.DroneId);
            XElement Weight = new XElement("Weight", parcel.Weight);
            XElement Priority = new XElement("Priority", parcel.Priority);
            XElement Requested = new XElement("Requested", (parcel.Requested == DateTime.MinValue ? DateTime.MinValue : parcel.Requested));
            XElement Scheduled = new XElement("Scheduled", (parcel.Scheduled == DateTime.MinValue ? DateTime.MinValue : parcel.Scheduled));
            XElement PickedUp = new XElement("PickedUp", (parcel.PickedUp == DateTime.MinValue ? DateTime.MinValue : parcel.PickedUp));
            XElement Delivered = new XElement("Delivered", (parcel.Delivered == DateTime.MinValue ? DateTime.MinValue : parcel.Delivered));

            ArrayOfParcel.Add(new XElement("Parcel", Id, SenderId, ReceiverId, DroneId,
               Weight, Priority, Requested, Scheduled, PickedUp, Delivered, IsActive));
            ArrayOfParcel.Save(parcelsPath);
        }

        public void AddDroneCharge(int _droneId, int _StationId)
        {
            ArrayOfDroneCharge = XElement.Load(droneChargesPath);
            XElement StationId = new XElement("StationId", _StationId);
            XElement DroneId = new XElement("DroneId", _droneId);
            XElement IsActive = new XElement("IsActive", true);

            ArrayOfDroneCharge.Add(new XElement("DroneCharge", StationId, DroneId, IsActive));
            ArrayOfDroneCharge.Save(droneChargesPath);
        }
        #endregion
        #region delete func
        public void DeleteStation(int myId)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement stationElement = (from item in ArrayOfStation.Elements()
                                       where int.Parse(item.Element("Id").Value) == myId
                                       select item).FirstOrDefault();

            stationElement.Element("IsActive").Value = "false";
            ArrayOfStation.Save(stationsPath);
        }

        public void DeleteParcel(int myId)
        {
            ArrayOfParcel = XElement.Load(parcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel.Elements()
                                      where int.Parse(item.Element("Id").Value) == myId
                                      select item).FirstOrDefault();

            parcelElement.Element("IsActive").Value = "false";
            ArrayOfParcel.Save(parcelsPath);
        }

        public void DeleteDrone(int myId)
        {
            ArrayOfDrone = XElement.Load(dronesPath);
            XElement droneElement = (from item in ArrayOfDrone.Elements()
                                     where int.Parse(item.Element("Id").Value) == myId
                                     select item).FirstOrDefault();

            droneElement.Element("IsActive").Value = "false";
            ArrayOfDrone.Save(dronesPath);
        }

        public void DeleteCustomer(int myId)
        {
            ArrayOfCustomer = XElement.Load(customersPath);
            XElement customerElement = (from item in ArrayOfCustomer.Elements()
                                        where int.Parse(item.Element("Id").Value) == myId
                                        select item).FirstOrDefault();

            customerElement.Element("IsActive").Value = "false";
            ArrayOfCustomer.Save(customersPath);
        }
        #endregion
        #region Individual view
        public Station GetStation(int id)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            Station station = new();
            XElement stationElement = (from item in ArrayOfStation.Elements()
                                       where int.Parse(item.Element("Id").Value) == id
                                       select item).FirstOrDefault();
            station.Id = int.Parse(stationElement.Element("Id").Value);
            station.Name = stationElement.Element("Name").Value;
            station.Location = new Location(double.Parse(stationElement.Element("Location").Element("Latitude").Value),
                double.Parse(stationElement.Element("Location").Element("Longitude").Value));
            station.NumOfChargeSlots = int.Parse(stationElement.Element("NumOfChargeSlots").Value);
            station.NumOfAvailableChargeSlots = int.Parse(stationElement.Element("NumOfAvailableChargeSlots").Value);
            station.IsActive = Convert.ToBoolean(stationElement.Element("IsActive").Value);
            return station;
        }

        public Drone GetDrone(int id)
        {
            ArrayOfDrone = XElement.Load(dronesPath);
            Drone drone = new();
            XElement droneElement = (from item in ArrayOfDrone.Elements()
                                     where int.Parse(item.Element("Id").Value) == id
                                     select item).FirstOrDefault();
            drone.Id = int.Parse(droneElement.Element("Id").Value);
            drone.Model = droneElement.Element("Model").Value;
            drone.Weight = WeightCategory(droneElement.Element("Weight").Value);
            drone.IsActive = Convert.ToBoolean(droneElement.Element("IsActive").Value);
            return drone;
        }

        public Customer GetCustomer(int id)
        {
            ArrayOfCustomer = XElement.Load(customersPath);
            Customer customer = new();
            XElement customerElement = (from item in ArrayOfCustomer.Elements()
                                        where int.Parse(item.Element("Id").Value) == id
                                        select item).FirstOrDefault();
            customer.Id = int.Parse(customerElement.Element("Id").Value);
            customer.Name = customerElement.Element("Name").Value;
            customer.PhoneNumber = customerElement.Element("PhoneNumber").Value;
            customer.Location = new Location(double.Parse(customerElement.Element("Location").Element("Latitude").Value),
                double.Parse(customerElement.Element("Location").Element("Longitude").Value));
            customer.IsActive = Convert.ToBoolean(customerElement.Element("IsActive").Value);
            return customer;
        }

        public Parcel GetParcel(int id)
        {
            ArrayOfParcel = XElement.Load(parcelsPath);
            Parcel parcel = new();
            XElement parcelElement = (from item in ArrayOfParcel.Elements()
                                      where int.Parse(item.Element("Id").Value) == id
                                      select item).FirstOrDefault();

            parcel.Id = int.Parse(parcelElement.Element("Id").Value);
            parcel.Weight = WeightCategory(parcelElement.Element("Weight").Value);
            parcel.Priority = PriorityLevel(parcelElement.Element("Priority").Value);
            parcel.SenderId = int.Parse(parcelElement.Element("SenderId").Value);
            parcel.ReceiverId = int.Parse(parcelElement.Element("ReceiverId").Value);
            parcel.DroneId = int.Parse(parcelElement.Element("DroneId").Value);
            parcel.Requested = (DateTime)parcelElement.Element("Requested");
            parcel.Scheduled = (DateTime)parcelElement.Element("Scheduled");
            parcel.PickedUp = (DateTime)parcelElement.Element("PickedUp");
            parcel.Delivered = (DateTime)parcelElement.Element("Delivered");
            parcel.IsActive = Convert.ToBoolean(parcelElement.Element("IsActive").Value);
            return parcel;
        }
        #endregion
        #region get lists
        public IEnumerable<Drone> GetDronesList()
        {
            //List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            ArrayOfDrone = XElement.Load(dronesPath);
            List<Drone> drones = new();
            try
            {
                drones = (from item in ArrayOfDrone.Elements()
                          select new Drone()
                          {
                              Id = int.Parse(item.Element("Id").Value),
                              Model = item.Element("Model").Value,
                              Weight = WeightCategory(item.Element("Weight").Value),
                              IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
                          }
                          ).ToList();
            }
            catch
            {
                drones = null;
            }
            return drones;
        }
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            //List<DroneCharge> droneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);
            ArrayOfDroneCharge = XElement.Load(droneChargesPath);
            List<DroneCharge> droneCharges = new(); 
            try
            {
                droneCharges = (from item in ArrayOfDroneCharge.Elements()
                                select new DroneCharge()
                                {
                                    DroneId = int.Parse(item.Element("DroneId").Value),
                                    StationId = int.Parse(item.Element("StationId").Value),
                                    IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
                                }
                          ).ToList();
            }
            catch
            {
                droneCharges = null;
            }
            return droneCharges;
        }
        public IEnumerable<Station> GetStationsList()
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            //ArrayOfStation = XElement.Load(stationsPath);
            //List<Station> stations = new(); 
            //try
            //{
            //    stations = (from item in ArrayOfStation.Elements()
            //                select new Station()
            //                {
            //                    Id = int.Parse(item.Element("Id").Value),
            //                    Name = item.Element("Name").Value,
            //                    Location = new Location(double.Parse(item.Element("Location").Element("Latitude").Value),
            //          double.Parse(item.Element("Location").Element("Longitude").Value)),
            //                    NumOfChargeSlots = int.Parse(item.Element("NumOfChargeSlots").Value),
            //                    NumOfAvailableChargeSlots = int.Parse(item.Element("NumOfAvailableChargeSlots").Value),
            //                    IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
            //                }
            //              ).ToList();
            //}
            //catch
            //{
            //    stations = null;
            //}
            XMLTools.SaveListToXMLSerializer(stations, stationsPath);
            return stations;
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            //List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(customersPath);
            ArrayOfCustomer = XElement.Load(customersPath);
            List<Customer> customers = new();
            try
            {
                customers = (from item in ArrayOfCustomer.Elements()
                             select new Customer()
                             {
                                 Id = int.Parse(item.Element("Id").Value),
                                 Name = item.Element("Name").Value,
                                 Location = new Location(double.Parse(item.Element("Location").Element("Latitude").Value),
                       double.Parse(item.Element("Location").Element("Longitude").Value)),
                                 PhoneNumber = item.Element("PhoneNumber").Value,
                                 IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
                             }
                             ).ToList();
            }
            catch
            {
                customers = null;
            }
            //XMLTools.SaveListToXMLSerializer(customers, customersPath);
            return customers;
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            //List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            ArrayOfParcel = XElement.Load(parcelsPath);
            List<Parcel> parcels = new();
                parcels = (from item in ArrayOfParcel.Elements()
                           select new Parcel()
                           {
                               Id = int.Parse(item.Element("Id").Value),
                               Weight = WeightCategory(item.Element("Weight").Value),
                               Priority = PriorityLevel(item.Element("Priority").Value),
                               SenderId = int.Parse(item.Element("SenderId").Value),
                               ReceiverId = int.Parse(item.Element("ReceiverId").Value),
                               DroneId = int.Parse(item.Element("DroneId").Value),
                               Requested = (DateTime)item.Element("Requested"), 
                               Scheduled = (DateTime)item.Element("Scheduled"),
                               PickedUp = (DateTime)item.Element("PickedUp"),
                               Delivered = (DateTime)item.Element("Delivered"),
                               IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
                           }
                           ).ToList();

            //XMLTools.SaveListToXMLSerializer(parcels, parcelsPath);
            return parcels;
        }
        #endregion
        public int ParcelRunId()
        {
            XElement parcelRunIdX = ConfigRoot.Element("ConfigRoot").Element("ParcelRunId");
            var parcelRunId = int.Parse(parcelRunIdX.Value);
            parcelRunIdX.Value = (parcelRunId + 1).ToString();
            return parcelRunId;
        }

        public double[] DroneElectricityConsumption()
        { 
            double[] droneElectricityConsumption = new double[5];
            droneElectricityConsumption[0] = 0.15;
            droneElectricityConsumption[1] =0.1;
            droneElectricityConsumption[2] = 0.2;
            droneElectricityConsumption[3] = 0.3;
            droneElectricityConsumption[4] =  0.4; 

            return droneElectricityConsumption;
        }

        public Location StationLocate(int StationId)
        {
            Location temp = new();
            for (int i = 0; i < DataSource.stations.Count; i++)
                if (DataSource.stations[i].Id == StationId)
                {
                    temp.Latitude = DataSource.stations[i].Location.Latitude;
                    temp.Longitude = DataSource.stations[i].Location.Longitude;
                }
            return temp;
        }

        public double GetDistance(Location a, Location b)
        {
            var d1 = a.Latitude * (Math.PI / 180.0);
            var num1 = a.Longitude * (Math.PI / 180.0);
            var d2 = b.Latitude * (Math.PI / 180.0);
            var num2 = b.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                (Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0));
            double resault = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            return resault / 100;
        }

        public void UpdateStation(int stationId, string newName, int numOfChargeSlots, int avialble)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement stationElement = (from item in ArrayOfStation.Elements()
                                       where int.Parse(item.Element("Id").Value) == stationId
                                       select item).FirstOrDefault();

            stationElement.Element("Name").Value = newName;
            stationElement.Element("NumOfChargeSlots").Value = numOfChargeSlots.ToString();
            stationElement.Element("NumOfAvailableChargeSlots").Value = avialble.ToString();
            ArrayOfStation.Save(stationsPath);



        }
    

        public void UpdateDrone(int droneId, string newModel)
        {
            ArrayOfDrone = XElement.Load(dronesPath);
            XElement droneChargeElement = (from item in ArrayOfDrone.Elements()
                                           where int.Parse(item.Element("Id").Value) == droneId
                                           select item).FirstOrDefault();
            droneChargeElement.Element("Model").Value = newModel;
            ArrayOfDroneCharge.Save(droneChargesPath);
        }
        

        public void UpdateCustomer(int customerId, string newName, string newPhone)
        {
            ArrayOfCustomer = XElement.Load(customersPath);
            XElement customerElement = (from item in ArrayOfCustomer.Elements()
                                           where int.Parse(item.Element("Id").Value) == customerId
                                        select item).FirstOrDefault();
            customerElement.Element("Name").Value = newName;
            customerElement.Element("Phone").Value = newPhone;
            ArrayOfDroneCharge.Save(droneChargesPath);

        }

        public void DecreaseChargeSlot(int stationId)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement stationElement = (from item in ArrayOfStation.Elements()
                                       where int.Parse(item.Element("Id").Value) == stationId
                                       select item).FirstOrDefault();

            var oldNum = int.Parse(stationElement.Element("NumOfAvailableChargeSlots").Value);
            stationElement.Element("NumOfAvailableChargeSlots").Value = (oldNum - 1).ToString();
            ArrayOfStation.Save(stationsPath);

        }

        public void IncreaseChargeSlot(int stationId)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement stationElement = (from item in ArrayOfStation.Elements()
                                       where int.Parse(item.Element("Id").Value) == stationId
                                       select item).FirstOrDefault();

            var oldNum = int.Parse(stationElement.Element("NumOfAvailableChargeSlots").Value);
            stationElement.Element("NumOfAvailableChargeSlots").Value = (oldNum + 1).ToString();
            ArrayOfStation.Save(stationsPath);
        }

        public void ScheduleParcelToDrone(int newParcelId, int droneId)
        {
            ArrayOfParcel = XElement.Load(parcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel.Elements()
                                      where int.Parse(item.Element("Id").Value) == newParcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("DroneId").Value = droneId.ToString();
            ArrayOfParcel.Save(parcelsPath);
        }

        public void PickUpParcel(int droneId, int parcelId)
        {
            ArrayOfParcel = XElement.Load(parcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel.Elements()
                                      where int.Parse(item.Element("Id").Value) == parcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("PickedUp").Value = DateTime.Now.ToString();
            ArrayOfParcel.Save(parcelsPath);
        }

        public void DeliverParcel(int droneId, int parcelId)
        {
            ArrayOfParcel = XElement.Load(parcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel.Elements()
                                      where int.Parse(item.Element("Id").Value) == parcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("Delivered").Value = DateTime.Now.ToString();
            ArrayOfParcel.Save(parcelsPath);
        }
        public void UpdatedroneIdInParcel(int ParcelId, int droneId)
        {
            ArrayOfParcel = XElement.Load(parcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel.Elements()
                                      where int.Parse(item.Element("Id").Value) == ParcelId
                                      select item).First();

            parcelElement.Element("DroneId").Value = droneId.ToString();
            ArrayOfParcel.Save(parcelsPath);
        }
      
        public void EndDroneCharge(int droneId)
        {
            ArrayOfDroneCharge = XElement.Load(droneChargesPath);
            XElement droneChargeElement = (from item in ArrayOfDroneCharge.Elements()
                                     where int.Parse(item.Element("Id").Value) == droneId
                                     select item).FirstOrDefault();
            droneChargeElement.Element("IsActive").Value = "false";
            ArrayOfDroneCharge.Save(droneChargesPath);
        }
    }
}