using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame
{
    class Password_Class
    {
        public static bool mainPassword(string userIn)
        {
            string passinput = string.Empty;

            MongoFunctions.readInformation(userIn, out string password, out string userOut, out int lvl, out int coins, out int pkmnCount, out List<informationClass.PokemonInfStruct> party);
            ConsoleKey key;

            Console.Write("\n\nEnter password: ");
            do
            {
                var infokey = Console.ReadKey(intercept: true);
                key = infokey.Key;

                if (!char.IsControl(infokey.KeyChar))
                {
                    Console.Write("*");
                    passinput += infokey.KeyChar;
                }
                else if (infokey.Key == ConsoleKey.Backspace)
                {
                    if (passinput.Length >= 1)
                    {
                        Console.Write("\b \b");
                        passinput = passinput.Remove(passinput.Length - 1, 1);
                    }
                }
            } while (key != ConsoleKey.Enter);

            if (passinput == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string newPassword()
        {
            do
            {
                string passinput = string.Empty;

                ConsoleKey key;

                Console.Clear();
                Console.Write("Enter desired password: ");
                do
                {
                    var infokey = Console.ReadKey(intercept: true);
                    key = infokey.Key;

                    if (!char.IsControl(infokey.KeyChar))
                    {
                        Console.Write("*");
                        passinput += infokey.KeyChar;
                    }
                    else if (infokey.Key == ConsoleKey.Backspace)
                    {
                        if (passinput.Length >= 1)
                        {
                            Console.Write("\b \b");
                            passinput = passinput.Remove(passinput.Length - 1, 1);
                        }
                    }
                } while (key != ConsoleKey.Enter);

                if (passinput.Length >= 1)
                    return passinput.ToLower();
                else
                    continue;
            } while (true);
        }
    }
}
