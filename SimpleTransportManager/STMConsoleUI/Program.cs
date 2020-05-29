using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using STMConsoleUI.Helpers;
using STMConsoleUI.Library.BusinessLogic;

namespace STMConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            StandardMessage Message = new StandardMessage();
            ProgramController Program = new ProgramController();

            FileProcessor FileProcessor = new FileProcessor(@"E:\Temp\Data\", "data.csv");

            try
            {
                FileProcessor.LoadData(Program.Company);
            }
            catch(IOException)
            {
                Message.NoFileFound();
            }

            Input Input = new Input();

            bool mainLoop = true;

            while(mainLoop == true)
            {
                Message.Menu();

                int option = Input.CatchUserOption();
                mainLoop = Program.InvokeAction(option);
            }

            try
            {
                FileProcessor.SaveData(Program.Company);
            }
            catch (IOException)
            {
                Console.WriteLine("Something went wrong. Check you path to file: ");
                Console.WriteLine(FileProcessor.GetFilePath());
                Console.WriteLine("You entered wrong drive letter or this file is used by another process.");
                Console.WriteLine("[Button] to continue...");
                Console.ReadKey();

                // TODO: Give a user oportunity, to change path?
            }
        }
    }
}