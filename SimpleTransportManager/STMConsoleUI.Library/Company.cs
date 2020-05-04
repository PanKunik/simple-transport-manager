using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMConsoleUI.Library
{
    public class Company
    {
        public List<Vehicle> CompanyFleet { get; set; } = new List<Vehicle>();
        public List<Driver> CompanyDrivers { get; set; } = new List<Driver>();

        public void PrintListOfHiredDrivers()
        {
            Console.WriteLine($"List of hired drivers in company");
            foreach(var driver in CompanyDrivers)
            {
                Console.WriteLine($"Id: { driver.Id }\nFirst name: { driver.FirstName }\nLast name: { driver.LastName }\n");
            }
        }

        public void PrintListOfVehicles()
        {
            Console.WriteLine($"List of vehicles that belong to the company");
            foreach (var vehicle in CompanyFleet)
            {
                Console.WriteLine($"Id: { vehicle.Id }\nName: { vehicle.Name }\nCapacity: { vehicle.Capacity }\nVolume: { vehicle.Volume }\nIs on the road?: { vehicle.IsOnTheRoad }\nNumber of completed courses: { vehicle.NumberOfCoursesCompleted }\nAssigned drivers:");
                vehicle.PrintAllAssignedDrivers();
            }
        }

        public void AddNewDriver(string firstName, string lastName)
        {
            if(CanHireNewDriver() == true)
            {
                CompanyDrivers.Add(new Driver()
                {
                    Id = Driver.lastFreeId,
                    FirstName = firstName,
                    LastName = lastName
                });

                Console.WriteLine($"New driver successfully added.");
            }
            else
            {
                Console.WriteLine($"Company has reached her limit of drivers.");
            }
        }

        private bool CanHireNewDriver()
        {
            bool result = true;

            if(CompanyDrivers.Count() >= 50)
            {
                result = false;
            }

            return result;
        }

        public void AddNewVehicle(string name, int capacity, int volume)
        {
            if (CanAddNewVehicle() == true)
            {
                CompanyFleet.Add(new Vehicle()
                {
                    Id = Vehicle.lastFreeId,
                    Name = name,
                    Capacity = capacity,
                    Volume = volume
                });

                Console.WriteLine($"New vehicle successfully added.");
            }
            else
            {
                Console.WriteLine($"Company has reached her limit of vehicles.");
            }
        }

        private bool CanAddNewVehicle()
        {
            bool result = true;

            if(CompanyFleet.Count() >= 100)
            {
                result = false;
            }

            return result;
        }
    }
}
