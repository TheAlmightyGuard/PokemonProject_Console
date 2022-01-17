using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.FunctionClasses.InventoryMain;
using PokemonGame.Main;
using PokemonGame.informationClass;

namespace PokemonGame.MainDes
{
    class MainDesigns
    {
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

        public static void InventoryBox()
        {
            Console.WriteLine("+------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("|                                                                                                            |");
            Console.WriteLine(string.Format("|          {0,-20}                 {1,-19}         {2,-21}          |", $"Your Pokemon: {pokeInformation.basicInfo.Health} HP", $"Username: {userInformation.username} ", $"Enemy pokemon: {pokeInformation.basicInfo.ENEMY_Health} HP"));
            Console.WriteLine("|                                                                                                            |");
            Console.WriteLine(string.Format("|                                             Your pokemon: {0,-10}                                       |", pokeInformation.basicInfo.pokemon));
            Console.WriteLine("|                                                                                                            |");
            Console.WriteLine("|                                               Choose your item!                                            |");
            Console.WriteLine("|                                                                                                            |");
            Console.WriteLine(string.Format("|      {0,-12} {1,-7} || {2,-12} {3,-7} || {4,-12} {5,-7} || {6,-12} {7,-7}          |",
                                            InventoryMain.invlist.itemNames[0],
                                           $"[{InventoryMain.invlist.LeftOver[0]}/1]",
                                            InventoryMain.invlist.itemNames[1],
                                            $"[{InventoryMain.invlist.LeftOver[1]}/1]",
                                           InventoryMain.invlist.itemNames[2],
                                            $"[{InventoryMain.invlist.LeftOver[2]}/1]",
                                           InventoryMain.invlist.itemNames[3],
                                           $"[{InventoryMain.invlist.LeftOver[3]}/1]"));
            Console.WriteLine("|                                                                                                            |");
            Console.WriteLine("+------------------------------------------------------------------------------------------------------------+");
            Console.ReadKey();
        }

        public static void PokemonLists()
        {
            string[] arr = pokeInformation.advInfo.pokemonLIST;
            int[] arr2 = pokeInformation.advInfo.pokemonLIST_HEALTH;
            Console.WriteLine("+----------------------------------------------------------------------------+");
            Console.WriteLine("|                                  POKEMON LIST                              |");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("|      1. {0,-10} {1,-8}                  2. {2,-10} {3,-8}        |", pokeInformation.basicInfo.pokemon, $"[{pokeInformation.basicInfo.Health} HP]", arr[0], $"[{arr2[0]} HP]");
            Console.WriteLine("|      3. {0,-10} {1,-8}                  4. {2,-10} {3,-8}        |", arr[1], $"[{arr2[1]} HP]", arr[2], $"[{arr2[2]} HP]");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("|                            5. {0,-10} {1,-8}                          |", arr[3], $"[{arr2[3]} HP]");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("+----------------------------------------------------------------------------+");
        }
    }
}
