using System;
using System.Collections.Generic;
using System.Text;

namespace STMConsoleUI.Helpers
{
    public class Input
    {
        public int CatchUserOption()
        {
            Console.Write("Enter number of option\n>>: ");
            return ValidateInt(Console.ReadLine());
        }
        
        public int CatchDriverId()
        {
            Console.Write("Enter driver's Id: ");
            return ValidateInt(Console.ReadLine());
        }
        
        public int CatchVehicleId()
        {
            Console.Write("Enter vehicles's Id: ");
            return ValidateInt(Console.ReadLine());
        }

        public int CatchVehicleCapacity()
        {
            Console.Write("Capacity in tonnes: ");
            return ValidateInt(Console.ReadLine());
        }

        public int CatchVehicleVolume()
        {
            Console.Write("Volume in cubic meters: ");
            return ValidateInt(Console.ReadLine());
        }

        private int ValidateInt(string line)
        {
            int result;

            if(int.TryParse(line.Trim(), out result) == false)
            {
                result = -1;
            }

            return result;
        }

        public string CatchDriverFirstName()
        {
            Console.Write("Enter first name: ");
            return Console.ReadLine();
        }

        public string CatchDriverLastName()
        {
            Console.Write("Enter last name: ");
            return Console.ReadLine();
        }

        public string CatchVehicleName()
        {
            Console.Write("Enter vehicle's name: ");
            return Console.ReadLine();
        }
    }
}