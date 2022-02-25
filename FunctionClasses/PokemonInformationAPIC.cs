using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.PokemonInformationAPIC
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

    public class MainMethodsCalculation
    {
        public static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        public static async Task<int> getNextLevelInformation(int lvl, string pokemon)
        {
            string URL_ = "http://pokeapi.co/api/v2/pokemon-species/" + pokemon.ToLower();
            string URL = await client.GetStringAsync(URL_);


            string URL_growth_rate = Newtonsoft.Json.Linq.JObject.Parse(Newtonsoft.Json.Linq.JObject.Parse(URL).ToString())["growth_rate"].Value<string>("url");
            string URL_growth = await client.GetStringAsync(URL_growth_rate);
            Newtonsoft.Json.Linq.JArray levelsArray = Newtonsoft.Json.Linq.JArray.Parse(Newtonsoft.Json.Linq.JObject.Parse(URL_growth)["levels"].ToString());

            Newtonsoft.Json.Linq.JObject selectedArr = Newtonsoft.Json.Linq.JObject.Parse(levelsArray[lvl].ToString());

            return selectedArr.Value<int>("experience");

        }

        public static async Task<int> getBaseExperience(string Enemypokemon)
        {
            string URL_ = "http://pokeapi.co/api/v2/pokemon/" + Enemypokemon.ToLower();
            string URL = await client.GetStringAsync(URL_);


            Newtonsoft.Json.Linq.JObject objectJSON = Newtonsoft.Json.Linq.JObject.Parse(URL.ToString());

            return objectJSON.Value<int>("base_experience");

        }

    }
    public class InformationEvolution
    {
        public string EVOLVE_TYPE { get; set;  }
        public int LEVEL_GOAL { get; set; }

        public int HAPPINESS_GOAL { get; set; }
        public string NEXT_EVOLUTION { get; set; }

        public string ITEM_EVOLVE { get; set; }
        public InformationEvolution(string evolve_type, int lvl, int happiness, string evolution, string item)
        {
            EVOLVE_TYPE = evolve_type;
            LEVEL_GOAL = lvl;
            HAPPINESS_GOAL = happiness;
            NEXT_EVOLUTION = evolution;
            ITEM_EVOLVE = item;
        }

        public InformationEvolution()
        {
            EVOLVE_TYPE = "N/A";
            LEVEL_GOAL = 0;
            HAPPINESS_GOAL = 0;
            NEXT_EVOLUTION = "N/A";
            ITEM_EVOLVE = "N/A";
        }
    }
    public class PokemonInformationAPI
    {
        public static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        private static Newtonsoft.Json.Linq.JObject damage_relations = new Newtonsoft.Json.Linq.JObject();

        public static Information informationSlot = new Information();



        public static async Task<Information> grabInformationAsync(string pokemon) // 1 - NORMAL // 2 - HALF EFFECTIVE // 3 - DOUBLE EFFECTIVE
        {
            string URL = await client.GetStringAsync("http://pokeapi.co/api/v2/pokemon/" + pokemon.ToLower());
            Newtonsoft.Json.Linq.JObject statsArray = Newtonsoft.Json.Linq.JObject.Parse(Newtonsoft.Json.Linq.JObject.Parse(URL).ToString());
            Newtonsoft.Json.Linq.JArray stats = new Newtonsoft.Json.Linq.JArray();

            stats = Newtonsoft.Json.Linq.JArray.Parse(statsArray["stats"].ToString());

            int[] statBase = new int[6];

            int a = 0;
            foreach (Newtonsoft.Json.Linq.JToken i in stats)
            {
                statBase[a] = Int32.Parse(i.Value<Newtonsoft.Json.Linq.JToken>("base_stat").ToString());
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
    public class PokemonNextEvolutionAPI
    {
        public static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();


        public static async Task<InformationEvolution> grabNextEvolution(string pokemon)
        {
            string URL_ = "http://pokeapi.co/api/v2/pokemon-species/" + pokemon.ToLower();
            string URL = await client.GetStringAsync(URL_);


            string URL_evolution_chain = Newtonsoft.Json.Linq.JObject.Parse(Newtonsoft.Json.Linq.JObject.Parse(URL).ToString())["evolution_chain"].Value<string>("url");
            string URL_evolution = await client.GetStringAsync(URL_evolution_chain);


            return grabNextEvolutionC(pokemon, Newtonsoft.Json.Linq.JObject.Parse(Newtonsoft.Json.Linq.JObject.Parse(URL_evolution)["chain"].ToString()));

        }

        public static InformationEvolution grabNextEvolutionC(string currPokemon, Newtonsoft.Json.Linq.JObject statsArray) // 1 - NORMAL // 2 - HALF EFFECTIVE // 3 - DOUBLE EFFECTIVE
        {
            InformationEvolution b = new InformationEvolution();
            int length = 0;

            int a = 0;
            while (a < 3)
            {
                if (a == 0)
                {
                    a = 1;
                }
                else if (a == 1)
                {
                    if (statsArray["evolves_to"] != null | statsArray["evolves_to"].ToArray().Length == 1)
                    {
                        length++;
                        a++;
                    }
                    else
                    {
                        length = 0;
                        a = 4;
                        break;
                    }
                }
                else if (a == 2)
                {
                    if (statsArray["evolves_to"][0]["evolves_to"] != null | statsArray["evolves_to"][0]["evolves_to"].ToArray().Length == 1)
                    {
                        length++;
                        a++;
                    }
                    else
                    {
                        length = 1;
                        a = 4;
                        break;
                    }
                }
            }
            if (statsArray["evolves_to"][0]["species"].Value<string>("name") != currPokemon.ToLower())
            {
                if (statsArray["evolves_to"][0]["evolution_details"][0]["trigger"].Value<string>("name") == "level-up")
                {
                    if (statsArray["evolves_to"][0]["evolution_details"][0]["min_level"].Type != Newtonsoft.Json.Linq.JTokenType.Null)
                    {
                        b = new InformationEvolution("level-up", statsArray["evolves_to"][0]["evolution_details"][0].Value<int>("min_level"), 0, statsArray["evolves_to"][0]["species"].Value<string>("name"), "N/A");
                    }
                    else if (statsArray["evolves_to"][0]["evolution_details"][0]["min_happiness"].Type != Newtonsoft.Json.Linq.JTokenType.Null)
                    {
                        b = new InformationEvolution("level-up", 0, statsArray["evolves_to"][0]["evolution_details"][0].Value<int>("min_happiness"), statsArray["evolves_to"][0]["species"].Value<string>("name"), "N/A");
                    }
                }
                else if (statsArray["evolves_to"][0]["evolution_details"][0]["trigger"].Value<string>("name") == "use-item")
                {
                    b = new InformationEvolution("use-item", 0, 0, statsArray["evolves_to"][0]["species"].Value<string>("name"), statsArray["evolves_to"][0]["evolution_details"][0]["item"].Value<string>("name"));
                }
            }

            else if (statsArray["evolves_to"][0]["species"].Value<string>("name") == currPokemon.ToLower() && statsArray["evolves_to"][0]["evolves_to"].Count() <= 0)
            {
                b = new InformationEvolution("N/A", 0, 0, "N/A", "N/A");
            }
            else if (statsArray["evolves_to"][0]["evolves_to"][0]["species"].Value<string>("name") != currPokemon.ToLower())
            {
                if (statsArray["evolves_to"][0]["evolves_to"][0]["evolution_details"][0]["trigger"].Value<string>("name") == "level-up")
                {
                    if (statsArray["evolves_to"][0]["evolves_to"][0]["evolution_details"][0]["min_level"].Type != Newtonsoft.Json.Linq.JTokenType.Null)
                    {
                        b = new InformationEvolution("level-up", statsArray["evolves_to"][0]["evolves_to"][0]["evolution_details"][0].Value<int>("min_level"), 0, statsArray["evolves_to"][0]["evolves_to"][0]["species"].Value<string>("name"), "N/A");
                    }
                    else if (statsArray["evolves_to"][0]["evolves_to"][0]["evolution_details"][0]["min_happiness"].Type != Newtonsoft.Json.Linq.JTokenType.Null)
                    {
                        b = new InformationEvolution("level-up", 0, statsArray["evolves_to"][0]["evolves_to"][0]["evolution_details"][0].Value<int>("min_happiness"), statsArray["evolves_to"][0]["evolves_to"][0]["species"].Value<string>("name"), "N/A");
                    }
                }
                else if (statsArray["evolves_to"][0]["evolves_to"][0]["evolution_details"][0]["trigger"].Value<string>("name") == "use-item")
                {
                    b = new InformationEvolution("use-item", 0, 0, statsArray["evolves_to"][0]["evolves_to"][0]["species"].Value<string>("name"), statsArray["evolves_to"][0]["evolves_to"][0]["evolution_details"][0]["item"].Value<string>("name"));
                }

            }

            return b;
        }

    }

    public class ItemGrabAPI
    {
        public class ItemINFO
        {
            public string ITEM_NAME { get; set; }
            public int ITEM_PRICE { get; set; }
            public string ITEM_HEALTH { get; set; }
            public string ITEM_DESCRIPTION { get; set; }
        }

        public static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();


        public static async Task<ItemINFO> grabItemINFO(string item)
        {
            ItemINFO infoItem = new ItemINFO();
            string newName = addDash(item.ToLower());
            string URL_ = "http://pokeapi.co/api/v2/item/" + newName;
            string URL = await client.GetStringAsync(URL_);


            if (newName.Contains("potion") && newName != "max-potion")
            {
                Newtonsoft.Json.Linq.JObject ObjectM = Newtonsoft.Json.Linq.JObject.Parse(URL);
                char[] number = new char[5];
                int charInt = 0;
                Newtonsoft.Json.Linq.JObject M = ObjectM["effect_entries"][0].ToObject<Newtonsoft.Json.Linq.JObject>();

                foreach (char b in M.Value<string>("short_effect"))
                {
                    int res = 0;
                    if (Int32.TryParse(b.ToString(), out res))
                    {
                        number[charInt] = Char.Parse(res.ToString());
                    }
                    else
                    {

                    }
                }


                infoItem = new ItemINFO() { ITEM_NAME = item, ITEM_HEALTH = new string(number), ITEM_DESCRIPTION = ObjectM["effect_entries"][0].Value<string>("short_effect"), ITEM_PRICE = ObjectM.Value<int>("cost") };
            }
            else if (newName == "max-potion")
            {
                infoItem = new ItemINFO() { ITEM_NAME = item, ITEM_HEALTH = "???", ITEM_DESCRIPTION = "Restores HP to full health", ITEM_PRICE = 2500 };
            }

            return infoItem;
        }

        public static string addDash(string text)
        {
            char[] nameChar = text.ToCharArray();
            char[] doneChar = new char[nameChar.Length];

            int a = 0;
            foreach(char i in nameChar)
            {
                if (i.ToString() != " ")
                {
                    doneChar[a] = i;
                }
                else if (i.ToString() == " ")
                {
                    doneChar[a] = Char.Parse("-");
                }
                a++;
            }

            return new string(doneChar);
        }

    }
}
