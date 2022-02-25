using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokemonGame.MainComponents
{
    class ActionFunctions
    {

        #region [Catch Rate Pokemon]

        #endregion

        public static void grabInitialMoves(string pokemon, out List<string> moveNames, out List<string> moveTypes, out List<int> powerMoves, out List<int> accuracyMoves, out List<int> maxUses)
        {
            var fileData = Encoding.UTF8.GetBytes((string)Properties.Resources.ResourceManager.GetObject(pokemon));

            List<string> foundLines = new List<string>();

            using (var memoryStream = new MemoryStream(fileData))
            using (var sr = new StreamReader(memoryStream))
            {

                while (!sr.EndOfStream)
                {
                    //var line = sr.ReadLine();
                    //var values = line.Split(',');
                    foundLines.Add(sr.ReadLine());
                }
            }

            List<string> chosenAttacks = new List<string>();
            List<int> completed = new List<int>();
            for (int i = 1; i <= 4; i++)
            {

                do
                {
                    Random rndm = new Random();
                    int num = rndm.Next(0, getLength(pokemon));
                    if (!completed.Contains(num))
                    {
                        chosenAttacks.Add(foundLines.ElementAt(num).ToString());
                        completed.Add(num);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                } while (true);
            }

            List<string> moveN = new List<string>();
            List<int> powerB = new List<int>();
            List<int> accuracyA = new List<int>();
            List<int> UsesM = new List<int>();
            List<string> MoveT = new List<string>();


            for (int i = 0; i < 4; i++)
            {
                string[] arrayChosen = chosenAttacks.ToArray();

                var splitedString = arrayChosen[i].Split(',');

                moveN.Add(splitedString[0]);
                powerB.Add(Int32.Parse(splitedString[1]));
                accuracyA.Add(Int32.Parse(splitedString[3]));
                UsesM.Add(Int32.Parse(splitedString[2]));
                MoveT.Add(splitedString[4]);
            }

            moveNames = moveN;
            powerMoves = powerB;
            accuracyMoves = accuracyA;
            maxUses = UsesM;
            moveTypes = MoveT;
        }

        public static int getLength(string pokemon)
        {
            var lineCount = 0;
            var fileData = Encoding.UTF8.GetBytes((string)Properties.Resources.ResourceManager.GetObject(pokemon));

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
