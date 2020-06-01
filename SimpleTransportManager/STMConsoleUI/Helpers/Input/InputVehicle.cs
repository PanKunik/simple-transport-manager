namespace STMConsoleUI.Helpers.Input
{
    public static class InputVehicle
    {
        public static int CatchVehicleId()
        {
            return Input.CatchInt("Enter vehicle's id: ");
        }

        public static string CatchVehicleName()
        {
            return Input.CatchString("Enter vehicle's name: ");
        }

        public static int CatchVehicleVolume()
        {
            return Input.CatchInt("Enter vehicle's volume (in m^3): ");
        }
        public static int CatchVehicleCapacity()
        {
            return Input.CatchInt("Enter vehicle's capacity (in tonnes): ");
        }
    }
}
