using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class GameTimer
    {
        public GameTimer(string name,double howLong, Action T) { //in seconds
            initTime = howLong - 1; //0 till 9 = 10 seconds
            countDown = howLong - 1 ;
            timerEvent = T;

        }

        public void updateTimer() {
            if (start == true)
            {
                //timer update
                if (countDown > 0)
                {
                    //as long as you are not out of time
                    newTimeSeconds = ((DateTime.Now.Ticks / 10000000) - timeDiff);
                    countDown = (initTime - (newTimeSeconds - oldTimeSeconds));

                }
                else
                {
                    timerFinished = true;
                    start = false;
                }
            }
        }

        public void stopTimer() {
            start = false;
        }

        public void restartTimer() {
            timerFinished = false;
            countDown = initTime;
            timeDiff = 0;
            oldTimeSeconds = (DateTime.Now.Ticks / 10000000);
        }

        public double getInitTime() {
            return initTime;
        }

        public void startTimer() {
            start = true;
        }

        public void PauseTimer() {
            pauseTime = newTimeSeconds;
            double a = pauseTime;
            double b = DateTime.Now.Ticks / 10000000;
            timeDiff = b - a;
        }

        double oldTimeSeconds = 0;
        double pauseTime = 0;
        double newTimeSeconds = 0;
        double timeDiff = 0;
        double countDown = 1;
        double currentTime = 0;
        double initTime = 0; //needed to restart
        bool start = false;
        bool timerFinished = false;
        Action timerEvent;


        public bool getStart()
        {
            return start;
        }

        //public void updateTask(Task t) {
        //    timerEvent = t;
        //}

        public void doTask() {
            
            timerEvent();
        }

        public void setCurrentTime(double newTimeSeconds)
        {
            currentTime = newTimeSeconds;
        }

        public double getCurrentTime()
        {
            return currentTime;
        }

        public void setOldGameTime(double newTimeSeconds)
        {
            oldTimeSeconds = newTimeSeconds;
        }

        public double getOldGameTime()
        {
            return oldTimeSeconds;
        }

        public double getPauseTime()
        {
            return pauseTime;
        }

        public void setPauseTime(double newTime)
        {
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

        public void setTimeDiff(double newTime)
        {
            timeDiff = newTime;
        }

        public double getTimeDiff()
        {
            return timeDiff;
        }

        public void setCountDown(double cd)
        {
            countDown = cd;
        }

        public double getCountDown()
        {
            return countDown;
        }



    }
}
