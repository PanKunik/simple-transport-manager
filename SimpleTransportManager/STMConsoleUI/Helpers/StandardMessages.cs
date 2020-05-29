using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace STMConsoleUI.Helpers
{
    public class StandardMessage
    {
        public void ValidationSummary(ICollection<ValidationResult> validationResults)
        {
            foreach(var validationResult in validationResults)
            {
                Console.WriteLine(validationResult);
            }
        }

        public void WaitForButton()
        {
            Console.WriteLine("Press [Enter] key to continue...");
            Console.ReadLine();
        }

        public void Goodbye()
        {
            Console.WriteLine("\nYou are exiting the program. Goodbye!");
        }

        public void NoDriverFound()
        {
            Console.WriteLine("\nNo driver found.");
        }

        public void NoDriversHired()
        {
            Console.WriteLine("\nThe company hasn't hired any drivers yet.");
        }

        public void NoDriversAssigned()
        {
            Console.WriteLine("No drivers assigned yet to this vehicle.\n");
        }

        public void NoVehicleFound()
        {
            Console.WriteLine("\nNo vehicle found.");
        }

        public void NoVehiclesOwned()
        {
            Console.WriteLine("\nThe company hasn't owned any vehicles yet.");
        }

        public void MaximumNumberOfDriversReached()
        {
            Console.WriteLine("\nThis vehicle has reached maximum number of drivers assigned.");
        }

        public void MaxNumberOfHiredDrivers()
        {
            Console.WriteLine("\nThe company has reached her limit of hired drivers.");
        }

        public void MaxNumberOfOwnedVehicles()
        {
            Console.WriteLine("\nThe company has reached her limit of owned vehicles.");
        }

        public void NoFileFound()
        {
            Console.WriteLine("\nCould not find file. Check your path to file.");
        }

        public void NoOptionFound()
        {
            Console.WriteLine("There is no option with given number.");
        }

        public void Menu()
        {
            Console.Clear();

            Console.WriteLine("--- MENU ---");
            Console.WriteLine("1. Display number of hired drivers");
            Console.WriteLine("2. Display number of owned vehicles");
            Console.WriteLine("3. Display list of hired drivers");
            Console.WriteLine("4. Display list of owned vehicles (with assigned drivers)");
            Console.WriteLine("5. Display list of owned vehicles (without assigned drivers)");
            Console.WriteLine("6. Hire new driver");
            Console.WriteLine("7. Add new vehicle");
            Console.WriteLine("8. Update driver's informations");
            Console.WriteLine("9. Update vehicles's informations");
            Console.WriteLine("10. Assign driver to the vehicle");
            Console.WriteLine("11. Notify vehicle set off");
            Console.WriteLine("12. Notify vehicle arrive");
            Console.WriteLine("13. Search for driver (by first and last name)");
            Console.WriteLine("14. Search for vehicle (by minimum capacity and volume)");
            Console.WriteLine("15. Display list of vehicles on the road");
            Console.WriteLine("16. Sort and display list of vehicles (by number of completed courses)");
            Console.WriteLine("404. Exit");
        }
    }
}
