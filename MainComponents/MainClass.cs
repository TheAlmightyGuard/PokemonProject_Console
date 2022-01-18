using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.MainComponents.actionBoxes;
using PokemonGame.MainComponents.AttackList;
using PokemonGame.MainComponents.attackMain;
using PokemonGame.Main;
using PokemonGame.FunctionClasses.InventoryMain;
using PokemonGame.FunctionClasses.HealthManip;
using PokemonGame.FunctionClasses.PokemonChoice;
using System.Threading;
using PokemonGame.informationClass;

namespace PokemonGame.MainComponents.Main
{
    public class MainChar
    {
        public static bool firstMove = true;
        public static void MainMethod()
        {
            Random rndm = new Random();

            int num = rndm.Next(1, 100);
            if (num >= 1 && num <= 50)
            {
                pokeInformation.basicInfo.ENEMY_TYPE = "[WILD POKEMON!]";
                pokeInformation.basicInfo.ENEMY_TYPENUM = 1;
            }
            else
            {
                pokeInformation.basicInfo.ENEMY_TYPE = "[?? TRAINER ??]";
                pokeInformation.basicInfo.ENEMY_TYPENUM = 2;
            }

            MusicPlayerC.BattleMusic();
            BattleUI.BattleGUI();
            do //WHILE 1 OR MORE THAN 1 POKEMON EXISTS
            {
                _attackLists.GetInfo();
                do //WHILE HEALTH IS OVER 0
                {
                    //ActionBoxes.mainBox(userInformation.username, pokeInformation.basicInfo.Health, pokeInformation.basicInfo.ENEMY_Health, pokeInformation.basicInfo.ENEMY_TYPE);
                    int i = BattleUI.ChooseAction();
                    if (i == 0)
                    {
                        MusicPlayerC.ButtonClick();
                        //ActionBoxes.attackBox(userInformation.username, pokeInformation.basicInfo.pokemon, Convert.ToInt32(pokeInformation.basicInfo.Health), Convert.ToInt32(pokeInformation.basicInfo.ENEMY_Health), firstMove);

                        BattleUI.AttackBox();
                        string chosen = Console.ReadLine();
                        
                        if (Array.IndexOf(pokeInformation.advInfo.MOVENAMES_LOWER, chosen.ToLower().TrimEnd()) >= 0)
                        {
                            _attackMain.Attack(chosen);
                        }
                        else
                        {
                            Console.WriteLine("Move is not option!");
                            Console.ReadKey();
                        }
                    }
                    else if (i == 3)
                    {
                        MusicPlayerC.ButtonClick();
                        HealthManipulation.RemHealth("YHealth", pokeInformation.basicInfo.Health);
                        Console.WriteLine($"{userInformation.username} took damage from the enemy! It was a critical hit!");
                        Console.ReadKey();
                    }
                    else if (i == 1)
                    {
                        MusicPlayerC.ButtonClick();
                        BattleUI.InventoryBox();
                    }
                    else if (i == 2)
                    {
                        MusicPlayerC.ButtonClick();
                        Console.Clear();
                        PokemonChoiceMain.ChoosePokemon();
                        Console.ReadKey();
                    }
                } while (pokeInformation.basicInfo.ENEMY_Health > 0 && pokeInformation.basicInfo.Health > 0);
                if (pokeInformation.basicInfo.pokemonC >= 1 && pokeInformation.basicInfo.ENEMY_Health > 0)
                {
                    pokeInformation.basicInfo.pokemonC -= 1;
                    Console.WriteLine("Your pokemon fainted! Choose your next pokemon..");
                    Console.ReadKey();
                    PokemonChoiceMain.ChoosePokemonF();
                    continue;
                }
                else
                    break;
            } while (pokeInformation.basicInfo.pokemonC >= 1 && pokeInformation.basicInfo.ENEMY_Health > 0);

            Console.Clear();
            if (pokeInformation.basicInfo.ENEMY_Health <= 0)
            {
                ProgramMain.variables.YWinner = true;
                EndMusic();
                ActionBoxes.results(userInformation.username, ProgramMain.variables.YWinner, Convert.ToInt32(pokeInformation.basicInfo.Health), Convert.ToInt32(pokeInformation.basicInfo.ENEMY_Health));
                ConsoleKeyInfo input;
                do
                {
                    Console.WriteLine("\n" + "Press E to exit.");
                    input = Console.ReadKey(true);
                } while (input.Key != ConsoleKey.E);

            }
            else
            {
                ProgramMain.variables.YWinner = false;
                ActionBoxes.results(userInformation.username, ProgramMain.variables.YWinner, Convert.ToInt32(pokeInformation.basicInfo.Health), Convert.ToInt32(pokeInformation.basicInfo.ENEMY_Health));
            }
            Thread.Sleep(3000);
            Console.ReadKey();
            Console.ReadKey();
        }

        public static void EndMusic()
        {

            Thread.Sleep(1000);
            MusicPlayerC.VictoryMusic();
        }
    }
}
