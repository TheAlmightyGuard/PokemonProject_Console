using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.MainComponents.AttackList;
using PokemonGame.informationClass;
using PokemonGame.FunctionClasses.HealthManip;
using PokemonGame.FunctionClasses.BattleEngine;

//using PokemonGame.Main;

namespace PokemonGame.MainComponents.attackMain
{
    class _attackMain
    {
        public static void Attack(string attackChosen) //FYI , THIS METHOD RUNS WHEN CHOSEN ATTACK EXISTS!
        {

            string[] loweredList = new string[pokeInformation.advInfo.MOVENAMES.Length];
            for (int i = 0; i < loweredList.Length; i++)
            {
                loweredList[i] = pokeInformation.advInfo.MOVENAMES[i].ToLower().TrimEnd();
            }
            int arrayNum = Array.IndexOf(loweredList, attackChosen.ToLower().TrimEnd());

            if (pokeInformation.advInfo.CurrUsesAR[arrayNum] >= 1)
            {
                Random eff = new Random();
                int effchosen = eff.Next(0, 7);
                if (effchosen >= 0 && effchosen <= 4)
                {
                    bool sameType = false;
                    if (pokeInformation.advInfo.TYPE[arrayNum] == pokeInformation.basicInfo.CurrpokemonType)
                    {
                        sameType = true;
                    }
                    else { sameType = false; }
                    int damage = BattleEngine.DamageCalculation(pokeInformation.advInfo.POWER[arrayNum], sameType, true, pokeInformation.advInfo.ACCURACY[arrayNum]);
                    HealthManipulation.RemHealth("EHealth", damage);

                    pokeInformation.advInfo.CurrUsesAR[arrayNum] -= 1;
                    Console.ReadKey();
                    botAttack();
                }
                else
                {
                    bool sameType2 = false;
                    if ((pokeInformation.advInfo.TYPE[arrayNum] == pokeInformation.basicInfo.CurrpokemonType))
                    {
                        sameType2 = true;
                    }
                    else { sameType2 = false; }
                    int damage = BattleEngine.DamageCalculation(pokeInformation.advInfo.POWER[arrayNum], sameType2, true, pokeInformation.advInfo.ACCURACY[arrayNum]);
                    HealthManipulation.RemHealth("EHealth", damage);

                    Console.WriteLine($"\nIt was not super effective! (-{damage} HP)");

                    pokeInformation.advInfo.CurrUsesAR[arrayNum] -= 1;
                    Console.ReadKey();
                    botAttack();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + "No more uses left for the move '{0}'.", pokeInformation.advInfo.MOVENAMES[arrayNum]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
        }

        public static void botAttack()
        {
            Random rndm = new Random();
            int num = rndm.Next(0, 20);
            HealthManipulation.RemHealth("YHealth", num);
        }
    }
}