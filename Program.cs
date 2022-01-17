<<<<<<< HEAD
Ôªøusing PokemonGame.MainComponents.Main;
using PokemonGame.MainDes;
using System;
using System.Media;
using System.Threading;
using PokemonGame.FunctionClasses.MusicPlayer;
using PokemonGame.FunctionClasses;
using PokemonGame.informationClass;
using PokemonGame;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;

using PokemonGame.MainComponents;

using NAudio.Wave;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;
using System.Net.Http;

namespace PokemonGame
{

    public class ProgramMain
    {
        public class variables
        {
            public static bool YWinner = false;
            public static string Current_Version = "V. 1";
        }

        public static string[] dllNames =
        {
            "DnsClient.dll",
            "MongoDB.Bson.dll",
            "MongoDB.Driver.Core.dll",
            "MongoDB.Driver.dll",
            "MongoDB.Libmongocrypt.dll",
            "NAudio.dll",
            "Newtonsoft.Json.dll",
            "System.Net.Http.dll",
            "System.Runtime.InteropServices.RuntimeInformation.dll",
            "SharpCompress.dll",
            "System.Buffers.dll",
            "System.Runtime.CompilerServices.Unsafe.dll",
            "System.Text.Encoding.CodePages.dll"
        };

        public static List<string> dllListFound = new List<string>();

        public static List<string> MissingDLL = new List<string>();

        public static string filePathFound = string.Empty;

        [Obsolete]
        public static void Main(string[] args)
        {
            string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DLLs");
            DirectoryInfo d = new DirectoryInfo(filepath);

            AppDomain.CurrentDomain.AppendPrivatePath(filepath);

            Console.WriteLine("Checking DLL files..");

            foreach (var dll in d.GetFiles("*.dll"))
            {
                Assembly.Load(File.ReadAllBytes(dll.FullName));
                dllListFound.Add(dll.Name);
            }

            foreach (string dllName in dllNames)
            {
                if (!dllListFound.Contains(dllName))
                {
                    MissingDLL.Add(dllName);
                }
            }

            Console.CursorVisible = false;


            if (MissingDLL.ToArray().Length >= 1)
            {
                int missingDownloaded = 0;
                Console.WriteLine("Downloading DLL files... (" + missingDownloaded + " / " + MissingDLL.Count + ")");
                string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                WebClient client = new WebClient();
                foreach (string i in MissingDLL)
                {
                    Task.Delay(4000);
                    Uri URL = new Uri("https://github.com/TheAlmightyGuard/PokemonProject_Console/raw/master/DLLs/" + i);
                    client.DownloadFile(URL, i);

                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), i);
                    string destPath = Path.Combine(filepath, i);
                    File.Move(i, destPath);
                    Assembly.Load(File.ReadAllBytes(destPath));
                    missingDownloaded++;
                    Console.SetCursorPosition(26, 1);
                    Console.Write(missingDownloaded);
                    Console.SetCursorPosition(0,3);
                }
            }

