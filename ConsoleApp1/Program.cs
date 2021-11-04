using System;

namespace ConsoleUI_BL
{
    public class Program
    {
        static void Main(string[] args)
        {
            IBL.BO.BL bl = new IBL.BO.BL();
            ConsoleUI_BL.InputOutput myInputOutput = new InputOutput();
            //תוכנית משנית הוספות
            bl.addStation(myInputOutput.Station());

            bl.addDrone(myInputOutput.Drone());
            bl.

            bl.addcustomer(myInputOutput.Customer());
            bl.addParcel(myInputOutput.Parcel(bl.dal.ParcelRunId()));





            //תוכנית משנית עדכונים

            //תוכנית משנית הוספות
            //תוכנית משנית הוספות

        }
    }
}
