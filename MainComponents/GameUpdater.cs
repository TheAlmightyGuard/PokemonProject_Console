using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace PokemonGame.MainComponents
{
    class GameUpdater
    {
        public static string currentVersionLOCAL = "1.4.1";

        public static bool checkUpdates() // RETURN FALSE IF NO UPDATES - RETURN TRUE IF UPDATED
        {
            bool returnValue = false;
            WebClient web = new WebClient();

            string binFolderURL = @"https://github.com/TheAlmightyGuard/PokemonProject_Console/raw/master/bin/Debug/net472";


            string currentVersionGIT = web.DownloadString(@"https://raw.githubusercontent.com/TheAlmightyGuard/PokemonProject_Console/master/currentVersion.txt");
            if (currentVersionGIT.Length > 0)
            {
                if (currentVersionGIT != currentVersionLOCAL)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nA new update found! Downloadling new version... v" + currentVersionGIT);

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                    Thread.Sleep(1200);
                    string pathLOCAL = AppDomain.CurrentDomain.BaseDirectory;
                    File.Move(Path.Combine(pathLOCAL, "PokemonGame.exe"), Path.Combine(pathLOCAL, "PokemonGame1.exe"));
                    File.Move(Path.Combine(pathLOCAL, "PokemonGame.exe.config"), Path.Combine(pathLOCAL, "PokemonGame1.exe.config"));


                    web.DownloadFile(binFolderURL + "/PokemonGame.exe", "PokemonGame.exe");
                    web.DownloadFile(binFolderURL + "/PokemonGame.exe.config", "PokemonGame.exe.config");

                    Process.Start(Path.Combine(pathLOCAL, "PokemonGame.exe"));
                    Environment.Exit(0);
                }
                else
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
    }
}
