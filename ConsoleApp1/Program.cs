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
            bl.addStation(myInputOutput.addStation());
            bl.addDrone(myInputOutput.addDrone());
            bl.addcustomer(myInputOutput.addCustomer());
            bl.addParcel(myInputOutput.addParcel());
            //תוכנית משנית עדכונים
            
            //תוכנית משנית הוספות
            //תוכנית משנית הוספות

        }
    }
}
