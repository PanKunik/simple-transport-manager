using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace STMConsoleUI.Library.BusinessLogic
{
    public class Vehicle
    {
        public static int freeId { get; private set; } = 0; // TODO: change

        [Required(ErrorMessage = "Id of the vehicle is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id of the vehicle must be integer number greater than 0.")]
        public int Id { get; set; } = 1;

        [Required(ErrorMessage = "Name of the vehicle is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name of the vehicle must be between 3 and 30 letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Capacity of the vehicle is required.")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Capacity of the vehicle must be non-negative integer number.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Volume of the vehicle is required.")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Volume of the vehicle must be non-negative integer number.")]
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

        public ICollection<ValidationResult> AssingNewDriver(Driver driver)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            if (CanAssignNewDriver())
            {
                AssignedDrivers.Add(driver);
                validationResults.Add(new ValidationResult("Successfully assigned driver to vehicle."));
            }
            else
            {
                validationResults.Add(new ValidationResult("This vehicle has reached maximum number of assigned drivers."));
            }

            return validationResults;
        }

        private bool CanAssignNewDriver()
        {
            bool result = true;

            if (NumberOfAssignedDrivers() >= 3)
            {
                result = false;
            }

            return result;
        }

        public int NumberOfAssignedDrivers()
        {
            return AssignedDrivers.Count();
        }

        public ValidationResult SetOff()
        {
            ValidationResult validationResult;

            if (IsOnTheRoad == false)
            {
                IsOnTheRoad = true;
                validationResult = new ValidationResult("Successfully notified vehicle set off.");
            }
            else
            {
                validationResult = new ValidationResult("This vehicle already is on his way.");
            }

            return validationResult;
        }

        public ValidationResult Arrive()
        {
            ValidationResult validationResult;

            if (IsOnTheRoad == true)
            {
                IsOnTheRoad = false;
                NumberOfCoursesCompleted++;
                validationResult = new ValidationResult("Successfully notified vehicle arrive.");
            }
            else
            {
                validationResult = new ValidationResult("This vehicle already is in company base.");
            }

            return validationResult;
        }
    }
}
