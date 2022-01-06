﻿using DalApi.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DalApi
{
    internal sealed partial class DalXml : IDal
    {
        #region singleton
        /// <summary>
        /// create a single instance of dal object
        /// </summary>
        public static IDal Instance { get; } = new DalXml();
        #endregion

        #region add func
        public void AddConfig()
        {
            ConfigRoot = XElement.Load(ConfigPath);
            XElement ParcelRunId = new XElement("ParcelRunId",DataSource.parcelRunId);
            XElement free = new("free", 0.15);
            XElement lightWeight = new("lightWeight", 0.1);
            XElement mediumWeight = new("mediumWeight", 0.2);
            XElement heavyWeight = new("heavyWeight", 0.3);
            XElement DroneLoadRate = new("DroneLoadRate", 0.4);

            ConfigRoot.Add( ParcelRunId, free, lightWeight, mediumWeight, heavyWeight, DroneLoadRate);
            ConfigRoot.Save(ConfigPath);
        }

        /// <summary>
        /// add station
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement Id = new("Id", station.Id);
            XElement Name = new("Name", station.Name);
            XElement Latitude = new("Latitude", station.Location.Latitude);
            XElement Longitude = new("Longitude", station.Location.Longitude);
            XElement Location = new("Location", Latitude, Longitude);
            XElement NumOfChargeSlots = new("NumOfChargeSlots", station.NumOfChargeSlots);
            XElement NumOfAvailableChargeSlots = new("NumOfAvailableChargeSlots", station.NumOfAvailableChargeSlots);
            XElement IsActive = new("IsActive", station.IsActive);

            ArrayOfStation.Add(new XElement("Station", Id, Name, Location,
                NumOfChargeSlots, NumOfAvailableChargeSlots, IsActive));
            ArrayOfStation.Save(stationsPath);
        }

        /// <summary>
        /// add drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="firstChargeStationId"></param>
        public void AddDrone(Drone drone, int firstChargeStationId)
        {
            ArrayOfDrone1 = XElement.Load(dronesPath);
            XElement Id = new("Id", drone.Id);
            XElement Model = new("Model", drone.Model);
            XElement Weight = new("Weight", drone.Weight);
            XElement IsActive = new("IsActive", drone.IsActive);

            ArrayOfDrone1.Add(new XElement("Drone", Id, Model, Weight, IsActive));
            ArrayOfDrone1.Save(dronesPath);
            AddDroneCharge(drone.Id, firstChargeStationId, DateTime.Now);
            DecreaseChargeSlot(firstChargeStationId);
        }

        /// <summary>
        /// add drone
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            ArrayOfDrone1 = XElement.Load(dronesPath);
            XElement Id = new("Id", drone.Id);
            XElement Model = new("Model", drone.Model);
            XElement Weight = new("Weight", drone.Weight);
            XElement IsActive = new("IsActive", drone.IsActive);

            ArrayOfDrone1.Add(new XElement("Drone", Id, Model, Weight, IsActive));
            ArrayOfDrone1.Save(dronesPath);
        }

        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            ArrayOfCustomer = XElement.Load(customersPath);
            XElement Id = new("Id", customer.Id);
            XElement Name = new("Name", customer.Name);
            XElement PhoneNumber = new("PhoneNumber", customer.PhoneNumber);
            XElement IsActive = new("IsActive", customer.IsActive);
            XElement Latitude = new("Latitude", customer.Location.Latitude);
            XElement Longitude = new("Longitude", customer.Location.Longitude);
            XElement Location = new("Location", Latitude, Longitude);
            ArrayOfCustomer.Add(new XElement("Customer", Id, Name, PhoneNumber, Location, IsActive));
            ArrayOfCustomer.Save(customersPath);
        }

        /// <summary>
        /// add parcel
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            ArrayOfParcel1 = XElement.Load(ParcelsPath);
            XElement Id = new("Id", parcel.Id);
            XElement SenderId = new("SenderId", parcel.SenderId);
            XElement ReceiverId = new("ReceiverId", parcel.ReceiverId);
            XElement IsActive = new("IsActive", parcel.IsActive);
            XElement DroneId = new("DroneId", parcel.DroneId);
            XElement Weight = new("Weight", parcel.Weight);
            XElement Priority = new("Priority", parcel.Priority);
            XElement Requested = new("Requested", parcel.Requested);
            XElement Scheduled = new("Scheduled", parcel.Scheduled);
            XElement PickedUp = new("PickedUp", parcel.PickedUp);
            XElement Delivered = new("Delivered", parcel.Delivered);

            ArrayOfParcel1.Add(new XElement("Parcel", Id, SenderId, ReceiverId, DroneId,
               Weight, Priority, Requested, Scheduled, PickedUp, Delivered, IsActive));
            ArrayOfParcel1.Save(ParcelsPath);
        }

        /// <summary>
        /// add entity "drone charge"
        /// </summary>
        /// <param name="_droneId"></param>
        /// <param name="_StationId"></param>
        public void AddDroneCharge(int _droneId, int _StationId, DateTime time)
        {
            ArrayOfDroneCharge1 = XElement.Load(DroneChargesPath);
            XElement StationId = new("StationId", _StationId);
            XElement DroneId = new("DroneId", _droneId);
            XElement IsActive = new("IsActive", true);
            XElement Time = new("Time", time);

            ArrayOfDroneCharge1.Add(new XElement("DroneCharge", StationId, DroneId, Time, IsActive));
            ArrayOfDroneCharge1.Save(DroneChargesPath);
        }
        #endregion

        #region delete func
        /// <summary>
        /// delete station
        /// </summary>
        /// <param name="myId"></param>
        public void DeleteStation(int myId)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement stationElement = (from item in ArrayOfStation.Elements()
                                       where int.Parse(item.Element("Id").Value) == myId
                                       select item).FirstOrDefault();

            stationElement.Element("IsActive").Value = "false";
            ArrayOfStation.Save(stationsPath);
        }

        /// <summary>
        /// delete parcel
        /// </summary>
        /// <param name="myId"></param>
        public void DeleteParcel(int myId)
        {
            ArrayOfParcel1 = XElement.Load(ParcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel1.Elements()
                                      where int.Parse(item.Element("Id").Value) == myId
                                      select item).FirstOrDefault();

            parcelElement.Element("IsActive").Value = "false";
            ArrayOfParcel1.Save(ParcelsPath);
        }

        /// <summary>
        /// delete drone
        /// </summary>
        /// <param name="myId"></param>
        public void DeleteDrone(int myId)
        {
            ArrayOfDrone1 = XElement.Load(dronesPath);
            XElement droneElement = (from item in ArrayOfDrone1.Elements()
                                     where int.Parse(item.Element("Id").Value) == myId
                                     select item).FirstOrDefault();

            droneElement.Element("IsActive").Value = "false";
            ArrayOfDrone1.Save(dronesPath);
        }

        /// <summary>
        /// delete customer
        /// </summary>
        /// <param name="myId"></param>
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
        /// <summary>
        /// get station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get drone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone GetDrone(int id)
        {
            ArrayOfDrone1 = XElement.Load(dronesPath);
           
            Drone drone = new();
            XElement droneElement = (from item in ArrayOfDrone1.Elements()
                                     where int.Parse(item.Element("Id").Value) == id
                                     select item).FirstOrDefault();

            drone.Id = int.Parse(droneElement.Element("Id").Value);
            drone.Model = droneElement.Element("Model").Value;
            drone.Weight = WeightCategory(droneElement.Element("Weight").Value);
            drone.IsActive = Convert.ToBoolean(droneElement.Element("IsActive").Value);

            return drone;
        }

        /// <summary>
        /// get customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get parcel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Parcel GetParcel(int id)
        {
            ArrayOfParcel1 = XElement.Load(ParcelsPath);

            Parcel parcel = new();
            XElement parcelElement = (from item in ArrayOfParcel1.Elements()
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
        /// <summary>
        /// get drones list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDronesList()
        {
            //List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            ArrayOfDrone1 = XElement.Load(dronesPath);
            List<Drone> drones = new();
            
            try
            {
                drones = (from item in ArrayOfDrone1.Elements()
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

        /// <summary>
        /// get list of entities "drone charge"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            //List<DroneCharge> droneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);
            ArrayOfDroneCharge1 = XElement.Load(DroneChargesPath);
            List<DroneCharge> droneCharges = new(); 
            
            try
            {
                droneCharges = (from item in ArrayOfDroneCharge1.Elements()
                                select new DroneCharge()
                                {
                                    DroneId = int.Parse(item.Element("DroneId").Value),
                                    StationId = int.Parse(item.Element("StationId").Value),
                                    IsActive = Convert.ToBoolean(item.Element("IsActive").Value),
                                    StartChargeTime = (DateTime)item.Element("Time")
                                }
                          ).ToList();
            }
            catch
            {
                droneCharges = null;
            }
            return droneCharges;
        }

        /// <summary>
        /// get list of stations
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// get customers list
        /// </summary>
        /// <returns></returns>
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
                                 Location = new Location(double.Parse(item.Element
                                    ("Location").Element("Latitude").Value), double.Parse
                                    (item.Element("Location").Element("Longitude").Value)),
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

        /// <summary>
        /// get parcels list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcelsList()
        {
            //List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            ArrayOfParcel1 = XElement.Load(ParcelsPath);
            List<Parcel> parcels = new();
            parcels = (from item in ArrayOfParcel1.Elements()
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

        /// <summary>
        /// parcel's identification (starts with 1000 and jumps by 1)
        /// </summary>
        /// <returns></returns>4 digit number
        public int ParcelRunId()
        {
            XElement parcelRunIdX = ConfigRoot.Element("ParcelRunId");
            int parcelRunId = int.Parse(parcelRunIdX.Value);
            parcelRunIdX.Value = (parcelRunId + 1).ToString();
            return parcelRunId;
        }

        /// <summary>
        /// determines electricity consumption for different situations (free, light weight. ect.) 
        /// </summary>
        /// <returns></returns>
        public double[] DroneElectricityConsumption()
        {
            double[] droneElectricityConsumption = new double[5];
            //free
            droneElectricityConsumption[0] = 0.15;
            //light weight
            droneElectricityConsumption[1] = 0.1;
            //mediumweight
            droneElectricityConsumption[2] = 0.2;
            //heavy weight
            droneElectricityConsumption[3] = 0.3;
            //
            droneElectricityConsumption[4] = 0.4;

            return droneElectricityConsumption;
        }

        /// <summary>
        /// get station's location, giving its ID
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// giving two locations, return the distance in between
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double GetDistance(Location a, Location b)
        {
            double d1 = a.Latitude * (Math.PI / 180.0);
            double num1 = a.Longitude * (Math.PI / 180.0);
            double d2 = b.Latitude * (Math.PI / 180.0);
            double num2 = b.Longitude * (Math.PI / 180.0) - num1;
            double d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                (Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0));
            double resault = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            
            return resault / 100;
        }

        /// <summary>
        /// update station's details
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="newName"></param>
        /// <param name="numOfChargeSlots"></param>
        /// <param name="avialble"></param>
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
        
        /// <summary>
        /// update drone's details
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="newModel"></param>
        public void UpdateDrone(int droneId, string newModel)
        {
            ArrayOfDrone1 = XElement.Load(dronesPath);
            XElement droneChargeElement = (from item in ArrayOfDrone1.Elements()
                                           where int.Parse(item.Element("Id").Value) == droneId
                                           select item).FirstOrDefault();

            droneChargeElement.Element("Model").Value = newModel;
            ArrayOfDroneCharge1.Save(DroneChargesPath);
        }
        
        /// <summary>
        /// update customer's details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="newName"></param>
        /// <param name="newPhone"></param>
        public void UpdateCustomer(int customerId, string newName, string newPhone)
        {
            ArrayOfCustomer = XElement.Load(customersPath);
            XElement customerElement = (from item in ArrayOfCustomer.Elements()
                                        where int.Parse(item.Element("Id").Value) == customerId
                                        select item).FirstOrDefault();

            customerElement.Element("Name").Value = newName;
            customerElement.Element("PhoneNumber").Value = newPhone;
            ArrayOfDroneCharge1.Save(DroneChargesPath);
        }

        /// <summary>
        /// decrease number of available charging slots
        /// </summary>
        /// <param name="stationId"></param>
        public void DecreaseChargeSlot(int stationId)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement stationElement = (from item in ArrayOfStation.Elements()
                                       where int.Parse(item.Element("Id").Value) == stationId
                                       select item).FirstOrDefault();

            int oldNum = int.Parse(stationElement.Element("NumOfAvailableChargeSlots").Value);
            stationElement.Element("NumOfAvailableChargeSlots").Value = (oldNum - 1).ToString();
            ArrayOfStation.Save(stationsPath);
        }

        /// <summary>
        /// increase number of available charging slots
        /// </summary>
        /// <param name="stationId"></param>
        public void IncreaseChargeSlot(int stationId)
        {
            ArrayOfStation = XElement.Load(stationsPath);
            XElement stationElement = (from item in ArrayOfStation.Elements()
                                       where int.Parse(item.Element("Id").Value) == stationId
                                       select item).FirstOrDefault();

            int oldNum = int.Parse(stationElement.Element("NumOfAvailableChargeSlots").Value);
            stationElement.Element("NumOfAvailableChargeSlots").Value = (oldNum + 1).ToString();
            ArrayOfStation.Save(stationsPath);
        }

        /// <summary>
        /// associate parcel to a specific drone
        /// </summary>
        /// <param name="newParcelId"></param>
        /// <param name="droneId"></param>
        public void ScheduleParcelToDrone(int newParcelId, int droneId)
        {
            ArrayOfParcel1 = XElement.Load(ParcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel1.Elements()
                                      where int.Parse(item.Element("Id").Value) == newParcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("DroneId").Value = droneId.ToString();
            parcelElement.Element("Scheduled").Value = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffZ");
            ArrayOfParcel1.Save(ParcelsPath);
        }

        /// <summary>
        /// collect parcel from sender's location
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcelId"></param>
        public void PickUpParcel(int droneId, int parcelId)
        {
            ArrayOfParcel1 = XElement.Load(ParcelsPath);

            XElement parcelElement = (from item in ArrayOfParcel1.Elements()
                                      where int.Parse(item.Element("Id").Value) == parcelId
                                      select item).FirstOrDefault();

            parcelElement.Element("PickedUp").Value = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffZ");
            ArrayOfParcel1.Save(ParcelsPath);
        }

        /// <summary>
        /// drop off parcel at receiver's location
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="parcelId"></param>
        public void DeliverParcel(int droneId, int parcelId)
        {
            ArrayOfParcel1 = XElement.Load(ParcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel1.Elements()
                                      where int.Parse(item.Element("Id").Value) == parcelId
                                      select item).FirstOrDefault();

            int dId = 0;
            parcelElement.Element("Delivered").Value = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffZ");
            parcelElement.Element("DroneId").Value = dId.ToString();
            ArrayOfParcel1.Save(ParcelsPath);
        }

        /// <summary>
        /// update parcel's host ID
        /// </summary>
        /// <param name="ParcelId"></param>
        /// <param name="droneId"></param>
        public void UpdatedroneIdInParcel(int ParcelId, int droneId)
        {
            ArrayOfParcel1 = XElement.Load(ParcelsPath);
            XElement parcelElement = (from item in ArrayOfParcel1.Elements()
                                      where int.Parse(item.Element("Id").Value) == ParcelId
                                      select item).First();

            parcelElement.Element("DroneId").Value = droneId.ToString();
            ArrayOfParcel1.Save(ParcelsPath);
        }
      
        /// <summary>
        /// release drone from charge
        /// </summary>
        /// <param name="droneId"></param>
        public double EndDroneCharge(int droneId, DateTime endTime)
        {
            ArrayOfDroneCharge1 = XElement.Load(DroneChargesPath);
            XElement droneChargeElement = (from item in ArrayOfDroneCharge1.Elements()
                                     where int.Parse(item.Element("DroneId").Value) == droneId
                                     select item).FirstOrDefault();
            droneChargeElement.Element("IsActive").Value = "false";
            var startTime = (DateTime)droneChargeElement.Element("Time");
            var chargeTime =(endTime - startTime).TotalSeconds;
            ArrayOfDroneCharge1.Save(DroneChargesPath);
            return chargeTime;
        }
    }
}