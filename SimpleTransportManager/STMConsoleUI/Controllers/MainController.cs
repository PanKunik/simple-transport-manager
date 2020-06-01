using STMConsoleUI.Helpers.Messages;
using STMConsoleUI.Library.BusinessLogic;
using System;

namespace STMConsoleUI.Controllers
{
    public class MainController
    {
        public Company Company;    // TODO: no public here and load data from file
        readonly DriverController DriverController;
        readonly VehicleController VehicleController;

        public MainController()
        {
            Company = new Company();
            DriverController = new DriverController(Company);
            VehicleController = new VehicleController(Company);
        }
        public bool InvokeAction(int option)
        {
            bool result = true;

            switch (option)
            {
                case -1:
                    Console.WriteLine("Option must be integer number greater than 0.");
                    break;
                case 1:
                    DriverController.NumberOfHiredDrivers();
                    break;
                case 2:
                    VehicleController.NumberOfOwnedVehicles();
                    break;
                case 3:
                    DriverController.HiredDrivers();
                    break;
                case 4:
                    VehicleController.OwnedVehiclesWithDriversAssigned();
                    break;
                case 5:
                    VehicleController.OwnedVehicles();
                    break;
                case 6:
                    DriverController.HireDriver();
                    break;
                case 7:
                    VehicleController.AddVehicle();
                    break;
                case 8:
                    DriverController.EditDriverInformations();
                    break;
                case 9:
                    VehicleController.EditVehicleInformations();
                    break;
                case 10:
                    DriverController.AssignDriverToVehicle();
                    break;
                case 11:
                    VehicleController.NotifyVehicleSetOff();
                    break;
                case 12:
                    VehicleController.NotifyVehicleArrive();
                    break;
                case 13:
                    DriverController.SearchForDrivers();
                    break;
                case 14:
                    VehicleController.SearchForVehicles();
                    break;
                case 15:
                    VehicleController.VehiclesOnTheRoad();
                    break;
                case 16:
                    VehicleController.VehiclesSortedByNumberOfCourses();
                    break;
                case 404:
                    StandardMessage.Goodbye();
                    result = false;
                    break;
                default:
                    StandardMessage.NoOptionFound();
                    break;
            }

            StandardMessage.WaitForButton();

            return result;
        }
    }
}