using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using PokemonGame.informationClass;

namespace PokemonGame.MainComponents.AttackList
{
    class _attackLists
    {
        public static class variables
        {
            public static int lengthINT = 0;

            public static string[] NameListAR = new string[lengthINT];
            public static string[] POWERAR = new string[lengthINT];
            public static int[] ACCURACYAR = new int[lengthINT];

            public static string[] TYPEAR = new string[lengthINT];
            public static int[] MaxUsesAR = new int[lengthINT];
        }

        public static void Get4Moves()
        {
            //GetInfo();
            List<int> MaxUsesLIST = new List<int>();
            List<string> MoveNameLIST = new List<string>();
            List<string> TypeList = new List<string>();
            List<int> PowerList = new List<int>();
            List<int> AccList = new List<int>();

            Random random = new Random();
            Random random2 = new Random();

            List<int> lastnum = new List<int>();
            for (int i = 0; i <= 4; i++)
            {
                do
                {
                    int arrayINT = random.Next(0, variables.lengthINT);
                    if (lastnum.Contains(arrayINT))
                    {
                        continue;
                    }
                    else
                    {
                        MoveNameLIST.Add(variables.NameListAR[arrayINT]);
                        PowerList.Add(Int32.Parse(variables.POWERAR[arrayINT]));
                        AccList.Add(variables.ACCURACYAR[arrayINT]);
                        MaxUsesLIST.Add(variables.MaxUsesAR[arrayINT]);
                        TypeList.Add(variables.TYPEAR[arrayINT]);
                        lastnum.Add(arrayINT);
                        break;
                    }
                } while (true);
            }

            pokeInformation.advInfo.MOVENAMES = MoveNameLIST.ToArray();
            pokeInformation.advInfo.POWER = PowerList.ToArray();
            pokeInformation.advInfo.ACCURACY = AccList.ToArray();
            pokeInformation.advInfo.TYPE = TypeList.ToArray();

            pokeInformation.advInfo.MaxUsesAR = MaxUsesLIST.ToArray();
            pokeInformation.advInfo.CurrUsesAR = MaxUsesLIST.ToArray();
        }

        public static void GetInfo()
        {
            variables.lengthINT = getLength();
            List<string> NameList = new List<string>();
            List<string> PowerLIST = new List<string>();
            List<string> TypeMove = new List<string>();
            List<int> MaxUses = new List<int>();
            List<int> AccLIST = new List<int>();

            var fileData = System.Text.Encoding.UTF8.GetBytes((string)Properties.Resources.ResourceManager.GetObject(pokeInformation.basicInfo.pokemon));

            using (var memoryStream = new MemoryStream(fileData))
            using (var sr = new StreamReader(memoryStream))
            {

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var values = line.Split(',');

                    NameList.Add(values[0].Trim());
                    PowerLIST.Add(values[1].Trim());
                    MaxUses.Add(Int32.Parse(values[2].Trim()));
                    AccLIST.Add(Int32.Parse(values[3].Trim()));
                    TypeMove.Add(values[4].Trim());
                }
            }
            variables.NameListAR = NameList.ToArray();
            variables.POWERAR = PowerLIST.ToArray();
            variables.MaxUsesAR = MaxUses.ToArray();
            variables.ACCURACYAR = AccLIST.ToArray();
            variables.TYPEAR = TypeMove.ToArray();
        }

        public static int getLength()
        {
            var lineCount = 0;
            var fileData = System.Text.Encoding.UTF8.GetBytes((string)Properties.Resources.ResourceManager.GetObject(pokeInformation.basicInfo.pokemon));

            using (var memoryStream = new MemoryStream(fileData))
            using (var reader = new StreamReader(memoryStream))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }

            return lineCount;
        }
    }
}