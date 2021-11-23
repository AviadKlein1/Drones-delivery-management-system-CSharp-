using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// display list of items
        /// </summary>
        public partial class BL
        {
            /// <summary>
            /// display stations list
            /// </summary>
            /// <returns></returns> return list of stations
            public List<StationToList> DisplayStations()
            {
                List<StationToList> tmp1 = new List<StationToList>();
                var v = dal.getStations();
                foreach (var element in v)
                {
                    StationToList myStation = new StationToList();
                    myStation.id = element.id;
                    myStation.name = element.name;
                    myStation.numOfAvailableChargeSlots = element.numOfChargeSlots - numOfDronesThatChargeingInThatStation(element.id);
                    myStation.numOfOccupiedChargeSlots = numOfDronesThatChargeingInThatStation(element.id);
                    tmp1.Add(myStation);
                }
                //if empty list
                if (tmp1 == null)
                    System.Console.WriteLine("empty list\n");
                return tmp1;
            }

            /// <summary>
            /// display drones list
            /// </summary>
            /// <returns></returns> reyurn list of drones
            public List<DroneToList> DisplayDrones()
            {
                var v = dronesList;
                return v;
            }

            /// <summary>
            /// display customers list
            /// </summary>
            /// <returns></returns> return list of customers
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
                    myCustomer.phoneNumber = element.phoneNumber;
                    tmp1.Add(myCustomer);
                }
                return tmp1;
            }

            /// <summary>
            /// display parcels list
            /// </summary>
            /// <returns></returns> return list of parcels
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
                    //display customers (sender and reciever)
                    string senderName = null;
                    string reciverName = null;
                    foreach (var cElement in customersList)
                    {
                        if (cElement.id == element.senderId)
                            senderName = cElement.name;
                        if (cElement.id == element.reciverId)
                            reciverName = cElement.name;
                    }
                    myParcel.senderName = senderName;
                    myParcel.senderName = reciverName;

                    tmp1.Add(myParcel);
                }
                return tmp1;
            }

            /// <summary>
            /// display unassociated parcels list
            /// </summary>
            /// <returns></returns> return list of parcels
            public List<ParcelToList> DisplayUnassociatedParcels()
            {
                List<ParcelToList> tmp1 = new List<ParcelToList>();
                var v = dal.getParcels();
                foreach (var element in v)
                {
                    if (element.droneId == 0)
                    {
                        ParcelToList myParcel = new ParcelToList();
                        myParcel.id = element.id;
                        myParcel.weight = element.weight;
                        myParcel.priority = element.priority;
                        var customersList = dal.getCustomers();
                        //disp;ay customers (sender and reciever)
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

            /// <summary>
            /// display available for charge stations list
            /// </summary>
            /// <returns></returns> return list of stations
            public List<StationToList> DisplayAvailableStations()
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