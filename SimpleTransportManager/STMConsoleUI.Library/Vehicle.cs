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
        public static int freeId { get; private set; } = 0;
        public int Id { get; set; } = 1;
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Volume { get; set; }
        public List<Driver> AssignedDrivers { get; set; } = new List<Driver>(); // max 3
        public bool IsOnTheRoad { get; set; } = false;
        public int NumberOfCoursesCompleted { get; set; } = 0;

        public Vehicle()
        {
            Id = ++freeId;
        }

        public Vehicle(string name, int capacity, int volume)
        {
            Id = ++freeId;
            Name = name;
            Capacity = capacity;
            Volume = volume;
        }

        public void EditInfo()
        {
            bool result = true;

            Console.WriteLine("Enter new name of the vehicle: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) == true)
            {
                Console.WriteLine("Name of vehicle cannot be null or empty.");
                result = false;
            }

            int capacity;
            Console.WriteLine("Capacity: ");

            if(int.TryParse(Console.ReadLine(), out capacity) == false)
            {
                Console.WriteLine("Capacity of vehicle cannot be null or empty.");
                result = false;
            }

            int volume;
            Console.WriteLine("Volume: ");

            if(int.TryParse(Console.ReadLine(), out volume) == false)
            {
                Console.WriteLine("Volume of vehicle cannot be null or empty.");
                result = false;
            }

            if(result == true)
            {
                this.Name = input;
                this.Capacity = capacity;
                this.Volume = volume;

                Console.WriteLine("Successfully updated vehicle info.");
            }
        }

        public void AssingNewDriver(Driver driver)
        {
            if (CanAssignNewDriver())
            {
                this.AssignedDrivers.Add(driver);
                Console.WriteLine("Successfully assigned driver to the vehicle.");
            }
            else
            {
                Console.WriteLine("This vehicle has reached maximum number of assigned drivers.");
            }
        }

        private bool CanAssignNewDriver()
        {
            bool result = true;

            if(NumberOfAssignedDrivers() >= 3)
            {
                result = false;
            }

            return result;
        }

        private int NumberOfAssignedDrivers()
        {
            return AssignedDrivers.Count();
        }

        public void SetOff()
        {
            if (this.IsOnTheRoad == false)
            {
                this.IsOnTheRoad = true;
            }
            else
            {
                Console.WriteLine("This vehicle already is on his way.");
            }
        }

        public void Arrive()
        {
            if (this.IsOnTheRoad == true)
            {
                this.IsOnTheRoad = false;
                NumberOfCoursesCompleted++;
            }
            else
            {
                Console.WriteLine("This vehicle already is in company base.");
            }
        }
    }
}
