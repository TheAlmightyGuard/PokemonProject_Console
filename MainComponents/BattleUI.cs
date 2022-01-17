using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PokemonGame.FunctionClasses.InventoryMain;
using PokemonGame.informationClass;

namespace PokemonGame.MainComponents
{
    public class Cords
    {
        public int x { get; set; }
        public int x2 { get; set; }
    }
    class BattleUI
    {

        public static Cords currentCords = new Cords() { x = 76, x2 = 91 };
        public static bool loopArrow = false;

        public static ThreadStart ts = new ThreadStart(BackgroundMethod);
        public static Thread backgroundThread = new Thread(ts);

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




            BlinkingAction();
            //Console.ReadKey();

            //Console.ReadKey();
            //loopArrow = false;
            //backgroundThread.Abort();
            //pokemontitle();
            //Console.ReadKey();
            //Console.Clear();
        }

        public static void BlinkingAction()
        {
            loopArrow = true;
            
            backgroundThread.Start();
        }

        public static void StopBlinking()
        {
            loopArrow = false;

            backgroundThread.Abort();
        }

        private static void BackgroundMethod()
        {
            while (loopArrow == true)
            {
                resetActionLine();
                Thread.Sleep(330);
                Console.SetCursorPosition(currentCords.x, 36);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(">");

                Console.SetCursorPosition(currentCords.x2, 36);
                Console.Write("<");
                Console.ResetColor();
                Thread.Sleep(330);
            }
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
            Console.WriteLine("|                         --[YOUR SIDE]--                                                                                                                   |");
            Console.WriteLine("|  +----------------------------------------------------------+                                                                                             |");
            Console.WriteLine(string.Format("|  |         NAME: {0,-11}             LVL: {1,-5}         |                                                                                             |", userInformation.username, pokeInformation.basicInfo.levelPokemon));
            Console.WriteLine("|  |                                                          |                                                                                             |");
            Console.WriteLine("|  |                                                          |                                                                                             |");
            Console.WriteLine(string.Format("|  |         POKEMON: {0,-9}            HEALTH: {1,-3} HP     |                                                                                             |", pokeInformation.basicInfo.pokemon, pokeInformation.basicInfo.Health));
            Console.WriteLine("|  +----------------------------------------------------------+                                                                                             |");
        }

        public static void boxMiddleShow()
        {
            string space = "";
            int num = 14;

            while (num <= 25)
            {

                Console.SetCursorPosition(0, num);
                Console.WriteLine("|" + space.PadRight(153) + "|");
                num++;
            }

            Console.SetCursorPosition(48, 16);
            Console.Write("+----------------------------------------------------------+");

            Console.SetCursorPosition(48, 17);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(48, 18);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(48, 19);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(48, 20);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(48, 21);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(48, 22);

            Console.Write("+----------------------------------------------------------+");
        }

