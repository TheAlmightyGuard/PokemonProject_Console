using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PokemonGame.MainDes;
using PokemonGame.Main;
using PokemonGame.informationClass;
using System.Media;

namespace PokemonGame.FunctionClasses.PokemonChoice
{
    class PokemonChoiceMain
    {
        public static void ChoosePokemon()
        {
            Console.Clear();
            MainDes.MainDesigns.PokemonLists();
            string chosen = Console.ReadLine().ToLower().TrimEnd();

            if (Array.IndexOf(pokeInformation.advInfo.pokemonLISTLOW, chosen) >= 0 && pokeInformation.basicInfo.pokemon.ToLower() != chosen && !chosen.Contains("N/A"))
            {
                int arr = Array.IndexOf(pokeInformation.advInfo.pokemonLISTLOW, chosen);
                if (pokeInformation.advInfo.pokemonLIST_HEALTH[arr] > 0)
                {
                    string curr1;
                    int curr2;
                    curr1 = pokeInformation.basicInfo.pokemon;
                    curr2 = Convert.ToInt32(pokeInformation.basicInfo.Health);

                    pokeInformation.basicInfo.pokemon = pokeInformation.advInfo.pokemonLIST[arr];
                    pokeInformation.basicInfo.Health = pokeInformation.advInfo.pokemonLIST_HEALTH[arr];

                    pokeInformation.advInfo.pokemonLIST[arr] = curr1;
                    pokeInformation.advInfo.pokemonLISTLOW[arr] = curr1.ToLower();
                    pokeInformation.advInfo.pokemonLIST_HEALTH[arr] = curr2;

                    Console.Clear();
                    Console.WriteLine("{0}, I CHOOSE YOU!", pokeInformation.basicInfo.pokemon);
                    MusicPlayerC.CrySound();

                    Thread.Sleep(1200);
                }
                else
                {
                    Console.WriteLine("Pokemon is fainted!");
                    MusicPlayerC.CrySound();
                    Console.ReadKey();
                }
            }
            else if (pokeInformation.basicInfo.pokemon.ToLower() == chosen)
            {
                Console.WriteLine("You already have that pokemon out!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Pokemon doesn't exist");
                Console.ReadKey();
            }
        }


        public static void ChoosePokemonF()
        {
            do
            {

                Console.Clear();
                MainDesigns.PokemonLists();
                string chosen = Console.ReadLine().ToLower().TrimEnd();

                if (Array.IndexOf(pokeInformation.advInfo.pokemonLISTLOW, chosen) >= 0 && pokeInformation.basicInfo.pokemon.ToLower() != chosen)
                {
                    int arr = Array.IndexOf(pokeInformation.advInfo.pokemonLISTLOW, chosen);
                    if (pokeInformation.advInfo.pokemonLIST_HEALTH[arr] > 0)
                    {
                        string curr1;
                        int curr2;
                        curr1 = pokeInformation.basicInfo.pokemon;
                        curr2 = Convert.ToInt32(pokeInformation.basicInfo.Health);

                        pokeInformation.basicInfo.pokemon = pokeInformation.advInfo.pokemonLIST[arr];
                        pokeInformation.basicInfo.Health = pokeInformation.advInfo.pokemonLIST_HEALTH[arr];

                        pokeInformation.advInfo.pokemonLIST[arr] = curr1;
                        pokeInformation.advInfo.pokemonLISTLOW[arr] = curr1.ToLower();
                        pokeInformation.advInfo.pokemonLIST_HEALTH[arr] = curr2;

                        Console.Clear();
                        Console.WriteLine("{0}, I CHOOSE YOU!", pokeInformation.basicInfo.pokemon);
                        MusicPlayerC.CrySound();

                        Thread.Sleep(1200);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pokemon is fainted!");
                        MusicPlayerC.CrySound();
                        Console.ReadKey();
                        continue;
                    }
                }
                else if (pokeInformation.basicInfo.pokemon.ToLower() == chosen)
                {
                    Console.WriteLine("You already have that pokemon out!");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    Console.WriteLine("Pokemon doesn't exist");
                    Console.ReadKey();
                    continue;
                }
            } while (true);
        }
    }
}
