using STMConsoleUI.Helpers.Input;
using STMConsoleUI.Helpers.Messages;
using STMConsoleUI.Library.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace STMConsoleUI.Controllers
{
    public class DriverController
    {
        readonly Company Company;

        public DriverController(Company Company)
        {
            this.Company = Company;
        }

        public void AssignDriverToVehicle()
        {
            int driverId, vehicleId;

            driverId = InputDriver.CatchDriverId();
            vehicleId = InputVehicle.CatchVehicleId();

            StandardMessage.ValidationSummary(Company.AssignDriverToVehicle(driverId, vehicleId));
        }

        public void NumberOfHiredDrivers()
        {
            Console.WriteLine($"Number of hired drivers in the company: { Company.NumberOfHiredDrivers() }");
        }

        public void SearchForDrivers()
        {
            if (Company.NumberOfHiredDrivers() > 0)
            {
                string firstName = InputDriver.CatchDriverFirstName();
                string lastName = InputDriver.CatchDriverLastName();

                TryFindDrivers(Company.SearchForDrivers(firstName, lastName);
            }
            else
            {
                StandardMessage.NoDriversHired();
            }
        }

        private void TryFindDrivers(List<Driver> drivers)
        {
            if (drivers.Count > 0)
            {
                PrintDrivers(drivers);
            }
            else
            {
                StandardMessage.NoDriverFound();
            }
        }

        private void TryPrintDrivers(List<Driver> drivers)
        {
            if(Company.NumberOfHiredDrivers() > 0)
            {
                PrintDrivers(drivers);
            }
            else
            {
                StandardMessage.NoDriversHired();
            }
        }

        public void PrintDrivers(IEnumerable<Driver> drivers)
        {
            Console.WriteLine($"{"ID",-10}{"FIRST NAME",-31}{"LAST NAME"}");

            foreach (var driver in drivers)
            {
                PrintDriver(driver);
            }

            Console.WriteLine();
        }

        private void PrintDriver(Driver Driver)
        {
            Console.WriteLine($"{Driver.Id,-10}{Driver.FirstName,-31}{Driver.LastName}");
        }

        public void TryPrintAssignedDrivers(IEnumerable<Driver> drivers)
        {
            if (drivers.Count() > 0)
            {
                Console.WriteLine("Assigned drivers:");
                PrintDrivers(drivers);
            }
            else
            {
                StandardMessage.NoDriversAssigned();
            }
        }

        public void EditDriverInformations()
        {
            if (Company.NumberOfHiredDrivers() > 0)
            {
                HiredDrivers();

                Driver Driver = new Driver();
                Driver.Id = Input.CatchInt("Enter driver's Id: ");

                if (Company.DoesDriverExist(Driver.Id))
                {
                    CatchDriverData(Driver);
                    StandardMessage.ValidationSummary(Company.UpdateDriverInfo(Driver));
                }
                else
                {
                    StandardMessage.NoDriverFound();
                }
            }
            else
            {
                StandardMessage.NoDriversHired();
            }
        }
        public void HiredDrivers()
        {
            TryPrintDrivers(Company.ListOfHiredDrivers());
        }

        private void CatchDriverData(Driver Driver)
        {
            Driver.FirstName = InputDriver.CatchDriverFirstName();
            Driver.LastName = InputDriver.CatchDriverLastName();
        }

        public void HireDriver()
        {
            Driver Driver = new Driver();
            CatchDriverData(Driver);

            StandardMessage.ValidationSummary(Company.AddNewDriver(Driver));
        }
    }
}