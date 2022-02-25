using PokemonGame.PokemonInformationAPIC;
using System;
using System.Collections.Generic;

namespace PokemonGame.informationClass
{
    public class PokemonInfStruct
    {
        public string pokemonName { get; set; }
        public string pokemonName_LOWER { get; set; }
        public double pokemon_HEALTH { get; set; }
        public double pokemon_HEALTH_MAX { get; set; }
        public int pokemon_EXP { get; set; }
        public int pokemon_LVL { get; set; }
        public string pokemon_TYPE { get; set; }
        public int ATTACK { get; set; }
        public int DEFENSE { get; set; }
        public int Sp_Atk { get; set; }
        public int Sp_Def { get; set; }
        public int Evasion { get; set; }
        public int ACCURACY { get; set; }
        public int Speed { get; set; }
        public int HAPPINESS = 0;
        public int catch_Rate { get; set; }
        public List<MongoFunctions.MoveSAMPLE> Pkmn_Moves = new List<MongoFunctions.MoveSAMPLE>()
        {
            new MongoFunctions.MoveSAMPLE() { MOVENAME = "~~~~~~~~~~~~~~", ACCURACY_M = 0, CURR_USES = 0, BASEPOWER = 0, MAX_USES = 0, MOVETYPE = "GRASS" },
            new MongoFunctions.MoveSAMPLE() { MOVENAME = "~~~~~~~~~~~~~~", ACCURACY_M = 0, CURR_USES = 0, BASEPOWER = 0, MAX_USES = 0, MOVETYPE = "GRASS" },
            new MongoFunctions.MoveSAMPLE() { MOVENAME = "~~~~~~~~~~~~~~", ACCURACY_M = 0, CURR_USES = 0, BASEPOWER = 0, MAX_USES = 0, MOVETYPE = "GRASS" },
            new MongoFunctions.MoveSAMPLE() { MOVENAME = "~~~~~~~~~~~~~~", ACCURACY_M = 0, CURR_USES = 0, BASEPOWER = 0, MAX_USES = 0, MOVETYPE = "GRASS" }
        };

        public InformationEvolution EvolutionINFO { get; set; }
    }
    public class pokeInformation
    {
        public class basicInfo //NON-ARRAYS INFOS
        {
            public static PokemonInfStruct currentPokemon = new PokemonInfStruct();


            public static int pokemonLeft = 6;
            public static int accuracy = 100;

            public static int Turns = 0;

            public static string ENEMY_NAME = "Pikachu";


            //ENEMY
            public static PokemonInfStruct EnemyPokemon = new PokemonInfStruct()
            { 
                pokemonName = "Pikachu",
                pokemonName_LOWER = "pikachu",
                pokemon_HEALTH = 10,
                DEFENSE = 40,
                ATTACK = 55,
                Sp_Atk = 50,
                Speed = 90,
                Sp_Def = 50,
                ACCURACY = 100,
                Evasion = 2,
                pokemon_EXP = 1,
                pokemon_LVL = 1,
                pokemon_TYPE = "ELECTRIC"
            };

            public static int ENEMY_TYPENUM = 0;
            public static string ENEMY_TYPE = "";

        }

        public class advInfo //ARRAY INFOS
        {

        }
    }

    public class userInformation
    {
        public static string username = "Ryann";
        public static Guid user_ID;
        public static string password = "";

        public static int user_LVL = 0;
        public static int user_COINS = 0;

        public static bool firstTime = true;

        public static string[] ITEM_NAME = new string[4];
        public static int[] ITEM_AMOUNT = new int[4];
        public static int[] ITEM_HEALTH = new int[4];

        public static List<PokemonInfStruct> pokemonList = new List<PokemonInfStruct>()
        {
            new PokemonInfStruct(),
            new PokemonInfStruct(),
            new PokemonInfStruct(),
            new PokemonInfStruct(),
            new PokemonInfStruct(),
            new PokemonInfStruct()
        };

        public List<PokemonInfStruct> pokemonListTOSEND = new List<PokemonInfStruct>()
        {
            new PokemonInfStruct(),
            new PokemonInfStruct(),
            new PokemonInfStruct(),
            new PokemonInfStruct(),
            new PokemonInfStruct(),
            new PokemonInfStruct()
        };

        public static int pokemonTOTAL = 0;
    }

    public class publicInformations
    {
        public static string[] pokemonchoices = { "Bulbasaur", "Charmander", "Squirtle", "Chikorita", "Cyndaquil", "Totodile", "Treecko", "Torchic", "Mudkip", "Turtwig", "Chimchar", "Piplup", "Mewtwo", "Pikachu" };
        public static string[] LowerCase = new string[pokemonchoices.Length];

        public static string[] pokemonTypes = { "GRASS", "FIRE", "WATER", "GRASS", "FIRE", "WATER", "GRASS", "FIRE", "WATER", "GRASS", "FIRE", "WATER", "PSYCHIC", "ELECTRIC"};

        public static int choicesLength()
        {
            return pokemonchoices.Length;
        }
    }

    public class DEVELOPER_OPTIONS
    {
        public static bool DEVELOPER_ENABLED = true;
    }
}