using System;
using System.Linq;
using STMConsoleUI.Library;

namespace STMConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company();

            Console.WriteLine("Last id of driver {0}", Driver.lastFreeId);

            company.AddNewDriver("Patryk", "Kunicki");

            company.AddNewVehicle("Volvo FH40", 12000, 340);

            if(Vehicle.UpdateVehicleInfo(company.CompanyFleet.First()) == true)
            {
                Console.WriteLine("Changed successfully");
            }

            company.PrintListOfHiredDrivers();
            company.PrintListOfVehicles();
        }
    }
}
