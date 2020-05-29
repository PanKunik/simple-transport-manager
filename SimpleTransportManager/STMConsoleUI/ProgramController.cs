using STMConsoleUI.Helpers;
using STMConsoleUI.Library.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace STMConsoleUI
{
    public class ProgramController
    {
        public Company Company = new Company();    // TODO: no public here and load data from file

        public bool InvokeAction(int option)
        {
            bool result = true;

            StandardMessage Message = new StandardMessage();
            
            switch (option)
            {
                case -1:
                        Console.WriteLine("Option must be integer number greater than 0.");
                    break;
                case 1:
                        NumberOfHiredDrivers();
                    break;
                case 2:
                        NumberOfOwnedVehicles();
                    break;
                case 3:
                        PrintHiredDrivers();
                    break;
                case 4:
                        PrintOwnedVehiclesWithDriversAssigned();
                    break;
                case 5:
                        PrintOwnedVehicles();
                    break;
                case 6:
                        HireDriver();
                    break;
                case 7:
                        AddVehicle();
                    break;
                case 8:
                        EditDriverInformations();
                    break;
                case 9:
                        EditVehicleInformations();
                    break;
                case 10:
                        AssignDriverToVehicle();
                    break;
                case 11:
                        NotifyVehicleSetOff();
                    break;
                case 12:
                        NotifyVehicleArrive();
                    break;
                case 13:
                        SearchForDrivers();
                    break;
                case 14:
                        SearchForVehicles();
                    break;
                case 15:
                        VehiclesOnTheRoad();
                    break;
                case 16:
                        VehiclesSortedByNumberOfCourses();
                    break;
                case 404:
                        Message.Goodbye();
                        result = false;
                    break;
                default:
                        Message.NoOptionFound();
                    break;
            }

            Message.WaitForButton();

            return result;
        }

        private void NumberOfHiredDrivers()
        {
            Console.WriteLine($"Number of hired drivers in the company: { Company.NumberOfHiredDrivers() }");
        }

        private void NumberOfOwnedVehicles()
        {
            Console.WriteLine($"Number of vehicles owned by the company: { Company.NumberOfOwnedVehicles() }");
        }

        private void PrintHiredDrivers()
        {
            List<Driver> drivers = Company.ListOfHiredDrivers();

            if(drivers != null)
            {
                PrintDrivers(drivers);
            }
            else
            {
                StandardMessage Message = new StandardMessage();
                Message.NoDriversHired();
            }
        }

        private void PrintDrivers(IEnumerable<Driver> drivers)
        {
            Console.WriteLine($"{"ID",-10}{"FIRST NAME",-31}{"LAST NAME"}");

            foreach (var driver in drivers)
            {
                Console.WriteLine($"{driver.Id,-10}{driver.FirstName,-31}{driver.LastName}");
            }

            Console.WriteLine();
        }

        private void PrintOwnedVehiclesWithDriversAssigned()
        {
            List<Vehicle> vehicles = Company.ListOfOwnedVehicles();
            StandardMessage Message = new StandardMessage();

            if (vehicles != null)
            {
                foreach(var vehicle in vehicles)
                {
                    Console.WriteLine($"{"ID",-10}{"NAME",-31}{"CAPACITY",-10}{"VOLUME",-10}{"ON THE ROAD",-13}{"NUMBER OF COURSES"}");
                    Console.WriteLine($"{vehicle.Id,-10}{vehicle.Name,-31}{vehicle.Capacity,-10}{vehicle.Volume,-10}{vehicle.IsOnTheRoad.ToString().ToUpper(),-13}{vehicle.NumberOfCoursesCompleted}");

                    if (vehicle.NumberOfAssignedDrivers() >= 1)
                    {
                        Console.WriteLine("Assigned drivers:");

                        PrintDrivers(vehicle.AssignedDrivers);
                    }
                    else
                    {
                        Message.NoDriversAssigned();
                    }
                }
            }
            else
            {
                Message.NoVehiclesOwned();
            }
        }

        private void PrintOwnedVehicles()
        {
            List<Vehicle> vehicles = Company.ListOfOwnedVehicles();

            if (vehicles != null)
            {
                PrintVehicles(vehicles);
            }
            else
            {
                StandardMessage Message = new StandardMessage();
                Message.NoVehiclesOwned();
            }
        }
        private void PrintVehicles(IEnumerable<Vehicle> vehicles)
        {
            Console.WriteLine($"{"ID",-10}{"NAME",-31}{"CAPACITY",-10}{"VOLUME",-10}{"ON THE ROAD",-13}{"NUMBER OF COURSES"}");

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"{vehicle.Id,-10}{vehicle.Name,-31}{vehicle.Capacity,-10}{vehicle.Volume,-10}{vehicle.IsOnTheRoad.ToString().ToUpper(),-13}{vehicle.NumberOfCoursesCompleted}");
            }
        }

        private void HireDriver()
        {
            Driver Driver = new Driver();
            Input Input = new Input();

            Driver.FirstName = Input.CatchDriverFirstName();
            Driver.LastName = Input.CatchDriverLastName();

            StandardMessage Message = new StandardMessage();
            Message.ValidationSummary(Company.AddNewDriver(Driver));
        }

        private void AddVehicle()
        {
            Vehicle Vehicle = new Vehicle();
            Input Input = new Input();

            Vehicle.Name = Input.CatchVehicleName();
            Vehicle.Capacity = Input.CatchVehicleCapacity();
            Vehicle.Volume = Input.CatchVehicleVolume();

            StandardMessage Message = new StandardMessage();
            Message.ValidationSummary(Company.AddNewVehicle(Vehicle));
        }

        private void EditDriverInformations()
        {
            StandardMessage Message = new StandardMessage();

            if (Company.NumberOfHiredDrivers() > 0)
            {
                PrintHiredDrivers();

                Input Input = new Input();
                Driver Driver = new Driver();

                Driver.Id = Input.CatchDriverId();

                if (Company.DoesDriverExist(Driver.Id))
                {
                    Driver.FirstName = Input.CatchDriverFirstName();
                    Driver.LastName = Input.CatchDriverLastName();

                    Message.ValidationSummary(Company.UpdateDriverInfo(Driver));
                }
                else
                {
                    Message.NoDriverFound();
                }
            }
            else
            {
                Message.NoDriversHired();
            }
        }

        private void EditVehicleInformations()
        {
            StandardMessage Message = new StandardMessage();

            if (Company.NumberOfOwnedVehicles() > 0)
            {
                PrintOwnedVehicles();

                Input Input = new Input();
                Vehicle Vehicle = new Vehicle();

                Vehicle.Id = Input.CatchVehicleId();

                if(Company.DoesVehicleExist(Vehicle.Id))
                {
                    Vehicle.Name = Input.CatchVehicleName();
                    Vehicle.Capacity = Input.CatchVehicleCapacity();
                    Vehicle.Volume = Input.CatchVehicleVolume();

                    Message.ValidationSummary(Company.UpdateVehicleInfo(Vehicle));
                }
                else
                {
                    Message.NoVehicleFound();
                }
            }
            else
            {
                Message.NoVehiclesOwned();
            }
        }

        private void AssignDriverToVehicle()
        {
            Input Input = new Input();
            int driverId, vehicleId;

            driverId = Input.CatchDriverId();
            vehicleId = Input.CatchVehicleId();

            StandardMessage Message = new StandardMessage();
            Message.ValidationSummary(Company.AssignDriverToVehicle(driverId, vehicleId));
        }

        private void NotifyVehicleSetOff()
        {
            Input Input = new Input();

            int vehicleId = Input.CatchVehicleId();

            StandardMessage Message = new StandardMessage();
            Message.ValidationSummary(Company.VehicleSetOff(vehicleId));
        }

        private void NotifyVehicleArrive()
        {
            Input Input = new Input();

            int vehicleId = Input.CatchVehicleId();

            StandardMessage Message = new StandardMessage();
            Message.ValidationSummary(Company.VehicleArrive(vehicleId));
        }

        private void SearchForDrivers()
        {
            StandardMessage Message = new StandardMessage();
            
            if (Company.NumberOfHiredDrivers() > 0)
            {
                Input Input = new Input();

                string firstName = Input.CatchDriverFirstName();
                string lastName = Input.CatchDriverLastName();

                List<Driver> drivers = Company.SearchForDrivers(firstName, lastName);

                if (drivers.Count > 0)
                {
                    PrintDrivers(drivers);
                }
                else
                {
                    Message.NoDriverFound();
                }
            }
            else
            {
                Message.NoDriversHired();
            }
        }

        private void SearchForVehicles()
        {
            StandardMessage Message = new StandardMessage();
            
            if (Company.NumberOfOwnedVehicles() > 0)
            {
                Input Input = new Input();

                int minimumCapacity = Input.CatchVehicleCapacity();
                int minimumVolume = Input.CatchVehicleVolume();

                List<Vehicle> vehicles = Company.SearchForVehicles(minimumCapacity, minimumVolume);

                if(vehicles.Count > 0)
                {
                    PrintVehicles(vehicles);
                }
                else
                {
                    Message.NoVehicleFound();
                }    
            }
            else
            {
                Message.NoVehiclesOwned();
            }
        }

        private void VehiclesOnTheRoad()
        {
            StandardMessage Message = new StandardMessage();

            List<Vehicle> vehicles = Company.VehiclesOnTheRoad();

            if (Company.NumberOfOwnedVehicles() > 0)
            {
                if (vehicles.Count > 0)
                {
                    PrintVehicles(vehicles);
                }
                else
                {
                    Message.NoVehicleFound();
                }
            }
            else
            {
                Message.NoVehiclesOwned();
            }
        }

        private void VehiclesSortedByNumberOfCourses()
        {
            StandardMessage Message = new StandardMessage();

            List<Vehicle> vehicles = Company.VehiclesOrderedByNumberOfCompletedCourses();

            if (Company.NumberOfOwnedVehicles() > 0)
            {
                PrintVehicles(vehicles);
            }
            else
            {
                Message.NoVehiclesOwned();
            }
        }
    }
}