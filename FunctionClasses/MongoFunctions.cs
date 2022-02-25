using System;
using System.Collections.Generic;
using System.Linq;
using PokemonGame.FunctionClasses;
using System.Threading.Tasks;
using DnsClient;

using PokemonGame.informationClass;
using PokemonGame.PokemonInformationAPIC;

namespace PokemonGame
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class MongoFunctions
    {
        public static MongoClass db = new MongoClass("MainDatabase");

        public static void saveAllInformation()
        {
            List<ItemSAMPLE> NEWBackpack = new List<ItemSAMPLE>()
            {
                new ItemSAMPLE(),
                new ItemSAMPLE(),
                new ItemSAMPLE(),
                new ItemSAMPLE(),
            };

            for (int a = 0; a < 4; a++)
            {
                NEWBackpack[a].ITEM_NAME = userInformation.ITEM_NAME[a];
                NEWBackpack[a].AMOUNT_LEFT = userInformation.ITEM_AMOUNT[a];
                NEWBackpack[a].HEALTH_ADD = userInformation.ITEM_HEALTH[a];
            }

            MongoFunctions.MainStyle mainNew = new MongoFunctions.MainStyle()
            {
                Username = userInformation.username,
                Username_LOWER = userInformation.username.ToLower(),
                user_password = userInformation.password,
                User_LVL = userInformation.user_LVL,

                PokemonCount = userInformation.pokemonTOTAL,

                PokeCoins = userInformation.user_COINS,

                AdditionalPkmn = userInformation.pokemonList,

                Backpack = NEWBackpack

            };
            updateAllInformation(mainNew);
                
        }
        public static void addInformation(Information pkmnStats, string user, string password, int lvl, int coins, string pokemon, string type, out List<PokemonInfStruct> partyPokemon)
        {
            MainComponents.ActionFunctions.grabInitialMoves(pokemon, out List<string> moveNames, out List<string> moveTypes, out List<int> powerMoves, out List<int> accuracyMoves, out List<int> maxUses);

            double HPTotal = Math.Floor(0.01 * (2 * pkmnStats.HP * lvl) + lvl + 10);


            InformationEvolution nextEvolve = PokemonNextEvolutionAPI.grabNextEvolution(pokemon).Result;

            int EXP_NEXTLVL = MainMethodsCalculation.getNextLevelInformation(1, pokemon).Result;
            string[] newmoves = moveNames.ToArray();
            string[] newtypes = moveTypes.ToArray();
            int[] newpower = powerMoves.ToArray();
            int[] newacc = accuracyMoves.ToArray();
            int[] newuses = maxUses.ToArray();

            List<MoveSAMPLE> n = new List<MoveSAMPLE>()
            {
                new MoveSAMPLE() { MOVENAME = newmoves[0], MOVETYPE = newtypes[0], BASEPOWER = newpower[0], ACCURACY_M = newacc[0], MAX_USES = newuses[0], CURR_USES = newuses[0]},
                new MoveSAMPLE() { MOVENAME = newmoves[1], MOVETYPE = newtypes[1], BASEPOWER = newpower[1], ACCURACY_M = newacc[1], MAX_USES = newuses[1], CURR_USES = newuses[1]},
                new MoveSAMPLE() { MOVENAME = newmoves[2], MOVETYPE = newtypes[2], BASEPOWER = newpower[2], ACCURACY_M = newacc[2], MAX_USES = newuses[2], CURR_USES = newuses[2]},
                new MoveSAMPLE() { MOVENAME = newmoves[3], MOVETYPE = newtypes[3], BASEPOWER = newpower[3], ACCURACY_M = newacc[3], MAX_USES = newuses[3], CURR_USES = newuses[3]}
            };

            MainStyle newUserINFO = new MainStyle
            {
                //ID = Guid.NewGuid(),
                Username = user,
                Username_LOWER = user.ToLower(),
                user_password = password,
                User_LVL = lvl,

                PokemonCount = 1,
                PokeCoins = coins,

                AdditionalPkmn = new List<PokemonInfStruct>()
                {
                    new PokemonInfStruct()
                    {
                        pokemonName = pokemon,
                        pokemonName_LOWER = pokemon.ToLower(),
                        pokemon_TYPE = type,
                        pokemon_HEALTH = HPTotal,
                        pokemon_HEALTH_MAX = HPTotal,
                        pokemon_LVL = 1,
                        pokemon_EXP = 1,
                        Pkmn_Moves = n,
                        ACCURACY = 100,

                        ATTACK = pkmnStats.ATTACK,
                        DEFENSE = pkmnStats.DEFENSE,
                        Sp_Atk = pkmnStats.Sp_Atk,
                        Sp_Def = pkmnStats.Sp_Def,
                        Speed = pkmnStats.Speed,

                        EvolutionINFO = nextEvolve,
                    },
                    new PokemonInfStruct(),
                    new PokemonInfStruct(),
                    new PokemonInfStruct(),
                    new PokemonInfStruct(),
                    new PokemonInfStruct()
                },

                Backpack = new List<ItemSAMPLE>()
                {
                    new ItemSAMPLE() { ITEM_NAME = "Potion", HEALTH_ADD = 20, AMOUNT_LEFT = 10 },
                    new ItemSAMPLE() { ITEM_NAME = "Poké ball", HEALTH_ADD = 0, AMOUNT_LEFT = 10 },
                    new ItemSAMPLE(),
                    new ItemSAMPLE()
                }
            };

            db.InsertRecord("UserInformations", MongoDB.Bson.BsonExtensionMethods.ToBsonDocument(newUserINFO));

            //id = newUserINFO.ID;
            partyPokemon = newUserINFO.AdditionalPkmn;

            //db.UpdateAllMoves(user, n, 0);
        }

        //TO-DO NEXT
        public static void readInformation(string userIn, out string password, out string userOut, out int lvl, out int coins, out int pkmnCount, out List<PokemonInfStruct> party)
        {
            var information = db.LoadInformationUSER("UserInformations", userIn);

            userOut = information.Username;
            password = information.user_password;
            lvl = information.User_LVL;
            coins = information.PokeCoins;

            party = information.AdditionalPkmn;

            pkmnCount = information.PokemonCount;


        }

        public static void changeUser(string userIn)
        {
            db.UpdateUsername<MainStyle>("UserInformations", userIn);

            userInformation.username = userIn;
        }

        public static bool userExist(string userIn)
        {
            var information = db.LoadInformationUSER("UserInformations", userIn);
            if (information != null)
            {
                return true;
            }
            else
                return false;
        }

        public static void updateAllInformation(MainStyle newStyle)
        {
            db.UpdateFullData<MainStyle>("UserInformations", newStyle);
        }

        public class MainStyle
        {
            [MongoDB.Bson.Serialization.Attributes.BsonId]
            public MongoDB.Bson.ObjectId ID { get; set; }
            //USER
            public string Username { get; set; }
            public string Username_LOWER { get; set; }

            public string user_password { get; set; }
            public int PokemonCount { get; set; }

            //MISC
            public int User_LVL { get; set; }


            public List<PokemonInfStruct> AdditionalPkmn = new List<PokemonInfStruct>();

            public List<ItemSAMPLE> Backpack = new List<ItemSAMPLE>();

            public int PokeCoins { get; set; }

        }


       

        public class ItemSAMPLE
        {
            public string ITEM_NAME = "";
            public int HEALTH_ADD = 0;
            public int AMOUNT_LEFT = 0;

        }

        public class MoveSAMPLE
        {
            public string MOVENAME = "N/A";
            public string MOVETYPE = "NORMAL";

            public int BASEPOWER = 0;
            public int CURR_USES = 0;   
            public int MAX_USES = 0;
            public int ACCURACY_M = 0;
        }

        public class MongoClass
        {
            private MongoDB.Driver.IMongoDatabase mdb;

            public MongoClass(string database)
            {

                //var client = new MongoClient("mongodb://localhost:27017");

                var settings = MongoDB.Driver.MongoClientSettings.FromConnectionString("mongodb+srv://OWNER_Developer:ianlourd15@pokedatabase.ld5w5.mongodb.net/PokeDatabase?retryWrites=true&w=majority");
                var client = new MongoDB.Driver.MongoClient(settings);
                mdb = client.GetDatabase(database);
            }
            
            public void InsertRecord(string table, MongoDB.Bson.BsonDocument record)
            {
                var collection = mdb.GetCollection<MongoDB.Bson.BsonDocument>(table);

                collection.InsertOne(record);
            }

            public MainStyle LoadInformationUSER(string table, string username)
            {
                var collect = mdb.GetCollection<MainStyle>(table);
                var filter = MongoDB.Driver.Builders<MainStyle>.Filter.Eq("Username_LOWER", username.ToLower());

                var list = collect.FindAsync<MainStyle>(filter).Result;

                return MongoDB.Driver.IAsyncCursorExtensions.FirstOrDefault(list);
            }

            public void UpdateUsername<T>(string table, string user)
            {
                var collect = mdb.GetCollection<MongoDB.Bson.BsonDocument>("UserInformations");

                var filter = MongoDB.Driver.Builders<MongoDB.Bson.BsonDocument>.Filter.Eq("ID", userInformation.user_ID);

                var update = MongoDB.Driver.Builders<MongoDB.Bson.BsonDocument>.Update.Set("Username", user);

                var update2 = MongoDB.Driver.Builders<MongoDB.Bson.BsonDocument>.Update.Set("Username_LOWER", user.ToLower());

                collect.UpdateOne(filter, update);

                collect.UpdateOne(filter, update2);

            }

            public void UpdateFullData<T>(string table, MainStyle newStyle)
            {
                var collect = mdb.GetCollection<MongoDB.Bson.BsonDocument>(table);

                var filter = MongoDB.Driver.Builders<MongoDB.Bson.BsonDocument>.Filter.Eq("Username_LOWER", userInformation.username.ToLower());

                var jsonDoc = Newtonsoft.Json.JsonConvert.SerializeObject(newStyle);
                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(jsonDoc);
                jo.Property("ID").Remove();

                var jsonDoc2 = Newtonsoft.Json.JsonConvert.SerializeObject(jo);
                var newDoc = MongoDB.Bson.BsonDocument.Parse(jsonDoc2);

                collect.ReplaceOne(filter, newDoc);


            }

            public void ChangePassword(string table, string newpassword)
            {
                var collect = mdb.GetCollection<MongoDB.Bson.BsonDocument>(table);

                var filter = MongoDB.Driver.Builders<MongoDB.Bson.BsonDocument>.Filter.Eq("Username_LOWER", userInformation.username.ToLower());

                var update = MongoDB.Driver.Builders<MongoDB.Bson.BsonDocument>.Update.Set("user_password", newpassword);

                collect.UpdateOne(filter, update);


            }
        }
    }
}
