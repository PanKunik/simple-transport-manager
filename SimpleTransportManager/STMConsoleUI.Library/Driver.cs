using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMConsoleUI.Library
{
    public class Driver
    {
        public static int freeId { get; private set; } = 0;
        public int Id { get; set; } = 1;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Driver()
        {
            Id = ++freeId;
        }

        public Driver(string firstName, string lastName)
        {
            Id = ++freeId;
            FirstName = firstName;
            LastName = lastName;
        }

        public void EditInfo()
        {
            Console.WriteLine("Enter new full name of the driver: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) == false)
            {
                string[] driverNames = input.Split(" ");

                this.FirstName = driverNames[0];
                this.LastName = driverNames[1];

                Console.WriteLine("Successfully edited driver.");
            }
            else
            {
                Console.WriteLine("Name of driver cannot be null or empty.");
            }
        }
    }
}
