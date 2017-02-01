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
        double oldTimeSeconds = 0;
        double pauseTime = 0;
        double newTimeSeconds = 0;
        double timeDiff = 0;
        double countDown = 1;

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
                oldTimeSeconds = (DateTime.Now.Ticks / 10000) / 1000;
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

        public void SetGameTime(double newTimeSeconds) {
            oldTimeSeconds = newTimeSeconds;
        }

        public double getGameTime() {
            return oldTimeSeconds;
        }

        public double getPauseTime() {
            return pauseTime;
        }

        public void setPauseTime(double newTime) {
            pauseTime = newTime;
        }

        public double getNewTime()
        {
            return newTimeSeconds;
        }

        public void setNewTime(double newTime)
        {
            newTimeSeconds = newTime;
        }

        public void setTimeDiff(double newTime) {
            timeDiff = newTime;
        }

        public double getTimeDiff() {
            return timeDiff;
        }

        public void setCountDown(double cd) {
            countDown = cd;
        }

        public double getCountDown() {
            return countDown;
        }

    }
}
