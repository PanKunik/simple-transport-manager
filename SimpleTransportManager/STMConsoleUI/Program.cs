using System;
using System.IO;
using STMConsoleUI.Controllers;
using STMConsoleUI.Helpers.FileProcessing;
using STMConsoleUI.Helpers.Input;
using STMConsoleUI.Helpers.Messages;

namespace STMConsoleUI
{
    class Program
    {
        static MainController Controller = new MainController();
        static FileProcessor FileProcessor = new FileProcessor(ref Controller.Company, @"E:\Temp\Data\", "data.csv");

        static void Main(string[] args)
        {
            LoadDataFromFile();
            MainLoop();
            SaveDataToFile();
        }

        static void LoadDataFromFile()
        {
            try
            {
                FileProcessor.LoadData();
            }
            catch (IOException)
            {
                StandardMessage.NoFileFound();
            }
        }

        static void MainLoop()
        {
            bool programIsLooped;

            do
            {
                StandardMessage.Menu();

                int option = InputUser.CatchUserOption();
                programIsLooped = Controller.InvokeAction(option);
            }
            while (programIsLooped);
        }

        static void SaveDataToFile()
        {
            try
            {
                FileProcessor.SaveData();
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