using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.Main;
using PokemonGame.informationClass;

namespace PokemonGame.FunctionClasses.HealthManip
{
    class HealthManipulation
    {
        public static void AddHealth(string n, double add)
        {
            if (n == "YHealth")
            {
                int change = Convert.ToInt32(pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH) + Convert.ToInt32(add);
                if (change > 100)
                    pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = 100;
                else
                {

                    if (change > 10)
                    {
                        MusicPlayerC.stopLow();
                        pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = change;
                    }
                    else
                    {
                        pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = change;
                    }
                }
                //Console.WriteLine("Your Pokemon gained {0} HP!", add);
            }
            else
            {
                int curr = Convert.ToInt32(pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH);
                if ((curr += Convert.ToInt32(add)) > 100)
                    pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH += add;
                else
                    pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH += add;
                // Console.WriteLine("The Enemy's Pokemon gained {0} HP!", add);
            }
        }

        public static void RemHealth(string n, double rem)
        {
            if (n == "YHealth")
            {
                int change = Convert.ToInt32(pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH) - Convert.ToInt32(rem);
                if (change <= 0)
                {
                    pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = 0;
                }
                else
                {
                    if (change <= 10)
                    {
                        if (MusicPlayerC.playingHealth == false)
                        {
                            MusicPlayerC.LowHealth();
                            pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = change;
                        }
                        else
                        {
                            pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = change;
                        }
                        
                    }
                    else
                    {
                        pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = change;
                    }
                }
            }
            else
            {
                int curr = Convert.ToInt32(pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH);
                if ((curr -= Convert.ToInt32(rem)) <= 0)
                    pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH = 0;
                else
                    pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH -= rem;
            }
        }
    }
}
