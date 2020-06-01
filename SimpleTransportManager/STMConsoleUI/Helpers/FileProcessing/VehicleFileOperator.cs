using STMConsoleUI.Library.BusinessLogic;
using System.Collections.Generic;
using System.Text;

namespace STMConsoleUI.Helpers.FileProcessing
{
    public class VehicleFileOperator
    {
        public StringBuilder SerializeVehicles(List<Vehicle> vehicles)
        {
            StringBuilder data = new StringBuilder();

            data.Append("\r\nId;Name;Capacity;Volume;IsOnTheRoad;NumberOfCompletedCourses;AssignedDriversId;");

            foreach (var vehicle in vehicles)
            {
                data.Append(SerializeVehicle(vehicle));
                data.Append(SerializeAssignedDrivers(vehicle.AssignedDrivers));
                data.Append(";");
            }

            return data;
        }

        private string SerializeVehicle(Vehicle Vehicle)
        {
            return $"\r\n{Vehicle.Id};{Vehicle.Name};{Vehicle.Capacity};{Vehicle.Volume};{Vehicle.IsOnTheRoad};{Vehicle.NumberOfCoursesCompleted};";
        }

        private StringBuilder SerializeAssignedDrivers(List<Driver> drivers)
        {
            StringBuilder data = new StringBuilder();

            foreach (Driver driver in drivers)
            {
                data.Append(SerializeDriverId(driver.Id));
            }

            return data;
        }

        private string SerializeDriverId(int id)
        {
            return $"{id},";
        }

        public Vehicle DeserializeVehicle(string line)
        {
            string[] data = line.Split(";");

            Vehicle vehicle = new Vehicle();

            bool successfullyParsed = true;

            int vehicleId;
            int vehicleCapacity;
            int vehicleVolume;
            int numberOfCourses;

            bool isOnTheRoad;

            if (int.TryParse(data[0], out vehicleId) == false)
            {
                successfullyParsed = false;
            }

            if (int.TryParse(data[2], out vehicleCapacity) == false)
            {
                successfullyParsed = false;
            }

            if (int.TryParse(data[3], out vehicleVolume) == false)
            {
                successfullyParsed = false;
            }

            if (bool.TryParse(data[4], out isOnTheRoad) == false)
            {
                successfullyParsed = false;
            }

            if (int.TryParse(data[5], out numberOfCourses) == false)
            {
                successfullyParsed = false;
            }

            if (successfullyParsed)
            {
                vehicle.Id = vehicleId;
                vehicle.Name = data[1];
                vehicle.Capacity = vehicleCapacity;
                vehicle.Volume = vehicleVolume;
                vehicle.IsOnTheRoad = isOnTheRoad;
                vehicle.NumberOfCoursesCompleted = numberOfCourses;
            }
            else
            {
                vehicle = null;
            }

            return vehicle;
        }
    }
}