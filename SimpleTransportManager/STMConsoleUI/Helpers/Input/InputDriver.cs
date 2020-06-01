namespace STMConsoleUI.Helpers.Input
{
    public static class InputDriver
    {
        public static int CatchDriverId()
        {
            return Input.CatchInt("Enter driver's id: ");
        }

        public static string CatchDriverFirstName()
        {
            return Input.CatchString("Enter driver's first name: ");
        }

        public static string CatchDriverLastName()
        {
            return Input.CatchString("Enter driver's last name: ");
        }
    }
}
