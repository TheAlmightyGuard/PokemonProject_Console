using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.FunctionClasses
{
    public class Information
    {
        public int HP { get; set; }
        public int ATTACK { get; set; }
        public int DEFENSE { get; set; }
        public int Sp_Atk { get; set; }
        public int Sp_Def { get; set; }
        public int Speed { get; set; }

        public Information(int p1, int p2, int p3, int p4, int p5, int p6)
        {
            HP = p1;
            ATTACK = p2;
            DEFENSE = p3;
            Sp_Atk = p4;
            Sp_Def = p5;
            Speed = p6;
        }

        public Information()
        {
            this.ATTACK = 0;
            this.DEFENSE = 0; 
            this.Sp_Atk = 0;
            this.Sp_Def = 0;
            this.Speed = 0;
        }
    }
    public class PokemonInformationAPI
    {
        public static HttpClient client = new HttpClient();
        private static JObject damage_relations = new JObject();

        public static Information informationSlot = new Information();



        public static async Task<Information> grabInformationAsync(string pokemon) // 1 - NORMAL // 2 - HALF EFFECTIVE // 3 - DOUBLE EFFECTIVE
        {
            string URL = await client.GetStringAsync("http://pokeapi.co/api/v2/pokemon/" + pokemon.ToLower());
            JObject statsArray = JObject.Parse(JObject.Parse(URL).ToString());
            JArray stats = new JArray();

            stats = JArray.Parse(statsArray["stats"].ToString());

            int[] statBase = new int[6];

            int a = 0;
            foreach (JToken i in stats)
            {
                statBase[a] = Int32.Parse(i.Value<JToken>("base_stat").ToString());
                a++;
            }

            //foreach (int c in statBase)
            //{
            //    Console.WriteLine(c);    //CHECK IF WORKS CODE0
            //}

            Information n = new Information(statBase[0], statBase[1], statBase[2], statBase[3], statBase[4], statBase[5]);

            return n;

        }
    }
}
