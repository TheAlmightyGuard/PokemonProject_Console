using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using PokemonGame.MainDes;
using PokemonGame.FunctionClasses.HealthManip;
using PokemonGame.informationClass;
using PokemonGame.Main;

namespace PokemonGame.FunctionClasses.InventoryMain
{
    class InventoryMain
    {
        public class invlist
        {
            public static string[] itemNames = new string[7];
            public static string[] itemNamesLOWER = new string[7];
            public static int[] IncreaseNum = new int[7];
            public static int[] LeftOver = new int[7];
        }

        public static bool firstInv = true;

        public static void ShowInventoryBox()
        {
            Console.Clear();
            if (firstInv == true)
            {

                GetInfo();
                MainDesigns.InventoryBox();
                string chosen = Console.ReadLine().ToLower();
                if (Array.IndexOf(invlist.itemNamesLOWER, chosen) >= 0)
                {
                    int arrnum = Array.IndexOf(invlist.itemNamesLOWER, chosen);
                    if (chosen.ToLower() == "revive")
                    {
                        if (invlist.LeftOver[arrnum] >= 1)
                        {
                            MainDesigns.PokemonLists();
                            Console.WriteLine("\n" + "Choose a pokemon to revive:");
                            ChooseToRevive(chosen.ToLower());
                        }
                    }
                    else
                    {
                        if (invlist.LeftOver[arrnum] >= 1)
                        {
                            if (pokeInformation.basicInfo.Health == 100)
                            {
                                Console.WriteLine("Your pokemon is already at full health!");
                                Console.ReadKey();
                            }
                            else
                            {
                                HealthManipulation.AddHealth("YHealth", invlist.IncreaseNum[arrnum]);
                                invlist.LeftOver[arrnum] -= 1;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No more {0} left!", invlist.itemNames[arrnum]);
                            Console.ReadKey();

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Item does not exist!");
                    Console.ReadKey();
                }

            }
            else
            {
                MainDesigns.InventoryBox();
                string chosen = Console.ReadLine();
                if (Array.IndexOf(invlist.itemNamesLOWER, chosen) >= 0)
                {
                    int arrnum = Array.IndexOf(invlist.itemNamesLOWER, chosen);
                    if (chosen.ToLower() == "revive")
                    {
                        if (invlist.LeftOver[arrnum] >= 1)
                        {
                            Console.Clear();
                            MainDesigns.PokemonLists();
                            Console.WriteLine("\n" + "Choose a pokemon to revive:");
                            string chosenPOKE = Console.ReadLine().ToLower();
                            ChooseToRevive(chosenPOKE);
                            invlist.LeftOver[arrnum] -= 1;

                        }
                    }
                    else if (invlist.LeftOver[arrnum] >= 1)
                    {
                        if (pokeInformation.basicInfo.Health == 100)
                        {
                            Console.WriteLine("Your pokemon is already at full health!");
                            Console.ReadKey();
                        }
                        else
                        {
                            HealthManipulation.AddHealth("YHealth", invlist.IncreaseNum[arrnum]);
                            invlist.LeftOver[arrnum] -= 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No more {0} left!", invlist.itemNames[arrnum]);
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Item does not exist!");
                    Console.ReadKey();
                }
            }
        }

        public static void ChooseToRevive(string chosen)
        {
            if (Array.IndexOf(pokeInformation.advInfo.pokemonLISTLOW, chosen) >= 0)
            {
                int arr = Array.IndexOf(pokeInformation.advInfo.pokemonLISTLOW, chosen);
                if (pokeInformation.advInfo.pokemonLIST_HEALTH[arr] == 0)
                {
                    pokeInformation.advInfo.pokemonLIST_HEALTH[arr] = 50;
                }
                else
                {
                    Console.WriteLine("\n" + "This pokemon doesn't need to be revived!");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Pokemon doesn't exist");
                Console.ReadKey();
            }
        }
        public static void GetInfo()
        {
            List<string> ItemName = new List<string>();
            List<int> ItemAdd = new List<int>();
            List<int> ItemUses = new List<int>();

            var fileData = System.Text.Encoding.UTF8.GetBytes((string)Properties.Resources.ResourceManager.GetObject("InventoryItems"));

            using (var memoryStream = new MemoryStream(fileData))
            using (var sr = new StreamReader(memoryStream))
            {

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var values = line.Split(',');

                    ItemName.Add(values[0].Trim());
                    ItemAdd.Add(Int32.Parse(values[1].Trim()));
                    ItemUses.Add(Int32.Parse(values[2].Trim()));
                }
            }

            string[] curr1 = new string[7];
            int[] curr2 = new int[7];
            int[] curr3 = new int[7];
            curr1 = ItemName.ToArray();
            curr2 = ItemAdd.ToArray();
            curr3 = ItemUses.ToArray();

            Random random = new Random();
            List<int> lastnum = new List<int>();

            List<string> chosen1 = new List<string>();
            List<string> chosenLOW = new List<string>();
            List<int> chosen2 = new List<int>();
            List<int> chosen3 = new List<int>();

            do
            {
                int arrayINT = random.Next(0, 6);
                if (lastnum.IndexOf(arrayINT) >= 0)
                {
                    continue;
                }
                else
                {
                    chosen1.Add(curr1[arrayINT]);
                    chosenLOW.Add(curr1[arrayINT].ToLower());
                    chosen2.Add(curr2[arrayINT]);
                    chosen3.Add(curr3[arrayINT]);
                    lastnum.Add(arrayINT);
                }
            } while (chosen1.Count < 4);

            invlist.itemNames = chosen1.ToArray();
            invlist.itemNamesLOWER = chosenLOW.ToArray();
            invlist.IncreaseNum = chosen2.ToArray();
            invlist.LeftOver = chosen3.ToArray();
            firstInv = false;
        }
    }
}
