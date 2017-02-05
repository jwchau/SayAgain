using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;

namespace Test
{
    class GameState
    {
        public GameState()
        {
            currentState = "menu";
            currentMenuState = "start";
        }

        string currentState;
        string currentMenuState;
        //double oldTimeSeconds = 0;
        //double pauseTime = 0;
        //double newTimeSeconds = 0;
        //double timeDiff = 0;
        //double countDown = 1;
        Dictionary<string, GameTimer> DictGameTimer = new Dictionary<string, GameTimer>();

        //Timer for keeping track of time given to the player
        //DictGameTimer.Add(new GameTimer("test",10.0, new Task(() => { /*EVERYTHING HERE*/}))); //new timer for 10 seconds

        public GameTimer getGameTimer(string tag) {
            return DictGameTimer[tag];
        }

        public void addTimer(string name, double initTime, Action T) {
            if (DictGameTimer.ContainsKey(name))
            {
                DictGameTimer[name] = new GameTimer(name,initTime,T);
            }
            else {
                DictGameTimer.Add(name, new GameTimer(name, initTime, T));
            }
        }

        public string GetState()
        {
            return currentState;
        }


        public void SetState(string state)
        {
            if (state != "menu" && state != "game" && state != "pause")
            {
                throw new FormatException();
            }
            if (state == "game" && currentMenuState == "start") {
                DictGameTimer["game"].setOldGameTime((DateTime.Now.Ticks / 10000) / 1000);
            }

            currentState = state;
        }

        public string GetMenuState()
        {
            return currentMenuState;
        }

        public void SetMenuState(string state)
        {
            if (state != "start" && state != "settings" && state != "pause")
            {
                throw new FormatException();
            }
            currentMenuState = state;
        }

        public void updateTimerz() {
            foreach (var pair in DictGameTimer) {
                //pair.Value is a gameTimer
                //pair.Key is the label of the game Timer;
                if (pair.Value.getStart())
                {
                    pair.Value.updateTimer();
                    
                }
                else
                {
                    if (pair.Value.getCountDown() == 0)
                    {
                        //DO STUFF BEFORE RESTARTING
                        //Process Player dialogue
                        pair.Value.doTask();
                        pair.Value.restartTimer();
                    }
                    else
                    {
                        pair.Value.startTimer();
                    }
                }
            }
        }

        //public void setOldGameTime(double newTimeSeconds) {
        //    oldTimeSeconds = newTimeSeconds;
        //}

        //public double getOldGameTime() {
        //    return oldTimeSeconds;
        //}

        //public double getPauseTime() {
        //    return pauseTime;
        //}

        //public void setPauseTime(double newTime) {
        //    pauseTime = newTime;
        //}

        //public double getNewTime()
        //{
        //    return newTimeSeconds;
        //}

        //public void setNewTime(double newTime)
        //{
        //    newTimeSeconds = newTime;
        //}

        //public void setTimeDiff(double newTime) {
        //    timeDiff = newTime;
        //}

        //public double getTimeDiff() {
        //    return timeDiff;
        //}

        //public void setCountDown(double cd) {
        //    countDown = cd;
        //}

        //public double getCountDown() {
        //    return countDown;
        //}

    }
}
