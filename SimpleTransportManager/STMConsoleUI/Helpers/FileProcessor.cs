using STMConsoleUI.Library.BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace STMConsoleUI.Helpers
{
    public class FileProcessor
    {
        StreamWriter _fileWriter;
        StreamReader _fileReader;

        private string _fileRootPath;
        private string _fileName;

        private string FileRootPath
        {
            get
            {
                return _fileRootPath;
            }

            set
            {
                _fileRootPath = value;

                if (_fileRootPath.Last() != '\\')
                {
                    _fileRootPath += "\\";
                }
            }
        }

        private string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;

                if (_fileName.EndsWith(".csv") == false)
                {
                    _fileName += ".csv";
                }
            }
        }

        public FileProcessor(string filePath, string fileName)
        {
            _fileWriter = null;
            _fileReader = null;

            FileRootPath = filePath;
            FileName = fileName;
        }

        public string GetFilePath()
        {
            return $"{FileRootPath}{FileName}";
        }

        private void InitializeWriter()
        {
            try
            {
                FileStream createdFile;

                if (File.Exists(GetFilePath()) == false)
                {
                    createdFile = File.Create(GetFilePath());
                    _fileWriter = new StreamWriter(createdFile);
                }
                else
                {
                    _fileWriter = new StreamWriter(GetFilePath());
                }
            }
            catch (IOException)
            {
                throw;
            }
        }

        private void SaveDrivers(List<Driver> drivers)
        {
            StringBuilder data = new StringBuilder();

            data.Append("Id;FirstName;LastName;");

            foreach (var driver in drivers)
            {
                data.Append($"\r\n{driver.Id};{driver.FirstName};{driver.LastName};");
            }

            try
            {
                _fileWriter.Write(data);
                _fileWriter.Flush();
            }
            catch (IOException)
            {
                throw;
            }
        }

        private void SaveVehicles(List<Vehicle> vehicles)
        {
            StringBuilder data = new StringBuilder();

            data.Append("\r\nId;Name;Capacity;Volume;IsOnTheRoad;NumberOfCompletedCourses;AssignedDriversId;");

            foreach (var vehicle in vehicles)
            {
                data.Append($"\r\n{vehicle.Id};{vehicle.Name};{vehicle.Capacity};{vehicle.Volume};{vehicle.IsOnTheRoad};{vehicle.NumberOfCoursesCompleted};");

                foreach (Driver driver in vehicle.AssignedDrivers)
                {
                    data.Append($"{driver.Id},");
                }

                data.Append(";");
            }

            try
            {
                _fileWriter.Write(data);
                _fileWriter.Flush();
            }
            catch (IOException)
            {
                throw;
            }
        }

        public void SaveData(Company company)
        {
            try
            {
                InitializeWriter();
                SaveDrivers(company.CompanyDrivers);
                SaveVehicles(company.CompanyFleet);
            }
            catch (IOException)
            {
                throw;
            }
            finally
            {
                if (_fileWriter != null)
                {
                    _fileWriter.Close();
                }
            }
        }

        private void InitializeReader()
        {
            try
            {
                _fileReader = new StreamReader(GetFilePath());
            }
            catch (IOException)
            {
                throw;
            }
        }

        private void LoadFromFile(Company company)
        {
            try
            {
                bool readingVehicles = false;

                while (_fileReader.EndOfStream == false)
                {
                    string line = _fileReader.ReadLine();

                    if (line.Contains("Id;FirstName;LastName;") == true)
                    {
                        continue;
                    }

                    if (line.Contains("Id;Name;Capacity;Volume;") == true)
                    {
                        readingVehicles = true;
                        continue;
                    }

                    if (readingVehicles == false)
                    {
                        Driver driver = DeserializeDriver(line);

                        if (driver == null)
                        {
                            continue;
                        }
                        else
                        {
                            company.CompanyDrivers.Add(driver);
                        }
                    }
                    else
                    {
                        int[] ids;
                        Vehicle VehicleFromFile = DeserializeVehicle(line, out ids);

                        if (VehicleFromFile == null)
                        {
                            continue;
                        }
                        else
                        {
                            company.CompanyFleet.Add(VehicleFromFile);
                            foreach (var id in ids)
                            {
                                company.CompanyFleet.Where(vehicle => vehicle.Id == VehicleFromFile.Id).FirstOrDefault().AssignedDrivers.Add(company.CompanyDrivers.Where(driver => driver.Id == id).FirstOrDefault());
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                throw;
            }
            finally
            {
                if (_fileReader != null)
                {
                    _fileReader.Close();
                }
            }
        }

        private Driver DeserializeDriver(string line)
        {
            string[] data = line.Split(";");

            Driver driver = new Driver();

            int driverId;

            if (int.TryParse(data[0], out driverId) == true)
            {
                driver.Id = driverId;
                driver.FirstName = data[1];
                driver.LastName = data[2];
            }
            else
            {
                driver = null;
            }

            return driver;
        }

        private Vehicle DeserializeVehicle(string line, out int[] ids)
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

                string[] drivers = data[6].Split(",");

                ids = new int[drivers.Length - 1];

                for (int i = 0; i < drivers.Length - 1; i++)
                {
                    int id;

                    if (int.TryParse(drivers[i], out id) == true)
                    {
                        ids[i] = id;
                    }
                }
            }
            else
            {
                vehicle = null;
                ids = null;
            }

            return vehicle;
        }

        public void LoadData(Company company)
        {
            try
            {
                InitializeReader();
                LoadFromFile(company);
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}