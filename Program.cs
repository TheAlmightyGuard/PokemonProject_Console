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
using System.Security.Cryptography;

using PokemonGame.MainComponents.AttackList;

using NAudio.Wave;
using System.Threading.Tasks;

namespace PokemonGame.Main
{
    public class ProgramMain
    {
        public class variables
        {
            public static bool YWinner = false;
            public static string Current_Version = "V. 1";
        }

        public static string[] dllNames =
        {
            "DnsClient.dll",
            "MongoDB.Bson.dll",
            "MongoDB.Driver.Core.dll",
            "MongoDB.Driver.dll",
            "MongoDB.Libmongocrypt.dll",
            "NAudio.dll",
            "SharpCompress.dll",
            "Sytem.Buffers",
            "System.Runtime.CompilerServices.Unsafe.dll",
            "System.Text.Encoding.CodePages.dll"
        };

        

        public static void Main(string[] args)
        {
            
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

        public static void MainRun()
        {
            MaximizeWindow.Maximize();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            MusicPlayerC.OpeningMusic();

            Thread.Sleep(2000);
            //MainDesigns.pokemontitle();
            miscellaneousFunc.copyrightText();
            MusicPlayerC.ButtonClick();

            Console.CursorVisible = false;
            do
            {
                int chosenSec = 0;
                do
                {
                    bool newgame = miscellaneousFunc.startMenu();
                    Console.CursorVisible = true;

                    string user = string.Empty;
                    if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == false)
                    {
                        user = miscellaneousFunc.askUser();
                    }

                    if (newgame == true && user.ToLower() != "return")
                    {
                        MusicPlayerC.ButtonClick();

                        string password = Password_Class.newPassword();

                        userInformation.password = password;

                        miscellaneousFunc.newpokemonChoiceMenu(out string pokemon, out string type);

                        Information pokeStats = PokemonInformationAPI.grabInformationAsync(pokemon.ToLower()).Result;

                        MongoFunctions.addInformation(pokeStats, user, password, 1, 100, pokemon, type, out List<MongoFunctions.PartySAMPLE> party, out Guid ID);

                        Task.Delay(5000);
                        loadAllInformation(user, ID);
                        //pokeInformation.advInfo.CurrUsesAR = pokeInformation.advInfo.MaxUsesAR;
                        break;
                    }
                    else if (user.ToLower() == "return")
                    {
                        MusicPlayerC.ButtonClick();
                        continue;
                    }
                    else if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == false)
                    {
                        MusicPlayerC.ButtonClick();

                        Console.CursorVisible = false;
                        Console.Write("\nLoading information for ");

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("{0}", user);

                        Console.ResetColor();
                        Console.WriteLine("! Please wait....");


                        for (int i = 1; i < 3; i++)
                        {


                            int leftLength = 23 + user.Length + 14;
                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("    ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write(".   ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("..  ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("... ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("....");
                            Thread.Sleep(1200);
                        }

                        Console.WriteLine("\nPress enter to continue.");
                        Console.ReadKey(true);

                        if (MongoFunctions.userExist(user.ToLower()) == true)
                        {
                            MongoFunctions.readInformation(user.ToLower(), out Guid userID, out string pswrd, out string userOut, out int levelOut, out int coinsOut, out List<MongoFunctions.PartySAMPLE> party);
                            bool correct = Password_Class.mainPassword(user);
                            if (correct == true)
                            {
                                loadAllInformation(user, userID);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nPassword incorrect!");
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
            diffChoice();
            MusicPlayerC.ButtonClick();
            Console.Clear();


            Thread.Sleep(1000);
            Console.WriteLine("\n{0}, I CHOOSE YOU!\n", pokeInformation.basicInfo.pokemon);
            MusicPlayerC.CrySound();

            Thread.Sleep(1200);


            MainChar.MainMethod();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            pokeInformation.advInfo.storeAllMoves(out List<MongoFunctions.PartySAMPLE> party);
            MongoFunctions.updateAllInformation(
                new MongoFunctions.MainStyle()
                {
                    Username = userInformation.username,
                    Username_LOWER = userInformation.username.ToLower(),
                    user_password = userInformation.password,
                    User_LVL = userInformation.user_LVL,

                    PokeCoins = userInformation.user_COINS,

                    AdditionalPkmn = party

                });
        }

        public static void DEVELOPER_INFO()
        {
            //pokeInformation.advInfo.MOVENAMES = "Confusion", "Ancient Power", "Psycho Cut", "Psystrike";
            userInformation.username = "DEVELOPER";
            userInformation.user_LVL = 100;
            userInformation.user_COINS = 10000;
            userInformation.user_ID = "DEVELOPER_ID";

            pokeInformation.basicInfo.pokemon = "Mewtwo";
            pokeInformation.basicInfo.accuracy = 100;
            pokeInformation.basicInfo.levelPokemon = 999;
            pokeInformation.basicInfo.Health = 999;

            pokeInformation.basicInfo.ATTACK = 110;
            pokeInformation.basicInfo.DEFENSE = 90;
            pokeInformation.basicInfo.Sp_Atk = 154;
            pokeInformation.basicInfo.Sp_Def = 90; 
            pokeInformation.basicInfo.Speed = 130;

            pokeInformation.basicInfo.pokemon_EXP = 999;

            List<MongoFunctions.ItemSAMPLE> Backpack = new List<MongoFunctions.ItemSAMPLE>()
            {
                    new MongoFunctions.ItemSAMPLE() { ITEM_NAME = "Potion", HEALTH_ADD = 20, AMOUNT_LEFT = 10 },
                    new MongoFunctions.ItemSAMPLE(),
                    new MongoFunctions.ItemSAMPLE(),
                    new MongoFunctions.ItemSAMPLE()
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

        public static void loadAllInformation(string user, Guid ID)
        {
            Task.Delay(5000);
            var information = MongoFunctions.db.LoadInformationUSER("UserInformations", user);
            var selectedInfo = information.Find(x => x.Username_LOWER == user.ToLower());

            userInformation.username = selectedInfo.Username;
            userInformation.user_LVL = selectedInfo.User_LVL;
            userInformation.user_COINS = selectedInfo.PokeCoins;
            userInformation.user_ID = selectedInfo.ID.ToString();


            MongoFunctions.PartySAMPLE firstPokemon = selectedInfo.AdditionalPkmn.GetRange(0, 4).ToArray()[0];

            pokeInformation.basicInfo.pokemon = firstPokemon.Pkmn_NAME;
            pokeInformation.basicInfo.accuracy = firstPokemon.ACCURACY_P;
            pokeInformation.basicInfo.levelPokemon = firstPokemon.LVL;
            pokeInformation.basicInfo.Health = firstPokemon.HEALTH;

            pokeInformation.basicInfo.Health = firstPokemon.HEALTH;
            pokeInformation.basicInfo.ATTACK = firstPokemon.ATTACK;
            pokeInformation.basicInfo.DEFENSE = firstPokemon.DEFENSE;
            pokeInformation.basicInfo.Sp_Atk = firstPokemon.Sp_Atk;
            pokeInformation.basicInfo.Sp_Def = firstPokemon.Sp_Def;
            pokeInformation.basicInfo.Speed = firstPokemon.Speed;

            pokeInformation.basicInfo.pokemon_EXP = firstPokemon.EXP;

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
                    userInformation.ITEM_NAME[a] = "~~~~~~~~~~~~~";
                    userInformation.ITEM_HEALTH[a] = 0;
                    userInformation.ITEM_AMOUNT[a] = 0;
                    a++;
                }
            }

            List<string> pkmnNames = new List<string>();
            List<string> pkmnNamesLOWER = new List<string>();
            foreach (MongoFunctions.PartySAMPLE i in selectedInfo.AdditionalPkmn)
            {
                if (i.Pkmn_NAME != pokeInformation.basicInfo.pokemon)
                {
                    pkmnNames.Add(i.Pkmn_NAME);
                    pkmnNamesLOWER.Add(i.Pkmn_NAME.ToLower());
                }
            }

            pokeInformation.advInfo.pokemonLIST = pkmnNames.ToArray();
            pokeInformation.advInfo.pokemonLISTLOW = pkmnNamesLOWER.ToArray();

            object[] infoarray = selectedInfo.AdditionalPkmn.GetRange(0, 4).ToArray();

            //pokeInformation.advInfo.loadFirstPokeMove(selectedInfo.AdditionalPkmn.GetRange(0,0).ToArray());
            pokeInformation.advInfo.loadAllMoves(infoarray);
        }
    }

}

