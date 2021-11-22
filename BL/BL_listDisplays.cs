using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public IEnumerable<StationToList> DisplayStations()
            {
                List<StationToList> tmp1 = new List<StationToList>();
                List<IDAL.DO.Station> tmp2 = new List<IDAL.DO.Station>();
                var v = dal.getStations();
                foreach (var element in v)
                {
                    StationToList myStation = new StationToList();
                    myStation.id = element.id;
                    myStation.name = element.name;
                    myStation.numOfAvailableChargeSlots = element.numOfAvailableChargeSlots;
                    myStation.numOfOccupiedChargeSlots = element.numOfChargeSlots - element.numOfAvailableChargeSlots;
                    tmp1.Add(myStation);
                }
                return tmp1;
            }
            public List<DroneToList> DisplayDrones()
            {
                List<DroneToList> tmp1 = new List<DroneToList>();
                List<IDAL.DO.Drone> tmp2 = new List<IDAL.DO.Drone>();
                var v = dal.getDrones();
                foreach (var element in v)
                {
                    DroneToList myDrone = new DroneToList();
                    myDrone.id = element.id;
                    myDrone.model = element.model;
                    myDrone.weight = element.weight;
                    tmp1.Add(myDrone);
                }
                return tmp1;
            }
            public List<CustomerToList> DisplayCustomers()
            {
                List<CustomerToList> tmp1 = new List<CustomerToList>();
                List<IDAL.DO.Customer> tmp2 = new List<IDAL.DO.Customer>();
                var v = dal.getCustomers();
                foreach (var element in v)
                {
                    CustomerToList myCustomer = new CustomerToList();
                    myCustomer.id = element.id;
                    myCustomer.name = element.name;
                    tmp1.Add(myCustomer);
                }
                return tmp1;
            }
            public List<ParcelToList> DisplayParcels()
            {
                List<ParcelToList> tmp1 = new List<ParcelToList>();
                List<IDAL.DO.Parcel> tmp2 = new List<IDAL.DO.Parcel>();
                var v = dal.getParcels();
                foreach (var element in v)
                {
                    ParcelToList myParcel = new ParcelToList();
                    myParcel.id = element.id;
                    myParcel.weight = element.weight;
                    myParcel.priority = element.priority;
                    var customersList = dal.getCustomers();
                    string senderName = null;
                    string reciverName = null;
                    foreach (var cElement in customersList)
                    {
                        if (cElement.id == element.senderId) senderName = cElement.name;
                        if (cElement.id == element.reciverId) reciverName = cElement.name;
                    }
                    myParcel.senderName = senderName;
                    myParcel.senderName = reciverName;

                    tmp1.Add(myParcel);
                }
                return tmp1;
            }
            public List<ParcelToList> notAssociatedParcelsDisplay()
            {
                List<ParcelToList> tmp1 = new List<ParcelToList>();
                List<IDAL.DO.Parcel> tmp2 = new List<IDAL.DO.Parcel>();
                var v = dal.getParcels();
                foreach (var element in v)
                {
                    if (element.droneId > 0)
                    {
                        ParcelToList myParcel = new ParcelToList();
                        myParcel.id = element.id;
                        myParcel.weight = element.weight;
                        myParcel.priority = element.priority;
                        var customersList = dal.getCustomers();
                        string senderName = null;
                        string reciverName = null;
                        foreach (var cElement in customersList)
                        {
                            if (cElement.id == element.senderId) senderName = cElement.name;
                            if (cElement.id == element.reciverId) reciverName = cElement.name;
                        }
                        myParcel.senderName = senderName;
                        myParcel.senderName = reciverName;

                        tmp1.Add(myParcel);
                    }
                }
                return tmp1;
            }
            public List<StationToList> availableToChargeStattions()
            {
                List<StationToList> tmp1 = new List<StationToList>();
                List<IDAL.DO.Station> tmp2 = new List<IDAL.DO.Station>();
                var v = dal.getStations();
                foreach (var element in v)
                {
                    if (element.numOfAvailableChargeSlots > 0)
                    {
                        StationToList myStation = new StationToList();
                        myStation.id = element.id;
                        myStation.name = element.name;
                        myStation.numOfAvailableChargeSlots = element.numOfAvailableChargeSlots;
                        myStation.numOfOccupiedChargeSlots = element.numOfChargeSlots - element.numOfAvailableChargeSlots;
                        tmp1.Add(myStation);
                    }
                }
                return tmp1;
            }
        }
    }
}