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
            int choice2 = -1;
            
            while (choice1 != 0)
            {
                choice2 = -1;
                Console.WriteLine("Choose one of the following:\n" +
                "1: Add a new item\n" +
                "2: Update an item\n" +
                "3: Display an item\n" +
                "4: Display a list of items\n" +
                "0: exit\n");
                int.TryParse(Console.ReadLine(), out choice1);
                switch (choice1)
                {
                    case 1:
                        //add
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
                                case 1:
                                    bl.addStation(myInputOutput.addStation());
                                    break;
                                case 2:
                                    bl.addDrone(myInputOutput.addDrone());
                                    break;
                                case 3:
                                    bl.addcustomer(myInputOutput.addCustomer());
                                    break;
                                case 4:
                                    bl.addParcel(myInputOutput.addParcel());
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                    //case 2:
                    //    //update
                    //    while (choice2 != 0)
                    //    {
                    //        Console.WriteLine("To belong parcel to drone enter 1" +
                    //        "\nTo update parcel's pickup time enter 2" +
                    //        "\nTo update parcel's delivery time enter 3" +
                    //        "\nTo charge drone enter 4" +
                    //        "\nTo end drone charge enter 5" +
                    //        "\nTo return to main menu enter 0\n");
                    //        int.TryParse(Console.ReadLine(), out choice2);
                    //        int sid;
                    //        switch (choice2)
                    //        {
                    //            case 1:
                    //                Console.WriteLine("enter parcel id\n");
                    //                int.TryParse(Console.ReadLine(), out id);
                    //                dal.paracelToDrone(id);
                    //                Console.WriteLine("successfully updated\n");
                    //                break;

                    //            case 2:
                    //                Console.WriteLine("enter parcel id\n");
                    //                int.TryParse(Console.ReadLine(), out id);
                    //                dal.pickUp(id);
                    //                Console.WriteLine("successfully updated\n");
                    //                break;

                    //            case 3:
                    //                Console.WriteLine("enter parcel id\n");
                    //                int.TryParse(Console.ReadLine(), out id);
                    //                dal.delivered(id);
                    //                Console.WriteLine("successfully updated\n");
                    //                break;

                    //            case 4:
                    //                Console.WriteLine("enter drone id\n");
                    //                int.TryParse(Console.ReadLine(), out id);
                    //                Console.WriteLine("enter station id\n");
                    //                int.TryParse(Console.ReadLine(), out sid);
                    //                dal.sendToCharge(id, sid);
                    //                Console.WriteLine("successfully updated\n");
                    //                break;

                    //            case 5:
                    //                Console.WriteLine("enter drone id\n");
                    //                int.TryParse(Console.ReadLine(), out id);
                    //                Console.WriteLine("enter station id\n");
                    //                int.TryParse(Console.ReadLine(), out sid);
                    //                dal.endCharge(id, sid);
                    //                Console.WriteLine("successfully updated\n");
                    //                break;
                    //        }
                    //    }
                    //    break;
                    case 3:
                        //display;
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
                                case 1:
                                    Console.WriteLine("enter station id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.stationDisplay(id));
                                    break;

                                case 2:
                                    Console.WriteLine("enter drone id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.droneDisplay(id));
                                    break;

                                case 3:
                                    Console.WriteLine("enter customer id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.customerDisplay(id));
                                    break;
                                    
                                case 4:
                                    Console.WriteLine("enter parcel id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.parcelDisplay(id));
                                    break;
                            }
                        }
                        break;
                    //case 4:
                    //    // lists-display
                    //    while (choice2 != 0)
                    //    {
                    //        Console.WriteLine("To display all stations enter 1" +
                    //        "\nTo display all drones enter 2" +
                    //        "\nTo display all customers enter 3" +
                    //        "\nTo display all parcels enter 4" +
                    //        "\nTo display not associated parcels enter 5" +
                    //        "\nTo display available to charge stations enter 6" +
                    //        "\nTo return to main menu enter 0\n");
                    //        int.TryParse(Console.ReadLine(), out choice2);
                    //        switch (choice2)
                    //        {
                    //            case 1:
                    //                foreach (IDAL.DO.Station element in dal.stationsDisplay())
                    //                {
                    //                    Console.WriteLine(element);
                    //                }
                    //                break;

                    //            case 2:
                    //                foreach (IDAL.DO.Drone element in dal.dronesDisplay())
                    //                {
                    //                    Console.WriteLine(element);
                    //                }
                    //                break;

                    //            case 3:
                    //                foreach (IDAL.DO.Customer element in dal.customersDisplay())
                    //                {
                    //                    Console.WriteLine(element);
                    //                }
                    //                break;

                    //            case 4:
                    //                foreach (IDAL.DO.Parcel element in dal.parcelsDisplay())
                    //                {
                    //                    Console.WriteLine(element);
                    //                }
                    //                break;

                    //            case 5:
                    //                foreach (IDAL.DO.Parcel element in dal.notAssociatedParcelsDisplay())
                    //                {
                    //                    Console.WriteLine(element);
                    //                }
                    //                break;

                    //            case 6:
                    //                foreach (IDAL.DO.Station element in dal.availableToChargeStattions())
                    //                {
                    //                    Console.WriteLine(element);
                    //                }
                    //                break;

                    //            default:
                    //                break;
                    //        }
                    //    }
                    //    break;
                    case 0:
                        Console.WriteLine("see you soon");
                        return;
                    default:
                        break;
                }
            }
        }
    }
}

