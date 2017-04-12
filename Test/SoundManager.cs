using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace Test
{
    class SoundManager
    {
        //constructor
        public SoundManager()
        {
<<<<<<< HEAD
            song_dict = new Dictionary<String, String>();
            sfx_dict = new Dictionary<string, SoundBuffer>();
=======
            song_dict = new Dictionary<string, string>() { { "Dad", "../../Sounds/sayagain-loop1.wav" },
                                                           { "Mom","../../Sounds/sayagain-loop2.wav" },
                                                           { "Alex", "" } };
            sfx_dict = new Dictionary<string, SoundBuffer>() { { "chatter", new SoundBuffer("../../Sounds/chatter.wav") },
                                                               { "button", new SoundBuffer("../../Sounds/button.wav")} };
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
            current = "None";
            next = "None";
            sound = new Sound();
        }

        //fields
        Sound sound;
        Music song;
        private String current;
        private String next;
        public Dictionary<String, String> song_dict;
        public Dictionary<String, SoundBuffer> sfx_dict;
        public bool soundpause = false;

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
<<<<<<< HEAD
           //load the click sound object
           sound.SoundBuffer = sfx_dict[soundName];
           sound.Play();
=======
            //load the click sound object
            if (!soundpause) {
                sound.SoundBuffer = sfx_dict[soundName];
                sound.Play();
            }
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
           return;
        }

        public void playMusic(string musicname)
        {
<<<<<<< HEAD
            Console.WriteLine(current + " " + musicname);
=======
            //Console.WriteLine(current + " " + musicname);
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
            if (current != musicname)
            {
                if (current != "None" && song.Status == SoundStatus.Playing)
                {
                    song.Stop();
                }
                song = new Music(song_dict[musicname]);
<<<<<<< HEAD
                song.Play();
                song.Loop = true;
                current = musicname;
            }
=======
                song.Volume = 0;
                song.Play();
                song.Loop = true;
                current = musicname;
            } 
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303

            return;
        }

        public void transitionSong(String musicName)
        {
            song.Loop = false;
            next = musicName;
        }

<<<<<<< HEAD
        public void soundUpdate()
        {
            if (song.Status == SoundStatus.Stopped)
=======
        public void soundUpdate(bool soundToggle)
        {
            if (!soundToggle && !this.soundpause)
            {
                this.song.Pause();
                soundpause = true;
            }
            else if (soundToggle && this.soundpause)
            {
                this.song.Play();
                soundpause = false;
            }
            else if (song.Status == SoundStatus.Stopped && !soundpause)
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
            {
                playMusic(next);
            }
        }
<<<<<<< HEAD

        public void init_sounds()
        {
            //load abs paths to music to global objects
            song_dict.Add("Dad","../../Sounds/sayagain-loop1.wav");
            song_dict.Add("Mom","../../Sounds/sayagain-loop2.wav");
            song_dict.Add("Alex", "AlexAddress");
            //add music as needed 
            //load all buffers to SFX to global objects
            sfx_dict.Add("chatter", new SoundBuffer("../../Sounds/chatter.wav"));
            sfx_dict.Add("button", new SoundBuffer("../../Sounds/button.wav"));
            //add buffers as needed   
        }
=======
>>>>>>> 24292412928b907bdb0e2cd81f7a16bf1fc4e303
    }
}
