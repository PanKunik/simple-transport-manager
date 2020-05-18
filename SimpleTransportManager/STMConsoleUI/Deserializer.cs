using STMConsoleUI.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace STMConsoleUI
{
    public class Deserializer
    {
        public List<Vehicle> Vehicles { get; private set; }
        public List<Driver> Drivers { get; private set; }

        public void DeserializeData(StringBuilder data)
        {
            string[] lines = data.ToString().Split('\n');
            Vehicles = null;
            Drivers = null;
        }
    }
}