        public static int ChooseAction()
        {
            List<Cords> cords = new List<Cords>
            {
                new Cords() { x = 76 , x2 = 91 }, //ATTACK
                new Cords() { x = 93 , x2 = 111 }, //INVENTORY
                new Cords() { x = 113 , x2 = 129}, //POKEMON
                new Cords() { x = 131 , x2 = 143}, //RUN
            };

            int i = 0;

            var arrayCords = cords.ToArray();

            ConsoleKey key;

            string chosen = string.Empty;
            do
            {
                var infokey = Console.ReadKey(intercept: true);
                key = infokey.Key;


                if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                {
<<<<<<< HEAD
                    Thread.Sleep(5);
                    MusicPlayerC.ButtonClick();
=======
>>>>>>> parent of 20aad4e (Update v1.1)
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
<<<<<<< HEAD
                    Thread.Sleep(5);
                    MusicPlayerC.ButtonClick();
=======
>>>>>>> parent of 20aad4e (Update v1.1)
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
                    break;
                }
                else if (key == ConsoleKey.I)
                {
                    i = 1;
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

            } while (key != ConsoleKey.Enter);

            return i;
        }

        public static void resetActionLine()
        {
            Console.SetCursorPosition(69, 36);

            Console.Write("|        [A] - Attack     [I] - Inventory     [P] - Pokemon     [R] - Run        |");
        }
        public static void mainBox(int X)
        {
            int X_ = X;

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

        public static void AttackBox()
        {
            StopBlinking();

            Console.ResetColor();

            Console.SetCursorPosition(66, 31);

            Console.Write("+------------------------------------------------------------------------------------+");


            Console.SetCursorPosition(66, 32);


            Console.Write("|                                               Choose your item!                    |");

            Console.SetCursorPosition(66, 33);

            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 34);

            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 35);


            Console.Write(string.Format("|     {0,-13}     {1,-13}     {2,-13}     {3,-13}         |",
                                           pokeInformation.advInfo.MOVENAMES[0],

                                           pokeInformation.advInfo.MOVENAMES[1],

                                           pokeInformation.advInfo.MOVENAMES[2],

                                          pokeInformation.advInfo.MOVENAMES[3]));

            Console.SetCursorPosition(66, 36);

            Console.Write(string.Format("|      {0,-8}           {1,-8}          {2,-8}        {3,-8}                |",
                                          $"[{pokeInformation.advInfo.CurrUsesAR[0]}/{pokeInformation.advInfo.MaxUsesAR[0]}]",

                                          $"[{pokeInformation.advInfo.CurrUsesAR[0]}/{pokeInformation.advInfo.MaxUsesAR[0]}]",

                                          $"[{pokeInformation.advInfo.CurrUsesAR[0]}/{pokeInformation.advInfo.MaxUsesAR[0]}]",

                                          $"[{pokeInformation.advInfo.CurrUsesAR[0]}/{pokeInformation.advInfo.MaxUsesAR[0]}]"));

            Console.SetCursorPosition(66, 37);
            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 38);
            Console.Write("+------------------------------------------------------------------------------------+");
        }

        public static void InventoryBox()
        {
            StopBlinking();

            Console.ResetColor();

            Console.SetCursorPosition(66, 31);

            Console.Write("+------------------------------------------------------------------------------------+");


            Console.SetCursorPosition(66, 32);


            Console.Write("|                                               Choose your item!                    |");

            Console.SetCursorPosition(66, 33);

            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 34);

            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 35);


            Console.Write(string.Format("|       {0,-12}     {1,-12}     {2,-12}     {3,-12}           |",
                                           userInformation.ITEM_NAME[0],

                                           userInformation.ITEM_NAME[1],

                                           userInformation.ITEM_NAME[2],

                                           userInformation.ITEM_NAME[3]));

            Console.SetCursorPosition(66, 36);

            Console.Write(string.Format("|      {0,-8}           {1,-8}          {2,-8}        {3,-8}                |",
                                          $"[{userInformation.ITEM_AMOUNT[0]} LEFT]",

                                          $"[{userInformation.ITEM_AMOUNT[1]} LEFT]",

                                          $"[{userInformation.ITEM_AMOUNT[2]} LEFT]",

                                          $"[{userInformation.ITEM_AMOUNT[3]} LEFT]"));

            Console.SetCursorPosition(66, 37);
            Console.Write("|                                                                                    |");

            Console.SetCursorPosition(66, 38);
            Console.Write("+------------------------------------------------------------------------------------+");
        }

        public static void actionTyping(bool playerCheck, string move, int attack, bool critical, int damage, bool hit)
        {
            //INT ATTACK - 1 == NORMAL // 2 == NOT EFFECTIVE // 3 == EFFECTIVE

            boxMiddleShow();

            Console.SetCursorPosition(59, 18);

            string effectiveM = "It was an effective move! (-" + damage.ToString() + " HP)";

            string missedM = "The move missed!";

            string directHit = "It was a direct hit! (-" + damage.ToString() + " HP)";

            string nonEffectiveM = "It was not very effective... (-" + damage.ToString() + " HP)";

            string criticalM = " A critical hit! ";

            int speed = 60;

            if (hit == true)
            {
                if (playerCheck == true && damage >= 1)
                {
                    string message = userInformation.username + "'s " + pokeInformation.basicInfo.pokemon + " uses " + move + "!";


                    //string message = "Ryann's Charizard uses Flamethrower!";

                    Console.ForegroundColor = ConsoleColor.Green;
                    //Console.Write("|" + space.PadLeft(leftPadding) + message + space.PadLeft(rightPadding) + "|");

                    foreach (char i in message)
                    {
                        Console.Write(i);
                        Thread.Sleep(speed);
                    }

                    Console.ResetColor();

                    Thread.Sleep(2500);
                    if (attack == 3)
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in effectiveM)
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                    else if (attack == 2)
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in nonEffectiveM)
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in directHit)
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                }
                else if (playerCheck == false && damage >= 1)
                {
                    string message = pokeInformation.basicInfo.ENEMY_NAME + "'s " + pokeInformation.basicInfo.ENEMY_pokemon + "uses" + move + "!";

                    //string message = "Ryann's Charizard uses Flamethrower!";

                    Console.ForegroundColor = ConsoleColor.Red;
                    //Console.Write("|" + space.PadLeft(leftPadding) + message + space.PadLeft(rightPadding) + "|");

                    foreach (char i in message)
                    {
                        Console.Write(i);
                        Thread.Sleep(speed);
                    }

                    Console.ResetColor();

                    Thread.Sleep(2500);
                    if (attack == 3)
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in effectiveM)
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                    else if (attack == 2)
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in nonEffectiveM)
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(59, 20);
                        foreach (char a in directHit)
                        {
                            Console.Write(a);
                            Thread.Sleep(speed);
                        }
                    }
                }

                Thread.Sleep(5000);
                Console.ResetColor();
                if (critical == true)
                {
                    Console.SetCursorPosition(48, 20);
                    Console.Write("|                                                          |");
                    Console.SetCursorPosition(68, 20);

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (char a in criticalM)
                    {
                        Console.Write(a);
                        Thread.Sleep(speed);
                    }
                }
                Console.ResetColor();

            }
            else
            {

                if (playerCheck == true && damage >= 1)
                {
                    string message = userInformation.username + "'s " + pokeInformation.basicInfo.pokemon + " uses " + move + "!";


                    //string message = "Ryann's Charizard uses Flamethrower!";

                    Console.ForegroundColor = ConsoleColor.Green;
                    //Console.Write("|" + space.PadLeft(leftPadding) + message + space.PadLeft(rightPadding) + "|");

                    foreach (char i in message)
                    {
                        Console.Write(i);
                        Thread.Sleep(speed);
                    }

                    Thread.Sleep(2500);
                }

                else if (playerCheck == false && damage >= 1)
                {
                    string message = pokeInformation.basicInfo.ENEMY_NAME + "'s " + pokeInformation.basicInfo.ENEMY_pokemon + "uses" + move + "!";

                    //string message = "Ryann's Charizard uses Flamethrower!";

                    Console.ForegroundColor = ConsoleColor.Red;
                    //Console.Write("|" + space.PadLeft(leftPadding) + message + space.PadLeft(rightPadding) + "|");

                    foreach (char i in message)
                    {
                        Console.Write(i);
                        Thread.Sleep(speed);
                    }

                    Console.ResetColor();

                    Thread.Sleep(2500);
                }

                Console.SetCursorPosition(68, 20);

                Console.ForegroundColor = ConsoleColor.Red;
                foreach (char a in missedM)
                {
                    Console.Write(a);
                    Thread.Sleep(speed);
                }
            }

        }

        public static void typeEnemy(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(60, 12);

            foreach (char i in text)
            {
                Console.Write(i);
                Task.Delay(2500);
            }
            Console.ResetColor();
        }

        public static void typePlayer(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(60, 26);

            foreach (char i in text)
            {
                Console.Write(i);
                Task.Delay(2500);
            }
            Console.ResetColor();
        }

        public static void EnemyBox(int x)
        {
            int X_ = x;

            Console.SetCursorPosition(X_, 2);

            Console.Write("+----------------------------------------------------------+");

            Console.SetCursorPosition(X_, 3);

            Console.Write("|         NAME: 12345678910             LVL: 10000         |");

            Console.SetCursorPosition(X_, 4);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(X_, 5);

            Console.Write("|                                                          |");

            Console.SetCursorPosition(X_, 6);

            Console.Write("|         POKEMON: CHARIZARD            HEALTH: 999 HP     |");

            Console.SetCursorPosition(X_, 7);

            Console.Write("+----------------------------------------------------------+");

            Console.SetCursorPosition(X_ + 24, 8);

            Console.Write("--[ENEMY SIDE]--");
        }



    }
}
