using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PokemonGame.informationClass;
using PokemonGame;

namespace PokemonGame.FunctionClasses
{
    public class miscellaneousFunc
    {
        public static userInformation userInfo = new userInformation();
        public static string[] Options = { "NEW GAME", "CONTINUE?" };
        public static string askUser()
        {
            do //USERNAME
            {
                Console.Clear();
                Console.WriteLine("Enter your username: [MAXIMUM OF 8 CHARACTERS] (Type 'return' to return)");
                string input = Console.ReadLine();
                if (input.Length >= 1 && input.Length <= 8)
                {
                    return input;
                }
                else if (input.Length >= 8)
                {
                    Console.WriteLine("Username is too long!");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            } while (true);
        }

        public static void pokemonChoiceMenu()
        {
            for (int i = 0; i < publicInformations.choicesLength(); i++)
            {
                publicInformations.LowerCase[i] = publicInformations.pokemonchoices[i].ToLower();
            }
            Main.ProgramMain.OpeningLoading();
            do //Pokemon
            {
                Console.Clear();
                PokemonListBOX();
                Console.Write("\nPick a pokemon: ");
                string input = Console.ReadLine().ToLower();
                if (Int32.TryParse(input, out int numinput) && numinput >= 0 && numinput <= (publicInformations.pokemonchoices.Length - 1))
                {
                    string chosen = publicInformations.pokemonchoices[numinput - 1];
                    pokeInformation.basicInfo.pokemon = publicInformations.pokemonchoices[numinput - 1];
                    Console.WriteLine("You have chosen the pokemon... {0}", chosen);
                    Console.WriteLine("Confirm? [Y/N]:");

                    string confirm = Console.ReadLine().ToUpper();
                    if (confirm == "Y")
                    {
                        userInformation.firstTime = false;
                        getExtra();
                        break;
                    }
                    else if (confirm == "N")
                    {
                        Console.WriteLine("Cancelled!");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        userInformation.firstTime = false;
                        getExtra();
                        break;
                    }
                }
                else if (!(input == "") && Array.IndexOf(publicInformations.LowerCase, input) >= 0)
                {
                    string chosen = publicInformations.pokemonchoices[Array.IndexOf(publicInformations.LowerCase, input)];
                    pokeInformation.basicInfo.pokemon = publicInformations.pokemonchoices[Array.IndexOf(publicInformations.LowerCase, input)];
                    Console.WriteLine("You have chosen the pokemon... {0}", chosen);
                    Console.WriteLine("Confirm? [Y/N]:");

                    string confirm = Console.ReadLine().ToUpper();
                    if (confirm == "Y")
                    {
                        userInformation.firstTime = false;
                        getExtra();
                        break;
                    }
                    else if (confirm == "N")
                    {
                        Console.WriteLine("Cancelled!");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        userInformation.firstTime = false;
                        getExtra();
                        break;
                    }
                }
                else if (input == "")
                {
                    Console.WriteLine("That pokemon does not exist!");
                    Console.ReadKey();
                    continue;
                }
            } while (true);
            Console.Clear();
        }

        public static void newpokemonChoiceMenu(out string pokemon, out string type)
        {
            string chosen;
            for (int i = 0; i < publicInformations.choicesLength(); i++)
            {
                publicInformations.LowerCase[i] = publicInformations.pokemonchoices[i].ToLower();
            }
            Main.ProgramMain.OpeningLoading();
            do //Pokemon
            {
                Console.Clear();
                PokemonListBOX();
                Console.Write("\nPick a pokemon: ");
                string input = Console.ReadLine().ToLower();
                if (Int32.TryParse(input, out int numinput) && numinput >= 0 && numinput <= (publicInformations.pokemonchoices.Length - 1))
                {
                    chosen = publicInformations.pokemonchoices[numinput - 1];
                    pokeInformation.basicInfo.pokemon = publicInformations.pokemonchoices[numinput - 1];
                    Console.WriteLine("You have chosen the pokemon... {0}", chosen);
                    Console.WriteLine("Confirm? [Y/N]:");

                    string confirm = Console.ReadLine().ToUpper();
                    if (confirm == "Y")
                    {
                        userInformation.firstTime = false;
                        break;
                    }
                    else if (confirm == "N")
                    {
                        Console.WriteLine("Cancelled!");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        userInformation.firstTime = false;
                        break;
                    }
                }
                else if (!(input == "") && Array.IndexOf(publicInformations.LowerCase, input) >= 0)
                {
                    chosen = publicInformations.pokemonchoices[Array.IndexOf(publicInformations.LowerCase, input)];
                    
                    Console.WriteLine("You have chosen the pokemon... {0}", chosen);
                    Console.WriteLine("Confirm? [Y/N]:");

                    string confirm = Console.ReadLine().ToUpper();
                    if (confirm == "Y")
                    {
                        userInformation.firstTime = false;
                        
                        break;
                    }
                    else if (confirm == "N")
                    {
                        Console.WriteLine("Cancelled!");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        userInformation.firstTime = false;
                        break;
                    }
                }
                else if (input == "")
                {
                    Console.WriteLine("That pokemon does not exist!");
                    Console.ReadKey();
                    continue;
                }
            } while (true);
            pokemon = publicInformations.pokemonchoices[Array.IndexOf(publicInformations.pokemonchoices, chosen)];
            type = publicInformations.pokemonTypes[Array.IndexOf(publicInformations.pokemonchoices, chosen)];
            Console.Clear();
        }

        public static bool startMenu()
        {
            int selectedIndex = 1;

            string prefix1 = "> ";
            string prefix2 = "";
            string prefix3 = "";

            string prefix10 = " <";
            string prefix20 = "";
            string prefix30 = "";

            do
            {
                Console.Clear();
                MainDes.MainDesigns.pokemontitle();

                Console.WriteLine("                                                                                           |");
                Console.WriteLine("                               Welcome to the PokeBattle!                                  |");
                Console.WriteLine("                                                                                           |");



                Console.WriteLine(string.Format("                                     {0,2}[NEW GAME]{1,2}                                        |", prefix1, prefix10));
                Console.WriteLine(string.Format("                                     {0,2}[CONTINUE]{1,2}                                        |", prefix2, prefix20));

                if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == true)
                {
                    Console.WriteLine(string.Format("                                 {0,2}[DEVELOPER ACCESS]{1,2}                                    |", prefix3, prefix30));
                }
                else
                {
                    Console.WriteLine("                                                                                           |");
                }



                Console.WriteLine("                                                                                           |");
                Console.WriteLine("-------------------------------------------------------------------------------------------+");


                var pressed = Console.ReadKey().Key;

                if (pressed == ConsoleKey.UpArrow || pressed == ConsoleKey.W)
                {
                    if ((selectedIndex - 1) < 1)
                    {
                        selectedIndex = 1;
                    }
                    else
                    {
                        selectedIndex -= 1;
                    }
                }
                else if (pressed == ConsoleKey.DownArrow || pressed == ConsoleKey.S)
                {
                    if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == true)
                    {
                        if ((selectedIndex + 1) > 2)
                        {
                            selectedIndex = 3;
                        }
                        else
                        {
                            selectedIndex += 1;
                        }
                    }
                    else
                    {
                        if ((selectedIndex + 1) > 2)
                        {
                            selectedIndex = 2;
                        }
                        else
                        {
                            selectedIndex += 1;
                        }
                    }
                }


                if (selectedIndex == 1)
                {
                    prefix1 = "> ";
                    prefix10 = " <";

                    prefix2 = "";
                    prefix20 = "";

                    prefix3 = "";
                    prefix30 = "";
                }
                else if (selectedIndex == 2)
                {
                    prefix2 = "> ";
                    prefix20 = " <";

                    prefix1 = "";
                    prefix10 = "";

                    prefix3 = "";
                    prefix30 = "";
                }
                else
                {
                    prefix2 = "";
                    prefix20 = "";

                    prefix1 = "";
                    prefix10 = "";

                    prefix3 = "> ";
                    prefix30 = " <";
                }

                MusicPlayerC.ButtonClick();
                //for (int i=0; i <=3; i++)
                //{
                //    Thread.Sleep(200);
                //}

                if (pressed != ConsoleKey.Enter)
                {
                    continue;
                }
                else
                    break;

            } while (true);

