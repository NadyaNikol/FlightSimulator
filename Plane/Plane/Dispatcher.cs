using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneFlight
{
    class Dispatcher
    {
        public event PropertyEventHandler PropertyMoreHeight;
        public event PropertyEventHandler PropertyMoreSpeed;
        public event PropertyEventHandler PropertyMorePenalty;
        public event PropertyEventHandler PropertyZeroHeightOrSpeed;

        int penalty = 0, controlspeed, controlheight;

        public string Name { get; }

        public int Penalty
        {
            set
            {
                penalty = value;
                if (value > 1000)
                    PropertyMorePenalty(this, new PropertyEventArgs("Непригоден к полетам"));
            }


            get { return penalty; }
        }

        public int ControlSpeed
        {
            set
            {
                controlspeed = value;
                if (value == 0)
                {
                    PropertyZeroHeightOrSpeed(this, new PropertyEventArgs("Самолет разбился"));
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
            }
            get { return controlspeed; }
        }

        public int ControlHeight
        {
            set
            {
                controlheight = value;
                if (value == 0)
                {
                    PropertyMoreHeight(this, new PropertyEventArgs("Самолет разбился"));
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
            }
            get { return controlheight; }
        }


        public Dispatcher() { }
        public Dispatcher(string name) { this.Name = name; }

        public int RecommendedHeight(int speed)
        {
            int N = GetRandom(-200, 200);

            return 7 * speed - N;
        }

        public int PenaltyPointsHeight(int difference)
        {
            if (difference >= 300 && difference <= 600) return 25;
            else if (difference >= 600 && difference <= 1000) return 50;
            else if (difference > 1000)
            {
                PropertyMoreHeight(this, new PropertyEventArgs("Самолет разбился"));
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }


            return 0;
        }

        public int PenaltyPointsSpeed()
        {
            PropertyMoreSpeed(this, new PropertyEventArgs("Скорость превышает 1000 км/ч. " +
                "Немедленно сбавте скорость!!!"));

            return 100;
        }


        readonly Random rnd = new Random();
        readonly Object mylock = new Object();

        int GetRandom(int min, int max)
        {
            lock (mylock)
                return rnd.Next(min, max);

        }

    }
}