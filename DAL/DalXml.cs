using DalApi.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public void AddStation(Station station)
        {
            XElement Id = new XElement("Id", station.Id);
            XElement Name = new XElement("Name", station.Name);
            XElement Latitude = new XElement("Latitude", station.Location.Latitude);
            XElement Longitude = new XElement("Longitude", station.Location.Longitude);
            XElement Location = new XElement("Location", Latitude, Longitude);
            XElement NumOfChargeSlots = new XElement("NumOfChargeSlots", station.NumOfChargeSlots);
            XElement NumOfAvailableChargeSlots = new XElement("NumOfAvailableChargeSlots", station.NumOfAvailableChargeSlots);
            XElement IsActive = new XElement("IsActive", station.IsActive);

            StationsRoot.Add(new XElement("Station", Id, Name, Location, NumOfChargeSlots, NumOfAvailableChargeSlots, IsActive));
            StationsRoot.Save(stationsPath);
        }

        public void AddDrone(Drone drone, int firstChargeStationId)
        {
            XElement Id = new XElement("Id", drone.Id);
            XElement Model = new XElement("Model", drone.Model);
            XElement Weight = new XElement("Weight", drone.Weight);
            XElement IsActive = new XElement("IsActive", drone.IsActive);

            DronesRoot.Add(new XElement("Drone", Id, Model, Weight, IsActive));
            DronesRoot.Save(dronesPath);
            AddDroneCharge(drone.Id, firstChargeStationId);
            DecreaseChargeSlot(firstChargeStationId);
        }
        public void AddDrone(Drone drone)
        {
            XElement Id = new XElement("Id", drone.Id);
            XElement Model = new XElement("Model", drone.Model);
            XElement Weight = new XElement("Weight", drone.Weight);
            XElement IsActive = new XElement("IsActive", drone.IsActive);

            DronesRoot.Add(new XElement("Drone", Id, Model, Weight, IsActive));
            DronesRoot.Save(dronesPath);
        }

        public void AddCustomer(Customer customer)
        {
            XElement Id = new XElement("Id", customer.Id);
            XElement Name = new XElement("Name", customer.Name);
            XElement PhoneNumber = new XElement("PhoneNumber", customer.PhoneNumber);
            XElement IsActive = new XElement("IsActive", customer.IsActive);
            XElement Latitude = new XElement("Latitude", customer.Location.Latitude);
            XElement Longitude = new XElement("Longitude", customer.Location.Longitude);
            XElement Location = new XElement("Location", Latitude, Longitude);
            CustomersRoot.Add(new XElement("Customer", Id, Name, PhoneNumber, Location, IsActive));
            CustomersRoot.Save(customersPath);
        }

        public void AddParcel(Parcel parcel)
        {
            XElement Id = new XElement("Id", parcel.Id);
            XElement SenderId = new XElement("SenderId", parcel.SenderId);
            XElement ReceiverId = new XElement("ReceiverId", parcel.ReceiverId);
            XElement IsActive = new XElement("IsActive", parcel.IsActive);
            XElement DroneId = new XElement("DroneId", parcel.DroneId);
            XElement Weight = new XElement("Weight", parcel.Weight);
            XElement Priority = new XElement("Priority", parcel.Priority);
            XElement Requested = new XElement("Requested", parcel.Requested);
            XElement Scheduled = new XElement("Scheduled", parcel.Scheduled);
            XElement PickedUp = new XElement("PickedUp", parcel.PickedUp);
            XElement Delivered = new XElement("Delivered", parcel.Delivered);

            ParcelsRoot.Add(new XElement("parcel", Id, SenderId, ReceiverId, DroneId,
               Weight, Priority, Requested, Scheduled, PickedUp, Delivered, IsActive));
            ParcelsRoot.Save(parcelsPath);
        }

        public void AddDroneCharge(int _droneId, int _StationId)
        {
            XElement StationId = new XElement("StationId", _StationId);
            XElement DroneId = new XElement("DroneId", _droneId);
            XElement IsActive = new XElement("IsActive", true);

            DroneChargesRoot.Add(new XElement("DroneCharge", StationId, DroneId, IsActive));
            DroneChargesRoot.Save(droneChargesPath);
        }
        #endregion
        #region delete func
        public void DeleteStation(int myId)
        {
            XElement stationElement = (from item in StationsRoot.Elements()
                                       where int.Parse(item.Element("id").Value) == myId
                                       select item).FirstOrDefault();

            stationElement.Element("IsActive").Value = "false";
            StationsRoot.Save(stationsPath);
        }

        public void DeleteParcel(int myId)
        {
            XElement parcelElement = (from item in ParcelsRoot.Elements()
                                      where int.Parse(item.Element("id").Value) == myId
                                      select item).FirstOrDefault();

            parcelElement.Element("IsActive").Value = "false";
            ParcelsRoot.Save(parcelsPath);
        }

        public void DeleteDrone(int myId)
        {
            XElement droneElement = (from item in DronesRoot.Elements()
                                     where int.Parse(item.Element("id").Value) == myId
                                     select item).FirstOrDefault();

            droneElement.Element("IsActive").Value = "false";
            DronesRoot.Save(dronesPath);
        }

        public void DeleteCustomer(int myId)
        {
            XElement customerElement = (from item in CustomersRoot.Elements()
                                        where int.Parse(item.Element("id").Value) == myId
                                        select item).FirstOrDefault();

            customerElement.Element("IsActive").Value = "false";
            CustomersRoot.Save(customersPath);
        }
        #endregion
        #region Individual view
        public Station GetStation(int id)
        {
            Station station = new();
            XElement stationElement = (from item in StationsRoot.Elements()
                                       where int.Parse(item.Element("id").Value) == id
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
            Drone drone = new();
            XElement droneElement = (from item in DronesRoot.Elements()
                                     where int.Parse(item.Element("id").Value) == id
                                     select item).FirstOrDefault();
            drone.Id = int.Parse(droneElement.Element("Id").Value);
            drone.Model = droneElement.Element("Model").Value;
            drone.Weight = WeightCategory(droneElement.Element("Weight").Value);
            drone.IsActive = Convert.ToBoolean(droneElement.Element("IsActive").Value);
            return drone;
        }

        public Customer GetCustomer(int id)
        {
            Customer customer = new();
            XElement customerElement = (from item in CustomersRoot.Elements()
                                        where int.Parse(item.Element("id").Value) == id
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
            Parcel parcel = new();
            XElement parcelElement = (from item in ParcelsRoot.Elements()
                                      where int.Parse(item.Element("id").Value) == id
                                      select item).FirstOrDefault();

            parcel.Id = int.Parse(parcelElement.Element("Id").Value);
            parcel.Weight = WeightCategory(parcelElement.Element("Weight").Value);
            parcel.Priority = PriorityLevel(parcelElement.Element("Priority").Value);
            parcel.SenderId = int.Parse(parcelElement.Element("SenderId").Value);
            parcel.ReceiverId = int.Parse(parcelElement.Element("ReceiverId").Value);
            parcel.DroneId = int.Parse(parcelElement.Element("DroneId").Value);
            parcel.Requested = Convert.ToDateTime(parcelElement.Element("Requested").Value);
            parcel.Scheduled = Convert.ToDateTime(parcelElement.Element("Scheduled").Value);
            parcel.PickedUp = Convert.ToDateTime(parcelElement.Element("PickedUp").Value);
            parcel.Delivered = Convert.ToDateTime(parcelElement.Element("Delivered").Value);
            parcel.IsActive = Convert.ToBoolean(parcelElement.Element("IsActive").Value);
            return parcel;
        }
        #endregion
        #region get lists
        public IEnumerable<Drone> GetDronesList()
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            //try
            //{
            //    drones = (from item in DronesRoot.Elements()
            //              select new Drone()
            //              {
            //                  Id = int.Parse(item.Element("id").Value),
            //                  Model = item.Element("Model").Value,
            //                  Weight = WeightCategory(item.Element("name").Element("lastName").Value),
            //                  IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
            //              }
            //              ).ToList();
            //}
            //catch
            //{
            //    drones = null;
            //}
            return drones;
        }
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            List<DroneCharge> droneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);
            //try
            //{
            //    droneCharges = (from item in DroneChargesRoot.Elements()
            //                    select new DroneCharge()
            //                    {
            //                        DroneId = int.Parse(item.Element("DroneId").Value),
            //                        StationId = int.Parse(item.Element("StationId").Value),
            //                        IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
            //                    }
            //              ).ToList();
            //}
            //catch
            //{
            //    droneCharges = null;
            //}
            return droneCharges;
        }
        public IEnumerable<Station> GetStationsList()
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            //try
            //{
            //    stations = (from item in StationsRoot.Elements()
            //                select new Station()
            //                {
            //                    Id = int.Parse(item.Element("id").Value),
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
            return stations;
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(customersPath);
            //List<Customer> customers = new();
            //try
            //{
            //    customers = (from item in CustomersRoot.Elements()
            //                 select new Customer()
            //                 {
            //                     Id = int.Parse(item.Element("id").Value),
            //                     Name = item.Element("Name").Value,
            //                     Location = new Location(double.Parse(item.Element("Location").Element("Latitude").Value),
            //           double.Parse(item.Element("Location").Element("Longitude").Value)),
            //                     PhoneNumber = item.Element("PhoneNumber").Value,
            //                     IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
            //                 }
            //                 ).ToList();
            //}
            //catch
            //{
            //    customers = null;
            //}
            return customers;
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            //List<Parcel> parcels = new();
            //Parcel tempParcel = new();

            //try
            //{
            //    foreach (var item in ParcelsRoot.Elements("ArrayOfParcel"))
            //    {
            //        tempParcel.Id = int.Parse(item.Element("id").Value);
            //        tempParcel.Weight = WeightCategory(item.Element("Weight").Value);
            //        tempParcel.Priority = PriorityLevel(item.Element("Priority").Value);
            //        tempParcel.SenderId = int.Parse(item.Element("SenderId").Value);
            //        tempParcel.ReceiverId = int.Parse(item.Element("ReceiverId").Value);
            //        tempParcel.DroneId = int.Parse(item.Element("DroneId").Value);
            //        tempParcel.Requested = Convert.ToDateTime(item.Element("Requested").Value);
            //        tempParcel.Scheduled = Convert.ToDateTime(item.Element("Scheduled").Value);
            //        tempParcel.PickedUp = Convert.ToDateTime(item.Element("PickedUp").Value);
            //        tempParcel.Delivered = Convert.ToDateTime(item.Element("Delivered").Value);
            //        tempParcel.IsActive = Convert.ToBoolean(item.Element("IsActive").Value);
            //        parcels.Add(tempParcel);
            //    }
            //}
            //catch
            //{
            //    parcels = null;
            //}
            return parcels;
        }
        #endregion
        public int ParcelRunId()
        {
            XElement parcelRunIdX = ConfigRoot.Element("Config").Element("ParcelRunId");
            var parcelRunId = int.Parse(parcelRunIdX.Value);
            parcelRunIdX.Value = (parcelRunId + 1).ToString();
            return parcelRunId;
        }

        public double[] DroneElectricityConsumption()
        {
            double[] droneElectricityConsumption = new double[5];
            droneElectricityConsumption[0] = DataSource.Config.free;
            droneElectricityConsumption[1] = DataSource.Config.lightWeight;
            droneElectricityConsumption[2] = DataSource.Config.mediumWeight;
            droneElectricityConsumption[3] = DataSource.Config.heavyWeight;
            droneElectricityConsumption[4] = DataSource.Config.DroneLoadRate;
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
            Station temp = new();
            for (int i = 0; i < DataSource.stations.Count; i++)
            {
                Station item = DataSource.stations[i];
                if (item.Id == stationId)
                {
                    temp.Id = stationId;
                    temp.Location = item.Location;
                    if (newName != null)
                        temp.Name = newName;
                    else
                        temp.Name = item.Name;
                    if (numOfChargeSlots != 0)
                    {
                        temp.NumOfChargeSlots = numOfChargeSlots;
                        temp.NumOfAvailableChargeSlots = avialble;
                    }
                    else
                        temp.NumOfChargeSlots = item.NumOfChargeSlots;
                    DataSource.stations[i] = temp;
                    XMLTools.SaveListToXMLSerializer(DataSource.stations, stationsPath);

                }
            }
        }

        public void UpdateDrone(int droneId, string newModel)
        {
            Drone temp = new();
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                Drone item = DataSource.drones[i];
                if (item.Id == droneId)
                {
                    temp.Id = droneId;
                    temp.Model = newModel;
                    temp.Weight = item.Weight;
                    DataSource.drones[i] = temp;
                }
            }
            XMLTools.SaveListToXMLSerializer(DataSource.drones, dronesPath);
        }

        public void UpdateCustomer(int customerId, string newName, string newPhone)
        {
            Customer temp = new();
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                Customer item = DataSource.customers[i];
                if (item.Id == customerId && item.IsActive)
                {
                    temp.Id = customerId;
                    temp.Location = item.Location;
                    if (newName != null)
                        temp.Name = newName;
                    else temp.Name = item.Name;
                    if (newPhone != null)
                        temp.PhoneNumber = newPhone;
                    else
                        temp.PhoneNumber = item.PhoneNumber;
                    temp.Location = item.Location;
                    temp.IsActive = true;
                    DataSource.customers[i] = temp;
                    XMLTools.SaveListToXMLSerializer(DataSource.customers, customersPath);

                }
            }
        }

        public void DecreaseChargeSlot(int stationId)
        {
            XElement stationElement = (from item in StationsRoot.Elements()
                                       where int.Parse(item.Element("id").Value) == stationId
                                       select item).FirstOrDefault();

            var oldNum = int.Parse(stationElement.Element("NumOfAvailableChargeSlots").Value);
            stationElement.Element("NumOfAvailableChargeSlots").Value = (oldNum - 1).ToString();
            StationsRoot.Save(stationsPath);

        }

        public void IncreaseChargeSlot(int stationId)
        {
            XElement stationElement = (from item in StationsRoot.Elements()
                                       where int.Parse(item.Element("id").Value) == stationId
                                       select item).FirstOrDefault();

            var oldNum = int.Parse(stationElement.Element("NumOfAvailableChargeSlots").Value);
            stationElement.Element("NumOfAvailableChargeSlots").Value = (oldNum + 1).ToString();
            StationsRoot.Save(stationsPath);
        }

        public void ScheduleParcelToDrone(int newParcelId, int droneId)
        {
            XElement parcelElement = (from item in ParcelsRoot.Elements()
                                      where int.Parse(item.Element("id").Value) == newParcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("DroneId").Value = droneId.ToString();
            ParcelsRoot.Save(parcelsPath);
        }

        public void PickUpParcel(int droneId, int parcelId)
        {

            XElement parcelElement = (from item in ParcelsRoot.Elements()
                                      where int.Parse(item.Element("id").Value) == parcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("PickedUp").Value = DateTime.Now.ToString();
            ParcelsRoot.Save(parcelsPath);
        }

        public void DeliverParcel(int droneId, int parcelId)
        {
            XElement parcelElement = (from item in ParcelsRoot.Elements()
                                      where int.Parse(item.Element("id").Value) == parcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("Delivered").Value = DateTime.Now.ToString();
            ParcelsRoot.Save(parcelsPath);
        }
        public void UpdatedroneIdInParcel(int ParcelId, int droneId)
        {
            XElement parcelElement = (from item in ParcelsRoot.Elements()
                                      where int.Parse(item.Element("id").Value) == ParcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("DroneId").Value = ParcelId.ToString();
            ParcelsRoot.Save(parcelsPath);
        }
      
        public void EndDroneCharge(int droneId)
        {
            XElement droneChargeElement = (from item in DroneChargesRoot.Elements()
                                     where int.Parse(item.Element("id").Value) == droneId
                                     select item).FirstOrDefault();
            droneChargeElement.Element("IsActive").Value = "false";
            DroneChargesRoot.Save(droneChargesPath);
        }
    }
}