            if (selectedIndex == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int secondMenu()
        {
            int selectedIndex = 1;

            string prefix1 = "> ";
            string prefix2 = "";

            string prefix10 = " <";
            string prefix20 = "";

            do
            {
                Console.Clear();
                MainDes.MainDesigns.pokemontitle();

                Console.WriteLine("                                                                                           |");
                Console.WriteLine("                               Welcome to the PokeBattle!                                  |");
                Console.WriteLine("                                                                                           |");



                Console.WriteLine(string.Format("                                       {0,2}[PLAY]{1,2}                                          |", prefix1, prefix10));

                if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == false)
                {
                    Console.WriteLine(string.Format("                                     {0,2}[EDIT USER]{1,2}                                       |", prefix2, prefix20));
                }
                else
                {
                    Console.WriteLine("                                                                                           |");
                }

                Console.WriteLine("                                                                                           |");
                Console.WriteLine("-------------------------------------------------------------------------------------------+");

                var pressed = Console.ReadKey().Key;

                if (pressed == ConsoleKey.UpArrow || pressed == ConsoleKey.W)
                {
                    if ((selectedIndex - 1) < 1)
                    {
                        selectedIndex = 1;
                    }
                    else
                    {
                        selectedIndex -= 1;
                    }
                }
                else if (pressed == ConsoleKey.DownArrow || pressed == ConsoleKey.S)
                {
                    if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == true)
                    {
                        if ((selectedIndex + 1) > 1)
                        {
                            selectedIndex = 1;
                        }
                        else
                        {
                            selectedIndex += 1;
                        }
                    }
                    else
                    {
                        if ((selectedIndex + 1) > 2)
                        {
                            selectedIndex = 2;
                        }
                        else
                        {
                            selectedIndex += 1;
                        }
                    }
                }


                if (selectedIndex == 1)
                {
                    prefix1 = "> ";
                    prefix10 = " <";

                    prefix2 = "";
                    prefix20 = "";
                }
                else
                {
                    prefix2 = "> ";
                    prefix20 = " <";

                    prefix1 = "";
                    prefix10 = "";
                }

                MusicPlayerC.ButtonClick();
                //for (int i=0; i <=3; i++)
                //{
                //    Thread.Sleep(200);
                //}
                if (pressed != ConsoleKey.Enter)
                {
                    continue;
                }
                else
                    break;

            } while (true);

