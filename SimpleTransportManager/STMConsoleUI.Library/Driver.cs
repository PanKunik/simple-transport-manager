using System;
using System.Collections.Generic;
using System.Text;

namespace STMConsoleUI.Library
{
    public class Driver
    {
        public static int lastFreeId { get; private set; } = 0;
        public int Id { get; set; } = 1;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Driver()
        {
            lastFreeId++;
        }

        public static void UpdateDriverInfo(Driver driver)
        {
            Console.Write("Podaj nowe imię i nazwisko kierowcy: ");
            string fullname = Console.ReadLine();

            string[] data = fullname.Split(" ");

            driver.FirstName = data[0];
            driver.LastName = data[1];
        }
    }
}
