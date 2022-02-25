using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using PokemonGame.MainDes;
using PokemonGame.FunctionClasses.HealthManip;
using PokemonGame.informationClass;
using PokemonGame.Main;
using PokemonGame.MainComponents;
using System.Threading;

namespace PokemonGame.FunctionClasses.InventoryMain
{
    class InventoryEngine
    {
        static userInformation userInfo = new userInformation();
        [Obsolete]
        public static void InventoryEngineFunc()
        {
            BattleUI.menu = 2;
            MusicPlayerC.ButtonClick();
            BattleUI.InventoryBox();
            Task.Delay(2000);
            BattleUI.loopArrow = false;

            int a = BattleUI.ChooseItem();
            if (a >= 0)
            {
                BattleUI.loopArrow = false;
                BattleUI.backgroundThread.Abort();
                if (userInformation.ITEM_AMOUNT[a] == 0)
                {
                    return;
                }

                #region [POKEBALL]
                else if (userInformation.ITEM_NAME[a].ToLower().Contains("ball"))
                {
                    if (pokeInformation.basicInfo.ENEMY_TYPENUM == 1)
                    {
                        bool brokefree = false;
                        int ballTimes = 12;
                        int ballType = 0; // 1 - POKEBALL // 2 - GREAT BALL // 3 - OTHERS
                        int N = 0;
                        if (userInformation.ITEM_NAME[a].ToLower() == "Poké ball")
                        {
                            N = new Random().Next(0, 255);
                            ballType = 1;
                        }
                        else if (userInformation.ITEM_NAME[a].ToLower() == "Great ball")
                        {
                            N = new Random().Next(0, 200);
                            ballTimes = 8;
                            ballType = 2;
                        }
                        else
                        {
                            N = new Random().Next(0, 150);
                            ballType = 3;
                        }

                        if (N > pokeInformation.basicInfo.EnemyPokemon.catch_Rate)
                        {
                            brokefree = true;
                        }
                        else
                        {
                            int M = new Random().Next(0, 255);
                            int f = 0;

                            int f1 = Convert.ToInt32(Math.Round(pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH_MAX * 255 * 4));
                            int f2 = Convert.ToInt32(Math.Round(pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH * ballTimes));

                            f = f1 / f2;

                            if (f >= M)
                            {
                                brokefree = false;
                            }
                            else
                            {
                                brokefree = true;
                            }
                        }

                        int shake = 0;
                        if (brokefree == true)
                        {
                            int shakeNum = shakeCalculator(ballType, pokeInformation.basicInfo.EnemyPokemon.catch_Rate);

                            if (shakeNum == 0)
                            {
                                shake = 75;
                            }
                            else if (shakeNum == 1)
                            {
                                shake = 80;
                            }
                            else if (shakeNum == 2)
                            {
                                shake = 85;
                            }
                            else if (shakeNum == 3)
                            {
                                shake = 90;
                            }
                        }
                        BattleUI.catchPokemonUI(brokefree, shake);

                        if (brokefree == false)
                        {
                            MusicPlayerC.BattleMPlayer.Stop();
                            MusicPlayerC.HealthPlayer.Stop();
                            MusicPlayerC.playingHealth = false;
                            string input = BattleUI.RetryBattle();

                            userInformation.pokemonTOTAL++;

                            if (userInformation.pokemonTOTAL > userInformation.pokemonList.Count)
                            {
                                userInformation.pokemonList.Add(pokeInformation.basicInfo.EnemyPokemon);
                            }
                            else
                            {
                                userInformation.pokemonList[userInformation.pokemonTOTAL - 1] = pokeInformation.basicInfo.EnemyPokemon;
                            }


                            if (input == "M")
                            {
                                MusicPlayerC.BattleMPlayer.Stop();
                                MusicPlayerC.BattleMPlayer.Dispose();

                                MusicPlayerC.CatchingPokemon.Stop();
                                MusicPlayerC.CatchingPokemon.Dispose();
                                ProgramMain.MainRun();
                            }
                            else if (input == "Y")
                            {
                                MusicPlayerC.BattleMPlayer.Stop();
                                MusicPlayerC.BattleMPlayer.Dispose();

                                MusicPlayerC.CatchingPokemon.Stop();
                                MusicPlayerC.CatchingPokemon.Dispose();
                                MainComponents.Main.MainChar.MainMethod();
                            }
                            else
                            {

                                Console.WriteLine("\n Au revoir! Good bye!");
                                Thread.Sleep(2000);
                                MongoFunctions.saveAllInformation();
                                Environment.Exit(0);
                            }
                        }
                        MusicPlayerC.BattleMPlayer.Play();
                    }
                    else
                    {
                        BattleUI.TrainedPokemonAttempt();
                    }
                }
                #endregion

                #region [POTION]
                else if (userInformation.ITEM_NAME[a].ToLower().Contains("potion"))
                {
                    if (pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH == pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH_MAX)
                    {
                        BattleUI.itemHealthType(true, true, "N/A", 0);
                    }
                    else
                    {
                        if (userInformation.ITEM_NAME[a].ToLower().Contains("Max Potion"))
                        {
                            BattleUI.itemHealthType(false, true, userInformation.ITEM_NAME[a], Convert.ToInt32(pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH_MAX - pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH));
                        }
                        else
                        {
                            BattleUI.itemHealthType(false, true, userInformation.ITEM_NAME[a], userInformation.ITEM_HEALTH[a]);
                        }
                        HealthManipulation.AddHealth("YHealth", userInformation.ITEM_HEALTH[a]);
                        userInformation.ITEM_AMOUNT[a] -= 1;
                    }
                }
                #endregion
            }
        }

        public static int shakeCalculator(int ball, int catch_rate)
        {
            int timesBall = 0;
            int numReturn = 0;

            if (ball == 1)
            {
                timesBall = 255;
            }
            else if (ball == 2)
            {
                timesBall = 200;
            }
            else
            {
                timesBall = 150;
            }

            int d1 = catch_rate * 100;
            int d2 = timesBall;

            int d = d1 / d2;

            if (d >= 256)
            {
                numReturn = 3;
            }

            int x = d / 255;
            if (x <= 10)
            {
                numReturn = 0;
            }
            else if (x <= 30 && x > 10)
            {
                numReturn = 1;
            }
            else if (x <= 70 && x > 30)
            {
                numReturn = 2;
            }
            else
            {
                numReturn = 3;
            }

            return numReturn;
        }
    }
}
