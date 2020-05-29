using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace STMConsoleUI.Library.BusinessLogic
{
    public class Company
    {
        public List<Vehicle> CompanyFleet { get; set; } = new List<Vehicle>();  // max 100
        public List<Driver> CompanyDrivers { get; set; } = new List<Driver>();  // max 50

        public int NumberOfHiredDrivers()
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

            if (NumberOfHiredDrivers() <= 0)
            {
                drivers = null;
            }
            else
            {
                drivers = CompanyDrivers.OrderBy(driver => driver.Id).ToList();
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
                vehicles = CompanyFleet.OrderBy(vehicle => vehicle.Id).ToList();
            }

            return vehicles;
        }

        public ICollection<ValidationResult> AddNewDriver(Driver driver)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            if (CanHireNewDriver())
            {
                ValidationContext context = new ValidationContext(driver);

                if (Validator.TryValidateObject(driver, context, validationResults, true) == true)
                {
                    CompanyDrivers.Add(driver);
                    validationResults.Add(new ValidationResult("Successfully added new driver."));
                }
            }
            else
            {
                validationResults.Add(new ValidationResult("The company has reached maximum number of hired drivers."));
            }

            return validationResults;
        }

        private bool CanHireNewDriver()
        {
            bool result = false;

            if (NumberOfHiredDrivers() < 50)
            {
                result = true;
            }

            return result;
        }

        public ICollection<ValidationResult> AddNewVehicle(Vehicle vehicle)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            if (CanOwnNewVehicle())
            {
                ValidationContext context = new ValidationContext(vehicle);

                if (Validator.TryValidateObject(vehicle, context, validationResults, true))
                {
                    CompanyFleet.Add(vehicle);
                    validationResults.Add(new ValidationResult("Successfully added new vehicle."));
                }
            }
            else
            {
                validationResults.Add(new ValidationResult("The company has reached maximum number of owned vehicles."));
            }

            return validationResults;
        }

        private bool CanOwnNewVehicle()
        {
            bool result = false;

            if (NumberOfOwnedVehicles() < 100)
            {
                result = true;
            }

            return result;
        }

        public ICollection<ValidationResult> UpdateDriverInfo(Driver updatedDriver)
        {
            ValidationContext context = new ValidationContext(updatedDriver);
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            if (Validator.TryValidateObject(updatedDriver, context, validationResults, true) == true)
            {
                Driver driver = GetDriverById(updatedDriver.Id);

                if (driver != null)
                {
                    driver.FirstName = updatedDriver.FirstName;
                    driver.LastName = updatedDriver.LastName;

                    validationResults.Add(new ValidationResult("Sucessfully updated driver's informations."));
                }
                else
                {
                    validationResults.Add(new ValidationResult("No driver found with given Id."));
                }
            }

            return validationResults;
        }

        public ICollection<ValidationResult> UpdateVehicleInfo(Vehicle updatedVehicle)
        {
            ValidationContext context = new ValidationContext(updatedVehicle);
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            if (Validator.TryValidateObject(updatedVehicle, context, validationResults, true) == true)
            {
                Vehicle vehicle = GetVehicleById(updatedVehicle.Id);

                if (vehicle != null)
                {
                    vehicle.Name = updatedVehicle.Name;
                    vehicle.Capacity = updatedVehicle.Capacity;
                    vehicle.Volume = updatedVehicle.Volume;

                    validationResults.Add(new ValidationResult("Sucessfully updated vehicle's informations."));
                }
                else
                {
                    validationResults.Add(new ValidationResult("No vehicle found with given Id."));
                }
            }

            return validationResults;
        }

        public ICollection<ValidationResult> AssignDriverToVehicle(int driverId, int vehicleId)
        {
            bool validatedSuccessfully = true;

            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            if (driverId < 1)
            {
                validatedSuccessfully = false;
                validationResults.Add(new ValidationResult("Driver's Id must be integer number greater than 1."));
            }

            if (vehicleId < 1)
            {
                validatedSuccessfully = false;
                validationResults.Add(new ValidationResult("Vehicle's Id must be integer number greater than 1."));
            }

            Driver driver = GetDriverById(driverId);

            if(driver == null && validatedSuccessfully)
            {
                validatedSuccessfully = false;
                validationResults.Add(new ValidationResult("There is no driver with given Id."));
            }

            Vehicle vehicle = GetVehicleById(vehicleId);

            if (vehicle == null && validatedSuccessfully)
            {
                validatedSuccessfully = false;
                validationResults.Add(new ValidationResult("There is no vehicle with given Id."));
            }

            if (validatedSuccessfully)
            {
                validationResults = vehicle.AssingNewDriver(driver);
            }

            return validationResults;
        }

        public ICollection<ValidationResult> VehicleSetOff(int id)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            if (id < 1)
            {
                validationResults.Add(new ValidationResult("Vehicle's Id must be integer number greater than 1."));
            }
            else
            {
                Vehicle vehicle = GetVehicleById(id);

                if (vehicle != null)
                {
                    validationResults.Add(vehicle.SetOff());
                }
                else
                {
                    validationResults.Add(new ValidationResult("There is no vehicle with given Id."));
                }
            }

            return validationResults;
        }

        public ICollection<ValidationResult> VehicleArrive(int id)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            if (id < 1)
            {
                validationResults.Add(new ValidationResult("Vehicle's Id must be integer number greater than 1."));
            }
            else
            {
                Vehicle vehicle = GetVehicleById(id);

                if (vehicle != null)
                {
                    validationResults.Add(vehicle.Arrive());
                }
                else
                {
                    validationResults.Add(new ValidationResult("There is no vehicle with given Id."));
                }
            }

            return validationResults;
        }

        public bool DoesDriverExist(int id)
        {
            bool exist = false;

            if(CompanyDrivers.Where(driver => driver.Id == id).Count() > 0)
            {
                exist = true;
            }

            return exist;
        }

        public bool DoesVehicleExist(int id)
        {
            bool exist = false;

            if(CompanyFleet.Where(vehicle => vehicle.Id == id).Count() > 0)
            {
                exist = true;
            }

            return exist;
        }

        private Vehicle GetVehicleById(int id)
        {
            return CompanyFleet.Where(vehicle => vehicle.Id == id).FirstOrDefault();
        }

        private Driver GetDriverById(int id)
        {
            return CompanyDrivers.Where(driver => driver.Id == id).FirstOrDefault();
        }

        public List<Driver> SearchForDrivers(string firstName, string lastName)
        {
            return CompanyDrivers.Where(driver => driver.FirstName == firstName).Where(driver => driver.LastName == lastName).ToList();
        }

        public List<Vehicle> SearchForVehicles(int minimumCapacity, int minimumVolume)
        {
            return CompanyFleet.Where(vehicle => vehicle.Capacity > minimumCapacity).Where(vehicle => vehicle.Volume > minimumVolume).ToList();
        }

        public List<Vehicle> VehiclesOnTheRoad()
        {
            return CompanyFleet.Where(vehicle => vehicle.IsOnTheRoad == true).ToList();
        }

        public List<Vehicle> VehiclesOrderedByNumberOfCompletedCourses()
        {
            return CompanyFleet.OrderByDescending(vehicle => vehicle.NumberOfCoursesCompleted).ToList();
        }
    }
}