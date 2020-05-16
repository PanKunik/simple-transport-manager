using System;
using System.Collections.Generic;
using System.Linq;
using STMConsoleUI.Library;

namespace STMConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company();

            company.AddNewDriver();

            int driverId = 0;

            List<Driver> drivers = company.ListOfHiredDrivers();
            List<Vehicle> vehicles = company.ListOfOwnedVehicles();

            company.AddNewVehicle();

            drivers = company.ListOfHiredDrivers();
            vehicles = company.ListOfOwnedVehicles();

            foreach (var i in drivers)
            {
                Console.WriteLine($"Name: {i.FirstName} {i.LastName}");
            }

            foreach (var i in vehicles)
            {
                Console.WriteLine($"Name: {i.Name}, capacity: {i.Capacity}, volume: {i.Volume}");
            }

            company.AssignDriverToVehicle();

            vehicles = company.ListOfOwnedVehicles();
        }
    }
}
