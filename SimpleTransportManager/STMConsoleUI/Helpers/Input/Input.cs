using System;

namespace STMConsoleUI.Helpers.Input
{
    public static class Input
    {
        public static int CatchInt(string message)
        {
            Console.Write(message);
            return ValidateInt(Console.ReadLine());
        }

        public static string CatchString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private static int ValidateInt(string line)
        {
            int result;

            if (int.TryParse(line.Trim(), out result) == false)
            {
                result = -1;
            }

            return result;
        }
    }
}