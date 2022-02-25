using PokemonGame.MainComponents.Main;
using PokemonGame.MainDes;
using System;
using System.Media;
using System.Threading;
using PokemonGame.FunctionClasses;
using PokemonGame.informationClass;
using PokemonGame;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PokemonGame.MainComponents;

using PokemonGame.MainComponents.AttackList;

using NAudio.Wave;
using System.Threading.Tasks;
using System.Net;
using PokemonGame.PokemonInformationAPIC;

namespace PokemonGame.Main
{
    public class ProgramMain
    {
        public class variables
        {
            public static bool YWinner = false;
        }

        public static string[] dllNames =
        {
            "DnsClient.dll",
            "MongoDB.Bson.dll",
            "MongoDB.Driver.Core.dll",
            "MongoDB.Driver.dll",
            "MongoDB.Libmongocrypt.dll",
            "NAudio.dll",
            "Newtonsoft.Json.dll",
            "System.Net.Http.dll",
            "System.Runtime.InteropServices.RuntimeInformation.dll",
            "SharpCompress.dll",
            "System.Buffers.dll",
            "System.Runtime.CompilerServices.Unsafe.dll",
            "System.Text.Encoding.CodePages.dll"
        };

        public static List<string> dllListFound = new List<string>();

        public static List<string> MissingDLL = new List<string>();

        public static string filePathFound = string.Empty;

        [Obsolete]
        public static void Main(string[] args)
        {
            #region [CHECK UPDATES / FILES]
            string pathLOCALEXTRA = AppDomain.CurrentDomain.BaseDirectory;

            Console.WriteLine("Running on version (v" +  Convert.ToString(MainComponents.GameUpdater.currentVersionLOCAL) + ")...");

            Thread.Sleep(1200);

            if (File.Exists(Path.Combine(pathLOCALEXTRA, "PokemonGame1.exe")))
            {
                File.Delete(Path.Combine(pathLOCALEXTRA, "PokemonGame1.exe"));
                File.Delete(Path.Combine(pathLOCALEXTRA, "PokemonGame1.exe.config"));
            }

            if (MainComponents.GameUpdater.checkUpdates() == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Version up-to-date!");
            }

            Console.ResetColor();
            string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DLLs");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DirectoryInfo d = new DirectoryInfo(filepath);

            AppDomain.CurrentDomain.AppendPrivatePath(filepath);

            Console.WriteLine("Checking DLL files..");

            foreach (var dll in d.GetFiles("*.dll"))
            {
                Assembly.Load(File.ReadAllBytes(dll.FullName));
                dllListFound.Add(dll.Name);
            }

            foreach (string dllName in dllNames)
            {
                if (!dllListFound.Contains(dllName))
                {
                    MissingDLL.Add(dllName);
                }
            }

            Console.CursorVisible = false;


            if (MissingDLL.ToArray().Length >= 1)
            {
                int missingDownloaded = 0;
                Console.WriteLine("Downloading DLL files... (" + missingDownloaded + " / " + MissingDLL.Count + ")");
                string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                WebClient client = new WebClient();
                foreach (string i in MissingDLL)
                {
                    Task.Delay(4000);
                    Uri URL = new Uri("https://github.com/TheAlmightyGuard/PokemonProject_Console/raw/master/DLLs/" + i);
                    client.DownloadFile(URL, i);

                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), i);
                    string destPath = Path.Combine(filepath, i);
                    File.Move(i, destPath);
                    Assembly.Load(File.ReadAllBytes(destPath));
                    missingDownloaded++;
                    Console.SetCursorPosition(26, 3);
                    Console.Write(missingDownloaded);
                    Console.SetCursorPosition(0, 3);
                }
            }

            Console.SetCursorPosition(0, 3);
            Console.WriteLine("All DLLs loaded!");
            Console.ReadKey();
            Console.Clear();

            #endregion

            BattleUI.PokeMartUI();
            Console.ReadKey();

            MainRun();
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PokemonGame.Resources.NAudio.dll"))
            {
                var assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }

