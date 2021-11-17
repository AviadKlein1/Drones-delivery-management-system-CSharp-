﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        //parcel's fields
        public class Parcel
        {
            public int id { get; set; }
            public CustomerInParcel sender { get; set; }
            public CustomerInParcel reciever { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public DroneInParcel DroneInParcel { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

            public Parcel()
            {
                sender = new CustomerInParcel();
                reciever = new CustomerInParcel();
                DroneInParcel = new DroneInParcel();
            }
        
            public Parcel(IDAL.DO.Parcel temp)
            {
                id = temp.id;
                sender = new CustomerInParcel(temp.sender);
                reciever = new CustomerInParcel(temp.reciever);
                DroneInParcel = new DroneInParcel(temp.DroneInParcel);
                weight = temp.weight;
                priority = temp.priority;
                requested = temp.requested;
                scheduled = temp.scheduled;
                pickedUp = temp.pickedUp;
                delivered = temp.delivered;
            }
            public override string ToString()
            {
                return $"ID: {id}\n sender: {sender}\n reciever: {reciever}\n drone: {DroneInParcel}\n" +
                    $" Weight Category: {weight}\n Priority: {priority}\n requested: {requested}\n" +
                    $" scheduled: {scheduled}\n picked up: {pickedUp}\n delivered: {delivered}\n";
            }
        }
        public class ParcelAtCustomer
        {
            public int id { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public IDAL.DO.MyEnums.ParcelStatus parcelStatus { get; set; }
            public CustomerInParcel theSecondSide { get; set; }

            public ParcelAtCustomer(IDAL.DO.ParcelAtCustomer parcel)
            {
                id = parcel.id;
                weight = parcel.weight;
                priority = parcel.priority;
                parcelStatus = parcel.parcelStatus;
                theSecondSide = new CustomerInParcel(parcel.theSecondSide);
            }

            public ParcelAtCustomer()
            {
                theSecondSide = new CustomerInParcel();
            }

            public override string ToString()
            {
                return $"ID: {id}\n the custoner in the second side: {theSecondSide}\n" +
                    $" Weight Category: {weight}\n Priority: {priority}\n Parcel Status: {parcelStatus}";
            }
        }
        public class ParcelInDelivery
        {
            public ParcelInDelivery(IDAL.DO.ParcelInDelivery temp)
            {
                id = temp.id;
                weight = temp.weight;
                priority = temp.priority;
                boolParcelStatus = temp.boolParcelStatus;
                pickUpLocation = new Location(temp.pickUpLocation);
                targetLocation = new Location(temp.targetLocation);
                distance = temp.distance;
                sender =  new CustomerInParcel( temp.sender);
                reciever = new CustomerInParcel(temp.reciever);
            }

            public int id { get; set; }
            public IDAL.DO.MyEnums.WeightCategory weight { get; set; }
            public IDAL.DO.MyEnums.PriorityLevel priority { get; set; }
            public bool boolParcelStatus { get; set; }
            public CustomerInParcel sender { get; set; }
            public CustomerInParcel reciever { get; set; }
            public Location pickUpLocation { get; set; }
            public Location targetLocation { get; set; }
            public double distance { get; set; }

            public override string ToString()
            {
                return $"ID: {id}\n sender: {sender}\n reciever: {reciever}\n bool Parcel Status: {boolParcelStatus}" +
                    $" Weight Category: {weight}\n Priority: {priority}\n distance: {distance}\n" +
                    $" pick Up Location: {pickUpLocation}\n target Location: {targetLocation}\n";
            }
        }
        public class ParcelToList
        {
            public int id { get; set; }
            public string senderName { get; set; }
            public string recieverName { get; set; }
            public MyEnums.WeightCategory weight { get; set; }
            public MyEnums.PriorityLevel priority { get; set; }
            public MyEnums.ParcelStatus parcelStatus { get; set; }

            public override string ToString()
            {
                return $"ID: {id}\n sender name: {senderName}\n reciever name: {recieverName}\n" +
                    $" Weight Category: {weight}\n Priority: {priority}\n Parcel Status: {parcelStatus}";
            }
        }
    }
}
