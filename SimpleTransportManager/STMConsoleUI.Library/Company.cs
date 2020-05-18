using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMConsoleUI.Library
{
    public class Company
    {
        public List<Vehicle> CompanyFleet { get; set; } = new List<Vehicle>();  // max 100
        public List<Driver> CompanyDrivers { get; set; } = new List<Driver>();  // max 50

        public int NumberOfHiredDriver()
        {
            return CompanyDrivers.Count();
        }

        public int NumberOfOwnedVehicles()
        {
            return CompanyFleet.Count();
        }

        public List<Driver> ListOfHiredDrivers()
        {
            List<Driver> drivers;

            if (NumberOfHiredDriver() <= 0)
            {
                drivers = null;
            }
            else
            {
                drivers = CompanyDrivers;
            }

            return drivers;
        }

        public List<Vehicle> ListOfOwnedVehicles()
        {
            List<Vehicle> vehicles;

            if (NumberOfOwnedVehicles() <= 0)
            {
                vehicles = null;
            }
            else
            {
                vehicles = CompanyFleet;
            }

            return vehicles;
        }

        public void AddNewDriver()
        {
            if (CanHireNewDriver())
            {
                Console.WriteLine("Enter full name of the new driver: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) == false)
                {
                    string[] driverNames = input.Split(" ");
                    CompanyDrivers.Add(new Driver(driverNames[0], driverNames[1]));

                    Console.WriteLine("Successfully added new driver.");
                }
                else
                {
                    Console.WriteLine("Name of driver cannot be null or empty.");
                }
            }
            else
            {
                Console.WriteLine("Company has reached her limit of hired drivers.");
            }
        }

        private bool CanHireNewDriver()
        {
            bool result = false;

            if (NumberOfHiredDriver() < 50)
            {
                result = true;
            }

            return result;
        }

        public void AddNewVehicle()
        {
            bool result = true;

            if (CanOweNewVehicle())
            {
                Console.WriteLine("Enter name of new vehicle: ");
                string vehicleName = Console.ReadLine();

                int capacity;
                Console.WriteLine("Enter capacity: ");

                if (int.TryParse(Console.ReadLine(), out capacity) == false)
                {
                    Console.WriteLine("Capacity must be integer number.");
                    result = false;
                }

                int volume;
                Console.WriteLine("Enter volume: ");

                if (int.TryParse(Console.ReadLine(), out volume) == false)
                {
                    Console.WriteLine("Volume must be integer number.");
                    result = false;
                }

                if(result == true)
                {
                    CompanyFleet.Add(new Vehicle(vehicleName, capacity, volume));
                    Console.WriteLine("Successfully added new vehicle.");
                }
            }
            else
            {
                Console.WriteLine("Company has reached her limit of owned vehicles.");
            }
        }

        private bool CanOweNewVehicle()
        {
            bool result = false;

            if (NumberOfOwnedVehicles() <= 100)
            {
                result = true;
            }

            return result;
        }

        public void UpdateDriverInfo(int id)
        {
            Driver driver = DoesDriverExist(id);

            if(driver == null)
            {
                Console.WriteLine("There is no driver with given id.");
            }
            else
            {
                driver.EditInfo();
            }
        }

        public void UpdateVehicleInfo(int id)
        {
            Vehicle vehicle = DoesVehicleExist(id);

            if (vehicle == null)
            {
                Console.WriteLine("There is no vehicle with given id.");
            }
            else
            {
                vehicle.EditInfo();
            }
        }

        public void AssignDriverToVehicle()
        {
            bool result = true;

            Console.WriteLine("Enter the driver's id number: ");

            int driverId;
            if(int.TryParse(Console.ReadLine(), out driverId) == false)
            {
                result = false;
            }

            Console.WriteLine("Enter vehicle's id number: ");

            int vehicleId;
            if(int.TryParse(Console.ReadLine(), out vehicleId) == false)
            {
                result = false;
            }

            if(result == true)
            {
                Vehicle vehicle = DoesVehicleExist(vehicleId);

                if(vehicle != null)
                {
                    Driver driver = DoesDriverExist(driverId);

                    if (driver != null)
                    {
                        vehicle.AssingNewDriver(driver);
                    }
                }
            }
        }

        public void VehicleSetOff(int id)
        {
            Vehicle vehicle = DoesVehicleExist(id);

            if(vehicle != null)
            {
                vehicle.SetOff();
            }
            else
            {
                Console.WriteLine("There is no vehicle with given Id.");
            }
        }

        public void VehicleArrive(int id)
        {
            Vehicle vehicle = DoesVehicleExist(id);

            if (vehicle != null)
            {
                vehicle.Arrive();
            }
            else
            {
                Console.WriteLine("There is no vehicle with given Id.");
            }
        }

        private Vehicle DoesVehicleExist(int id)
        {
            Vehicle vehicle = CompanyFleet.Where(vehicle => vehicle.Id == id).FirstOrDefault();

            if(vehicle == null)
            {
                Console.WriteLine("There is no vehicle with given Id parameter.");
            }

            return vehicle;
        }

        private Driver DoesDriverExist(int id)
        {
            Driver driver = CompanyDrivers.Where(driver => driver.Id == id).FirstOrDefault();

            if (driver == null)
            {
                Console.WriteLine("There is no driver with given Id parameter.");
            }

            return driver;
        }

        public List<Driver> SearchForDrivers()
        {
            List<Driver> drivers = new List<Driver>();

            Console.WriteLine("Enter full name of driver: ");
            string input = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(input) == false)
            {
                string[] names = input.Split(" ");

                drivers = CompanyDrivers.Where(driver => driver.FirstName.ToLower() == names[0].ToLower())
                                .Where(driver => driver.LastName.ToLower() == names[1].ToLower())
                                .ToList();
            }
            else
            {
                Console.WriteLine("Names of driver cannot be empty.");
            }

            return drivers;
        }

        public List<Vehicle> SearchForVehicle()
        {
            bool result = true;

            List<Vehicle> vehicles = new List<Vehicle>();

            Console.WriteLine("Enter minimum capacity: ");

            int capacity;

            if(int.TryParse(Console.ReadLine(), out capacity) == false)
            {
                Console.WriteLine("Capacity must be integer number.");
                result = false;
            }

            Console.WriteLine("Enter minimum volume: ");

            int volume;

            if(int.TryParse(Console.ReadLine(), out volume) == false)
            {
                Console.WriteLine("Volume must be integer number.");
                result = false;
            }

            if(result == true)
            {
                vehicles = CompanyFleet.Where(vehicle => vehicle.Capacity > capacity)
                                        .Where(vehicle => vehicle.Volume > volume)
                                        .ToList();
            }
            else
            {
                Console.WriteLine("You entered bad data.");
            }

            return vehicles;
        }

        public List<Vehicle> VehiclesOnTheRoad()
        {
            return CompanyFleet.Where(vehicle => vehicle.IsOnTheRoad == true).ToList();
        }

        public List<Vehicle> VehiclesOrderedByNumberOfCompletedCourses()
        {
            return CompanyFleet.OrderBy(vehicle => vehicle.NumberOfCoursesCompleted).ToList();
        }
    }
}
