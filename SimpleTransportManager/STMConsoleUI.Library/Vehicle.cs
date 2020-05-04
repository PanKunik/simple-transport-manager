using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace STMConsoleUI.Library
{
    public class Vehicle
    {
        public static int lastFreeId { get; private set; } = 0;
        public int Id { get; set; } = 1;
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Volume { get; set; }
        public List<Driver> AssignedDrivers { get; set; } = new List<Driver>(); // max 3
        public bool IsOnTheRoad { get; set; } = false;
        public int NumberOfCoursesCompleted { get; set; } = 0;

        public Vehicle()
        {
            lastFreeId++;
        }

        public void PrintAllAssignedDrivers()
        {
            Console.WriteLine($"List of all assigned drivers to this vehicle:");
            foreach (var driver in AssignedDrivers)
            {
                Console.WriteLine($"Id: { driver.Id } - Fullname: { driver.FirstName } { driver.LastName }");
            }
        }

        public static bool UpdateVehicleInfo(Vehicle vehicle)
        {
            Console.Write("Enter new name of vehicle: ");
            string newName = Console.ReadLine();

            Console.Write("Enter new capacity of vehicle: ");

            int newCapacity;
            if(Int32.TryParse(Console.ReadLine(), out newCapacity) == false)
            {
                Console.WriteLine("Entered capacity is incorrect.");
                return false;
            }

            Console.Write("Enter new volume of vehicle: ");

            int newVolume;
            if(Int32.TryParse(Console.ReadLine(), out newVolume) == false)
            {
                Console.WriteLine("Entered volume is incorrect.");
                return false;
            }

            vehicle.Name = newName;
            vehicle.Capacity = newCapacity;
            vehicle.Volume = newVolume;

            return true;
        }

        public void AssignNewDriver(Driver driver)
        {
            if(CanAssignNewDriver() == true)
            {
                AssignedDrivers.Add(driver);
            }

            Console.WriteLine($"New driver successfully assigned.");
        }

        private bool CanAssignNewDriver()
        {
            bool result = true;

            if(AssignedDrivers.Count() >= 3)
            {
                Console.WriteLine("This vehicle has reached maximum nubmer of drivers.");
                result = false;
            }

            return result;
        }
    }
}
