using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PokemonGame.FunctionClasses.InventoryMain;
using PokemonGame.informationClass;
using PokemonGame;

namespace PokemonGame.MainComponents
{
    public class Cords
    {
        public int x { get; set; }
        public int x2 { get; set; }
    }

    public class CordsY
    {
        public int x { get; set; }
        public int x2 { get; set; }

        public int y { get; set; }
    }
    class BattleUI
    {

        public static Cords currentCords = new Cords() { x = 76, x2 = 91 };

        public static CordsY currentCordsY = new CordsY();

        public static int HeightY = 36;
        public static bool loopArrow = false;

        public static List<Cords> actionCords = new List<Cords>
        {
                new Cords() { x = 76 , x2 = 91 }, //ATTACK
                new Cords() { x = 93 , x2 = 111 }, //INVENTORY
                new Cords() { x = 113 , x2 = 129}, //POKEMON
                new Cords() { x = 131 , x2 = 143}, //RUN 
        };

        public static List<CordsY> test = new List<CordsY>();

        public static ThreadStart ts = new ThreadStart(BackgroundMethod);
        public static Thread backgroundThread = new Thread(ts);

        public static ThreadStart ts2 = new ThreadStart(ArrowBlink);
        public static Thread backgroundThread2 = new Thread(ts2);

        public static bool ThreadRunning = false;

        public static string[] finalNames = new string[4];
        public static string[] finalNames2 = new string[4];

        public static int menu = 0; //0 - ACTIONS || 1 - ATTACK

        #region [DO NOT TOUCH] IMPORTANT UI COMPONENT /BATTLE UI MAIN\
        [Obsolete]
        public static void BattleGUI()
        {
            //MainDes.MainDesigns.pokemontitle();

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("+-----------------------------------------------------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");

            pokemontitle();

            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");

            playerBox();

            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("+-----------------------------------------------------------------------------------------------------------------------------------------------------------+");

            //Console.ReadKey();

            int X = Console.CursorLeft;
            int Y = Console.CursorTop;

            mainBox(69);
            //Console.ReadKey();
            Console.SetCursorPosition(91, 2);
            EnemyBox(85);

            Console.SetCursorPosition(X, Y);

            Thread.Sleep(900);
        }

        #endregion

        #region [Inventory Methods]

