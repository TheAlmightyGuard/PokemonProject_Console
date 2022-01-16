using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.informationClass;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace PokemonGame.FunctionClasses.BattleEngine
{
    class BattleEngine
    {
        public static bool critical = false;
        public static readonly Random rndm = new Random();

        public static HttpClient client = new HttpClient();
        private static JObject damage_relations = new JObject();

        public static async Task LoadCheck(string type)
        {
            string URL_ = "http://pokeapi.co/api/v2/type/" + type.ToLower();
            string URL = await client.GetStringAsync(URL_);
            damage_relations = JObject.Parse(JObject.Parse(URL)["damage_relations"].ToString());

            //Console.WriteLine(effectiveHitsPLAYER_DOUBLE[0].GetValue("name"));
            int i = checkTypeEffection("poop", damage_relations);

        }

        public static int checkTypeEffection(string type, JObject damage_OBJECT) // 1 - NORMAL // 2 - HALF EFFECTIVE // 3 - DOUBLE EFFECTIVE
        {
            JArray playerToEnemyDouble = new JArray();

            JArray playerToEnemyHalf = new JArray();

            JToken damage_relations = JToken.Parse(damage_OBJECT.ToString());

            playerToEnemyDouble = JArray.Parse(damage_relations["double_damage_to"].ToString());
            playerToEnemyHalf = JArray.Parse(damage_relations["half_damage_to"].ToString());

            int i = 0;
            if (playerToEnemyDouble.Children<JObject>().FirstOrDefault(x => x["name"].ToString() == type) != null)
            {
                i = 3;
            }
            else if (playerToEnemyHalf.Children<JObject>().FirstOrDefault(x => x["name"].ToString() == type) != null)
            {
                i = 2;
            }
            else
            {
                i = 1;
            }
            return i;
        }

        public static int Randomizer(int max)
        {
            int i = rndm.Next(0, max);
            return i;
        }
        public static int DamageCalculation(double basepw, bool effType, bool STAB, int acc)
        {
            if (HitOrMiss(acc) == true)
            {
                Console.WriteLine("Move missed!");
                return 0;
            }
            else
            {
                criticalCheck();
                double STABNum = 1;
                double levelMod = ((2 * userInformation.user_LVL) / 5) + 2;
                //double levelModF = levelMod + 3;
                double baseDamage = ((levelMod * basepw) / 50) + 2;
                if (STAB == true)
                    STABNum = 2;
                else
                    STABNum = 1.5;

                if (effType == true)
                {
                    if (critical == true)
                    {
                        Console.WriteLine("Critcal!");
                        return Int32.Parse(Math.Round(baseDamage * 2 * 3 * STABNum).ToString());
                    }
                    else
                        return Int32.Parse(Math.Round(baseDamage * 2 * STABNum).ToString());
                }
                else
                {
                    if (critical == true)
                    {

                        Console.WriteLine("Critcal!");
                        return Int32.Parse(Math.Round(baseDamage * 0.5 * 3 * STABNum).ToString());
                    }
                    else
                        return Int32.Parse(Math.Round(baseDamage * 0.5 * STABNum).ToString());
                }
            }
        }

        public static double AccuracyGenerator(double accMove, double accUser, double enemyInavsion)
        {
            double accModified = accMove * accUser * enemyInavsion;

            return accModified;
        }

        public static void criticalCheck()
        {
            int num1 = Randomizer(255);
            System.Threading.Thread.Sleep(1000);
            int num2 = Randomizer(255);

            if (num2 < num1)
                critical = true;
            else
                critical = false;
        }

        public static bool HitOrMiss(int move_accuracy)
        {
           // double calcaccuracy = pokeInformation.basicInfo.accuracy * pokeInformation.basicInfo.ENEMY_evasion; //GEN 1-2
            double calcaccuracy = pokeInformation.basicInfo.accuracy - pokeInformation.basicInfo.ENEMY_evasion; //GEN 3+

            double int_acc = move_accuracy * calcaccuracy;

            return (Odds(int_acc));

        }

        public static bool Odds(double num)
        {
            int rng = Randomizer(100);

            if (rng <= num)
                return true;
            else
                return false;
        }

    }
}
