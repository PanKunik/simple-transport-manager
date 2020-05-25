using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using STMConsoleUI.Library;

namespace STMConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company();

            FileProcessor fp = new FileProcessor(@"E:\Temp\Data\", "data.csv");

            try
            {
                fp.LoadData(company);
            }
            catch(IOException)
            {

            }

            bool programLooping = true;

            while(programLooping == true)
            {
                DisplayMenu();

                int userOption = CaptureUserInput();

                programLooping = InvokeAction(company, userOption);

                Console.WriteLine("[Button] to continue...");
                Console.ReadKey();
            }

            try
            {
                fp.SaveData(company);
            }
            catch(IOException)
            {
                Console.WriteLine("Something went wrong. Check you path to file: ");
                Console.WriteLine(fp.GetFilePath());
                Console.WriteLine("You entered wrong drive letter or this file is used by another process.");
                Console.WriteLine("[Button] to continue...");
                Console.ReadKey();

                // TODO: Give a user oportunity, to change path?
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("--- MENU ---");
            Console.WriteLine("1. Display number of hired drivers");
            Console.WriteLine("2. Display number of owned vehicles");
            Console.WriteLine("3. Display list of hired drivers");
            Console.WriteLine("4. Display list of owned vehicles (with assigned drivers)");
            Console.WriteLine("5. Display list of owned vehicles (without assigned drivers)");
            Console.WriteLine("6. Hire new driver");
            Console.WriteLine("7. Add new vehicle");
            Console.WriteLine("8. Update driver's informations");
            Console.WriteLine("9. Update vehicles's informations");
            Console.WriteLine("10. Assign driver to the vehicle");
            Console.WriteLine("11. Notify vehicle set off");
            Console.WriteLine("12. Notify vehicle arrive");
            Console.WriteLine("13. Search for driver (by first and last name)");
            Console.WriteLine("14. Search for vehicle (by minimum capacity and volume)");
            Console.WriteLine("15. Display list of vehicles on the road");
            Console.WriteLine("16. Sort and display list of vehicles (by number of completed courses)");
            Console.WriteLine("404. Exit");
            Console.Write("Enter number of option\n>>: ");
        }

        static int CaptureUserInput()
        {
            int option;

            if (ValidateInput(out option) == false)
            {
                option = -1;
            }

            return option;
        }

        static bool ValidateInput(out int input)
        {
            bool result = true;

            if(int.TryParse(Console.ReadLine(), out input) == false)
            {
                Console.WriteLine("You have entered wrong input. Option must be integer number.");
                result = false;
            }

            return result;
        }

        static bool InvokeAction(Company company, int id)
        {
            bool result = true;

            switch(id)
            {
                case 1:
                    Console.WriteLine($"Number of hired drivers: {company.NumberOfHiredDriver()}"); 
                    break;
                case 2:
                    Console.WriteLine($"Number of owned vehicles: {company.NumberOfOwnedVehicles()}");
                    break;
                case 3:
                    Console.WriteLine("List of hired drivers: ");
                    List <Driver> drivers = company.ListOfHiredDrivers();

                    foreach(Driver driver in drivers)
                    {
                        Console.WriteLine($"ID: {driver.Id} - {driver.FirstName} {driver.LastName}");
                    }
                    break;
                case 4:
                    Console.WriteLine("List of owned vehicles (with assigned drivers): ");
                    List<Vehicle> vehicles = company.ListOfOwnedVehicles();

                    foreach(Vehicle vehicle in vehicles)
                    {
                        Console.WriteLine($"ID: {vehicle.Id} {vehicle.Name} - Capacity: {vehicle.Capacity}, volume: {vehicle.Volume}\nnumber of courses: {vehicle.NumberOfCoursesCompleted}, on the road: {vehicle.IsOnTheRoad}");

                        foreach(Driver driver in vehicle.AssignedDrivers)
                        {
                            Console.WriteLine($"ID: {driver.Id} - {driver.FirstName} {driver.LastName}");
                        }
                    }
                    break;
                case 5:
                    Console.WriteLine("List of owned vehicles (without assigned drivers): ");
                    List<Vehicle> vehicles1 = company.ListOfOwnedVehicles();

                    foreach (Vehicle vehicle in vehicles1)
                    {
                        Console.WriteLine($"ID: {vehicle.Id} {vehicle.Name} - Capacity: {vehicle.Capacity}, volume: {vehicle.Volume}\nnumber of courses: {vehicle.NumberOfCoursesCompleted}, on the road: {vehicle.IsOnTheRoad}");
                    }
                    break;
                case 6:
                    company.AddNewDriver();
                    break;
                case 7:
                    company.AddNewVehicle();
                    break;
                case 8:
                    Console.WriteLine("Enter id of driver: ");
                    int driverId;

                    if (ValidateInput(out driverId) == true)
                    {
                        company.UpdateDriverInfo(driverId);
                    }
                    break;
                case 9:
                    Console.WriteLine("Enter id of vehicle: ");
                    int vehicleId;

                    if (ValidateInput(out vehicleId) == true)
                    {
                        company.UpdateVehicleInfo(vehicleId);
                    }
                    break;
                case 10:
                        company.AssignDriverToVehicle();
                    break;
                case 11:
                    Console.WriteLine("Enter id of vehicle: ");
                    int vehicleId1;

                    if (ValidateInput(out vehicleId1) == true)
                    {
                        company.VehicleSetOff(vehicleId1);
                    }
                    break;
                case 12:
                    Console.WriteLine("Enter id of vehicle: ");
                    int vehicleId2;

                    if (ValidateInput(out vehicleId2) == true)
                    {
                        company.VehicleArrive(vehicleId2);
                    }
                    break;
                case 13:
                    List<Driver> drivers1 = company.SearchForDrivers();

                    foreach(Driver driver in drivers1)
                    {
                        Console.WriteLine($"ID: {driver.Id} - {driver.FirstName} {driver.LastName}");
                    }
                    break;
                case 14:
                    List<Vehicle> vehicles2 = company.SearchForVehicle();

                    foreach (Vehicle vehicle in vehicles2)
                    {
                        Console.WriteLine($"ID: {vehicle.Id} {vehicle.Name} - Capacity: {vehicle.Capacity}, volume: {vehicle.Volume}\nnumber of courses: {vehicle.NumberOfCoursesCompleted}, on the road: {vehicle.IsOnTheRoad}");
                    }
                    break;
                case 15:
                    List<Vehicle> vehicles3 = company.VehiclesOnTheRoad();

                    foreach (Vehicle vehicle in vehicles3)
                    {
                        Console.WriteLine($"ID: {vehicle.Id} {vehicle.Name} - Capacity: {vehicle.Capacity}, volume: {vehicle.Volume}\nnumber of courses: {vehicle.NumberOfCoursesCompleted}, on the road: {vehicle.IsOnTheRoad}");
                    }
                    break;
                case 16:
                    List<Vehicle> vehicles4 = company.VehiclesOnTheRoad();

                    foreach (Vehicle vehicle in vehicles4)
                    {
                        Console.WriteLine($"ID: {vehicle.Id} {vehicle.Name} - Capacity: {vehicle.Capacity}, volume: {vehicle.Volume}\nnumber of courses: {vehicle.NumberOfCoursesCompleted}, on the road: {vehicle.IsOnTheRoad}");
                    }
                    break;
                case 404:
                        result = false;
                    break;
                default:
                    Console.WriteLine("There is no option with this number.");
                    break;
            }

            return result;
        }
    }
}
