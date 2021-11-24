//Aviad Klein 315552679
//Tomer Peretz 314083080
//C# Mini Project
//Exercise 2
//The program handles and monitors the ongoing management and activity of a courier company using drones  

using System;

namespace ConsoleUI_BL
{
    public class Program
    {
        static void Main(string[] args)
        {
            IBL.BO.BL bl = new IBL.BO.BL();

            ConsoleUI_BL.InputOutput myInputOutput = new ConsoleUI_BL.InputOutput();

            Console.WriteLine("\n-- Welcome to -- Delivery by Drones --  System management interface --\n\n");

            int id = 0;
            int choice1 = -1;
            try
            {
                //allow user to manage and coordinate deliveries
                while (choice1 != 0)
                {
                    int choice2 = -1;
                    Console.WriteLine("Choose one of the following:\n" +
                    "1: Add a new item\n" +
                    "2: Update an item\n" +
                    "3: Display an item\n" +
                    "4: Display a list of items\n" +
                    "0: exit\n");
                    int.TryParse(Console.ReadLine(), out choice1);
                    switch (choice1)
                    {
                        //add items
                        case 1:
                            while (choice2 != 0)
                            {
                                Console.WriteLine(
                                "To add station enter 1" +
                                "\nTo add a drone enter 2" +
                                "\nTo add a customer enter 3" +
                                "\nTo add a parcel enter 4" +
                                "\nTo return to main menu enter 0\n");
                                int.TryParse(Console.ReadLine(), out choice2);
                                switch (choice2)
                                {
                                    //add station
                                    case 1:
                                        bl.addStation(myInputOutput.addStation());
                                        break;
                                    //add drone
                                    case 2:
                                        bl.addDrone(myInputOutput.addDrone());
                                        break;
                                    //add customer
                                    case 3:
                                        bl.addcustomer(myInputOutput.addCustomer());
                                        break;
                                    //add parcel
                                    case 4:
                                        bl.addParcel(myInputOutput.addParcel());
                                        break;

                                    default:
                                        break;
                                }
                            }
                            break;

                        case 2:
                            //update
                            while (choice2 != 0)
                            {
                                Console.WriteLine("To update dorne's data enter 1" + 
                                "\nTo update stations's data enter 2" +
                                "\nTo update customer's data enter 3" +
                                "\nTo send drone to charge enter 4" +
                                "\nTo end drone charge enter 5" +
                                "\nTo shedule parcel to drone enter 6" +
                                "\nTo pick-up parcel enter 7" +
                                "\nTo deliver parcel enter 8" +
                                "\nTo return to main menu enter 0\n");
                                int.TryParse(Console.ReadLine(), out choice2);
                                int cin1;
                                int cin2;
                                string string1 = "";
                                string string2 = "";

                                switch (choice2)
                                {
                                    case 1:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine("enter new model:\n");
                                        string1 = Console.ReadLine();
                                        var flag1 = bl.updatDrone(id, string1);
                                        if (flag1)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("not successfully updated\n");
                                        break;

                                   case 2:
                                        Console.WriteLine("enter station id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine("enter new station name:\n");
                                        string1 = Console.ReadLine();
                                        Console.WriteLine("enter amount of charge slots:\n");
                                        int.TryParse(Console.ReadLine(), out cin1);
                                        var flag2 = bl.updatStation(id, string1, cin1);
                                        if (flag2)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("not successfully updated\n");
                                        break;

                                    case 3:
                                        Console.WriteLine("enter customer id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine("enter new name:\n");
                                        string1 = Console.ReadLine();
                                        Console.WriteLine("enter new phone:\n");
                                        string2 = Console.ReadLine();
                                        var flag3 = bl.updateCustomer(id, string1, string2);
                                        if (flag3)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("not successfully updated\n");
                                        break;

                                    case 4:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                       
                                        var flag4 = bl.sendDroneToCharge(id);
                                        if (flag4)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("not successfully updated\n");
                                        break;

                                    case 5:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine("enter charge - time (in minutes) id:\n");
                                        int.TryParse(Console.ReadLine(), out cin1);
                                        var flag5 = bl.releaseDroneFromCharge(id, cin1);
                                        if (flag5)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("not successfully updated\n");
                                        break;

                                    case 6:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        var flag6 = bl.sheduleParcelToDrone(id);
                                        if (flag6)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("not successfully updated\n");
                                        break;
                                    case 7:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        var flag7 = bl.pickUpParcelByDrone(id);
                                        if (flag7)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("not successfully updated\n");
                                        break;
                                    case 8:
                                        Console.WriteLine("enter drone id:\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        var flag8 = bl.sheduleParcelToDrone(id);
                                        if (flag8)
                                            Console.WriteLine("successfully updated\n");
                                        else
                                            Console.WriteLine("not successfully updated\n");
                                        break;
                                }
                            }
                                break;
                                    case 3:
                            while (choice2 != 0)
                            {
                                Console.WriteLine("To display a station enter 1" +
                                "\nTo display a drone enter 2" +
                                "\nTo display a customer enter 3" +
                                "\nTo display a parcel enter 4" +
                                "\nTo return to main menu enter 0\n");
                                int.TryParse(Console.ReadLine(), out choice2);
                                switch (choice2)
                                {
                                    //dusplay station
                                    case 1:
                                        Console.WriteLine("enter station id\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine(bl.DisplayStation(id));
                                        break;
                                    //display drone
                                    case 2:
                                        Console.WriteLine("enter drone id\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine(bl.DisplayDrone(id));
                                        break;
                                    //display customer
                                    case 3:
                                        Console.WriteLine("enter customer id\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine(bl.DisplayCustomer(id));
                                        break;
                                    //display parcel
                                    case 4:
                                        Console.WriteLine("enter parcel id\n");
                                        int.TryParse(Console.ReadLine(), out id);
                                        Console.WriteLine(bl.parcelDisplay(id));
                                        break;

                                    default:
                                        break;
                                }
                            }
                            break;

                        //display list of items
                        case 4:
                            while (choice2 != 0)
                            {
                                Console.WriteLine("To display list of stations enter 1" +
                                "\nTo display list of drones enter 2" +
                                "\nTo display list of customers enter 3" +
                                "\nTo display list of parcels enter 4" +
                                "\nTo display list of not associated parcels enter 5" +
                                "\nTo display list of available to charge stations enter 6" +
                                "\nTo return to main menu enter 0\n");
                                int.TryParse(Console.ReadLine(), out choice2);
                                switch (choice2)
                                {
                                    //display list of stations
                                    case 1:
                                        var stationsList = bl.DisplayStations();
                                        foreach (var element in stationsList)
                                        {
                                            Console.WriteLine(element + "\n");
                                        }
                                        break;
                                    //display list of drones
                                    case 2:
                                        var dronesList = bl.DisplayDrones();
                                        foreach (var element in dronesList)
                                        {
                                            Console.WriteLine(element + "\n");
                                        }
                                        break;
                                    //display list of customers
                                    case 3:
                                        var customerList = bl.DisplayCustomers();
                                        foreach (var element in customerList)
                                        {
                                            Console.WriteLine(element + "\n");
                                        }
                                        break;
                                    //display list of parcels
                                    case 4:
                                        var parcelsList = bl.DisplayParcels();
                                        foreach (var element in parcelsList)
                                        {
                                            Console.WriteLine(element + "\n");
                                        }
                                        break;
                                    //display list of not associated parcels
                                    case 5:
                                        var notAssociatedParcelsList = bl.DisplayUnassociatedParcels();
                                        foreach (var element in notAssociatedParcelsList)
                                        {
                                            Console.WriteLine(element + "\n");
                                        }
                                        break;
                                    //display list of available to charge stations
                                    case 6:
                                        var avilableToChargeStations = bl.DisplayAvailableStations();
                                        foreach (var element in avilableToChargeStations)
                                        {
                                            Console.WriteLine(element + "\n");
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                            break;

                        //end
                        case 0:
                            Console.WriteLine("see you soon");
                            return;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}