        public static void OpeningLoading()
        {
            Console.Clear();
            Console.Write("Loading");
            for (double i = 0; i < 7.5; i += 1.5)
            {
                Console.Write(".");
                Thread.Sleep(1500);
            }
            Console.WriteLine("\n" + "DONE!");
            Thread.Sleep(1000);

        }
        public static void diffChoice()
        {
            do
            {

                pokeInformation.basicInfo basicInfo = new pokeInformation.basicInfo();
                Console.Clear();
                Console.WriteLine("Choose your dificulty:" + "\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[EASY] (Player & Enemy Health | 100)");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[MEDIUM] (Player & Enemy Health | 500)");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[HARD] (Player & Enemy Health | 900)");

                Console.ForegroundColor = ConsoleColor.White;
                string chosen = Console.ReadLine().ToUpper();

                if (chosen == "EASY" || chosen == "E")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Difficulty Mode: EASY");
                    //basicInfo.changeAllHealth(100);
                    Console.ReadKey();

                }
                else if (chosen == "MEDIUM" || chosen == "M")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Difficulty Mode: MEDIUM");
                    //basicInfo.changeAllHealth(500);
                    Console.ReadKey();
                }
                else if (chosen == "HARD" || chosen == "H")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Difficulty Mode: HARD");
                    //basicInfo.changeAllHealth(900);
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Pick a valid choice!");
                    Console.ReadKey();
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
                break;
            } while (true);
            return;

        }

        [Obsolete]
        public static void MainRun()
        {
            Console.Clear();
            MaximizeWindow.Maximize();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            MusicPlayerC.OpeningMusic();

            Thread.Sleep(2000);
            miscellaneousFunc.copyrightText();
            MusicPlayerC.ButtonClick();

            Console.CursorVisible = false;
            do
            {
                int chosenSec = 0;
                do
                {
                    int menuChosen = miscellaneousFunc.startMenu(); // 1 = NEW [] 2 = CONTINUE [] 3 = DEVELOPER
                    Console.CursorVisible = true;
                    string user = string.Empty;

                    ConsoleKey keyOut = ConsoleKey.End;

                    if (menuChosen <= 2)
                    {
                        user = miscellaneousFunc.askUser(out keyOut);
                    }

                    if (user.ToLower() == "return" || keyOut == ConsoleKey.Escape)
                    {
                        MusicPlayerC.ButtonClick();
                        continue;
                    }
                    else if (menuChosen == 1)
                    {
                        MusicPlayerC.ButtonClick();

                        string password = Password_Class.newPassword();

                        userInformation.password = password;

                        miscellaneousFunc.newpokemonChoiceMenu(out string pokemon, out string type);

                        Information pokeStats = PokemonInformationAPI.grabInformationAsync(pokemon.ToLower()).Result;

                        MongoFunctions.addInformation(pokeStats, user, password, 1, 100, pokemon, type, out List<PokemonInfStruct> party);

                        Task.Delay(5000);
                        loadAllInformation(user);
                        //pokeInformation.advInfo.CurrUsesAR = pokeInformation.advInfo.MaxUsesAR;
                        break;
                    }

                    else if (menuChosen == 2)
                    {
                        MusicPlayerC.ButtonClick();

                        if (MongoFunctions.userExist(user.ToLower()) == true)
                        {
                            MongoFunctions.readInformation(user.ToLower(), out string pswrd, out string userOut, out int levelOut, out int coinsOut, out int pkmnCount, out List<PokemonInfStruct> party);
                            bool correct = Password_Class.mainPassword(user);
                            if (correct == true)
                            {
                                loadAllInformation(user);
                            }
                            else
                            {
                                Console.WriteLine("\n\nPassword incorrect!");
                                Console.ReadKey();
                                MusicPlayerC.ButtonClick();
                                continue;

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Username does not exist! Create a new username instead..");
                            Console.ReadKey();
                            MusicPlayerC.ButtonClick();
                            continue;
                        }

                        Console.CursorVisible = false;
                        Console.Write("\n\nLoading information for ");

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("{0}", userInformation.username);

                        Console.ResetColor();
                        Console.WriteLine("! Please wait....");


                        for (int i = 1; i < 3; i++)
                        {


                            int leftLength = 23 + user.Length + 14;
                            Console.SetCursorPosition(leftLength, 5);
                            Console.Write("    ");
                            Thread.Sleep(900);

                            Console.SetCursorPosition(leftLength, 5);
                            Console.Write(".   ");
                            Thread.Sleep(1000);

                            Console.SetCursorPosition(leftLength, 5);
                            Console.Write("..  ");
                            Thread.Sleep(900);

                            Console.SetCursorPosition(leftLength, 5);
                            Console.Write("... ");
                            Thread.Sleep(1000);

                            Console.SetCursorPosition(leftLength, 5);
                            Console.Write("....");
                            Thread.Sleep(900);
                        }

                        Console.WriteLine("\n\nPress enter to continue.");
                        Console.ReadKey(true);
                        break;
                    }
                    else
                    {
                        DEVELOPER_INFO();
                        break;
                    }
                } while (true);

                do
                {
                    chosenSec = miscellaneousFunc.secondMenu();
                    if (chosenSec == 2)
                    {
                        bool changed = false;
                        do
                        {
                            changed = miscellaneousFunc.editProfileAccount();
                            MusicPlayerC.ButtonClick();
                            break;

                        } while (true);

                        if (changed == true)
                        {
                            break;
                        }
                        else
                            continue;
                    }
                    break;
                } while (true);
                break;
            } while (true);

            //miscellaneousFunc.pokemonChoiceMenu();
            MusicPlayerC.ButtonClick();
            Console.Clear();
            MusicPlayerC.ButtonClick();
            Console.Clear();


            Thread.Sleep(1000);
            Console.WriteLine("\n{0}, I CHOOSE YOU!\n", pokeInformation.basicInfo.currentPokemon.pokemonName);
            //MusicPlayerC.CrySound();

            Thread.Sleep(1200);


            MainChar.MainMethod();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            MongoFunctions.saveAllInformation();
        }

        public static void DEVELOPER_INFO()
        {
            //pokeInformation.advInfo.MOVENAMES = "Confusion", "Ancient Power", "Psycho Cut", "Psystrike";
            userInformation.username = "DEVELOPER";
            userInformation.user_LVL = 100;
            userInformation.user_COINS = 10000;
            userInformation.user_ID = Guid.NewGuid();

            userInformation.pokemonList[0].pokemonName = "Mewtwo";
            userInformation.pokemonList[0].pokemonName_LOWER = "Mewtwo";

            pokeInformation.basicInfo.currentPokemon.pokemonName = "Mewtwo";
            pokeInformation.basicInfo.currentPokemon.pokemon_TYPE = "PSYCHIC";
            pokeInformation.basicInfo.currentPokemon.ACCURACY = 100;
            pokeInformation.basicInfo.currentPokemon.pokemon_LVL = 1;
            pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = 20;
            pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH_MAX = 20;

            pokeInformation.basicInfo.currentPokemon.ATTACK = 110;
            pokeInformation.basicInfo.currentPokemon.DEFENSE = 90;
            pokeInformation.basicInfo.currentPokemon.Sp_Atk = 154;
            pokeInformation.basicInfo.currentPokemon.Sp_Def = 90; 
            pokeInformation.basicInfo.currentPokemon.Speed = 130;

            pokeInformation.basicInfo.currentPokemon.pokemon_EXP = 999;
            pokeInformation.basicInfo.pokemonLeft = 1;


            //userInformation.pokemonList[1] = userInformation.pokemonList[0];
            //userInformation.pokemonList[1].pokemonName = "Mewdw";
            //userInformation.pokemonList[1].pokemonName_LOWER = "mewtwox";
            //userInformation.pokemonList[1].pokemon_HEALTH = 100;

            userInformation.pokemonList[0] = pokeInformation.basicInfo.currentPokemon;

            List<MongoFunctions.ItemSAMPLE> Backpack = new List<MongoFunctions.ItemSAMPLE>()
            {
                    new MongoFunctions.ItemSAMPLE() { ITEM_NAME = "Potion", HEALTH_ADD = 20, AMOUNT_LEFT = 10 },
                    new MongoFunctions.ItemSAMPLE() { ITEM_NAME = "Max Potion", HEALTH_ADD = 20, AMOUNT_LEFT = 10 },
                    new MongoFunctions.ItemSAMPLE() { ITEM_NAME = "Poké ball", HEALTH_ADD = 0, AMOUNT_LEFT = 10 },
                    new MongoFunctions.ItemSAMPLE() { ITEM_NAME = "Great ball", HEALTH_ADD = 20, AMOUNT_LEFT = 10 },
            };

            int a = 0;

            foreach (MongoFunctions.ItemSAMPLE b in Backpack)
            {
                if (b.ITEM_NAME.Trim() != "")
                {
                    userInformation.ITEM_NAME[a] = b.ITEM_NAME;
                    userInformation.ITEM_HEALTH[a] = b.HEALTH_ADD;
                    userInformation.ITEM_AMOUNT[a] = b.AMOUNT_LEFT;
                    a++;
                }
                else
                {
                    userInformation.ITEM_NAME[a] = "~~~~~~~~~~~~~";
                    userInformation.ITEM_HEALTH[a] = 0;
                    userInformation.ITEM_AMOUNT[a] = 0;
                    a++;
                }
            }

            _attackLists.Get4Moves();
        }

        public static void loadAllInformation(string user)
        {
            Task.Delay(5000);
            var selectedInfo = MongoFunctions.db.LoadInformationUSER("UserInformations", user);

            userInformation.username = selectedInfo.Username;
            userInformation.user_LVL = selectedInfo.User_LVL;
            userInformation.user_COINS = selectedInfo.PokeCoins;
            userInformation.password = selectedInfo.user_password;
            //userInformation.user_ID = selectedInfo.ID;

            userInformation.pokemonTOTAL = selectedInfo.PokemonCount;
            pokeInformation.basicInfo.pokemonLeft = selectedInfo.PokemonCount;

            List<MongoFunctions.ItemSAMPLE> items = selectedInfo.Backpack; 
            int a = 0;

            foreach (MongoFunctions.ItemSAMPLE b in items)
            {
                if (b.ITEM_NAME.Trim() != "")
                {
                    userInformation.ITEM_NAME[a] = b.ITEM_NAME;
                    userInformation.ITEM_HEALTH[a] = b.HEALTH_ADD;
                    userInformation.ITEM_AMOUNT[a] = b.AMOUNT_LEFT;
                    a++;
                }
                else
                {
                    userInformation.ITEM_NAME[a] = "~~~~~~~~~~~~";
                    userInformation.ITEM_HEALTH[a] = 0;
                    userInformation.ITEM_AMOUNT[a] = 0;
                    a++;
                }
            }


            object[] infoarray = selectedInfo.AdditionalPkmn.GetRange(0, 4).ToArray();

            pokeInformation.basicInfo.currentPokemon = selectedInfo.AdditionalPkmn[0];

            userInformation.pokemonList = selectedInfo.AdditionalPkmn;

        }
    }

}

