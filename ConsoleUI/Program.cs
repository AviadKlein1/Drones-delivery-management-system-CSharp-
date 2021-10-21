using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IDAL.DO.DalObject.DalObject dal = new IDAL.DO.DalObject.DalObject();

            Console.WriteLine("\nWelcome to your --Skimmer delivery system management interface--\n\n" );
            int id=1;
            int i=1;
            int j=1;
            
            while (j!=0)
            {
                Console.WriteLine("Do you want to add(enter 1)?\n" +
                "Do you want do update(enter 2)?\n" +
                "Do you want do get display(enter 3)?\n" +
                "do you want to get lists- displays(enter 4)?\n" +
                "to exit enter 0\n");
                int.TryParse(Console.ReadLine(), out j);
                switch (j)
                {
                    case 1:
                        //add
                        while (i != 0)
                        {
                            Console.WriteLine("To add station enter 1," +
                            "\nTo add drone enter 2," +
                            "\nTo add customer enter 3," +
                            "\nTo add paracel enter 4," +
                            "\nTo continuo enter 0");
                            int.TryParse(Console.ReadLine(), out i);
                            switch (i)
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
                            }
                        }
                        break;
                    case 2:
                        //update
                        while (i != 0)
                        {
                            Console.WriteLine("To update parcel to drone enter 1," +
                            "\nTo updateparcel pikeup time enter 2," +
                            "\nTo updatethe time parcel deliverd enter 3," +
                            "\nTo send drone to charge enter 4," +
                            "\nTo end drone charge enter 5, " +
                            "\nTo continuo enter 0");
                            int.TryParse(Console.ReadLine(), out i);
                            int sid;
                            switch (i)
                            {
                                case 1:
                                    Console.WriteLine("enter drone id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.paracelToDrone(id);
                                    break;

                                case 2:
                                    Console.WriteLine("enter parcel id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.pickUp(id);
                                    break;

                                case 3:
                                    Console.WriteLine("enter parcel id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal.delivered(id);
                                    break;

                                case 4:
                                    Console.WriteLine("enter drone id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine("enter station id\n");
                                    int.TryParse(Console.ReadLine(), out sid);
                                    dal.sendToCharge(id, sid);
                                    break;

                                case 5:
                                    Console.WriteLine("enter drone id\n");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine("enter station id\n");
                                    int.TryParse(Console.ReadLine(), out sid);
                                    dal.endCharge(id, sid);
                                    break;
                            }
                        }
                        break;
                    case 3:
                        //display;
                        while (i != 0)
                        {
                            Console.WriteLine("To display station enter 1," +
                            "\nTo display drone enter 2," +
                            "\nTo display customer enter 3," +
                            "\nTo display paracel enter 4," +
                            " \nTo continuo enter 0");
                            int.TryParse(Console.ReadLine(), out i);
                            switch (i)
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
                        while (i != 0)
                        {
                            Console.WriteLine("To display stations enter 1," +
                            "\nTo display drones enter 2," +
                            "\nTo display customers enter 3," +
                            "\nTo display paracels enter 4," +
                            "\nTo display 'not associated parcels' enter 5," +
                            "\nTo display 'available to charge stations' enter 6," +
                            " \nTo continuo enter 0");
                            int.TryParse(Console.ReadLine(), out i);
                            switch (i)
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


