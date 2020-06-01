using STMConsoleUI.Library.BusinessLogic;
using System.Collections.Generic;
using System.Text;

namespace STMConsoleUI.Helpers.FileProcessing
{
    public class DriverFileOperator
    {
        public StringBuilder SerializeDrivers(List<Driver> drivers)
        {
            StringBuilder data = new StringBuilder();

            data.Append("Id;FirstName;LastName;");

            foreach (var driver in drivers)
            {
                data.Append(SerializeDriver(driver));
            }

            return data;
        }

        private string SerializeDriver(Driver Driver)
        {
            return $"\r\n{Driver.Id};{Driver.FirstName};{Driver.LastName};";
        }

        public Driver DeserializeDriver(string line)
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

        public List<int> DeserializeDriversIds(string line)
        {
            List<int> ids = new List<int>();

            string[] driversIds = line.Split(",");

            foreach (var id in driversIds)
            {
                int parsedId;

                if (int.TryParse(id, out parsedId) == true)
                {
                    ids.Add(parsedId);
                }
            }

            return ids;
        }
    }
}