using System;
using System.Collections.Generic;
using PokemonGame.FunctionClasses.MusicPlayer;

namespace PokemonGame.informationClass
{
    public class pokeInformation
    {
        public class basicInfo //NON-ARRAYS INFOS
        {
            public static int levelPokemon = 1;
            public static string pokemon = "Charizard";
            public static string CurrpokemonType = "";
            public static int pokemon_EXP = 1;


            public static double Health = 0;
            public static int ATTACK = 1;
            public static int DEFENSE = 1;
            public static int Sp_Atk = 1;
            public static int Sp_Def = 1;
            public static int Speed = 1;






            public static List<MongoFunctions.MoveSAMPLE> CurrMoves = new List<MongoFunctions.MoveSAMPLE>()
            {
                new MongoFunctions.MoveSAMPLE(),
                new MongoFunctions.MoveSAMPLE(),
                new MongoFunctions.MoveSAMPLE(),
                new MongoFunctions.MoveSAMPLE()
            };

            public static int pokemonC = 5;
            public static int accuracy = 100;

            public static int Turns = 0;

            public static string ENEMY_NAME = "";


            //ENEMY
            public static int ENEMY_levelPokemon = 1;
            public static string ENEMY_pokemon = " ";
            public static string ENEMY_CurrpokemonType = "";
            public static int ENEMY_accuracy = 100;
            public static int ENEMY_evasion = 100;

            public static int ENEMY_power = 100;
            public static int ENEMY_defense = 100;

            public static string ENEMY_TYPE = "";
            public static int ENEMY_TYPENUM = 0;


            public static double ENEMY_Health = 100;

            public void changeAllHealth(int healthNum)
            {
                Health = healthNum;
                ENEMY_Health = healthNum;
            }

            public void entryChanges(string namePoke, string type)
            {
                pokemon = namePoke;
                CurrpokemonType = type;
            }

        }

        public class PokemonMovesLIST
        {
            public List<MongoFunctions.MoveSAMPLE> PokemonMoves = new List<MongoFunctions.MoveSAMPLE>()
            {
                new MongoFunctions.MoveSAMPLE(),
                new MongoFunctions.MoveSAMPLE(),
                new MongoFunctions.MoveSAMPLE(),
                new MongoFunctions.MoveSAMPLE()
            };
        }

        public class advInfo //ARRAY INFOS
        {
            //LIST OF POKEMON AVAILABLE
            public static string[] pokemonLIST = new string[4]; //NAME UPPER
            public static string[] pokemonLISTLOW = new string[pokemonLIST.Length]; //NAME LOWER
            public static int[] pokemonLIST_HEALTH = new int[4]; //POKEMON HEALTH
            public static int[] pokemonLIST_EXP = new int[4]; //POKEMON EXP
            public static int[] pokemonLIST_LVL = new int[4]; //POKEMON LVL
            public static string[] pokemonLIST_TYPE = new string[4]; //POKEMON TYPE


            //ATTACK /\ MOVES VARIABLES
            public static string[] MOVENAMES = new string[4];
            public static string[] MOVENAMES_LOWER = new string[4];

            public static string[] TYPE = new string[4];
            public static int[] POWER = new int[4];
            public static int[] ACCURACY = new int[4];
            public static int[] MaxUsesAR = new int[4];

            public static int[] CurrUsesAR = new int[4];



            // ^^CURRENTS || UNDER IS RESERVED
            public static List<MongoFunctions.MoveSAMPLE> reservedList = new List<MongoFunctions.MoveSAMPLE>();

