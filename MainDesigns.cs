using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.FunctionClasses.InventoryMain;
using PokemonGame.Main;
using PokemonGame.informationClass;
using System.Threading;
using PokemonGame.MainComponents;

namespace PokemonGame.MainDes
{
    class MainDesigns
    {
        public static string textShow = "";
        public static ThreadStart ts = new ThreadStart(snakeBorder);
        public static ThreadStart ts2 = new ThreadStart(evolutionBar);
        public static int a = 0;
        public static int b = 1;
        public static int midCord = Console.WindowWidth / 2 - 20;
        public static Thread backgroundThread = new Thread(ts);
        public static Thread backgroundThread2 = new Thread(ts2);

        public static void pokemontitle()
        {
            string line = new string('-', 91) + "+";
            Console.WriteLine(line);
            Console.WriteLine(@"                                                                                           |
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
                                           `'                            '-._|             |");
        }

        public static void EvolveFunc(string pokemonEvolveFrom, string pokemonEvolveTo)
        {
            //while (true)
            //{
            //    var random = new Random();
            //    ConsoleColor consolec = (ConsoleColor)random.Next(1, 14);
            //    Console.BackgroundColor = consolec;
            //    Console.Clear();
            //    Thread.Sleep(1);
            //    Console.ResetColor();
            //    Console.Clear();
            //    Thread.Sleep(1);
            //}


            Console.CursorVisible = false;

            Console.ReadKey();
            MusicPlayerC.EvolutionSound();
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Thread.Sleep(20);
            Console.ResetColor();
            Console.Clear();
            Thread.Sleep(30);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Thread.Sleep(20);
            Console.ResetColor();
            Console.Clear();

            int maxWidth = Console.WindowWidth;
            int length = 50;

            int top = 0;


            //int midCord = Console.WindowWidth / 2 - 30;

            // Console.SetCursorPosition(midCord, 20);

            textShow = string.Format("Your {0,-11} is starting to evolve..!", BattleUI.middleAlign(11, pokemonEvolveFrom));

            string text2 = string.Format("Your {0,-11} has evolved into a {1,-13}!", BattleUI.middleAlign(11, pokemonEvolveFrom), BattleUI.middleAlign(13, pokemonEvolveTo));

            backgroundThread.IsBackground = true;
            backgroundThread.Start();
            //Line1Middle(midCord, 20, text1);
            Thread.Sleep(3000);
            //Line1Middle(midCord, 20, text2);


            Console.ReadKey();
        }

        public static void Line1Middle(int midCord, int YAxis, string text)
        {
            Console.Write("                                            ");
            Console.SetCursorPosition(midCord, YAxis);
            Console.Write(BattleUI.middleAlign(100, text));
        }

        public static void snakeBorder()
        {
            char[] textChar = textShow.ToCharArray();

            Console.SetCursorPosition(midCord + a, 20);

            Console.Write(textShow);

            bool first = false;
            while (a < 90)
            {
                var random = new Random();
                ConsoleColor consolec = (ConsoleColor)random.Next(1, 14);
                int maxWidth = Console.WindowWidth;
                int length = 50;

                for (int i = 0; i < maxWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.BackgroundColor = consolec;
                    Console.Write(" ");
                    //Thread.Sleep(1);
                }

                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.BackgroundColor = consolec;
                    Console.Write("  ");
                    //Thread.Sleep(1);
                }

                for (int i = 0; i < length; i++)
                {
                    Console.SetCursorPosition(maxWidth - 2, i);
                    Console.BackgroundColor = consolec;
                    Console.Write("  ");
                    // Thread.Sleep(1);
                }

                for (int i = 0; i < maxWidth; i++)
                {
                    Console.SetCursorPosition(i, length);
                    Console.BackgroundColor = consolec;
                    Console.Write(" ");
                    // Thread.Sleep(1);
                }


                Console.ResetColor();
                Console.SetCursorPosition(midCord + a, 22);
                if (first == false)
                {
                    Console.SetCursorPosition(midCord, 22);
                    Console.Write("[");
                    Console.SetCursorPosition(midCord + 21, 22);
                    Console.Write("]");
                    first = true;
                }
                if (b == 1)
                {
                    backgroundThread2.Start();
                }
                a++;
                Thread.Sleep(250);
            }

            backgroundThread.Abort();
        }

        public static void evolutionBar()
        {
            while (b < 21)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(midCord + b, 22);
                Console.Write("■");
                b++;
                Thread.Sleep(200);
                //Task.Delay(1500);
            }
        }

    }
}
