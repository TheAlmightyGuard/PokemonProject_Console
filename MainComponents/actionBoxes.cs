using System;
using System.Media;
using System.IO;
using PokemonGame.MainComponents.AttackList;
using PokemonGame.informationClass;
using PokemonGame.MainComponents.Main;
using System.Threading;

namespace PokemonGame.MainComponents.actionBoxes
{

    class ActionBoxes
    {
        //public class EndLineType
        //{
        //    public static string EndLine1 = "You have won this battle with pride and teamwork among your pokemon...";
        //    public static string EndLine2 = "But.. This is just the beginning of a new chapter.. Young one..";
        //}

        public class EndLineType
        {
            public static string EndLine1 = "Congratulations! You have won this battle. You may now rest until next time!";
            public static string EndLine2 = "The Enemy says: This... Is just... The beginning...";
        }
        public static void mainBox(string username, int YHealth, int EHealth, string enemy)
        {
            Console.WriteLine("+--------------------------------------------------------------------------------+");
            Console.WriteLine(string.Format("|   {0,-20}       {1,-19}       {2,-21}   |", $"Your Pokemon: {YHealth} HP", $"Username: {username} ", $"Enemy pokemon: {EHealth} HP"));
            Console.WriteLine("|                                                                                |");
            Console.WriteLine("|                                                           {0,15}      |",enemy);
            Console.WriteLine("|                               Choose your action                               |");
            Console.WriteLine("|                                                                                |");
            Console.WriteLine("|        [A] - Attack     [I] - Inventory     [P] - Pokemon     [R] - Run        |");
            Console.WriteLine("+--------------------------------------------------------------------------------+");
        }

        public static void results(string username, bool YWinner, int YHealth, int EHealth)
        {
            string winner = string.Empty;
            if (YWinner == true)
            {
                winner = username;
            }
            else
            {
                winner = "Enemy";
            }
            Console.WriteLine("+------------------------------------------------------------------------------+");
            Console.WriteLine(string.Format("|    {0,-20}     {1,-17}        {2,-21}   |", $"Your Pokemon: {YHealth} HP", $"Username: {username} ", $"Enemy pokemon: {EHealth} HP"));
            Console.WriteLine("|                                                                              |");
            Console.WriteLine("|                             CONGRATULATIONS!                                 |");
            Console.WriteLine("|                                                                              |");
            Console.WriteLine("|                           {0,-23}                            |", $"{winner} is the winner!");
            Console.WriteLine("+------------------------------------------------------------------------------+" + "\n");

            if (winner == username)
            {
                for (int i = 0; i < 26; i++)
                {
                    Console.Write("-");
                    Thread.Sleep(50);
                }
                Console.WriteLine(" ");
                foreach (char c in EndLineType.EndLine1)
                {
                    Console.Write(c);
                    Thread.Sleep(60);
                }
                Console.WriteLine(" ");
                foreach (char c in EndLineType.EndLine2)
                {
                    Console.Write(c);
                    Thread.Sleep(60);
                }

            }
        }
    }
}