            Console.SetCursorPosition(0, 3);
            Console.WriteLine("All DLLs loaded!");
            Console.ReadKey();
            Console.Clear();
            MainRun();
        }

        private static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Download Completed!");
        }

        private static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine("Missing DLLs! Downloading..");
        }

        public static void OpeningLoading()
        {
            Console.Clear();
            Console.Write("Loading");
            for (double i = 0; i < 7.5; i += 1.5)
            {
                Console.Write(".");
                Thread.Sleep(1500);
            }
            Console.WriteLine("\n" + "DONE!");
            Thread.Sleep(1000);

        }
        public static void diffChoice()
        {
            do
            {

                pokeInformation.basicInfo basicInfo = new pokeInformation.basicInfo();
                Console.Clear();
                Console.WriteLine("Choose your dificulty:" + "\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[EASY] (Player & Enemy Health | 100)");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[MEDIUM] (Player & Enemy Health | 500)");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[HARD] (Player & Enemy Health | 900)");

                Console.ForegroundColor = ConsoleColor.White;
                string chosen = Console.ReadLine().ToUpper();

                if (chosen == "EASY" || chosen == "E")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Difficulty Mode: EASY");
                    //basicInfo.changeAllHealth(100);
                    Console.ReadKey();

                }
                else if (chosen == "MEDIUM" || chosen == "M")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Difficulty Mode: MEDIUM");
                    //basicInfo.changeAllHealth(500);
                    Console.ReadKey();
                }
                else if (chosen == "HARD" || chosen == "H")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Difficulty Mode: HARD");
                    //basicInfo.changeAllHealth(900);
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Pick a valid choice!");
                    Console.ReadKey();
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
                break;
            } while (true);
            return;

        }

        public static void MainRun()
        {
            MaximizeWindow.Maximize();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            MusicPlayerC.OpeningMusic();

            Thread.Sleep(2000);
            //MainDesigns.pokemontitle();
            miscellaneousFunc.copyrightText();
            MusicPlayerC.ButtonClick();

            Console.CursorVisible = false;
            do
            {
                int chosenSec = 0;
                do
                {
                    bool newgame = miscellaneousFunc.startMenu();
                    Console.CursorVisible = true;

                    string user = string.Empty;
                    if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == false)
                    {
                        user = miscellaneousFunc.askUser();
                    }

                    if (newgame == true && user.ToLower() != "return")
                    {
                        MusicPlayerC.ButtonClick();

                        string password = Password_Class.newPassword();

                        userInformation.password = password;

                        miscellaneousFunc.newpokemonChoiceMenu(out string pokemon, out string type);

                        Information pokeStats = PokemonInformationAPI.grabInformationAsync(pokemon.ToLower()).Result;

                        MongoFunctions.addInformation(pokeStats, user, password, 1, 100, pokemon, type, out List<MongoFunctions.PartySAMPLE> party, out Guid ID);

                        Task.Delay(5000);
                        loadAllInformation(user, ID);
                        //pokeInformation.advInfo.CurrUsesAR = pokeInformation.advInfo.MaxUsesAR;
                        break;
                    }
                    else if (user.ToLower() == "return")
                    {
                        MusicPlayerC.ButtonClick();
                        continue;
                    }
                    else if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == false)
                    {
                        MusicPlayerC.ButtonClick();

                        Console.CursorVisible = false;
                        Console.Write("\nLoading information for ");

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("{0}", user);

                        Console.ResetColor();
                        Console.WriteLine("! Please wait....");


                        for (int i = 1; i < 3; i++)
                        {


                            int leftLength = 23 + user.Length + 14;
                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("    ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write(".   ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("..  ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("... ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("....");
                            Thread.Sleep(1200);
                        }

                        Console.WriteLine("\nPress enter to continue.");
                        Console.ReadKey(true);

                        if (MongoFunctions.userExist(user.ToLower()) == true)
                        {
                            MongoFunctions.readInformation(user.ToLower(), out Guid userID, out string pswrd, out string userOut, out int levelOut, out int coinsOut, out List<MongoFunctions.PartySAMPLE> party);
                            bool correct = Password_Class.mainPassword(user);
                            if (correct == true)
                            {
                                loadAllInformation(user, userID);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nPassword incorrect!");
                                Console.ReadKey();
                                MusicPlayerC.ButtonClick();
                                continue;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Username does not exist! Create a new username instead..");
                            Console.ReadKey();
                            MusicPlayerC.ButtonClick();
                            continue;
                        }
                    }
                    else
                    {
                        DEVELOPER_INFO();
                        break;
                    }
                } while (true);

                do
                {
                    chosenSec = miscellaneousFunc.secondMenu();
                    if (chosenSec == 2)
                    {
                        bool changed = false;
                        do
                        {
                            changed = miscellaneousFunc.editProfileAccount();
                            MusicPlayerC.ButtonClick();
                            break;

                        } while (true);

                        if (changed == true)
                        {
                            break;
                        }
                        else
                            continue;
                    }
                    break;
                } while (true);
                break;
            } while (true);

            //miscellaneousFunc.pokemonChoiceMenu();
            MusicPlayerC.ButtonClick();
            Console.Clear();
            diffChoice();
            MusicPlayerC.ButtonClick();
            Console.Clear();


            Thread.Sleep(1000);
            Console.WriteLine("\n{0}, I CHOOSE YOU!\n", pokeInformation.basicInfo.pokemon);
            MusicPlayerC.CrySound();

            Thread.Sleep(1200);


            MainChar.MainMethod();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            pokeInformation.advInfo.storeAllMoves(out List<MongoFunctions.PartySAMPLE> party);
            MongoFunctions.updateAllInformation(
                new MongoFunctions.MainStyle()
                {
                    Username = userInformation.username,
                    Username_LOWER = userInformation.username.ToLower(),
                    user_password = userInformation.password,
                    User_LVL = userInformation.user_LVL,

                    PokeCoins = userInformation.user_COINS,

                    AdditionalPkmn = party

                });
        }

        public static void DEVELOPER_INFO()
        {
            pokeInformation.advInfo.MOVENAMES = ["Confusion", "Ancient Power", "Psycho Cut", "Psystrike"];
            userInformation.username = "DEVELOPER";
            userInformation.user_LVL = 100;
            userInformation.user_COINS = 10000;
            userInformation.user_ID = "DEVELOPER_ID";

            pokeInformation.basicInfo.pokemon = "Mewtwo";
            pokeInformation.basicInfo.accuracy = 100;
            pokeInformation.basicInfo.levelPokemon = 999;
            pokeInformation.basicInfo.Health = 999;

            pokeInformation.basicInfo.ATTACK = 110;
            pokeInformation.basicInfo.DEFENSE = 90;
            pokeInformation.basicInfo.Sp_Atk = 154;
            pokeInformation.basicInfo.Sp_Def = 90;
            pokeInformation.basicInfo.Speed = 130;

            pokeInformation.basicInfo.pokemon_EXP = 999;

            List<MongoFunctions.ItemSAMPLE> Backpack = new List<MongoFunctions.ItemSAMPLE>()
            {
                    new MongoFunctions.ItemSAMPLE() { ITEM_NAME = "Potion", HEALTH_ADD = 20, AMOUNT_LEFT = 10 },
                    new MongoFunctions.ItemSAMPLE(),
                    new MongoFunctions.ItemSAMPLE(),
                    new MongoFunctions.ItemSAMPLE()
            };

            int a = 0;
            foreach (MongoFunctions.ItemSAMPLE b in Backpack)
            {
                if (b.ITEM_NAME.Trim() !=(  t	Îã  …  Ÿ´k â  |	z®  3  |	2É a  |	ı«  u  Ñ	Îã  …  å	Îã  …  î	Îã  …  ‘ Îã  l(  S (  ú	~ \  AöÉ  à  ÒpI ì(  A§z  à  Ã_f 6  ‘_f 6  ‹_f 6  ‰_f 6  Ï_f 6  Ù_f 6  ¸_f 6  	_f 6  ë®å Ê  	_f 6  	_f 6  	_f 6  $	_f 6  ,	_f 6  4	_f 6  <	_f 6  Tôf ¬  	{ó “(  ÒpI ú  ëªb )  	Âº  )  ‘+e ⁄  ‹+e ⁄  ‰+e ⁄  Ï+e ⁄  Ù+e ⁄  	+e ⁄  	+e ⁄  	+e ⁄  4	+e ⁄  <	+e ⁄  	M0    Ò}  S)  ÒQf  ∂  ) î e)  ¨	M0 á  I	@å t)  ¥	e ï)  )e †)  `  Ω)  ¯a ⁄	  º	z®  3  º	2É a  º	ı«  u  ƒ	z®  3  ƒ	2É a  ƒ	ı«  u  Ÿéã  *  i{ó $*  πq .*  Ø  B*  qØ    Å+e Q*  Åáó j*  Ã	BÆ  å*  ‘	z®  3  ‘	2É a  ‘	ı«  u  Ã	M0 ≤*  Ã	Äf     ‹	M0 Õ*  ‹	vy  2  ‹	tz  ∑  ‹	t  ‚*  ‹	+õ  Á*  ‹	# >  ‹	ºF [  ‹	¬°  Ò*  ‹	b≥  c  ‹	Y·  ¯*  ‹	ˇk ¸*  ‹	#} ˇ*  ‹	rd 6  ‹	>ô  +  ‹	∑ô  +  ‹	ìf    ‹	œ& Ò*  ‹	BÆ  &+  ‹	õ     ‹	„  [+  ‰	BÆ  z+  Ï	Úò  ç+  Ù	q  ®  ‹	⁄& ü+  ¸	z®  3  
µ
 ÿ+  
vH Ê+  
uπ  Ô+  
Úÿ  Y  
é™  ^  ¸	2É a  ¸	ı«  u  
µ
 ÿ+  
vH Ê+  
å ,  
Úÿ  Y  
é™  ^  ‹	  √  ‹	«  √  ‹	Æ0  √  ‹	€8  √  ‹	⁄=  √  ‹	ØA  √  ‹	PD  √  Åw‘  U	  ‹	~r  5,  
M0 ì  ‹	iF I,  ‰	M0 ≤*  ‰	Äf     Ï	Y  O,  ‹	Gi ü+  ‹	Ñg V,  îôf 6  Ï	`z  ∑  Ï	≥] s,  Ï	mÿ  6  
M0 Ä,  ‹	ây  å  ‹	ºö  ≤,  $
–  ≈,  $
J  œ,  $
⁄  ˆ  ,
M0 á  4
BÆ  ˇ,  4
M0 ≤*  …∏f —
  …Ñ —
  ÅBõ  √  <
