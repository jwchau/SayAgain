using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CSCore;
using CSCore.SoundOut;
using CSCore.Codecs;
using CSCore.Streams;
using CSCore.Streams.Effects;

namespace Test
{
    class SoundManager
    {
        //constructor
        public SoundManager()
        {
            sound_dict = new Dictionary<string, string>() { { "loop", "../../Sounds/jazz-loop.wav" },
                                                          { "Alex", "" },
                                                          { "chatter","../../Sounds/chatter.wav" },
                                                          { "button","../../Sounds/button.wav"} };
            Step = 1;
            next = "None";
            musicOut = GetSoundOut();
            soundOut2 = GetSoundOut();
        }

        ISoundOut musicOut;
        ISoundOut soundOut2;
        private String next;
        private Dictionary<String, String> sound_dict;
        public bool soundpause = false;
        private int Step;
        private List<IWaveSource> music_blocks;

        public bool getSoundPause()
        {
            return soundpause;
        }
        public void setSoundPause(bool b)
        {
            soundpause = b;
        }

        //methods

        public void playSFX(String soundName)
        {
            IWaveSource soundSource = GetSoundSource(soundName);
            soundOut2.Stop();
            PlayASound(soundSource,soundOut2);
        }

        public void transitionSong(String musicName)
        {

        }

        public void init_music()
        {
           // music_blocks = new List<IWaveSource> { GetSoundSource("chatter"), GetSoundSource("chatter"), GetSoundSource("chatter"), GetSoundSource("chatter") };
            //Step = 0;
            musicOut.Stop();
            PlayASound(GetSoundSource("loop"), musicOut);

        }
        public void update_music()
        {
            if (musicOut.PlaybackState == PlaybackState.Stopped)
            {
                //if (Step > 3) Step = 0;
                PlayASound(GetSoundSource("loop"), musicOut);
            }

        }

        [STAThread]
        private void PlayASound( IWaveSource soundSource, ISoundOut thisSound)
        {
            //Tell the SoundOut which sound it has to play
            thisSound.Initialize(soundSource);
            //Play the sound
            thisSound.Play();
            //Stop the playback
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor);
        }
    

        private ISoundOut GetSoundOut()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut();
            else
                return new DirectSoundOut();
        }

        private IWaveSource GetSoundSource(String name)
        {
            //return any source ... in this example, we'll just play a mp3 file
            return CodecFactory.Instance.GetCodec(sound_dict[name]);
        }

        private IWaveSource CreateASineSource()
        {
            double frequency = 80 ;
            double amplitude = 0.5;
            double phase = 0.0;
            //Create a ISampleSource
            ISampleSource sampleSource = new SineGenerator(frequency, amplitude, phase);

            //Convert the ISampleSource into a IWaveSource
            PitchShifter shifted = new PitchShifter(sampleSource);
            shifted.PitchShiftFactor = 3;
            IWaveSource wvsrc = shifted.ToWaveSource();


            return wvsrc;
        }
    }
}