            return selectedIndex;
        }

        public static bool editProfileAccount()
        {
            int selectedIndex = 1;

            string prefix1 = "> ";
            string prefix2 = "";
            string prefix3 = "";

            string prefix10 = " <";
            string prefix20 = "";
            string prefix30 = "";

            do
            {
                Console.Clear();
                MainDes.MainDesigns.pokemontitle();

                Console.WriteLine("                                                                                           |");
                Console.WriteLine("                               Welcome to the PokeBattle!                                  |");
                Console.WriteLine("                                                                                           |");



                Console.WriteLine(string.Format("                                     {0,2}[CHANGE USERNAME]{1,2}                                 |", prefix1, prefix10));
                Console.WriteLine(string.Format("                                     {0,2}[CHANGE PASSWORD]{1,2}                                 |", prefix2, prefix20));
                Console.WriteLine(string.Format("                                     {0,2}[EXIT TO MENU!]{1,2}                                   |", prefix3, prefix30));




                Console.WriteLine("                                                                                           |");
                Console.WriteLine("-------------------------------------------------------------------------------------------+");

                var pressed = Console.ReadKey().Key;

                if (pressed == ConsoleKey.UpArrow || pressed == ConsoleKey.W)
                {
                    if ((selectedIndex - 1) < 1)
                    {
                        selectedIndex = 1;
                    }
                    else
                    {
                        selectedIndex -= 1;
                    }
                }
                else if (pressed == ConsoleKey.DownArrow || pressed == ConsoleKey.S)
                {
                    if ((selectedIndex + 1) > 3)
                    {
                        selectedIndex = 3;
                    }
                    else
                    {
                        selectedIndex += 1;
                    }
                }


                if (selectedIndex == 1)
                {
                    prefix1 = "> ";
                    prefix10 = " <";

                    prefix2 = "";
                    prefix20 = "";

                    prefix3 = "";
                    prefix30 = "";
                }
                else if (selectedIndex == 2)
                {
                    prefix1 = "";
                    prefix10 = "";

                    prefix2 = "> ";
                    prefix20 = " <";

                    prefix3 = "";
                    prefix30 = "";
                }
                else
                {
                    prefix2 = "";
                    prefix20 = "";

                    prefix1 = "";
                    prefix10 = "";

                    prefix3 = "> ";
                    prefix30 = " <";
                }

                MusicPlayerC.ButtonClick();
                if (pressed != ConsoleKey.Enter)
                {
                    continue;
                }
                else
                    break;

            } while (true);

            if (selectedIndex == 1) //IF CHOSEN CHANGE USER
            {
                Console.WriteLine("\nChange your username to?: [MAXIMUM 8 CHARACTERS]");
                string input = Console.ReadLine();
                if (input.Length >= 1 && input.Length <= 8)
                {
                    if (MongoFunctions.userExist(input.ToLower()) == false)
                    {
                        //MongoFunctions.changeUser(input);
                        userInformation.username = input;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("\nUsername already exists!");
                        Console.ReadKey();
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("\nUsername is too short or too long!");
                    Console.ReadKey();
                    return false;
                }
            }
            else if (selectedIndex == 2) //IF CHOSEN CHANGE PASSWORD!
            {
                Console.Clear();
                MainDes.MainDesigns.pokemontitle();

                Console.WriteLine("                                                                                           |");
                Console.WriteLine("                               Welcome to the PokeBattle!                                  |");
                Console.WriteLine("                                                                                           |");



                Console.WriteLine("                              Enter your desired password:                                 |");
                Console.WriteLine("                                                                                           |");



                Console.WriteLine("                                                                                           |");
                Console.WriteLine("                                                                                           |");
                Console.WriteLine("                                                                                           |");
                Console.WriteLine("-------------------------------------------------------------------------------------------+");

                Console.SetCursorPosition(31, 19);
                ConsoleKey key;
                string passinput = string.Empty;
                do
                {
                    var infokey = Console.ReadKey(intercept: true);
                    key = infokey.Key;

                    if (!char.IsControl(infokey.KeyChar))
                    {
                        if (Console.CursorLeft <= 59)
                        {
                            //Console.Write("\b \b");
                            Console.CursorLeft -= 1;
                            Console.Write(infokey.KeyChar);
                            passinput += infokey.KeyChar;
                            Console.CursorLeft++;
                        }
                        else
                        {

                        }
                    }
                    else if (infokey.Key == ConsoleKey.Backspace)
                    {
                        if (passinput.Length >= 1)
                        {
                            if (Console.CursorLeft >= 30)
                            {
                                //Console.Write("\b\b");
                                passinput = passinput.Remove(passinput.Length - 1, 1);

                                Console.CursorLeft -= 1;
                                Console.Write("\b ");
                            }
                        }
                    }
                } while (key != ConsoleKey.Enter);
                MongoFunctions.db.ChangePassword("UserInformations", passinput);
                return false;
            }
            else
            {
                return false; //FALSE = BACK TO SECOND MENU || TRUE = NEXT MENU
            }
        }

        public static void getExtra()
        {
            Random rndm = new Random();
            for (int i = 0; i < 4; i++)
            {
                do
                {
                    int arrnum = rndm.Next(0, 8);
                    if (publicInformations.pokemonchoices[arrnum] != pokeInformation.basicInfo.pokemon && !(Array.IndexOf(pokeInformation.advInfo.pokemonLIST, publicInformations.pokemonchoices[arrnum]) >= 0))
                    {
                        pokeInformation.advInfo.pokemonLIST[i] = publicInformations.pokemonchoices[arrnum];
                        pokeInformation.advInfo.pokemonLISTLOW[i] = publicInformations.pokemonchoices[arrnum].ToLower();
                        pokeInformation.advInfo.pokemonLIST_HEALTH[i] = 100;
                        break;
                    }
                    else
                        continue;
                } while (true);
            }
            pokeInformation.basicInfo.CurrpokemonType = publicInformations.pokemonTypes[Array.IndexOf(publicInformations.pokemonchoices, pokeInformation.basicInfo.pokemon)];
        }

        public static void PokemonListBOX()
        {
            Console.WriteLine("+----------------------------------------------------------------------------+");
            Console.WriteLine("|                                  POKEMON LIST                              |");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("|        1. Charizard        4. Gyarados        7. Dragonite    10. N/A      |");
            Console.WriteLine("|        2. Pikachu          5. Gardevoir       8. Snorlax      11. N/A      |");
            Console.WriteLine("|        3. Onix             6. Lucario         9. Mewtwo       12. N/A      |");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("+----------------------------------------------------------------------------+");
        }

        public static void copyrightText()
        {
            string str = string.Empty;
            string emptyLine = string.Format(str.PadRight(91) + "|");
            string line = new string('-', 91) + "+";

            Console.WriteLine(line);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);

            string pkmon = @"                                                                                           |
               _.----.        ____         ,'  _\   ___    ___     ____                    |
           _,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.              |
           \      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |             |
            \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |             | 
              \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |             |
               \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |             |
                \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |             |
                 \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |             |
                  \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |             |
                   \_.-'       |__|    `-._ |              '-.|     '-.| |   |             |
                                           `'                            '-._|             |";

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 2);
            foreach (char b in pkmon)
            {
                Console.Write(b);
                Thread.Sleep(TimeSpan.FromSeconds(0.0001));
            }
            //Console.Write(str.PadRight(91 - a.Count() - a.Count()) + "|" + "\n");


            Console.WriteLine("\n" + emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine); //14 STARTS
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);
            Console.WriteLine(emptyLine);

            Console.WriteLine(line);

            string[] CopyrightClaims = { @"© 2021 RYANN Inc.", @"© 2003-2020 Pokémon", @"© 1995-2003 Nintendo Inc.", @"© 1995-2003 Creatures Inc.", @"© 1995-2003 GAME FREAK inc.", };

            int currLine = 15;
            foreach (string a in CopyrightClaims)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(35,currLine);
                foreach (char b in a)
                {
                    Console.Write(b);
                    Thread.Sleep(TimeSpan.FromSeconds(0.04));
                }
                //Console.Write(str.PadRight(91 - a.Count() - a.Count()) + "|" + "\n");
                currLine++;
            }

            Console.CursorVisible = true;
            Console.SetCursorPosition(0, currLine + 3);
            Console.WriteLine("\nPress ENTER to start!");
            ConsoleKeyInfo input;

            do
            {
                input = Console.ReadKey();
            } while (input.Key != ConsoleKey.Enter);

            Thread.Sleep(1500);
            Console.ReadKey();
        }

    }
}
