namespace STMConsoleUI.Helpers.Input
{
    public static class InputUser
    {
        public static int CatchUserOption()
        {
            return Input.CatchInt("Enter your option\n>>: ");
        }

        public static string CatchUserPathToFile()
        {
            return Input.CatchString("Enter absolute path to file (*.csv) or leave blank to set the default path (Temp\\Data\\data.csv) in main program folder.\n>>: ");
        }
    }
}