M0 \  <
~ \  <
w‘  A-  
kX  O,  
}  Q-  
tz  ∑  
™ì 6  
#ø  Ö  
%Y  ù  
Çÿ  6  
b≥  c  
‚e c  
$ >  
ºF [  
∑ô  +  D
~ \  …◊ã  ^-  
MÇ ^  
™ò   
¯Z     
éb     
ø  Ö  
nb  z-  
Aß     L
µ
 p  L
vH ì-  L
Úÿ  Y  L
é™  ^  
   √  
⁄  √  
9r  ®  
„" Õ-  
LÙ  0  
—æ  ◊-  T
µ
   T
vH ì-  T
‹Ã  ˆ-  T
Úÿ  Y  T
é™  ^  
æ \  
u  ®  
/^  .  \
µ
 p  \
vH ì-  \
‹Ã  ˆ-  \
Úÿ  Y  \
é™  ^  
Aß     
‘b    
&ß     
Üb    
ŸX z-  d
µ
 p  d
vH ì-  d
Úÿ  Y  d
é™  ^  ∂7 ⁄	  ÒM0 M.  
Nr  c.  
–  c.  l
µ
   l
vH ì-  l
Úÿ  Y  l
é™  ^  
6u  ú.  
±–  ú.  t
µ
 p  t
vH ì-  t
Úÿ  Y  t
é™  ^  
'ä Õ.  
/ø  ”.  
∂æ  ‡.  |
µ
 G  |
vH ì-  |
Úÿ  Y  |
é™  ^  ^ $  Y    ÿ  ®  È~ \  Ñ
J  !/  Ñ
J  +/  Ñ
T  4/  Q	M0    Y	M0 ;/  å
& £  å
Òÿ  Y  å
´c    î
M0 #  å
V/ œ  î
& £  î
Òÿ  Y  î
éb     î
«d    î
¸\    î
Õ+ S/  î
øu ¬  î
‘b    yM0    ú
∑    ú
Òÿ  Y  §
Á& ‰  ú
V/ œ  ,M0 u/  ,ây  å  ¨
›` Z  ,iF I,  ,`  ∂  ,Ì ç/  ,mf  ∂  ,Å  ï/  4M0 u/  4ây  å  ¥
›` Z  4iF I,  º
M0 ≠/  4í‡  ƒ/  º
Ø‡  Ã/  4`  ∂  4Ì ç/  4mf  ∂  4Å  ï/  ƒ
z®  3  ƒ
2É a  ƒ
ı«  u  ,`     ,ˇ -0  ,=¡  -0  ,í  60  ,Äf     4Ø‡  Ã/  4`     4ˇ -0  4=¡  -0  4ÑF M0  4í  60  4Äf     ÑØ‡  Ã/  Ñ`     Ñˇ -0  Ñ=¡  -0  Ñí  60  ÑÄf     Ã
V/ /  ‘
øu ¬  ‘
*ä ∂  \BÆ  ñ0  ‹
z®  3  ‹
2É a  ‹
ı«  u  ‰
M0 –0  ‰
`    ‰
∞ ‡0  ‰
tz  ∑  ‰
Ê` Ä  ‰
ºF [  ‰
Õ‡  Ò*  ‰
Ä c  ‰
P¡  c  ‰
ìf    ‰
%·  Ô0  ‰
˙| ¸0  ‰
p; 1  ‰
BÆ  1  ‰
U  1  Ï
µ
 =1  Ï
vH G1  Ï
uπ  P1  Ï
Úÿ  Y  Ï
é™  ^  Ù
›` Z  ,πJ $  ‰
D·  ú  ‰
} ú  ‰
ÿ| ò1  ‰
»„  ∂1  ‰
◊g »1  ¸
µ
 Ì1  ¸
