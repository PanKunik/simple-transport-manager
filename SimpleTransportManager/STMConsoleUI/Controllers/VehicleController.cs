using STMConsoleUI.Helpers.Input;
using STMConsoleUI.Helpers.Messages;
using STMConsoleUI.Library.BusinessLogic;
using System;
using System.Collections.Generic;

namespace STMConsoleUI.Controllers
{
    public class VehicleController
    {
        readonly Company Company;

        public VehicleController(Company Company)
        {
            this.Company = Company;
        }

        public void NumberOfOwnedVehicles()
        {
            Console.WriteLine($"Number of vehicles owned by the company: { Company.NumberOfOwnedVehicles() }");
        }

        public void SearchForVehicles()
        {
            if (Company.NumberOfOwnedVehicles() > 0)
            {
                int minimumCapacity = InputVehicle.CatchVehicleCapacity();
                int minimumVolume = InputVehicle.CatchVehicleVolume();

                TryFindVehicles(Company.SearchForVehicles(minimumCapacity, minimumVolume));
            }
            else
            {
                StandardMessage.NoVehiclesOwned();
            }
        }

        private void TryFindVehicles(List<Vehicle> vehicles)
        {
            if (vehicles.Count > 0)
            {
                PrintVehicles(vehicles);
            }
            else
            {
                StandardMessage.NoVehicleFound();
            }
        }

        private void TryPrintVehicles(List<Vehicle> vehicles)
        {
            if (Company.NumberOfOwnedVehicles() > 0)
            {
                PrintVehicles(vehicles);
            }
            else
            {
                StandardMessage.NoVehiclesOwned();
            }
        }

        private void PrintVehicles(IEnumerable<Vehicle> vehicles)
        {
            Console.WriteLine($"{"ID",-10}{"NAME",-31}{"CAPACITY",-10}{"VOLUME",-10}{"ON THE ROAD",-13}{"NUMBER OF COURSES"}");

            foreach (var vehicle in vehicles)
            {
                PrintVehicle(vehicle);
            }
        }

        private void PrintVehicle(Vehicle Vehicle)
        {
            Console.WriteLine($"{Vehicle.Id,-10}{Vehicle.Name,-31}{Vehicle.Capacity,-10}{Vehicle.Volume,-10}{Vehicle.IsOnTheRoad.ToString().ToUpper(),-13}{Vehicle.NumberOfCoursesCompleted}");
        }

        public void OwnedVehiclesWithDriversAssigned()
        {
            List<Vehicle> vehicles = Company.ListOfOwnedVehicles();

            if (vehicles != null)
            {
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"{"ID",-10}{"NAME",-31}{"CAPACITY",-10}{"VOLUME",-10}{"ON THE ROAD",-13}{"NUMBER OF COURSES"}");
                    PrintVehicle(vehicle);

                    DriverController DriverController = new DriverController(Company);
                    DriverController.TryPrintAssignedDrivers(vehicle.AssignedDrivers);
                }
            }
            else
            {
                StandardMessage.NoVehiclesOwned();
            }
        }

        public void EditVehicleInformations()
        {
            if (Company.NumberOfOwnedVehicles() > 0)
            {
                OwnedVehicles();

                Vehicle Vehicle = new Vehicle();
                Vehicle.Id = InputVehicle.CatchVehicleId();

                if (Company.DoesVehicleExist(Vehicle.Id))
                {
                    CatchVehicleData(Vehicle);
                    StandardMessage.ValidationSummary(Company.UpdateVehicleInfo(Vehicle));
                }
                else
                {
                    StandardMessage.NoVehicleFound();
                }
            }
            else
            {
                StandardMessage.NoVehiclesOwned();
            }
        }

        public void OwnedVehicles()
        {
            TryPrintVehicles(Company.ListOfOwnedVehicles());
        }

        private void CatchVehicleData(Vehicle Vehicle)
        {
            Vehicle.Name = InputVehicle.CatchVehicleName();
            Vehicle.Capacity = InputVehicle.CatchVehicleCapacity();
            Vehicle.Volume = InputVehicle.CatchVehicleVolume();
        }

        public void AddVehicle()
        {
            Vehicle Vehicle = new Vehicle();
            CatchVehicleData(Vehicle);

            StandardMessage.ValidationSummary(Company.AddNewVehicle(Vehicle));
        }

        public void VehiclesOnTheRoad()
        {
            TryPrintVehicles(Company.VehiclesOnTheRoad());
        }

        public void VehiclesSortedByNumberOfCourses()
        {
            TryPrintVehicles(Company.VehiclesOrderedByNumberOfCompletedCourses());
        }

        public void NotifyVehicleSetOff()
        {
            int vehicleId = InputVehicle.CatchVehicleId();
            StandardMessage.ValidationSummary(Company.VehicleSetOff(vehicleId));
        }

        public void NotifyVehicleArrive()
        {
            int vehicleId = InputVehicle.CatchVehicleId();
            StandardMessage.ValidationSummary(Company.VehicleArrive(vehicleId));
        }
    }
}