        [Obsolete]
        public static int ChooseItem()
        {

            List<Cords> cords = new List<Cords>
            {
                new Cords() { x = 70 , x2 = 84 },
                new Cords() { x = 91 , x2 = 105 },
                new Cords() { x = 111 , x2 = 126 },
                new Cords() { x = 133 , x2 = 146},
            };

            int i = 0;

            var arrayCords = cords.ToArray();

            HeightY = 35;
            Cords begin = arrayCords[0];
            currentCords = begin;
            BlinkingAction(false);

            ConsoleKey key;

            string chosen = string.Empty;
            do
            {
                var infokey = Console.ReadKey(intercept: true);
                key = infokey.Key;


                if (!Console.KeyAvailable)
                {
                    if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                    {
                        Thread.Sleep(500);
                        MusicPlayerC.ButtonClick();
                        resetItemsLine();
                        int a = i;
                        a++;
                        if (a <= 3 && a >= 0)
                        {
                            i++;
                            Cords chosenXX = arrayCords[i];


                            currentCords = chosenXX;
                        }
                        else
                        {
                            i = 3;

                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                    }
                    else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                    {
                        Thread.Sleep(500);
                        MusicPlayerC.ButtonClick();
                        resetItemsLine();

                        int a = i;
                        a -= 1;
                        if (a <= 3 && a >= 0)
                        {
                            i -= 1;
                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                        else
                        {
                            i = 0;

                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                    }
                }

                Task.Delay(500);
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);

            if (key == ConsoleKey.Escape)
            {
                i = -1;
            }

            return i;
        }

        public static void resetItemsLine()
        {
            Console.SetCursorPosition(66, 35);

            string[] moveNames = new string[4];

            int a = 0;
            foreach (string i in userInformation.ITEM_NAME)
            {
                string temp = middleAlign(12, i);
                moveNames[a] = temp;
                a++;
            }


            Console.Write(string.Format("|    {0,13}        {1,13}       {2,13}        {3,13}      |",
                                           moveNames[0],

                                           moveNames[1],

                                           moveNames[2],

                                           moveNames[3]));

        }

        public static void InventoryBox()
        {
            loopArrow = false;
            Console.ResetColor();

            string[] moveNames = new string[4];

            int a = 0;
            foreach (string i in userInformation.ITEM_NAME)
            {
                string temp = middleAlign(13, i);
                moveNames[a] = temp;
                a++;
            }

            Console.SetCursorPosition(66, 31);

            Console.Write("+-------------------------------------------------------------------------------------+");


            Console.SetCursorPosition(66, 32);


            Console.Write("|                                    Choose your item!                                |");

            Console.SetCursorPosition(66, 33);

            Console.Write("|                                                                                     |");

            Console.SetCursorPosition(66, 34);

            Console.Write("|                                                                                     |");

            Console.SetCursorPosition(66, 35);


            Console.Write(string.Format("|    {0,13}        {1,13}       {2,13}        {3,13}      |",
                                           moveNames[0],

                                           moveNames[1],

                                           moveNames[2],

                                           moveNames[3]));


            Console.SetCursorPosition(66, 36);

            Console.Write(string.Format("|    {0,13}        {1,13}       {2,13}         {3,13}     |",
                                          middleAlign(13, $"[{userInformation.ITEM_AMOUNT[0]} LEFT]"),

                                          middleAlign(13, $"[{userInformation.ITEM_AMOUNT[1]} LEFT]"),

                                          middleAlign(13, $"[{userInformation.ITEM_AMOUNT[2]} LEFT]"),

                                          middleAlign(13, $"[{userInformation.ITEM_AMOUNT[3]} LEFT]")));

            Console.SetCursorPosition(66, 37);
            Console.Write("|                                                                                     |");

            Console.SetCursorPosition(66, 38);
            Console.Write("+-------------------------------------------------------------------------------------+");
        }
        #endregion

        #region [Blinking Threading]
        [Obsolete]
        public static void BlinkingAction(bool arrowBlink)
        {
            loopArrow = true;

            if (arrowBlink == false)
            {
                if (ThreadRunning == false)
                {
                    backgroundThread.Start();
                    ThreadRunning = true;
                }
                else if (ThreadRunning == true)
                {
                    backgroundThread = null;
                    Thread backgroundNew = new Thread(ts);
                    backgroundThread = backgroundNew;
                    backgroundThread.Start();
                }
            }
            else
            {
                if (ThreadRunning == false)
                {
                    backgroundThread2.Start();
                    ThreadRunning = true;
                }
                else if (ThreadRunning == true)
                {
                    backgroundThread2 = null;
                    Thread backgroundNew = new Thread(ts2);
                    backgroundThread2 = backgroundNew;
                    backgroundThread2.Start();
                }
            }
        }

        private static void BackgroundMethod() //MENU
        {
            while (loopArrow == true)
            {
                if (menu == 0)
                {
                    resetActionLine();
                }
                else if (menu == 1)
                {
                    resetAttackLine();
                }
                else if (menu == 2)
                {
                    resetItemsLine();
                }
                else if (menu == 3)
                {
                    resetPokemonLine();
                }
                Thread.Sleep(330);

                if (menu == 3)
                {
                    Console.SetCursorPosition(currentCordsY.x, currentCordsY.y);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(">");

                    Console.SetCursorPosition(currentCordsY.x2, currentCordsY.y);
                    Console.Write("<");
                    Console.ResetColor();
                }
                else
                {
                    Console.SetCursorPosition(currentCords.x, HeightY);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(">");

                    Console.SetCursorPosition(currentCords.x2, HeightY);
                    Console.Write("<");
                    Console.ResetColor();
                }

                Thread.Sleep(350);
            }
        }

        private static void ArrowBlink()
        {
            while (loopArrow == true)
            {
                Console.SetCursorPosition(102, 21);
                Console.Write("▼");
                Thread.Sleep(1000);
                Console.SetCursorPosition(102, 21);
                Console.Write(" ");
                Thread.Sleep(1500);
            }
        }


        #endregion

        #region [UI Components]

        public static void resetUIExtra()
        {
            loopArrow = false;
            backgroundThread = null;
            backgroundThread2 = null;
            Thread.Sleep(500);
            Console.ResetColor();
            EnemyBox(85);
            playerBox();
        }
        public static string middleAlign(int width, string word)
        {

            string space = " ";
            string wordEnd = string.Empty;
            double leftPadding = Math.Round(Convert.ToDouble((width - word.Length) / 2));
            double rightPadding = Math.Round(Convert.ToDouble(width - word.Length - leftPadding));

            if (leftPadding == 0 && rightPadding != 0)
            {
                wordEnd = word + space.PadRight(Convert.ToInt32(rightPadding));
            }
            else if (rightPadding == 0 && leftPadding != 0)
            {
                wordEnd = space.PadRight(Convert.ToInt32(leftPadding)) + word;
            }
            else if (rightPadding == 0 && leftPadding == 0)
            {
                wordEnd = word;
            }
            else
            {
                wordEnd = space.PadLeft(Convert.ToInt32(leftPadding)) + word + space.PadRight(Convert.ToInt32(rightPadding));
            }
            return wordEnd;
        }

        public static void EnemyBox(int x)
        {
            int X_ = x;

            Console.SetCursorPosition(X_, 2);

            Console.Write("+----------------------------------------------------------+");

            Console.SetCursorPosition(X_, 3);

            Console.Write(string.Format("|         NAME: {0,-11}             LVL: {1,-5}         |", pokeInformation.basicInfo.ENEMY_NAME, pokeInformation.basicInfo.EnemyPokemon.pokemon_LVL));

            Console.SetCursorPosition(X_, 4);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(X_, 5);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(X_, 6);

            Console.Write(string.Format("|     POKEMON: {0,-11}            HEALTH: {1,-3} HP       |", pokeInformation.basicInfo.EnemyPokemon.pokemonName, pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH));

            Console.SetCursorPosition(X_, 7);

            Console.Write("+----------------------------------------------------------+");

            Console.SetCursorPosition(X_ + 24, 8);

            Console.Write("--[ENEMY SIDE]--");
        }


        public static void RemovePlayerBox()
        {
            int X_ = 65;

            Console.SetCursorPosition(X_, 31);

            Console.Write("                                                                                       ");

            Console.SetCursorPosition(X_, 32);

            Console.Write("                                                                                       ");

            Console.SetCursorPosition(X_, 33);

            Console.Write("                                                                                       ");

            Console.SetCursorPosition(X_, 34);

            Console.Write("                                                                                       ");

            Console.SetCursorPosition(X_, 35);

            Console.Write("                                                                                       ");

            Console.SetCursorPosition(X_, 36);

            Console.Write("                                                                                       ");

            Console.SetCursorPosition(X_, 37);

            Console.Write("                                                                                       ");

            Console.SetCursorPosition(X_, 38);

            Console.Write("                                                                                       ");
        }

        public static void pokemontitle()
        {
            //string line = new string('-', 91) + "+";
            //Console.WriteLine(line);
            Console.SetCursorPosition(0, 13);
            Console.WriteLine(@"|                                                                         ,'\                                                                               |
|                                           _.----.        ____         ,'  _\   ___    ___     ____                                                        |
|                                       _,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.                                                  |
|                                       \      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |                                                 |
|                                        \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |                                                 |
|                                          \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |                                                 |
|                                           \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |                                                 |
|                                            \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |                                                 |
|                                             \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |                                                 |
|                                              \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |                                                 |
|                                               \_.-'       |__|    `-._ |              '-.|     '-.| |   |                                                 |
|                                                                       `'                            '-._|                                                 |");
        }

        public static void playerBox()
        {
            Console.SetCursorPosition(0, 30);
            Console.WriteLine("|                                                                                                                                                           |");
            Console.WriteLine("|                         --[YOUR SIDE]--                                                                                                                   |");
            Console.WriteLine("|  +----------------------------------------------------------+                                                                                             |");
            Console.WriteLine(string.Format("|  |         NAME: {0,-11}                 LVL: {1,-5}     |                                                                                             |", userInformation.username, pokeInformation.basicInfo.currentPokemon.pokemon_LVL));
            Console.WriteLine("|  |                                                          |                                                                                             |");
            Console.WriteLine("|  |                                                          |                                                                                             |");
            Console.WriteLine(string.Format("|  |        POKEMON: {0,-10}            HEALTH: {1,-3} HP     |                                                                                             |", pokeInformation.basicInfo.currentPokemon.pokemonName, pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH));
            Console.WriteLine("|  +----------------------------------------------------------+                                                                                             |");
            Console.WriteLine("|                                                                                                                                                           |");
        }

        public static void boxMiddleShow()
        {

            Console.SetCursorPosition(48, 16);
            Console.Write("+-----------------------------------------------------------+");

            Console.SetCursorPosition(48, 17);

            Console.Write("|                                                           |");

            Console.SetCursorPosition(48, 18);

            Console.Write("|                                                           |");

            Console.SetCursorPosition(48, 19);

            Console.Write("|                                                           |");

            Console.SetCursorPosition(48, 20);

            Console.Write("|                                                           |");

            Console.SetCursorPosition(48, 21);

            Console.Write("|                                                           |");

            Console.SetCursorPosition(48, 22);

            Console.Write("+-----------------------------------------------------------+");
        }
        #endregion

        #region [Action Methods]

        public static void mainBox(int X)
        {
            int X_ = X;

            BattleUI.RemovePlayerBox();

            Console.SetCursorPosition(X_, 31);

            Console.Write("+--------------------------------------------------------------------------------+");

            Console.SetCursorPosition(X_, 32);

            Console.Write("|                                                                                |");

            Console.SetCursorPosition(X_, 33);

            Console.Write("|                               Choose your action                               |");

            Console.SetCursorPosition(X_, 34);

            Console.Write("|                                                                                |");

            Console.SetCursorPosition(X_, 35);

            Console.Write("|                                                                                |");

            Console.SetCursorPosition(X_, 36);

            Console.Write("|      > [A] - Attack <   [I] - Inventory     [P] - Pokemon     [R] - Run        |");

            Console.SetCursorPosition(X_, 37);

            Console.Write("|                                                                                |");

            Console.SetCursorPosition(X_, 38);

            Console.Write("+--------------------------------------------------------------------------------+");
        }

        public static void resetActionLine()
        {
            Console.SetCursorPosition(69, 36);

            Console.Write("|        [A] - Attack     [I] - Inventory     [P] - Pokemon     [R] - Run        |");
        }

        [Obsolete]
        public static int ChooseAction()
        {
            List<Cords> cords = new List<Cords>
            {
                new Cords() { x = 76 , x2 = 91 }, //ATTACK
                new Cords() { x = 93 , x2 = 111 }, //INVENTORY
                new Cords() { x = 113 , x2 = 129}, //POKEMON
                new Cords() { x = 131 , x2 = 143}, //RUN
            };

            HeightY = 36;
            currentCords = cords[0];
            BlinkingAction(false);
            int i = 0;

            var arrayCords = cords.ToArray();

            ConsoleKey key;

            string chosen = string.Empty;
            do
            {
                var infokey = Console.ReadKey(intercept: true);
                key = infokey.Key;

                if (!Console.KeyAvailable)
                {
                    if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                    {
                        Thread.Sleep(5);

                        resetActionLine();
                        int a = i;
                        a++;
                        if (a <= 3 && a >= 0)
                        {
                            i++;
                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                        else
                        {
                            i = 3;

                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                    }
                    else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                    {
                        Thread.Sleep(5);
                        MusicPlayerC.ButtonClick();
                        resetActionLine();

                        int a = i;
                        a -= 1;
                        if (a <= 3 && a >= 0)
                        {
                            i -= 1;
                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                        else
                        {
                            i = 0;

                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                    }
                    else if (key == ConsoleKey.A)
                    {
                        i = 0;
                        menu = 1;

                        break;
                    }
                    else if (key == ConsoleKey.I)
                    {
                        i = 1;
                        menu = 2;

                        break;
                    }
                    else if (key == ConsoleKey.P)
                    {
                        i = 2;
                        break;
                    }
                    else if (key == ConsoleKey.R)
                    {
                        i = 3;
                        break;
                    }
                }
                MusicPlayerC.ButtonClick();

            } while (key != ConsoleKey.Enter);

            backgroundThread.Abort();
            return i;
        }
        #endregion

        #region [Attack Methods]

        public static void AttackBox()
        {
            loopArrow = false;
            Console.ResetColor();

            string[] moveNames = new string[4];

            int a = 0;
            foreach (var i in pokeInformation.basicInfo.currentPokemon.Pkmn_Moves)
            {
                string temp = middleAlign(16, i.MOVENAME);
                moveNames[a] = temp;
                a++;
            }

            finalNames = moveNames;

            AttackSTATS(0, moveNames[0], true);

            Console.SetCursorPosition(66, 31);

            Console.Write("+-------------------------------------------------------------------------------------+");


            Console.SetCursorPosition(66, 32);


            Console.Write("|                                    Choose your move!                                |");

            Console.SetCursorPosition(66, 33);

            Console.Write("|                                                                                     |");

            Console.SetCursorPosition(66, 34);

            Console.Write("|                                                                                     |");

            Console.SetCursorPosition(66, 35);


            Console.Write(string.Format("|   {0,16}     {1,16}     {2,16}    {3,16}    |",
                                           moveNames[0],

                                          moveNames[1],

                                          moveNames[2],

                                          moveNames[3]));

            Console.SetCursorPosition(66, 36);

            Console.Write("|                                                                                     |");

            Console.SetCursorPosition(66, 37);
            Console.Write("|                                                                                     |");

            Console.SetCursorPosition(66, 38);
            Console.Write("+-------------------------------------------------------------------------------------+");
        }

        public static void resetAttackLine()
        {
            Console.SetCursorPosition(66, 35);
            Console.Write(string.Format("|   {0,16}     {1,16}     {2,16}    {3,16}    |",
                                           finalNames[0],

                                          finalNames[1],

                                          finalNames[2],

                                          finalNames[3]));

        }


        [Obsolete]
        public static int ChooseAttack()
        {

            List<Cords> cords = new List<Cords>
            {
                new Cords() { x = 69 , x2 = 86 }, //ATTACK
                new Cords() { x = 90 , x2 = 107 }, //INVENTORY
                new Cords() { x = 111 , x2 = 128 }, //POKEMON
                new Cords() { x = 131 , x2 = 148 }, //RUN
            };

            int i = 0;

            var arrayCords = cords.ToArray();

            HeightY = 35;
            Cords begin = arrayCords[0];
            currentCords = begin;
            BlinkingAction(false);

            ConsoleKey key;

            string chosen = string.Empty;
            do
            {
                var infokey = Console.ReadKey(intercept: true);
                key = infokey.Key;


                if (!Console.KeyAvailable)
                {
                    if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                    {
                        Thread.Sleep(500);
                        MusicPlayerC.ButtonClick();
                        resetAttackLine();
                        int a = i;
                        a++;
                        if (a <= 3 && a >= 0)
                        {
                            i++;
                            Cords chosenXX = arrayCords[i];


                            currentCords = chosenXX;
                        }
                        else
                        {
                            i = 3;

                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                        AttackSTATS(i, finalNames[i], true);
                    }
                    else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                    {
                        Thread.Sleep(500);
                        MusicPlayerC.ButtonClick();
                        resetAttackLine();

                        int a = i;
                        a -= 1;
                        if (a <= 3 && a >= 0)
                        {
                            i -= 1;
                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }
                        else
                        {
                            i = 0;

                            Cords chosenXX = arrayCords[i];

                            currentCords = chosenXX;
                        }

                        AttackSTATS(i, finalNames[i], true);
                    }
                }
                else
                {

                }

                Task.Delay(500);
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);

            if (key == ConsoleKey.Escape)
            {
                i = -1;
            }

            return i;
        }

        #endregion

        #region [POKEMART]

        public static void pokemontitleMART()
        {
            Console.SetCursorPosition(0, 0);

            Console.Write("+-----------------------------------------------------------------------------------------------------------------------------------------------------------+");

            Console.SetCursorPosition(0, 1);

            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 2);

            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 3);

            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 4);
            Console.WriteLine(@"|                                                                           ,'\                                                                             |
|                                             _.----.        ____         ,'  _\   ___    ___     ____                                                      |
|                                         _,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.                                                |
|                                         \      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |                                               |
|                                          \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |                                               |
|                                            \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |                                               |
|                                             \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |                                               |
|                                              \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |                                               |
|                                               \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |                                               |
|                                                \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |                                               |
|                                                 \_.-'       |__|    `-._ |              '-.|     '-.| |   |                                               |
|                                                                         `'                            '-._|                                               |");
        }

        public static void PokeMartUI()
        {
            FunctionClasses.MaximizeWindow.Maximize();
            loopArrow = false;
            Console.ResetColor();

            //string[] moveNames = new string[4];

            //int a = 0;
            //foreach (string i in userInformation.ITEM_NAME)
            //{
            //    string temp = middleAlign(13, i);
            //    moveNames[a] = temp;
            //    a++;
            //}

            pokemontitleMART();

            Console.SetCursorPosition(0, 16);

            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 17);

            Console.Write("|                                                                 Welcome to the PokéMart!                                                                  |");

            Console.SetCursorPosition(0, 18);

            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 19);

            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 20);

            #region [POTION]

            Console.Write(string.Format("|               [  Potions  ]        {0,15}        {1,15}       {2,15}        {3,15}                                    |",
                                           "    Potion    ",

                                           " Super Potion ",

                                           " Hyper Potion ",

                                           "  MAX Potion  "));

            Console.SetCursorPosition(0, 21);

            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 22);

            Console.Write("|                                      ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~                                    |");

            #endregion

            #region [POKEBALLS]
            Console.SetCursorPosition(0, 23);

            Console.Write("|                                                                                                                                                           |");


            Console.SetCursorPosition(0, 24);
            Console.Write(string.Format("|               [ Poké Balls ]         {0,15}        {1,15}       {2,15}        {3,15}                                  |",
                                          "   Poke Ball   ",

                                          "  Great Ball   ",

                                          "  Ultra Ball   ",

                                          "  Master Ball  "));

            Console.SetCursorPosition(0, 25);
            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 26);
            Console.Write("|                                      ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~                                    |");

            #endregion

            #region [REPELS]
            Console.SetCursorPosition(0, 27);

            Console.Write("|                                                                                                                                                           |");


            Console.SetCursorPosition(0, 28);
            Console.Write(string.Format("|               [ Poké Repels ]        {0,15}        {1,15}       {2,15}        {3,15}                                  |",
                                          "    Repel      ",

                                          "    Max Repel  ",

                                          "  Super Repel  ",

                                          "      N/A      "));

            Console.SetCursorPosition(0, 29);
            Console.Write("|                                                                                                                                                           |");

            Console.SetCursorPosition(0, 30);
            Console.Write("|                                      ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~                                    |");

            #endregion

            #region [STATS HEAL]
            Console.SetCursorPosition(0, 31);

            Console.Write("|                                                                                                                                                           |");


            Console.SetCursorPosition(0, 32);
            Console.Write(string.Format("|               [ STATS HEAL ]        {0,15}        {1,15}       {2,15}        {3,15}                                   |",
                                          "  Parlyz Heal  ",

                                          "   Burn Heal   ",

                                          "   Ice Heal    ",

                                          "   Antidote    "));

            Console.SetCursorPosition(0, 33);
            Console.Write("|                                                                                                                                                           |");

            #endregion
            Console.SetCursorPosition(0, 34);
            Console.Write("+-----------------------------------------------------------------------------------------------------------------------------------------------------------+");

            PokemonInformationAPIC.ItemGrabAPI.ItemINFO info = PokemonInformationAPIC.ItemGrabAPI.grabItemINFO("Potion").Result;
            ItemPropUI(info, true);
        }


        public static void ItemPropUI(PokemonInformationAPIC.ItemGrabAPI.ItemINFO item, bool show)
        {
            
            if (show == true)
            {

                if (Console.WindowWidth >= 230)
                {
                    string moveName = middleAlign(15, item.ITEM_NAME);

                    Console.SetCursorPosition(165, 6);

                    Console.Write(string.Format("                     +----[{0,-15}]----+                  ", moveName)); //13

                    Console.SetCursorPosition(165, 7);

                    Console.Write("+---------------------------------------------------------------+");

                    Console.SetCursorPosition(165, 8);

                    Console.Write("|                                                               |");

                    Console.SetCursorPosition(165, 9);

                    Console.Write(string.Format("|       ITEM PRICE: [${0,-3}]        ITEM HEALTH (+): [{1,-3}]       |", item.ITEM_PRICE, item.ITEM_HEALTH));

                    Console.SetCursorPosition(165, 10);

                    Console.Write("|                                                               |");
                    Console.SetCursorPosition(165, 11);

                    Console.Write("+---------------------------------------------------------------+");

                    Console.SetCursorPosition(165, 12);

                    Console.Write(item.ITEM_DESCRIPTION);
                }
            }
            else
            {

                if (Console.WindowWidth >= 230)
                {
                    for (int a = 6; a <= 13; a++)
                    {
                        Console.SetCursorPosition(165, a);
                        Console.Write("                                                                 ");
                    }
                }
            }

        }



        #endregion

        #region [DO NOT TOUCH] IMPORTANT UI COMPONENT /ATTACK TYPING\
        public static void attackTyping(bool playerCheck, string move, int attack, bool critical, int damage, bool hit)
        {
            //INT ATTACK - 1 == NORMAL // 2 == NOT EFFECTIVE // 3 == EFFECTIVE

            boxMiddleShow();

            Console.SetCursorPosition(59, 18);

            string effectiveM = "It was an effective move! (-" + damage.ToString() + " HP)";

            string missedM = "The move missed!";

            string directHit = "It was a direct hit! (-" + damage.ToString() + " HP)";

            string nonEffectiveM = "It was not very effective... (-" + damage.ToString() + " HP)";

            string criticalM = " A critical hit! ";

            int speed = 30;

            if (hit == true)
            {
                if (damage >= 1)
                {
                    ConsoleColor colorChosen = ConsoleColor.Green;
                    string message = string.Empty;
                    if (playerCheck == true)
                    {
                        message = userInformation.username + "'s " + pokeInformation.basicInfo.currentPokemon.pokemonName + " uses " + move + "!";
                        colorChosen = ConsoleColor.Green;
                    }
                    else
                    {
                        message = pokeInformation.basicInfo.ENEMY_NAME + "'s " + pokeInformation.basicInfo.currentPokemon.pokemonName + "uses" + move + "!";
                        colorChosen = ConsoleColor.Red;
                    }

                    Console.ForegroundColor = colorChosen;

                    foreach (char i in middleAlign(message.Length + 2, message))
                    {
                        Console.Write(i);
                        Thread.Sleep(speed);
                    }

                    Console.ResetColor();

                    Thread.Sleep(2500);
                    if (attack == 3)
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in middleAlign(effectiveM.Length, effectiveM))
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                    else if (attack == 2)
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in middleAlign(nonEffectiveM.Length, nonEffectiveM))
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in middleAlign(directHit.Length, directHit))
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                }

            }
            else
            {

                if (playerCheck == true && damage >= 1)
                {
                    ConsoleColor colorChosen = ConsoleColor.Green;
                    string message = string.Empty;

                    if (playerCheck == true)
                    {
                        message = userInformation.username + "'s " + pokeInformation.basicInfo.currentPokemon.pokemonName + " uses " + move + "!";
                        colorChosen = ConsoleColor.Green;
                    }
                    else
                    {
                        message = pokeInformation.basicInfo.ENEMY_NAME + "'s " + pokeInformation.basicInfo.currentPokemon.pokemonName + "uses" + move + "!";
                        colorChosen = ConsoleColor.Red;
                    }

                    Console.ForegroundColor = colorChosen;

                    foreach (char i in message)
                    {
                        Console.Write(i);
                        Thread.Sleep(speed);
                    }

                    Thread.Sleep(2500);
                }

                Console.SetCursorPosition(68, 20);

                Console.ForegroundColor = ConsoleColor.Red;
                foreach (char a in missedM)
                {
                    Console.Write(a);
                    Thread.Sleep(speed);
                }

                Console.ResetColor();

                Thread.Sleep(2500);
            }

        }
        #endregion

        #region [DO NOT TOUCH] IMPORTANT UI COMPONENT /ACTION TYPING\
        [Obsolete]
        public static void itemHealthType(bool maxHealth, bool playerCheck, string item, int healthUp)
        {
            string msg1 = string.Empty;
            string msg2 = string.Empty;

            ConsoleColor colorChosen = ConsoleColor.Red;
            if (playerCheck == true && maxHealth == false)
            {
                msg1 = userInformation.username + " uses " + item + " on " + pokeInformation.basicInfo.currentPokemon.pokemonName + "!";
                msg2 = userInformation.username + "'s " + pokeInformation.basicInfo.currentPokemon.pokemonName + " HP rose! (+" + healthUp + " HP)";
                colorChosen = ConsoleColor.Green;
            }
            else if (playerCheck == false && maxHealth == false)
            {
                msg1 = pokeInformation.basicInfo.ENEMY_NAME + " uses " + item + " on " + pokeInformation.basicInfo.currentPokemon.pokemonName + "!";
                msg2 = pokeInformation.basicInfo.ENEMY_NAME + "'s " + pokeInformation.basicInfo.currentPokemon.pokemonName + " HP rose! (+" + healthUp + " HP)";
                colorChosen = ConsoleColor.Red;
            }
            else if (playerCheck == true && maxHealth == true)
            {
                msg1 = userInformation.username + "'s " + pokeInformation.basicInfo.currentPokemon.pokemonName + " is at full health!";
                msg2 = userInformation.username + "'s "+ pokeInformation.basicInfo.currentPokemon.pokemonName + " MAX HP is " + pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH_MAX.ToString() + " HP";
                colorChosen = ConsoleColor.Green;
            }
            int speed = 50;

            boxMiddleShow();

            Console.SetCursorPosition(59, 18);
            Console.ForegroundColor = colorChosen;
            foreach (char i in middleAlign(msg1.Length, msg1))
            {
                Console.Write(i);
                Thread.Sleep(speed);
            }

            Console.ResetColor();

            Thread.Sleep(2200);
            Console.SetCursorPosition(59, 20);
            foreach (char a in middleAlign(msg2.Length, msg2))
            {
                Console.Write(a);
                Thread.Sleep(speed);
            }

            BlinkingAction(true);
            Console.ReadKey();
            backgroundThread2.Abort();
        }

        #endregion

        #region [DO NOT TOUCH] IMPORTANT UI COMPONENT /POKEMON TYPING\
        [Obsolete]
        public static void PokemonChangeType(bool current, bool playerCheck, string pokemonold, string pokemonnew)
        {
            string msg1 = string.Empty;
            string msg2 = string.Empty;
            int speed = 50;

            if (current == false)
            {
                ConsoleColor colorChosen = ConsoleColor.Red;
                if (playerCheck == true)
                {
                    msg1 = userInformation.username + ": " + pokemonold + " return!";
                    msg2 = userInformation.username + " sends out " + pokemonnew + " to battle!";
                    colorChosen = ConsoleColor.Green;
                }
                else
                {
                    msg1 = pokeInformation.basicInfo.ENEMY_NAME + ": " + pokemonold + " return!";
                    msg2 = pokeInformation.basicInfo.ENEMY_NAME + " sends out " + pokemonnew + " to battle!";
                    colorChosen = ConsoleColor.Red;
                }

                boxMiddleShow();

                Console.SetCursorPosition(59, 18);
                Console.ForegroundColor = colorChosen;

                MusicPlayerC.pokeballReturn();
                foreach (char i in middleAlign(msg1.Length, msg1))
                {
                    Console.Write(i);
                    Thread.Sleep(speed);
                }

                Console.ResetColor();

                Thread.Sleep(2200);
                Console.SetCursorPosition(59, 20);
                MusicPlayerC.pokeballOut();
                foreach (char a in middleAlign(msg2.Length, msg2))
                {
                    Console.Write(a);
                    Thread.Sleep(speed);
                }
                BlinkingAction(true);
                Console.ReadKey();
                backgroundThread2.Abort();
            }
            else
            {
                Console.ResetColor();
                msg1 = pokemonold + " is already out! Pick another pokemon!";

                Console.SetCursorPosition(54, 18);
                foreach (char i in middleAlign(msg1.Length, msg1))
                {
                    Console.Write(i);
                    Thread.Sleep(speed);
                }
            }

            BlinkingAction(true);
            Console.ReadKey();

        }
        #endregion

        #region [DO NOT TOUCH] IMPORTANT UI COMPONENT /ATTACK STATS\
        public static void AttackSTATS(int arr, string move, bool show)
        {
            if (show == true)
            {

                if (Console.WindowWidth >= 230)
                {
                    string moveName = middleAlign(16, move);

                    Console.SetCursorPosition(165, 6);

                    Console.Write(string.Format("                     +----[{0,-15}]----+                  ", moveName)); //13

                    Console.SetCursorPosition(165, 7);

                    Console.Write("+---------------------------------------------------------------+");

                    Console.SetCursorPosition(165, 8);

                    Console.Write("|                                                               |");

                    Console.SetCursorPosition(165, 9);

                    Console.Write(string.Format("|         ATTACK POWER: [{0,-3}]         POWER POINT: [{1,-2}]         |", pokeInformation.basicInfo.currentPokemon.Pkmn_Moves[arr].BASEPOWER, pokeInformation.basicInfo.currentPokemon.Pkmn_Moves[arr].CURR_USES));

                    Console.SetCursorPosition(165, 10);

                    Console.Write("|                                                               |");

                    Console.SetCursorPosition(165, 11);

                    Console.Write(string.Format("|         MAX PWR. POINT: [{0,-2}]        MOVE ACCURACY: [{1,-3}]      |", pokeInformation.basicInfo.currentPokemon.Pkmn_Moves[arr].MAX_USES, pokeInformation.basicInfo.currentPokemon.Pkmn_Moves[arr].ACCURACY_M));

                    Console.SetCursorPosition(165, 12);

                    Console.Write("|                                                               |");

                    Console.SetCursorPosition(165, 13);

                    Console.Write("+---------------------------------------------------------------+");
                }
            }
            else
            {

                if (Console.WindowWidth >= 230)
                {
                    for (int a = 6; a <= 13; a++)
                    {
                        Console.SetCursorPosition(165, a);
                        Console.Write("                                                                 ");
                    }
                }
            }

        }

        #endregion

        #region[DO NOT TOUCH] IMPORTANT UI COMPONENT /POKEMON LIST\
        [Obsolete]
        public static int ChoosePokemon()
        {

            List<CordsY> cords = new List<CordsY>
            {
                //TOP ROW
                new CordsY() { x = 71, x2 = 86, y = 34 },
                new CordsY() { x = 100 , x2 = 115 , y = 34},
                new CordsY() { x = 128 , x2 = 143 , y = 34},

                //BOTTOM ROW
                new CordsY() { x = 71, x2 = 86, y = 36 },
                new CordsY() { x = 100 , x2 = 115 , y = 36},
                new CordsY() { x = 128 , x2 = 143 , y = 36}
            };

            int i = 0;

            CordsY[] arrayCords = cords.ToArray();

            HeightY = 35;
            CordsY begin = arrayCords[0];
            currentCordsY = begin;
            BlinkingAction(false);

            ConsoleKey key;

            string chosen = string.Empty;
            do
            {
                currentCordsY = arrayCords[i];
                var infokey = Console.ReadKey(intercept: true);
                key = infokey.Key;


                if (!Console.KeyAvailable)
                {
                    if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                    {
                        Thread.Sleep(500);
                        MusicPlayerC.ButtonClick();
                        resetPokemonLine();
                        int a = i;
                        a++;
                        if (a <= 5 && a >= 0)
                        {
                            i++;
                            CordsY chosenXX = arrayCords[i];


                            currentCordsY = chosenXX;
                        }
                        else
                        {
                            i = 5;

                            CordsY chosenXX = arrayCords[i];

                            currentCordsY = chosenXX;
                        }
                    }
                    else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                    {
                        Thread.Sleep(500);
                        MusicPlayerC.ButtonClick();
                        resetPokemonLine();

                        int a = i;
                        a -= 1;
                        if (a <= 5 && a >= 0)
                        {
                            i -= 1;
                            CordsY chosenXX = arrayCords[i];

                            currentCordsY = chosenXX;
                        }
                        else
                        {
                            i = 0;

                            CordsY chosenXX = arrayCords[i];

                            currentCordsY = chosenXX;
                        }
                    }

                    else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                    {
                        Thread.Sleep(500);
                        MusicPlayerC.ButtonClick();
                        resetPokemonLine();

                        if (i == 0)
                        {
                            i = 3;
                        }
                        else if (i == 1)
                        {
                            i = 4;
                        }
                        else if (i == 2)
                        {
                            i = 5;
                        }


                    }
                    else if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                    {
                        Thread.Sleep(500);
                        MusicPlayerC.ButtonClick();
                        resetPokemonLine();

                        if (i == 3)
                        {
                            i = 0;
                        }
                        else if (i == 4)
                        {
                            i = 1;
                        }
                        else if (i == 5)
                        {
                            i = 2;
                        }
                    }
                }

                Task.Delay(300);
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);

            if (key == ConsoleKey.Escape)
            {
                i = -1;
            }

            return i;
        }

        public static void resetPokemonLine()
        {
            string[] moveNames = new string[userInformation.pokemonList.Count];

            int a = 0;
            foreach (PokemonInfStruct i in userInformation.pokemonList)
            {
                string temp = "";
                if (i.pokemonName == null)
                {
                    temp = middleAlign(14, "~~~~~~~~~~~~~~");
                }
                else
                {
                    temp = middleAlign(14, i.pokemonName);
                }

                moveNames[a] = temp;
                a++;
            }

            finalNames2 = moveNames;

            Console.SetCursorPosition(66, 34);
            Console.Write(string.Format("|     {0,14}               {1,14}              {2,14}        |",
                                           finalNames2[0],

                                           finalNames2[1],

                                           finalNames2[2]));

            Console.SetCursorPosition(66, 35);

            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 36);

            Console.Write(string.Format("|     {0,14}               {1,14}              {2,14}        |",
                                           finalNames2[3],

                                           finalNames2[4],

                                           finalNames2[5]));

        }

        public static void PokemonBox()
        {
            loopArrow = false;
            Console.ResetColor();

            string[] moveNames = new string[userInformation.pokemonList.Count];

            int a = 0;
            foreach (PokemonInfStruct i in userInformation.pokemonList)
            {
                string temp = "";
                if (i.pokemonName == null)
                {
                    temp = middleAlign(14, "~~~~~~~~~~~~~~");
                }
                else
                {
                    temp = middleAlign(14, i.pokemonName);
                }

                moveNames[a] = temp;
                a++;
            }

            Console.SetCursorPosition(66, 30);

            Console.Write("+------------------------------------------------------------------------------------+");


            Console.SetCursorPosition(66, 31);


            Console.Write("|                                    Choose your pokemon!                            |");

            Console.SetCursorPosition(66, 32);

            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 33);

            Console.Write("|                                                                                    |");

            resetPokemonLine();

            Console.SetCursorPosition(66, 37);
            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 38);
            Console.Write("+------------------------------------------------------------------------------------+");
        }
        #endregion

        #region [DO NOT TOUCH] IMPORTANT UI COMPONENT /POKEMON FAINTED\
        [Obsolete]
        public static void PokemonFainted(bool playerCheck, string pokemon)
        {
            string msg1 = string.Empty;
            string msg2 = string.Empty;
            int speed = 50;

            ConsoleColor colorChosen = ConsoleColor.Red;
            if (playerCheck == true)
            {
                msg1 = userInformation.username + "'s " + pokemon + " has fainted!";
                if (pokeInformation.basicInfo.pokemonLeft >= 1)
                {
                    msg2 = "Choose your next pokemon!";
                }
                else
                {
                    msg2 = "";
                }
                colorChosen = ConsoleColor.Green;
            }
            else
            {
                if (pokeInformation.basicInfo.ENEMY_NAME == "TRAINER")
                {
                    msg1 = pokeInformation.basicInfo.ENEMY_NAME + "'s " + pokeInformation.basicInfo.EnemyPokemon.pokemonName + " has fainted!";
                    //msg2 = pokeInformation.basicInfo.ENEMY_NAME + " sends out " + pokemonnew + " to battle!";
                    colorChosen = ConsoleColor.Red;
                }
                else
                {
                    msg1 = pokeInformation.basicInfo.EnemyPokemon.pokemonName + " has fainted!";
                    //msg2 = pokeInformation.basicInfo.ENEMY_NAME + " sends out " + pokemonnew + " to battle!";
                    colorChosen = ConsoleColor.Red;
                }
            }

            boxMiddleShow();

            Console.SetCursorPosition(57, 18);
            Console.ForegroundColor = colorChosen;
            foreach (char i in middleAlign(42, msg1))
            {
                Console.Write(i);
                Thread.Sleep(speed);
            }

            Console.ResetColor();

            Thread.Sleep(2200);
            Console.SetCursorPosition(57, 20);
            foreach (char a in middleAlign(42, msg2))
            {
                Console.Write(a);
                Thread.Sleep(speed);
            }
            BlinkingAction(true);
            Console.ReadKey();
            backgroundThread2.Abort();

        }

        [Obsolete]
        public static void RunAwayText(int enemyType)
        {
            string msg1 = string.Empty;
            string msg2 = string.Empty;
            int speed = 50;

            ConsoleColor colorChosen = ConsoleColor.Red;
            if (enemyType == 1)
            {
                msg1 = userInformation.username + " got away safely!";
            }
            else
            {
                msg1 = "The Enemy is a trainer!";
            }

            if (pokeInformation.basicInfo.pokemonLeft >= 1)
            {
                msg2 = "";
            }
            else
            {
                msg2 = "You cannot run away from this battle!";
            }
            colorChosen = ConsoleColor.Green;

            boxMiddleShow();

            Console.SetCursorPosition(64, 18);
            Console.ForegroundColor = colorChosen;
            foreach (char i in middleAlign(msg1.Length, msg1))
            {
                Console.Write(i);
                Thread.Sleep(speed);
            }

            Console.ResetColor();

            Thread.Sleep(2200);
            Console.SetCursorPosition(59, 20);
            foreach (char a in middleAlign(msg2.Length, msg2))
            {
                Console.Write(a);
                Thread.Sleep(speed);
            }
            BlinkingAction(true);
            Console.ReadKey();
            backgroundThread2.Abort();

        }
        #endregion

        #region [BATTLE Transition]
        public static void BattleStartTransition()
        {
            ConsoleColor red = ConsoleColor.Red;
            ConsoleColor white = ConsoleColor.White;

            int maxWidth = Console.WindowWidth;
            int length = 49;
            MusicPlayerC.BattleMusic();
            Console.CursorVisible = false;
            ChangeColor(maxWidth, length, white);
            Thread.Sleep(90);

            Console.BackgroundColor = red;
            Console.Clear();
            Thread.Sleep(130);

            Console.BackgroundColor = white;
            Console.Clear();
            Thread.Sleep(130);

            Console.BackgroundColor = red;
            Console.Clear();
            Thread.Sleep(100);

            Console.BackgroundColor = white;
            Console.Clear();
            Thread.Sleep(100);


            Console.ResetColor();
            Console.Clear();
        }

        public static void ChangeColor(int maxWidth, int length, ConsoleColor color)
        {
            Console.ResetColor();
            Console.Clear();
            for (int a = 0; a < length; a += 12) //Y
            {
                for (int c = 0; c < maxWidth; c++) //X
                {
                    int b = a;
                    Console.BackgroundColor = color;
                    Console.SetCursorPosition(c, a);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                    Console.SetCursorPosition(c, b += 1);
                    Console.Write(" ");
                }
            }

            Console.Clear();
        }
        #endregion

        #region [WINNER OR LOST]
        public static void Winner(int exp)
        {
            pokemontitleEnd();

            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("|                                                            You have won this battle !                                                             |");
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine(string.Format("|                                                              You have gained {0,-3} XP!                                                              |", exp));
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("+---------------------------------------------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("\n Press any key to continue");


        }

        public static void pokemontitleEnd()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(@"+---------------------------------------------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine(@"|                                                                    ,'\                                                                            |
|                                      _.----.        ____         ,'  _\   ___    ___     ____                                                     |
|                                  _,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.                                               |
|                                   \      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |                                             |
|                                    \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |                                             |
|                                      \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |                                             |
|                                       \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |                                             |
|                                        \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |                                             |
|                                         \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |                                             |
|                                          \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |                                             |
|                                           \_.-'       |__|    `-._ |              '-.|     '-.| |   |                                             |
|                                                                   `'                            '-._|                                             |");
        }

        public static void Lost()
        {
            int randomNum = new Random().Next(1, 30);

            pokemontitleEnd();

            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("|                                                           You sadly lost this battle..                                                            |");
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine(string.Format("|                                                              You lost {0,-3} Pokécoins!                                                              |", randomNum));
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("+---------------------------------------------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("\n Press any key to continue");

            userInformation.user_COINS -= randomNum;
        }
        #endregion

        #region [RETRY MENU]
        public static string RetryBattle()
        {
            Console.Clear();
            pokemontitleEnd();

            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("|                                                              You want to try again?                                                               |");
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("|                                                       [M] - Main Menu // [Y] - Yes // [N] - No                                                    |");
            Console.WriteLine("|                                                                                                                                                   |");
            Console.WriteLine("+---------------------------------------------------------------------------------------------------------------------------------------------------+");

            string RETURN_VALUE = " ";

            ConsoleKeyInfo key;


            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.M && key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

            if (key.Key == ConsoleKey.M)
            {
                RETURN_VALUE = "M";
            }
            else if (key.Key == ConsoleKey.Y)
            {
                RETURN_VALUE = "Y";
            }
            else if (key.Key == ConsoleKey.N)
            {
                RETURN_VALUE = "N";
            }

            return RETURN_VALUE;
        }
        #endregion

        #region [CATCH POKEMON]
        public static void catchPokemonUI(bool brokefree, int timesShake)
        {
            MusicPlayerC.BattleMPlayer.Pause();
            MusicPlayerC.pokeballhit();
            Thread.Sleep(3500);
            MusicPlayerC.pokeballWoop();
            boxMiddleShow();
            Console.SetCursorPosition(66, 18);
            int length = ("Attempting to catch " + pokeInformation.basicInfo.EnemyPokemon.pokemonName + "...").Length;
            Console.Write("Attempting to catch " + pokeInformation.basicInfo.EnemyPokemon.pokemonName + "...");
            Console.SetCursorPosition(69, 20);
            Console.Write("[                    ]");


            if (brokefree == false)
            {

                int a = 70;
                double b = 0;
                do
                {
                    b = b + 5;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(a, 20);
                    Console.Write(" ");

                    Console.SetCursorPosition(92, 20);
                    Console.ResetColor();
                    Console.Write(string.Format("{0,-3}%", b));
                    a++;
                    Thread.Sleep(300);
                } while (a < 90);
                Console.ResetColor();

                MusicPlayerC.pokeballCaught();
                Console.SetCursorPosition(66, 18);
                Console.Write((" ".PadRight(length)));

                Console.SetCursorPosition(69, 20);
                Console.Write(" ".PadRight(28));
                Thread.Sleep(1400);
                Console.SetCursorPosition(66, 18);
                Console.Write("You caught a " + pokeInformation.basicInfo.EnemyPokemon.pokemonName + "!");
                MusicPlayerC.CaughtMusic();
                Thread.Sleep(6100);
            }
            else
            {
                int a = 70;
                double b = 0;
                do
                {
                    b = b + 5;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(a, 20);
                    Console.Write(" ");

                    Console.SetCursorPosition(92, 20);
                    Console.ResetColor();
                    Console.Write(string.Format("{0,-3}%", b));
                    a++;
                    Thread.Sleep(300);
                } while (a < timesShake + 1);
                Console.ResetColor();

                Console.SetCursorPosition(66, 18);
                Console.Write((" ".PadRight(length)));

                Console.SetCursorPosition(69, 20);
                Console.Write(" ".PadRight(28));

                MusicPlayerC.pokeballOut();

                Console.SetCursorPosition(66, 18);
                Console.Write(pokeInformation.basicInfo.EnemyPokemon.pokemonName + " broke free!");
                Thread.Sleep(3500);
            }
        }

        [Obsolete]
        public static void TrainedPokemonAttempt()
        {
            boxMiddleShow();
            Console.SetCursorPosition(64, 18);
            int length = "You cannot catch a trainer pokemon!".Length;
            Console.Write("You cannot catch a trainer pokemon!");

            BlinkingAction(true);
            Console.ReadKey();
            loopArrow = false;
        }

        #endregion
    }
}
