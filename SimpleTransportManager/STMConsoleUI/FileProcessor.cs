using STMConsoleUI.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace STMConsoleUI
{
    public class FileProcessor
    {
        StreamWriter _fileWriter;
        StreamReader _fileReader;

        private string _fileRootPath;
        private string _fileName;

        public string FileRootPath
        {
            private get
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

        public string FileName
        {
            private get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;

                if(_fileName.EndsWith(".csv") == false)
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

        private void InitializeWriter()
        {
            try
            {
                if (File.Exists($"{FileRootPath}{FileName}") == false)
                {
                    File.Create($"{FileRootPath}{FileName}");
                }

                _fileWriter = new StreamWriter($"{FileRootPath}{FileName}");
            }
            catch(IOException exception)
            {
                throw;
            }
        }

        private void InitializeReader()
        {
            try
            {
                _fileReader = new StreamReader($"{FileRootPath}/{FileName}");
            }
            catch(IOException exception)
            {
                throw;
            }
        }

        public void SaveData(Company company)
        {
            try
            {
                this.InitializeWriter();

                StringBuilder drivers = new StringBuilder();

                drivers.Append("Id;FirstName;LastName;");

                foreach(var driver in company.CompanyDrivers)
                {
                    drivers.Append($"{driver.Id};{driver.FirstName};{driver.LastName};\r\n");
                }

                drivers.Append("\r\n");

                SaveDrivers(drivers);

                StringBuilder vehicles = new StringBuilder();

                vehicles.Append("Id;Name;Capacity;Volume;IsOnTheRoad;NumberOfCompletedCourses;");

                foreach (var vehicle in company.CompanyFleet)
                {
                    vehicles.Append($"{vehicle.Id};{vehicle.Name};{vehicle.Capacity};{vehicle.Volume};{vehicle.IsOnTheRoad};{vehicle.NumberOfCoursesCompleted};");
                    vehicles.Append("\r\n");
                }

                SaveVehicles(vehicles);
                // SaveRelations();
            }
            catch(IOException exception)
            {
                throw;
            }
            finally
            {
                _fileWriter.Close();
            }
        }

        public void SaveDrivers(StringBuilder data)
        {
            try
            {
                _fileWriter.Write(data);
                _fileWriter.Flush();
            }
            catch(IOException exception)
            {
                throw;
            }
            finally
            {
                _fileWriter.Close();
            }
        }

        public void SaveVehicles(StringBuilder data)
        {

        }

        public void SaveRelations()
        {

        }

        public StringBuilder LoadData()
        {
            try
            {
                this.InitializeReader();
            }
            catch(IOException exception)
            {
                _fileReader.Close();
                throw;
            }

            StringBuilder dataFromFile = new StringBuilder();

            while (_fileReader.EndOfStream == false)
            {
                dataFromFile.Append(_fileReader.ReadLine());
            }

            return dataFromFile;
        }

        public void ReturnDeserializedData()
        {
            Deserializer deserialize = new Deserializer();

            deserialize.DeserializeData(this.LoadData());
        }
    }
}
