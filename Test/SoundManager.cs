﻿using System;
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
            song_dict = new Dictionary<String, String>();
            sfx_dict = new Dictionary<string, SoundBuffer>();
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

        //methods
        public void playSFX(String soundName)
        {
           //load the click sound object
           sound.SoundBuffer = sfx_dict[soundName];
           sound.Play();
           return;
        }

        public void playMusic(string musicname)
        {
            if (!(current == musicname))
            {
                if (current != "None" && song.Status == SoundStatus.Playing)
                    song.Stop();
                song = new Music(song_dict[musicname]);
                song.Play();
                song.Loop = true;
                current = musicname;
            }

            return;
        }

        public void transitionSong(String musicName)
        {
            song.Loop = false;
            next = musicName;
        }

        public void soundUpdate()
        {
            if(song.Status == SoundStatus.Stopped)
            {
                playMusic(next);
            }
        }

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
    }
}