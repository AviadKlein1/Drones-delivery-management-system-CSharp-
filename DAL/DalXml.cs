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


        public int ParcelRunId()
        {
            throw new NotImplementedException();
        }
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

        public void Addcustomer(Customer customer)
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
        public IEnumerable<Drone> GetDrones()
        {
            List<Drone> drones;
            try
            {
                drones = (from item in DronesRoot.Elements()
                          select new Drone()
                          {
                              Id = int.Parse(item.Element("id").Value),
                              Model = item.Element("Model").Value,
                              Weight = WeightCategory(item.Element("name").Element("lastName").Value),
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
            List<DroneCharge> droneCharges;
            try
            {
                droneCharges = (from item in DroneChargesRoot.Elements()
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
            List<Station> stations;
            try
            {
                stations = (from item in StationsRoot.Elements()
                            select new Station()
                            {
                                Id = int.Parse(item.Element("id").Value),
                                Name = item.Element("Name").Value,
                                Location = new Location(double.Parse(item.Element("Location").Element("Latitude").Value),
                      double.Parse(item.Element("Location").Element("Longitude").Value)),
                                NumOfChargeSlots = int.Parse(item.Element("NumOfChargeSlots").Value),
                                NumOfAvailableChargeSlots = int.Parse(item.Element("NumOfAvailableChargeSlots").Value),
                                IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
                            }
                          ).ToList();
            }
            catch
            {
                stations = null;
            }
            return stations;
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            List<Customer> customers;
            try
            {
                customers = (from item in CustomersRoot.Elements()
                             select new Customer()
                             {
                                 Id = int.Parse(item.Element("id").Value),
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
            return customers;
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            List<Parcel> parcels;
            try
            {
                parcels = (from item in ParcelsRoot.Elements()
                           select new Parcel()
                           {
                               Id = int.Parse(item.Element("id").Value),
                               Weight = WeightCategory(item.Element("Weight").Value),
                               Priority = PriorityLevel(item.Element("Priority").Value),
                               SenderId = int.Parse(item.Element("SenderId").Value),
                               ReceiverId = int.Parse(item.Element("ReceiverId").Value),
                               DroneId = int.Parse(item.Element("DroneId").Value),
                               Requested = Convert.ToDateTime(item.Element("Requested").Value),
                               Scheduled = Convert.ToDateTime(item.Element("Scheduled").Value),
                               PickedUp = Convert.ToDateTime(item.Element("PickedUp").Value),
                               Delivered = Convert.ToDateTime(item.Element("Delivered").Value),
                               IsActive = Convert.ToBoolean(item.Element("IsActive").Value)
                           }
                          ).ToList();
            }
            catch
            {
                parcels = null;
            }
            return parcels;
        } 
        #endregion

        public double[] DroneElectricityConsumption()
            {
                throw new NotImplementedException();
            }

            public Location StationLocate(int StationId)
            {
                throw new NotImplementedException();
            }

            public double GetDistance(Location a, Location b)
            {
                throw new NotImplementedException();
            }

            public void UpdateStation(int stationId, string newName, int numOfChargeSlots, int avialble)
            {
                throw new NotImplementedException();
            }

            public void UpdateDrone(int droneId, string newModel)
            {
                throw new NotImplementedException();
            }

            public void UpdateCustomer(int customerId, string newName, string newPhone)
            {
                throw new NotImplementedException();
            }

            public void DecreaseChargeSlot(int stationId)
            {
                throw new NotImplementedException();
            }

            public void IncreaseChargeSlot(int stationId)
            {
                throw new NotImplementedException();
            }

            public void ScheduleParcelToDrone(int newParcelId, int droneId)
            {
                throw new NotImplementedException();
            }

            public void PickUpParcel(int droneId, int parcelId)
            {
                throw new NotImplementedException();
            }

            public void DeliverParcel(int droneId, int parcelId)
            {
                throw new NotImplementedException();
            }

           

          

            public void UpdatedroneIdInParcel(int ParcelId, int droneId)
            {
                throw new NotImplementedException();
            }
      
        public void EndDroneCharge(int droneId)
        {
            throw new NotImplementedException();
        }
    }
}