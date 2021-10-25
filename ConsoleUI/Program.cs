﻿using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IDAL.DO.DalObject.DalObject dal = new IDAL.DO.DalObject.DalObject();

            Console.WriteLine("\nWelcome to your --Skimmer delivery system management interface--\n\n");
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
                            Console.WriteLine("To add station enter 1" +
                            "\nTo add a drone enter 2" +
                            "\nTo add a customer enter 3" +
                            "\nTo add a paracel enter 4" +
                            "\nTo continue enter 0");
                            int.TryParse(Console.ReadLine(), out choice2);
                            switch (choice2)
                            {
                                case 1:
                                    dal.addStation();
                                    break;
                                case 2:
                                    dal.addDrone();
                                    break;
                                case 3:
                                    dal.addcustomer();
                                    break;
                                case 4:
                                    dal.addParcel();
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
                            Console.WriteLine("To belong parcel to drone enter 1" +
                            "\nTo update parcel's pickup time enter 2" +
                            "\nTo update parcel's delivery time enter 3" +
                            "\nTo charge drone enter 4" +
                            "\nTo end drone charge enter 5" +
                            "\nTo continue enter 0");
                            int.TryParse(Console.ReadLine(), out choice2);
                            int sid;
                            switch (choice2)
                            {
                                case 1:
                                    Console.WriteLine("enter parcel id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.paracelToDrone(id);
                                    Console.WriteLine("successfuly updated\n");
                                    break;

                                case 2:
                                    Console.WriteLine("enter parcel id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.pickUp(id);
                                    Console.WriteLine("successfuly updated\n");
                                    break;

                                case 3:
                                    Console.WriteLine("enter parcel id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.delivered(id);
                                    Console.WriteLine("successfuly updated\n");
                                    break;

                                case 4:
                                    Console.WriteLine("enter drone id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine("enter station id\n");
                                    int.TryParse(Console.ReadLine(), out sid);
                                    dal.sendToCharge(id, sid);
                                    Console.WriteLine("successfuly updated\n");
                                    break;

                                case 5:
                                    Console.WriteLine("enter drone id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine("enter station id\n");
                                    int.TryParse(Console.ReadLine(), out sid);
                                    dal.endCharge(id, sid);
                                    Console.WriteLine("successfuly updated\n");
                                    break;
                            }
                        }
                        break;

                    case 3:
                        //display;
                        while (choice2 != 0)
                        {
                            Console.WriteLine("To display station enter 1" +
                            "\nTo display drone enter 2" +
                            "\nTo display customer enter 3" +
                            "\nTo display paracel enter 4" +
                            "\nTo continuo enter 0");
                            int.TryParse(Console.ReadLine(), out choice2);
                            switch (choice2)
                            {
                                case 1:
                                    Console.WriteLine("enter station id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.stationDisplay(id);
                                    break;

                                case 2:
                                    Console.WriteLine("enter drone id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.droneDisplay(id);
                                    break;

                                case 3:
                                    Console.WriteLine("enter customer id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.customerDisplay(id);
                                    break;

                                case 4:
                                    Console.WriteLine("enter parcel id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.parcelDisplay(id);
                                    break;
                            }
                        }
                        break;

                    case 4:
                        // lists-display
                        while (choice2 != 0)
                        {
                            Console.WriteLine("To display all stations enter 1" +
                            "\nTo display all drones enter 2" +
                            "\nTo display all customers enter 3" +
                            "\nTo display all parcels enter 4" +
                            "\nTo display not associated parcels enter 5" +
                            "\nTo display available to charge stations enter 6" +
                            "\nTo continue enter 0");
                            int.TryParse(Console.ReadLine(), out choice2);
                            switch (choice2)
                            {
                                case 1:
                                    dal.stationsDisplay();
                                    break;

                                case 2:
                                    dal.dronesDisplay();
                                    break;

                                case 3:
                                    dal.customersDisplay();
                                    break;

                                case 4:
                                    dal.parcelsDisplay();
                                    break;

                                case 5:
                                    dal.notAssociatedParcelsDisplay();
                                    break;

                                case 6:
                                    dal.availableToChargeStattions();
                                    break;

                                default:
                                    break;
                            }
                        }
                    break;

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