vH G1  ¸
å ˜1  ¸
Nø  ¸1  ¸
Úÿ  Y  ¸
é™  ^  M0 32  ; B2  ‰
©æ  P2  Ù
,j c2  må h2  µ
 =1  vH G1  å ˜1  Úÿ  Y  é™  ^  M0 –0  `    ∞ ‡0  tz  ∑  Ê` Ä  ºF [  Ä c  P¡  c  M0 π2  ; «2  $(Ö n  $˙å ^  BÑ ›2  $,j c2  Üå Ï2  ,µ
 =1  ,vH 3  ,å ˜1  ,Úÿ  Y  ,é™  ^  H–   3  ¸i .3  4µ
 Ì1  4vH 3  4‹Ã  ˆ-  41Ö n  4	ç ^  4Úÿ  Y  4é™  ^  ÑM0 u/  Ñây  å  <›` Z  ÑiF 
=======
Ôªøusing PokemonGame.MainComponents.Main;
using PokemonGame.MainDes;
using System;
using System.Media;
using System.Threading;
using PokemonGame.FunctionClasses.MusicPlayer;
using PokemonGame.FunctionClasses;
using PokemonGame.informationClass;
using PokemonGame;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

using System.Runtime.InteropServices;

using NAudio.Wave;
using System.Threading.Tasks;

namespace PokemonGame.Main
{
    public class ProgramMain
    {
        public class variables
        {
            public static bool YWinner = false;
            public static string Current_Version = "V. 1";
        }

        public static string[] dllNames =
        {
            "DnsClient.dll",
            "MongoDB.Bson.dll",
            "MongoDB.Driver.Core.dll",
            "MongoDB.Driver.dll",
            "MongoDB.Libmongocrypt.dll",
            "NAudio.dll",
            "SharpCompress.dll",
            "Sytem.Buffers",
            "System.Runtime.CompilerServices.Unsafe.dll",
            "System.Text.Encoding.CodePages.dll"
        };

        

        public static void Main(string[] args)
        {
            
            MainRun();
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PokemonGame.Resources.NAudio.dll"))
            {
                var assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }

        public static void OpeningLoading()
        {
            Console.Clear();
            Console.Write("Loading");
            for (double i = 0; i < 7.5; i += 1.5)
            {
                Console.Write(".");
                Thread.Sleep(1500);
            }
            Console.WriteLine("\n" + "DONE!");
            Thread.Sleep(1000);

        }
        public static void diffChoice()
        {
            do
            {

                pokeInformation.basicInfo basicInfo = new pokeInformation.basicInfo();
                Console.Clear();
                Console.WriteLine("Choose your dificulty:" + "\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[EASY] (Player & Enemy Health | 100)");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[MEDIUM] (Player & Enemy Health | 500)");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[HARD] (Player & Enemy Health | 900)");

                Console.ForegroundColor = ConsoleColor.White;
                string chosen = Console.ReadLine().ToUpper();

                if (chosen == "EASY" || chosen == "E")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Difficulty Mode: EASY");
                    //basicInfo.changeAllHealth(100);
                    Console.ReadKey();

                }
                else if (chosen == "MEDIUM" || chosen == "M")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Difficulty Mode: MEDIUM");
                    //basicInfo.changeAllHealth(500);
                    Console.ReadKey();
                }
                else if (chosen == "HARD" || chosen == "H")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Difficulty Mode: HARD");
                    //basicInfo.changeAllHealth(900);
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Pick a valid choice!");
                    Console.ReadKey();
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
                break;
            } while (true);
            return;

        }

        public static void MainRun()
        {
            MaximizeWindow.Maximize();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            MusicPlayerC.OpeningMusic();

            Thread.Sleep(2000);
            //MainDesigns.pokemontitle();
            miscellaneousFunc.copyrightText();
            MusicPlayerC.ButtonClick();

            Console.CursorVisible = false;
            do
            {
                int chosenSec = 0;
                do
                {
                    bool newgame = miscellaneousFunc.startMenu();
                    Console.CursorVisible = true;

                    string user = string.Empty;
                    if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == false)
                    {
                        user = miscellaneousFunc.askUser();
                    }

                    if (newgame == true && user.ToLower() != "return")
                    {
                        MusicPlayerC.ButtonClick();

                        string password = Password_Class.newPassword();

                        userInformation.password = password;

                        miscellaneousFunc.newpokemonChoiceMenu(out string pokemon, out string type);

                        Information pokeStats = PokemonInformationAPI.grabInformationAsync(pokemon.ToLower()).Result;

                        MongoFunctions.addInformation(pokeStats, user, password, 1, 100, pokemon, type, out List<MongoFunctions.PartySAMPLE> party, out Guid ID);

                        Task.Delay(5000);
                        loadAllInformation(user, ID);
                        //pokeInformation.advInfo.CurrUsesAR = pokeInformation.advInfo.MaxUsesAR;
                        break;
                    }
                    else if (user.ToLower() == "return")
                    {
                        MusicPlayerC.ButtonClick();
                        continue;
                    }
                    else if (DEVELOPER_OPTIONS.DEVELOPER_ENABLED == false)
                    {
                        MusicPlayerC.ButtonClick();

                        Console.CursorVisible = false;
                        Console.Write("\nLoading information for ");

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("{0}", user);

                        Console.ResetColor();
                        Console.WriteLine("! Please wait....");


                        for (int i = 1; i < 3; i++)
                        {


                            int leftLength = 23 + user.Length + 14;
                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("    ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write(".   ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("..  ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("... ");
                            Thread.Sleep(1200);

                            Console.SetCursorPosition(leftLength, 3);
                            Console.Write("....");
                            Thread.Sleep(1200);
                        }

                        Console.WriteLine("\nPress enter to continue.");
                        Console.ReadKey(true);

                        if (MongoFunctions.userExist(user.ToLower()) == true)
                        {
                            MongoFunctions.readInformation(user.ToLower(), out Guid userID, out string pswrd, out string userOut, out int levelOut, out int coinsOut, out List<MongoFunctions.PartySAMPLE> party);
                            bool correct = Password_Class.mainPassword(user);
                            if (correct == true)
                            {
                                loadAllInformation(user, userID);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nPassword incorrect!");
                                Console.ReadKey();
                                MusicPlayerC.ButtonClick();
                                continue;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Username does not exist! Create a new username instead..");
                            Console.ReadKey();
                            MusicPlayerC.ButtonClick();
                            continue;
                        }
                    }
                    else
                    {
                        DEVELOPER_INFO();
                        break;
                    }
                } while (true);

                do
                {
                    chosenSec = miscellaneousFunc.secondMenu();
                    if (chosenSec == 2)
                    {
                        bool changed = false;
                        do
                        {
                            changed = miscellaneousFunc.editProfileAccount();
                            MusicPlayerC.ButtonClick();
                            break;

                        } while (true);

                        if (changed == true)
                        {
                            break;
                        }
                        else
                            continue;
                    }
                    break;
                } while (true);
                break;
            } while (true);

            //miscellaneousFunc.pokemonChoiceMenu();
            MusicPlayerC.ButtonClick();
            Console.Clear();
            diffChoice();
            MusicPlayerC.ButtonClick();
            Console.Clear();


            Thread.Sleep(1000);
            Console.WriteLine("\n{0}, I CHOOSE YOU!\n", pokeInformation.basicInfo.pokemon);
            MusicPlayerC.CrySound();

            Thread.Sleep(1200);


            MainChar.MainMethod();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            pokeInformation.advInfo.storeAllMoves(out List<MongoFunctions.PartySAMPLE> party);
            MongoFunctions.updateAllInformation(
                new MongoFunctions.MainStyle()
                {
                    Username = userInformation.username,
                    Username_LOWER = userInformation.username.ToLower(),
                    user_password = userInformation.password,
                    User_LVL = userInformation.user_LVL,

                    PokeCoins = userInformation.user_COINS,

                    AdditionalPkmn = party

                });
        }

        public static void DEVELOPER_INFO()
        {
            userInformation.username = "DEVELOPER";
            userInformation.user_LVL = 100;
            userInformation.user_COINS = 10000;
            userInformation.user_ID = "DEVELOPER_ID";
        }

        public static void loadAllInformation(string user, Guid ID)
        {
            Task.Delay(5000);
            var information = MongoFunctions.db.LoadInformationUSER("UserInformations", user);
            var selectedInfo = information.Find(x => x.Username_LOWER == user.ToLower());

            userInformation.username = selectedInfo.Username;
            userInformation.user_LVL = selectedInfo.User_LVL;
            userInformation.user_COINS = selectedInfo.PokeCoins;
            userInformation.user_ID = selectedInfo.ID.ToString();


            MongoFunctions.PartySAMPLE firstPokemon = selectedInfo.AdditionalPkmn.GetRange(0, 4).ToArray()[0];

            pokeInformation.basicInfo.pokemon = firstPokemon.Pkmn_NAME;
            pokeInformation.basicInfo.accuracy = firstPokemon.ACCURACY_P;
            pokeInformation.basicInfo.levelPokemon = firstPokemon.LVL;
            pokeInformation.basicInfo.Health = firstPokemon.HEALTH;

            pokeInformation.basicInfo.Health = firstPokemon.HEALTH;
            pokeInformation.basicInfo.ATTACK = firstPokemon.ATTACK;
            pokeInformation.basicInfo.DEFENSE = firstPokemon.DEFENSE;
            pokeInformation.basicInfo.Sp_Atk = firstPokemon.Sp_Atk;
            pokeInformation.basicInfo.Sp_Def = firstPokemon.Sp_Def;
            pokeInformation.basicInfo.Speed = firstPokemon.Speed;

            pokeInformation.basicInfo.pokemon_EXP = firstPokemon.EXP;

            List<MongoFunctions.ItemSAMPLE> items = selectedInfo.Backpack;

            int a = 0;

            foreach (MongoFunctions.ItemSAMPLE b in items)
            {
                if (b.ITEM_NAME.Trim() != "")
                {
                    userInformation.ITEM_NAME[a] = b.ITEM_NAME;
                    userInformation.ITEM_HEALTH[a] = b.HEALTH_ADD;
                    userInformation.ITEM_AMOUNT[a] = b.AMOUNT_LEFT;
                    a++;
                }
                else
                {
                    userInformation.ITEM_NAME[a] = "~~~~~~~~~~~~~";
                    userInformation.ITEM_HEALTH[a] = 0;
                    userInformation.ITEM_AMOUNT[a] = 0;
                    a++;
                }
            }

            List<string> pkmnNames = new List<string>();
            List<string> pkmnNamesLOWER = new List<string>();
            foreach (MongoFunctions.PartySAMPLE i in selectedInfo.AdditionalPkmn)
            {
                if (i.Pkmn_NAME != pokeInformation.basicInfo.pokemon)
                {
                    pkmnNames.Add(i.Pkmn_NAME);
                    pkmnNamesLOWER.Add(i.Pkmn_NAME.ToLower());
                }
            }

            pokeInformation.advInfo.pokemonLIST = pkmnNames.ToArray();
            pokeInformation.advInfo.pokemonLISTLOW = pkmnNamesLOWER.ToArray();

            object[] infoarray = selectedInfo.AdditionalPkmn.GetRange(0, 4).ToArray();

            //pokeInformation.advInfo.loadFirstPokeMove(selectedInfo.AdditionalPkmn.GetRange(0,0).ToArray());
            pokeInformation.advInfo.loadAllMoves(infoarray);
        }
    }

}
>>>>>>> parent of 20aad4e (Update v1.1)
