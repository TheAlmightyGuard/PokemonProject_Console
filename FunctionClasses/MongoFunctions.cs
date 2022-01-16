using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using PokemonGame.FunctionClasses;
using System.Threading.Tasks;

using PokemonGame.informationClass;
using MongoDB.Bson.Serialization;

namespace PokemonGame
{
    public class MongoFunctions
    {
        public static MongoClass db = new MongoClass("MainDatabase");

        public static void addInformation(Information pkmnStats, string user, string password, int lvl, int coins, string pokemon, string type, out List<PartySAMPLE> partyPokemon, out Guid id)
        {
            MainComponents.ActionFunctions.grabInitialMoves(pokemon, out List<string> moveNames, out List<string> moveTypes, out List<int> powerMoves, out List<int> accuracyMoves, out List<int> maxUses);

            double HPTotal = Math.Floor(0.01 * (2 * pkmnStats.HP * lvl) + lvl + 10);




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

                PokeCoins = coins,

                AdditionalPkmn = new List<PartySAMPLE>()
                {
                    new PartySAMPLE()

                    {
                        Pkmn_NAME = pokemon,
                        TYPE = type,
                        HEALTH = HPTotal,
                        LVL = 1,
                        EXP = 1,
                        Pkmn_MOVES = n,
                        ACCURACY_P = 100,

                        ATTACK = pkmnStats.ATTACK,
                        DEFENSE = pkmnStats.DEFENSE,
                        Sp_Atk = pkmnStats.Sp_Atk,
                        Sp_Def = pkmnStats.Sp_Def,
                        Speed = pkmnStats.Speed,

                    },
                    new PartySAMPLE(),
                    new PartySAMPLE(),
                    new PartySAMPLE(),
                    new PartySAMPLE()
                },

                Backpack = new List<ItemSAMPLE>()
                {
                    new ItemSAMPLE() { ITEM_NAME = "Potion", HEALTH_ADD = 20, AMOUNT_LEFT = 10 },
                    new ItemSAMPLE(),
                    new ItemSAMPLE(),
                    new ItemSAMPLE()
                }
            };

            db.InsertRecord("UserInformations", newUserINFO.ToBsonDocument());

            id = newUserINFO.ID;
            partyPokemon = newUserINFO.AdditionalPkmn;

            //db.UpdateAllMoves(user, n, 0);
        }

        //TO-DO NEXT
        public static void readInformation(string userIn, out Guid userID, out string password, out string userOut, out int lvl, out int coins, out List<PartySAMPLE> party)
        {
            var information = db.LoadInformationUSER("UserInformations", userIn);
            var selectedInfo = information.Find(x => x.Username_LOWER == userIn.ToLower());

            userOut = selectedInfo.Username;
            password = selectedInfo.user_password;
            lvl = selectedInfo.User_LVL;
            coins = selectedInfo.PokeCoins;
            userID = selectedInfo.ID;

            party = selectedInfo.AdditionalPkmn;


        }

        public static void changeUser(string userIn)
        {
            db.UpdateUsername<MainStyle>("UserInformations", userIn);

            userInformation.username = userIn;
        }

        public static bool userExist(string userIn)
        {
            var information = db.LoadInformationUSER("UserInformations", userIn);
            if (information.Find(x => x.Username_LOWER == userIn.ToLower()) != null)
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
            //USER
            [BsonId]
            public Guid ID { get; set; }
            public string Username { get; set; }
            public string Username_LOWER { get; set; }

            public string user_password { get; set; }

            //MISC
            public int User_LVL { get; set; }


            public List<PartySAMPLE> AdditionalPkmn = new List<PartySAMPLE>();

            public List<ItemSAMPLE> Backpack = new List<ItemSAMPLE>();

            public int PokeCoins { get; set; }

        }


        public class PartySAMPLE
        {
            public string Pkmn_NAME = "N/A";
            public string TYPE = "NORMAL";

            public List<MoveSAMPLE> Pkmn_MOVES = new List<MoveSAMPLE>()
            {
                new MoveSAMPLE(),
                new MoveSAMPLE(),
                new MoveSAMPLE(),
                new MoveSAMPLE()
            };

            public int ACCURACY_P = 100;
            public double HEALTH = 0;

            public int ATTACK = 0;
            public int DEFENSE = 0;
            public int Sp_Atk = 0;
            public int Sp_Def = 0;
            public int Speed = 0;

            public int LVL = 1;
            public int EXP = 1;
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
            private IMongoDatabase mdb;

            public MongoClass(string database)
            {

                //var client = new MongoClient("mongodb://localhost:27017");

                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://OWNER_Developer:ianlourd15@pokedatabase.ld5w5.mongodb.net/PokeDatabase?retryWrites=true&w=majority");
                var client = new MongoClient(settings);
                mdb = client.GetDatabase(database);
            }
            
            public void InsertRecord(string table, BsonDocument record)
            {
                var collection = mdb.GetCollection<BsonDocument>(table);

                collection.InsertOne(record);
            }

            public T LoadInformation<T>(string table, Guid ID)
            {
                var collect = mdb.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("_id", ID);

                var list = collect.Find(filter).FirstOrDefault();

                return list;

            }

            public List<MainStyle> LoadInformationUSER(string table, string user)
            {
                var collect = mdb.GetCollection<MainStyle>(table);


                List<MainStyle> list = collect.Find(x => x.Username_LOWER == user.ToLower()).ToList();

                return list;

            }

            public void UpdateCoins<T>(string table, Guid ID, int newCurr)
            {
                var collect = mdb.GetCollection<BsonDocument>("UserInformations");

                var filter = Builders<BsonDocument>.Filter.Eq("ID", ID);

                var update = Builders<BsonDocument>.Update.Set("PokeCoins", newCurr);

                var res = collect.UpdateOne(filter, update);

            }

            public void UpdateUsername<T>(string table, string user)
            {
                var collect = mdb.GetCollection<BsonDocument>("UserInformations");

                var filter = Builders<BsonDocument>.Filter.Eq("ID", userInformation.user_ID);

                var update = Builders<BsonDocument>.Update.Set("Username", user);

                var update2 = Builders<BsonDocument>.Update.Set("Username_LOWER", user.ToLower());

                collect.UpdateOne(filter, update);

                collect.UpdateOne(filter, update2);

            }

            public void UpdateFullData<T>(string table, MainStyle newStyle)
            {
                var collect = mdb.GetCollection<BsonDocument>(table);

                var filter = Builders<BsonDocument>.Filter.Eq("Username_LOWER", userInformation.username.ToLower());

                var newDoc = newStyle.ToBsonDocument();

                collect.ReplaceOne(filter, newDoc);


            }

            public void ChangePassword(string table, string newpassword)
            {
                var collect = mdb.GetCollection<BsonDocument>(table);

                var filter = Builders<BsonDocument>.Filter.Eq("Username_LOWER", userInformation.username.ToLower());

                var update = Builders<BsonDocument>.Update.Set("user_password", newpassword);

                collect.UpdateOne(filter, update);


            }

            public void UpdateAllMoves(Guid ID, List<MoveSAMPLE> moveList, int arrNum)
            {
                var collect = mdb.GetCollection<BsonDocument>("UserInformations");

                var filter = Builders<BsonDocument>.Filter.Eq("ID", ID);


                var update = Builders<BsonDocument>.Update.Set($"AdditionalPkmn." + arrNum.ToString() + $".Pkmn_MOVES", moveList);

                collect.UpdateOne(filter, update);

            }
        }
    }
}
