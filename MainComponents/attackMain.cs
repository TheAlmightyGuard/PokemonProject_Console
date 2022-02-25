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
            MongoFunctions.MoveSAMPLE MoveChosen = pokeInformation.basicInfo.currentPokemon.Pkmn_Moves.Find(x => x.MOVENAME.ToLower() == attackChosen.ToLower());

            int index = pokeInformation.basicInfo.currentPokemon.Pkmn_Moves.FindIndex(x => x.MOVENAME.ToLower() == attackChosen.ToLower());
            if (MoveChosen.CURR_USES >= 1)
            {
                bool sameType = false;
                if (MoveChosen.MOVETYPE == pokeInformation.basicInfo.currentPokemon.pokemon_TYPE)
                {
                    sameType = true;
                }
                else { sameType = false; }

                int damage = BattleEngine.DamageCalculation(MoveChosen.BASEPOWER, true, MoveChosen.ACCURACY_M, pokeInformation.basicInfo.currentPokemon.pokemon_TYPE, out int effective, out bool critcalB, out bool hit);
                HealthManipulation.RemHealth("EHealth", damage);

                BattleUI.attackTyping(true, attackChosen, effective, critcalB, damage, hit);
                pokeInformation.basicInfo.currentPokemon.Pkmn_Moves[index].CURR_USES -= 1;

                if (pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH > 0)
                {
                    botAttack();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + "No more uses left for the move '{0}'.", pokeInformation.basicInfo.currentPokemon.Pkmn_Moves[index].MOVENAME);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
        }

        public static void botAttack()
        {
            Random rndm = new Random();
            int num = rndm.Next(0, 10);
            HealthManipulation.RemHealth("YHealth", num);
        }
    }
}