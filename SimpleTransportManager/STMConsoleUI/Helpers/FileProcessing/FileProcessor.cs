using STMConsoleUI.Library.BusinessLogic;
using System.IO;
using System.Linq;
using System.Text;

namespace STMConsoleUI.Helpers.FileProcessing
{
    public class FileProcessor
    {
        Company Company;

        readonly DriverFileOperator DriverFileOperator;
        readonly VehicleFileOperator VehicleFileOperator;

        bool DefaultPath = false;

        StreamWriter _fileWriter = null;
        StreamReader _fileReader = null;

        string _fileRootPath;
        string _fileName;

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

        public FileProcessor(ref Company Company)
        {
            this.Company = Company;

            DriverFileOperator = new DriverFileOperator();
            VehicleFileOperator = new VehicleFileOperator();
        }

        public bool HasDefaultPath()
        {
            return DefaultPath;
        }

        public void TrySetPath(string filePath)
        {
            try
            {
                if (filePath.Length > 0)
                {
                    SetPath(filePath);
                }
                else
                {
                    SetDefaultPath();
                    DefaultPath = true;
                }
            }
            catch(System.IndexOutOfRangeException)
            {
                SetDefaultPath();
                throw;
            }
        }

        private void SetPath(string filePath)
        {
            bool readingFileName = true;
            int i = filePath.Length - 1;

            while (readingFileName)
            {
                if (filePath[i] != '\\')
                {
                    i--;
                }
                else
                {
                    i++;
                    readingFileName = false;
                }
            }

            _fileRootPath = filePath.Remove(i);
            _fileName = filePath.Substring(--i);
        }

        private void SetDefaultPath()
        {
            _fileRootPath = @"..\..\..\Temp\Data\";
            _fileName = "data.csv";
        }

        public string GetFilePath()
        {
            return $"{FileRootPath}{FileName}";
        }

        public void SaveData()
        {
            try
            {
                InitializeWriter();

                StringBuilder SavingData = new StringBuilder();
                SavingData.Append(DriverFileOperator.SerializeDrivers(Company.CompanyDrivers));
                SavingData.Append(VehicleFileOperator.SerializeVehicles(Company.CompanyFleet));

                WriteToFile(SavingData);
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

        private void InitializeWriter()
        {
            try
            {
                if (File.Exists(GetFilePath()))
                {
                    _fileWriter = new StreamWriter(GetFilePath());
                }
                else
                {
                    CreateDesiredDirectories();
                    _fileWriter = new StreamWriter(File.Create(GetFilePath()));
                }
            }
            catch (IOException)
            {
                throw;
            }
        }

        private void CreateDesiredDirectories()
        {
            if (!Directory.Exists(_fileRootPath))
            {
                Directory.CreateDirectory(_fileRootPath);
            }
        }

        private void WriteToFile(StringBuilder SavingData)
        {
            try
            {
                _fileWriter.Write(SavingData);
                _fileWriter.Flush();
            }
            catch(IOException)
            {
                throw;
            }
        }

        public void LoadData()
        {
            try
            {
                InitializeReader();
                LoadFromFile();
            }
            catch (IOException)
            {
                throw;
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

        private void LoadFromFile()
        {
            try
            {
                ReadFileInLoop();
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

        private void ReadFileInLoop()
        {
            bool readingVehicles = false;

            while (_fileReader.EndOfStream == false)
            {
                string line = _fileReader.ReadLine();

                if (line.Contains("Id;FirstName;LastName;"))
                {
                    continue;
                }

                if (line.Contains("Id;Name;Capacity;Volume;"))
                {
                    readingVehicles = true;
                    continue;
                }

                if (readingVehicles)
                {
                    ReadOneVehicle(line);
                }
                else
                {
                    ReadOneDriver(line);
                }
            }
        }

        private void ReadOneDriver(string line)
        {
            Driver Driver = DriverFileOperator.DeserializeDriver(line);

            if (Driver == null)
            {
                return;
            }
            else
            {
                Company.CompanyDrivers.Add(Driver);
            }
        }

        private void ReadOneVehicle(string line)
        {
            Vehicle Vehicle = VehicleFileOperator.DeserializeVehicle(line);

            if (Vehicle == null)
            {
                return;
            }
            else
            {
                Company.CompanyFleet.Add(Vehicle);

                foreach (var driversId in DriverFileOperator.DeserializeDriversIds(line.Split(";")[6]))
                {
                    Company.AssignDriverToVehicle(driversId, Vehicle.Id);
                }
            }
        }
    }
}