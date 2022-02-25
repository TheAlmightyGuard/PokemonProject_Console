using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.informationClass;
using PokemonGame.PokemonInformationAPIC;

namespace PokemonGame.MainComponents
{
    class EnemyPokemonGen
    {

        public static int lengthLimit = 56;
        private static string link = "https://pokeapi.co/api/v2/pokemon?limit=" + lengthLimit;
        public static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        public static async Task<PokemonInfStruct> getEnemyAsync()
        {
            string URL = await client.GetStringAsync(link);
            Newtonsoft.Json.Linq.JArray arrayFound = Newtonsoft.Json.Linq.JArray.Parse(Newtonsoft.Json.Linq.JObject.Parse(URL)["results"].ToString());

            int rndm = new Random().Next(0,lengthLimit-1);
            Newtonsoft.Json.Linq.JObject enemyPokemon = Newtonsoft.Json.Linq.JObject.Parse(arrayFound[rndm].ToString());

            string URL_ = enemyPokemon.Value<string>("url");

            string linkPokemon = await client.GetStringAsync(URL_);

            string linkSpecies = await client.GetStringAsync(Newtonsoft.Json.Linq.JObject.Parse(linkPokemon)["species"].Value<string>("url"));

            Newtonsoft.Json.Linq.JArray stats = Newtonsoft.Json.Linq.JArray.Parse(Newtonsoft.Json.Linq.JObject.Parse(linkPokemon)["stats"].ToString());

            Newtonsoft.Json.Linq.JArray moves = Newtonsoft.Json.Linq.JArray.Parse(Newtonsoft.Json.Linq.JObject.Parse(linkPokemon)["moves"].ToString());

            Newtonsoft.Json.Linq.JArray types = Newtonsoft.Json.Linq.JArray.Parse(Newtonsoft.Json.Linq.JObject.Parse(linkPokemon)["types"].ToString());

            string pokemonName_L = Newtonsoft.Json.Linq.JObject.Parse(linkPokemon).Value<string>("name");
            string pokemonName = " ";

            pokemonName = pokemonName_L;

            pokemonName = firstCharCaps(pokemonName);

            List<MongoFunctions.MoveSAMPLE> Pkmn_Moves = new List<MongoFunctions.MoveSAMPLE>()
            {
                 new MongoFunctions.MoveSAMPLE() { MOVENAME = "~~~~~~~~~~~~~~", ACCURACY_M = 0, CURR_USES = 0, BASEPOWER = 0, MAX_USES = 0, MOVETYPE = "GRASS" },
                 new MongoFunctions.MoveSAMPLE() { MOVENAME = "~~~~~~~~~~~~~~", ACCURACY_M = 0, CURR_USES = 0, BASEPOWER = 0, MAX_USES = 0, MOVETYPE = "GRASS" },
                 new MongoFunctions.MoveSAMPLE() { MOVENAME = "~~~~~~~~~~~~~~", ACCURACY_M = 0, CURR_USES = 0, BASEPOWER = 0, MAX_USES = 0, MOVETYPE = "GRASS" },
                 new MongoFunctions.MoveSAMPLE() { MOVENAME = "~~~~~~~~~~~~~~", ACCURACY_M = 0, CURR_USES = 0, BASEPOWER = 0, MAX_USES = 0, MOVETYPE = "GRASS" }
            };


            List<int> usedNums = new List<int>();
            for (int a = 0; a < 4; a++)
            {
                int num = 0;
                do
                {
                    num = new Random().Next(0, moves.Count-1);

                    if (usedNums.Contains(num))
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                } while (true);
                string moveName = firstCharCaps(checkDashes(moves[num]["move"].Value<string>("name")));
                string linkMove = moves[num]["move"].Value<string>("url");

                string URL_MoveObject = await client.GetStringAsync(linkMove);
                Newtonsoft.Json.Linq.JObject moveObject = Newtonsoft.Json.Linq.JObject.Parse(URL_MoveObject);

                int Accuracy = checkNull(moveObject.Value<string>("accuracy"), 100);
                int PowerPoint = checkNull(moveObject.Value<string>("pp"), 20);
                int Power = checkNull(moveObject.Value<string>("power"), 100);

                string MoveType = firstCharCaps(moveObject["type"].Value<string>("name"));

                Pkmn_Moves[a] = new MongoFunctions.MoveSAMPLE()
                {
                    MOVENAME = moveName,
                    MAX_USES = PowerPoint,
                    CURR_USES = PowerPoint,
                    BASEPOWER = Power,
                    MOVETYPE = MoveType,
                    ACCURACY_M = Accuracy
                };
            } //END OF MOVESREAD

            double baseHP = Math.Floor(0.01 * (2 * stats[0].Value<int>("base_stat") * 1) + 1 + 25);

            InformationEvolution info = PokemonNextEvolutionAPI.grabNextEvolution(pokemonName_L).Result;

            Newtonsoft.Json.Linq.JObject speciesObject = Newtonsoft.Json.Linq.JObject.Parse(linkSpecies);
            int catch_rate = checkNull(speciesObject.Value<string>("catch_rate"), 100);

            PokemonInfStruct structBase = new PokemonInfStruct()
            {
                pokemonName = pokemonName,
                pokemonName_LOWER = pokemonName_L,
                pokemon_HEALTH = baseHP,
                pokemon_HEALTH_MAX = baseHP,
                DEFENSE = stats[2].Value<int>("base_stat"),
                ATTACK = stats[1].Value<int>("base_stat"),
                Sp_Atk = stats[3].Value<int>("base_stat"),
                Speed = stats[5].Value<int>("base_stat"),
                Sp_Def = stats[4].Value<int>("base_stat"),
                ACCURACY = 100,
                Evasion = 100,
                pokemon_EXP = 1,
                pokemon_LVL = 1,
                pokemon_TYPE = types[0]["type"].Value<string>("name").ToUpper(),
                Pkmn_Moves = Pkmn_Moves,
                catch_Rate = catch_rate,
                EvolutionINFO = info
            };

            return structBase;
        }

        public static string firstCharCaps(string text)
        {
            char[] nameChar = text.ToCharArray();

            nameChar[0] = char.ToUpper(nameChar[0]);

            return new string(nameChar);
        }

        public static int checkNull(string num, int changeTo)
        {
            if (String.IsNullOrEmpty(num))
            {
                return changeTo;
            }
            else
            {
                return Int32.Parse(num);
            }

        }

        public static string checkDashes(string text)
        {
            char[] nameChar = text.ToCharArray();

            if (nameChar.Contains('-'))
            {
                nameChar[Array.IndexOf(nameChar, '-')] = Convert.ToChar(" ");

                return new string(nameChar);
            }
            else
            {
                return text;
            }

        }
    }
}
