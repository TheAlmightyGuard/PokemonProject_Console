using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.MainComponents.actionBoxes;
using PokemonGame.MainComponents.AttackList;
using PokemonGame.MainComponents.attackMain;
using PokemonGame.Main;
using PokemonGame.FunctionClasses.InventoryMain;
using PokemonGame.FunctionClasses.HealthManip;
using PokemonGame.FunctionClasses;
using PokemonGame.FunctionClasses.BattleEngine;
using System.Threading;
using PokemonGame.informationClass;
using PokemonGame.MainComponents;

namespace PokemonGame.MainComponents.Main
{
    public class MainChar
    {
        public static bool firstMove = true;
        public static bool runaway = false;
        public static Random rndm = new Random();

        [Obsolete]
        public static void MainMethod()
        {
            pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH = pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH_MAX;
            pokeInformation.basicInfo.EnemyPokemon = EnemyPokemonGen.getEnemyAsync().Result;

            //MusicPlayerC.OpeningPlayer.Stop();
            //MainDes.MainDesigns.EvolveFunc(pokeInformation.basicInfo.currentPokemon.pokemonName, pokeInformation.basicInfo.currentPokemon.EvolutionINFO.NEXT_EVOLUTION);
            //Console.ReadKey();


            int num = rndm.Next(1, 100);
            if (num >= 1 && num <= 50)
            {
                pokeInformation.basicInfo.ENEMY_TYPE = "[WILD POKEMON!]";
                pokeInformation.basicInfo.ENEMY_TYPENUM = 1;
                pokeInformation.basicInfo.ENEMY_NAME = pokeInformation.basicInfo.EnemyPokemon.pokemonName;
            }
            else
            {
                pokeInformation.basicInfo.ENEMY_TYPE = "[?? TRAINER ??]";
                pokeInformation.basicInfo.ENEMY_TYPENUM = 2;
                pokeInformation.basicInfo.ENEMY_NAME = "TRAINER";
            }
            BattleUI.BattleStartTransition();
            BattleUI.BattleGUI();
            do //WHILE 1 OR MORE THAN 1 POKEMON EXISTS
            {
                do //WHILE HEALTH IS OVER 0
                {
                    BattleUI.menu = 0;
                    BattleUI.pokemontitle();
                    BattleUI.mainBox(69);
                    BattleUI.AttackSTATS(0, "None", false);

                    int i = BattleUI.ChooseAction();
                    if (i == 0)
                    {
                        BattleUI.menu = 1;
                        MusicPlayerC.ButtonClick();

                        BattleUI.AttackBox();
                        Task.Delay(2000);
                        int a = BattleUI.ChooseAttack();
                        BattleUI.loopArrow = false;
                        Task.Delay(2000);
                        if (a >= 0)
                        {

                            _attackMain.Attack(pokeInformation.basicInfo.currentPokemon.Pkmn_Moves[a].MOVENAME);
                            BattleUI.AttackSTATS(0, "None", false);
                        }
                        MusicPlayerC.ButtonClick();
                        BattleUI.resetUIExtra();
                    }
                    else if (i == 1)
                    {
                        BattleUI.menu = 2;
                        MusicPlayerC.ButtonClick();
                        InventoryEngine.InventoryEngineFunc();
                        MusicPlayerC.ButtonClick();
                        BattleUI.resetUIExtra();

                    }
                    else if (i == 2)
                    {
                        BattleUI.loopArrow = false;
                        BattleUI.menu = 3;
                        MusicPlayerC.ButtonClick();
                        BattleUI.PokemonBox();
                        int b = BattleUI.ChoosePokemon();
                        if (b >= 0)
                        {
                            miscellaneousFunc.pokemonSwitch(b);
                        }
                        MusicPlayerC.ButtonClick();
                        BattleUI.resetUIExtra();
                    }
                    else if (i == 3)
                    {
                        MusicPlayerC.ButtonClick();
                        BattleUI.RunAwayText(pokeInformation.basicInfo.ENEMY_TYPENUM);
                        pokeInformation.basicInfo.pokemonLeft = 0;
                        runaway = true;
                    }
                } while (pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH > 0 && pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH > 0 && runaway != true);

                

                if (pokeInformation.basicInfo.currentPokemon.pokemon_HEALTH <= 0)
                {
                    pokeInformation.basicInfo.pokemonLeft -= 1;
                    BattleUI.PokemonFainted(true, pokeInformation.basicInfo.currentPokemon.pokemonName);
                }
                else if (pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH <= 0)
                {
                    BattleUI.PokemonFainted(false, pokeInformation.basicInfo.currentPokemon.pokemonName);
                }




                if (pokeInformation.basicInfo.pokemonLeft >= 1 && pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH > 0)
                {
                    BattleUI.loopArrow = false;
                    BattleUI.menu = 3;
                    MusicPlayerC.ButtonClick();
                    BattleUI.PokemonBox();
                    int b = BattleUI.ChoosePokemon();
                    if (b >= 0)
                    {
                        miscellaneousFunc.pokemonSwitch(b);
                    }
                    MusicPlayerC.ButtonClick();
                    BattleUI.resetUIExtra();

                    continue;
                }
                else
                    break;

            } while (pokeInformation.basicInfo.pokemonLeft >= 1 && pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH > 0 && runaway != true);

            MusicPlayerC.HealthPlayer.Stop();
            Console.Clear();
            if (pokeInformation.basicInfo.EnemyPokemon.pokemon_HEALTH <= 0 && runaway == false)
            {
                ProgramMain.variables.YWinner = true;
                EndMusic();

                int expGained = BattleEngine.gainExperience();

                BattleUI.Winner(expGained);

                Console.ReadKey();

            }
            else if (runaway == false)
            {
                BattleUI.Lost();
                Console.ReadKey();
            }


            BattleUI.loopArrow = false;
            Thread.Sleep(1500);
            string input = BattleUI.RetryBattle();
            if (input == "M")
            {
                MusicPlayerC.BattleMPlayer.Stop();
                MusicPlayerC.BattleMPlayer.Dispose();

                MusicPlayerC.EndPlayer.Stop();
                ProgramMain.MainRun();
            }
            else if (input == "Y")
            {
                MusicPlayerC.BattleMPlayer.Stop();
                MusicPlayerC.BattleMPlayer.Dispose();

                MusicPlayerC.EndPlayer.Stop();
                MainMethod();
            }
            else
            {
                Console.WriteLine("\n Au revoir! Good bye!");
                Thread.Sleep(2000);
                MongoFunctions.saveAllInformation();
            }
            
        }

        public static void EndMusic()
        {

            Thread.Sleep(1000);
            MusicPlayerC.VictoryMusic();
        }
    }
}
