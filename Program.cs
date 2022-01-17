﻿using PokemonGame.MainComponents.Main;
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
                if (b.ITEM_NAME.Trim() !=(  t	�  �  ��k �  |	z�  3  |	2� a  |	��  u  �	�  �  �	�  �  �	�  �  � �  l(  S (  �	~ \  A��  �  �pI �(  A�z  �  �_f 6  �_f 6  �_f 6  �_f 6  �_f 6  �_f 6  �_f 6  	_f 6  ��� �  	_f 6  	_f 6  	_f 6  $	_f 6  ,	_f 6  4	_f 6  <	_f 6  T�f �  	{� �(  �pI �  ��b )  	�  )  �+e �  �+e �  �+e �  �+e �  �+e �  	+e �  	+e �  	+e �  4	+e �  <	+e �  	M0    �}  S)  �Qf  �  ) � e)  �	M0 �  I	@� t)  �	e �)  )e �)  `�  �)  �a �	  �	z�  3  �	2� a  �	��  u  �	z�  3  �	2� a  �	��  u  ���  *  i{� $*  �q .*  �  B*  q�    �+e Q*  ��� j*  �	B�  �*  �	z�  3  �	2� a  �	��  u  �	M0 �*  �	�f     �	M0 �*  �	vy  2  �	tz  �  �	t  �*  �	+�  �*  �	# >  �	�F [  �	¡  �*  �	b�  c  �	Y�  �*  �	�k �*  �	#} �*  �	rd 6  �	>�  +  �	��  +  �	�f    �	�& �*  �	B�  &+  �	�     �	�  [+  �	B�  z+  �	�  �+  �	�q  �  �	�& �+  �	z�  3  
�
 �+  
vH �+  
u�  �+  
��  Y  
��  ^  �	2� a  �	��  u  
�
 �+  
vH �+  
� ,  
��  Y  
��  ^  �	  �  �	�  �  �	�0  �  �	�8  �  �	�=  �  �	�A  �  �	PD  �  �w�  U	  �	~r  5,  
M0 �  �	iF I,  �	M0 �*  �	�f     �	Y  O,  �	Gi �+  �	�g V,  ��f 6  �	`z  �  �	�] s,  �	m�  6  
M0 �,  �	�y  �  �	��  �,  $
�  �,  $
J  �,  $
�  �  ,
M0 �  4
B�  �,  4
M0 �*  ��f �
  �� �
  �B�  �  <
M0 \  <
~ \  <
w�  A-  
kX  O,  
}  Q-  
tz  �  
�� 6  
#�  �  
%Y  �  
��  6  
b�  c  
�e c  
$ >  
�F [  
��  +  D
~ \  �׋  ^-  
M� ^  
��   
�Z     
�b     
�  �  
nb  z-  
A�     L
�
 p  L
vH �-  L
��  Y  L
��  ^  
   �  
�  �  
9r  �  
�" �-  
L�  0  
Ѿ  �-  T
�
   T
vH �-  T
��  �-  T
��  Y  T
��  ^  
� \  
u  �  
/^  .  \
�
 p  \
vH �-  \
��  �-  \
��  Y  \
��  ^  
A�     
�b    
&�     
�b    
�X z-  d
�
 p  d
vH �-  d
��  Y  d
��  ^  �7 �	  �M0 M.  
Nr  c.  
�  c.  l
�
   l
vH �-  l
��  Y  l
��  ^  
6u  �.  
��  �.  t
�
 p  t
vH �-  t
��  Y  t
��  ^  
'� �.  
/�  �.  
��  �.  |
�
 G  |
vH �-  |
��  Y  |
��  ^  ^ $  Y    �  �  �~ \  �
J  !/  �
J  +/  �
T  4/  Q	M0    Y	M0 ;/  �
�& �  �
��  Y  �
�c    �
M0 #  �
V/ �  �
�& �  �
��  Y  �
�b     �
�d    �
�\    �
�+ S/  �
�u �  �
�b    yM0    �
�    �
��  Y  �
�& �  �
V/ �  ,M0 u/  ,�y  �  �
�` Z  ,iF I,  ,`  �  ,� �/  ,mf  �  ,�  �/  4M0 u/  4�y  �  �
�` Z  4iF I,  �
M0 �/  4��  �/  �
��  �/  4`  �  4� �/  4mf  �  4�  �/  �
z�  3  �
2� a  �
��  u  ,`     ,� -0  ,=�  -0  ,�  60  ,�f     4��  �/  4`     4� -0  4=�  -0  4�F M0  4�  60  4�f     ���  �/  �`     �� -0  �=�  -0  ��  60  ��f     �
V/ /  �
�u �  �
*� �  \B�  �0  �
z�  3  �
2� a  �
��  u  �
M0 �0  �
`    �
� �0  �
tz  �  �
�` �  �
�F [  �
��  �*  �
� c  �
P�  c  �
�f    �
%�  �0  �
�| �0  �
p; 1  �
B�  1  �
U  1  �
�
 =1  �
vH G1  �
u�  P1  �
��  Y  �
��  ^  �
�` Z  ,�J $  �
D�  �  �
} �  �
�| �1  �
��  �1  �
�g �1  �
�
 �1  �
vH G1  �
� �1  �
N�  �1  �
��  Y  �
��  ^  M0 32  ; B2  �
��  P2  �
,j c2  m� h2  �
 =1  vH G1  � �1  ��  Y  ��  ^  M0 �0  `    � �0  tz  �  �` �  �F [  � c  P�  c  M0 �2  ; �2  $(� n  $�� ^  B� �2  $,j c2  �� �2  ,�
 =1  ,vH 3  ,� �1  ,��  Y  ,��  ^  H�   3  �i .3  4�
 �1  4vH 3  4��  �-  41� n  4	� ^  4��  Y  4��  ^  �M0 u/  ��y  �  <�` Z  �iF 