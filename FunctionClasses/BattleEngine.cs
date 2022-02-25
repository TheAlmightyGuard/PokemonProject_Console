using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.informationClass;

namespace PokemonGame.FunctionClasses.BattleEngine
{
    class BattleEngine
    {
        public static double critical = 0;
        public static readonly Random rndm = new Random();

        public static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        private static Newtonsoft.Json.Linq.JObject damage_relations = new Newtonsoft.Json.Linq.JObject();

        public static int effNum = 0;

        public static async Task<int> LoadCheck(string type)
        {
            string URL_ = "http://pokeapi.co/api/v2/type/" + type.ToLower();
            string URL = await client.GetStringAsync(URL_);
            damage_relations = Newtonsoft.Json.Linq.JObject.Parse(Newtonsoft.Json.Linq.JObject.Parse(URL)["damage_relations"].ToString());

            //Console.WriteLine(effectiveHitsPLAYER_DOUBLE[0].GetValue("name"));
            return checkTypeEffection(type, damage_relations);

        }

        public static int checkTypeEffection(string type, Newtonsoft.Json.Linq.JObject damage_OBJECT) // 1 - NORMAL // 2 - HALF EFFECTIVE // 3 - DOUBLE EFFECTIVE
        {
            Newtonsoft.Json.Linq.JArray playerToEnemyDouble = new Newtonsoft.Json.Linq.JArray();

            Newtonsoft.Json.Linq.JArray playerToEnemyHalf = new Newtonsoft.Json.Linq.JArray();

            Newtonsoft.Json.Linq.JToken damage_relations = Newtonsoft.Json.Linq.JToken.Parse(damage_OBJECT.ToString());

            playerToEnemyDouble = Newtonsoft.Json.Linq.JArray.Parse(damage_relations["double_damage_to"].ToString());
            playerToEnemyHalf = Newtonsoft.Json.Linq.JArray.Parse(damage_relations["half_damage_to"].ToString());

            int i = 0;
            if (playerToEnemyDouble.Children<Newtonsoft.Json.Linq.JObject>().FirstOrDefault(x => x["name"].ToString() == type) != null)
            {
                i = 3;
            }
            else if (playerToEnemyHalf.Children<Newtonsoft.Json.Linq.JObject>().FirstOrDefault(x => x["name"].ToString() == type) != null)
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

        public static int gainExperience()
        {
            double wildOrTrainer = 0;
            if (pokeInformation.basicInfo.ENEMY_TYPENUM == 1) //WILD
            {
                wildOrTrainer = 1;
            }
            else
            {
                wildOrTrainer = 1.5;
            }

            double base_EXP = PokemonInformationAPIC.MainMethodsCalculation.getBaseExperience(pokeInformation.basicInfo.currentPokemon.pokemonName).Result;


            double gainEXP = wildOrTrainer * 1 * base_EXP * 1 * pokeInformation.basicInfo.currentPokemon.pokemon_LVL * 1 * 1 * 1;

            double finalCalculation = gainEXP / (7 * 1);

            return Int32.Parse(Math.Round(finalCalculation).ToString());


        }
        public static int DamageCalculation(double basepw, bool STAB, int acc, string type, out int eff, out bool criticalB, out bool hit)
        {
            if (HitOrMiss(acc) == true)
            {
                eff = 0;
                criticalB = false;
                hit = false;
                return 0;
            }
            else
            {
                hit = true;
                criticalCheck();
                double STABNum = 1;
                double calc1 = (((2 * userInformation.user_LVL) / 5) + 2);

                //double attackDefense = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(pokeInformation.basicInfo.currentPokemon.ATTACK / pokeInformation.basicInfo.currentPokemon.DEFENSE)));
                double calc2 = calc1 * basepw * 1;
                double calc3 = calc2 / 50;

                decimal rndmN = Decimal.Round(new Random().Next(217, 255) / 255m, 1);

                if (critical > 1)
                {
                    criticalB = true;
                }
                else
                {
                    criticalB = false;
                }
                double calc4 = calc3 * critical * Convert.ToDouble(rndmN);

                if (STAB == true)
                    STABNum = 2;
                else
                    STABNum = 1.5;

                int effN = LoadCheck(type).Result;

                eff = effN;

                double calc5 = Math.Round(calc4 * STABNum * effN);

                return Convert.ToInt32(Math.Round(calc5));


                ////double levelModF = levelMod + 3;
                //double baseDamage = (levelMod * attackDefense) + 2;
                //if (STAB == true)
                //    STABNum = 2;
                //else
                //    STABNum = 1.5;

               

                //criticalB = critical;
                //hit = true;
                //if (effNum == 3)
                //{
                //    if (critical == true)
                //    {
                //        return Int32.Parse(Math.Round(baseDamage * 2 * 3 * STABNum).ToString());
                //    }
                //    else
                //        return Int32.Parse(Math.Round(baseDamage * 2 * STABNum).ToString());
                //}
                //else if (effNum == 2)
                //{
                //    if (critical == true)
                //    {
                //        return Int32.Parse(Math.Round(baseDamage * 0.5 * 3 * STABNum).ToString());
                //    }
                //    else
                //        return Int32.Parse(Math.Round(baseDamage * 0.5 * STABNum).ToString());
                //}
                //else
                //{
                //    if (critical == true)
                //    {
                //        return Int32.Parse(Math.Round(baseDamage * 1 * 3 * STABNum).ToString());
                //    }
                //    else
                //        return Int32.Parse(Math.Round(baseDamage * 1 * STABNum).ToString());
                //}
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
                critical = 2;
            else
                critical = 1;
        }

        public static bool HitOrMiss(int move_accuracy)
        {
             double calcaccuracy = pokeInformation.basicInfo.accuracy * pokeInformation.basicInfo.EnemyPokemon.Evasion; //GEN 1-2
           //double calcaccuracy = pokeInformation.basicInfo.accuracy - pokeInformation.basicInfo.currentPokemon.Evasion; //GEN 3+

            double int_acc = move_accuracy * calcaccuracy;

            return (Odds(int_acc));

        }

        public static bool Odds(double num)
        {
            int rng = Randomizer(9999);

            if (rng <= num)
                return false;
            else
                return true;
        }

    }
}
