using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.IO;
using PokemonGame.informationClass;
using System.Reflection;
using System.Runtime.InteropServices;
using PokemonGame;

namespace PokemonGame.FunctionClasses.MusicPlayer
{

    class MusicPlayerC
    {
        public static bool playingHealth = false;
        public static Random rndm = new Random();
        public static NAudio.Wave.WaveOutEvent OpeningPlayer = new NAudio.Wave.WaveOutEvent();
        public static NAudio.Wave.WaveOutEvent EffectPlayer = new NAudio.Wave.WaveOutEvent();

        public static NAudio.Wave.WaveOutEvent HealthPlayer = new NAudio.Wave.WaveOutEvent();
        public static NAudio.Wave.WaveOutEvent BattleMPlayer = new NAudio.Wave.WaveOutEvent();
        public static NAudio.Wave.WaveOutEvent EndPlayer = new NAudio.Wave.WaveOutEvent();

        public static void CrySound()
        {
            EffectPlayer.Dispose();

            var url = "https://drive.google.com/uc?export=download&id=1wMPRtI2yfUoIRS5uf89YomYzPTIPznJa";
            using (var mf = new NAudio.Wave.MediaFoundationReader(url))
            {
                NAudio.Wave.WaveChannel32 volumeStream = new NAudio.Wave.WaveChannel32(mf);

                EffectPlayer.Init(volumeStream);

                EffectPlayer.Play();
            }
        }

        public static void BattleMusic()
        {
            OpeningPlayer.Dispose();
            BattleMPlayer.Dispose();

            string[] wild = { "https://drive.google.com/uc?export=download&id=1zyDaDizQX5v54pwZCBLKpaNTvxdFBrxF", "https://drive.google.com/uc?export=download&id=1RJj8eAQMLVnXXFrPqXC2gmIMimW55FF9", "https://drive.google.com/uc?export=download&id=1YrIKU6s9gob3CqRb_g8XNB1FluR5fcIa" };

            string[] gym = { "https://drive.google.com/uc?export=download&id=1w19ESZCOj7W0Gk861vjb7bmil2gAyelY", "https://drive.google.com/uc?export=download&id=1uvRv6CAchdX2Bi6QOsbxJ1z0QTH_piVJ", "https://drive.google.com/uc?export=download&id=1PVvZcpiel59YEq3RF3Xl0vBcykbGz2IN" };

            if (pokeInformation.basicInfo.ENEMY_TYPENUM == 1)
            {
                using (var mf = new NAudio.Wave.MediaFoundationReader(wild[rndm.Next(0, 2)]))
                {
                    LoopStream loop = new LoopStream(mf);

                    BattleMPlayer.Init(loop);

                    BattleMPlayer.Play();
                }
            }
            else
            {
                using (var mf = new NAudio.Wave.MediaFoundationReader(gym[rndm.Next(0, 2)]))
                {
                    LoopStream loop = new LoopStream(mf);

                    BattleMPlayer.Init(loop);

                    BattleMPlayer.Play();
                }
            }
        }

        public static void VictoryMusic()
        {
            BattleMPlayer.Dispose();
            EndPlayer.Dispose();

            string[] url = { "https://drive.google.com/uc?export=download&id=1DLQYgdeI_VpUwfXYj15KVU0R1NvBSlqo", "https://drive.google.com/uc?export=download&id=1SsNOB0rqHakP3fKfAmaO7qHYnMBjf10e", "https://drive.google.com/uc?export=download&id=1pqNDLPbNyzEzu4gKmoCE3SxzramZlPcS" };

            using (var mf = new NAudio.Wave.MediaFoundationReader(url[rndm.Next(0, 2)]))
            {
                LoopStream loop = new LoopStream(mf);

                EndPlayer.Init(loop);

                EndPlayer.Play();
            }
        }

        public static void OpeningMusic()
        {
            OpeningPlayer.Dispose();

            string[] url = { 
                "https://drive.google.com/uc?export=download&id=1b1C7XSfWtRCHuwaeyEpKNqD0S1NMWbbQ", 
                "https://drive.google.com/uc?export=download&id=1VP56KWU0DBDjQQ03WColwL3AHUoJlwtG", 
                "https://drive.google.com/uc?export=download&id=13Zfvj8juv1e3JggwYhEYzIo5CEHaUbe1", 
                "https://drive.google.com/uc?export=download&id=1Ob9p2LaIO9nH4URQ9oBpES_ZrzDKXdhn", 
                "https://drive.google.com/uc?export=download&id=1Rq_05N2O1LKrSF6_ziWRviyIHYdyWCOO",
                "https://drive.google.com/uc?export=download&id=11kltb8XkW6RimTSQy_VhDESl3GTGLCPH" };



            using (var mf = new NAudio.Wave.MediaFoundationReader(url[rndm.Next(0,5)]))
            {
                LoopStream loop = new LoopStream(mf);

                OpeningPlayer.Init(loop);

                OpeningPlayer.Play();
            }
        }

        public static void ButtonClick()
        {
            EffectPlayer.Dispose();
            //var url = "https://drive.google.com/uc?export=download&id=1utFlYne1K7vkJOD5pQddHmACT0SxDOk1";
            NAudio.Wave.WaveStream reader = new NAudio.Wave.WaveFileReader(Properties.Resources.ButtonClicked);
            NAudio.Wave.WaveChannel32 volumeStream = new NAudio.Wave.WaveChannel32(reader);

            EffectPlayer.Init(volumeStream);

            EffectPlayer.Play();

        }

        public static void LowHealth()
        {
            HealthPlayer.Dispose();

            string url = "https://drive.google.com/uc?export=download&id=1PNhnjUAXVQCYtcHiebEFQBY5OgxYlRog";

            using (var mf = new NAudio.Wave.MediaFoundationReader(url))
            {
                LoopStream loop = new LoopStream(mf);

                HealthPlayer.Init(loop);

                HealthPlayer.Play();

                playingHealth = true;
            }
        }

        public static void stopLow()
        {
            HealthPlayer.Stop();
            playingHealth = false;
        }
    }
}