            public static void loadAllMoves(object[] party)
            {
                int i = 0; //0 - HASNT LOADED FIRST POKEMON || 4 - LOADED FIRST POKEMON
                foreach (MongoFunctions.PartySAMPLE poke in party)
                {
                    if (i < 4)
                    {
                        foreach (MongoFunctions.MoveSAMPLE move in poke.Pkmn_MOVES)
                        {

                            MOVENAMES[i] = move.MOVENAME;
                            MOVENAMES_LOWER[i] = move.MOVENAME.ToLower();
                            TYPE[i] = move.MOVETYPE;
                            POWER[i] = move.BASEPOWER;
                            CurrUsesAR[i] = move.CURR_USES;
                            ACCURACY[i] = move.ACCURACY_M;
                            MaxUsesAR[i] = move.MAX_USES;
                            i++;
                        }

                    }
                    else
                    {
                        List<MongoFunctions.MoveSAMPLE> moves = poke.Pkmn_MOVES;
                        moves.RemoveRange(0, 0);

                        foreach (MongoFunctions.MoveSAMPLE move in moves)
                        {

                            reservedList.Add(new MongoFunctions.MoveSAMPLE()
                            {
                                MOVENAME = move.MOVENAME,
                                MOVETYPE = move.MOVETYPE,
                                BASEPOWER = move.BASEPOWER,
                                ACCURACY_M = move.ACCURACY_M,
                                CURR_USES = move.CURR_USES,
                                MAX_USES = move.MAX_USES
                            });
                        }
                    }
                }
            }

            public static void storeAllMoves(out List<MongoFunctions.PartySAMPLE> partyList)
            {

                List<MongoFunctions.PartySAMPLE> partyListAdding = new List<MongoFunctions.PartySAMPLE>();

                for (int i = 1; i <= 5; i++)
                {
                    if (i == 1)
                    {
                        partyListAdding.Add(new MongoFunctions.PartySAMPLE()
                        {
                            Pkmn_NAME = basicInfo.pokemon,
                            LVL = basicInfo.levelPokemon,
                            EXP = basicInfo.pokemon_EXP,
                            HEALTH = basicInfo.Health,
                            TYPE = basicInfo.CurrpokemonType,
                            Pkmn_MOVES = basicInfo.CurrMoves
                        });
                    }
                    else
                    {
                        partyListAdding.Add(new MongoFunctions.PartySAMPLE()
                        {
                            Pkmn_NAME = "WORKS",
                            LVL = pokemonLIST_LVL[i - 2],
                            EXP = pokemonLIST_EXP[i - 2],
                            HEALTH = pokemonLIST_HEALTH[i - 2],
                            TYPE = pokemonLIST_TYPE[i - 2],
                            Pkmn_MOVES = reservedList.GetRange(i - 2, i - 2)
                        });
                    }

                }

                partyList = partyListAdding;
            }
        }
    }

    public class userInformation
    {
        public static string username = "Ryann";
        public static string user_ID = "";
        public static string password = "";

        public static int user_LVL = 0;
        public static int user_COINS = 0;

        public static bool firstTime = true;

        public static string[] ITEM_NAME = new string[4];
        public static int[] ITEM_AMOUNT = new int[4];
        public static int[] ITEM_HEALTH = new int[4];
    }

    public class publicInformations
    {
        public static string[] pokemonchoices = { "Charizard", "Pikachu", "Onix", "Gyarados", "Gardevoir", "Lucario", "Dragonite", "Snorlax", "Mewtwo" };
        public static string[] LowerCase = new string[pokemonchoices.Length];

        public static string[] pokemonTypes = { "FIRE", "ELECTRIC", "ROCK", "WATER", "PSYCHIC", "FIGHTING", "DRAGON", "NORMAL", "PSYCHIC" };

        public static string[] choices()
        {
            return (string[])pokemonchoices.Clone();
        }

        public static string[] choicesLower()
        {
            return (string[])LowerCase.Clone();
        }

        public static int choicesLength()
        {
            return pokemonchoices.Length;
        }

        public static string[] typesPokemon()
        {
            return (string[])pokemonTypes.Clone();
        }
    }

    public class DEVELOPER_OPTIONS
    {
        public static bool DEVELOPER_ENABLED = false;
    }
}