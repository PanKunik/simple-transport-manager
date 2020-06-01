namespace STMConsoleUI.Helpers.Input
{
    public static class InputUser
    {
        public static int CatchUserOption()
        {
            return Input.CatchInt("Enter your option\n>>: ");
        }
    }
}
