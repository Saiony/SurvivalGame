using System;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Player
{
    public class Stamina
    {
        public int Max { get; private set; }
        public int Current { get; private set; }

        private Stamina()
        {
            Max = 0;
            Current = 0;
        }

        public Stamina(int current, int max) : this()
        {
            SetMax(max);
            SetCurrent(current);
        }

        private void SetMax(int max)
        {
            if(Max < 0)
                throw new InvalidOperationException("Max can't be negative");
            
            Max = max;
        }

        private void SetCurrent(int current)
        {   
            if(current < 0)
                throw new InvalidOperationException("Current can't be negative");

            Current = current;
        }

        public void Decrease(int value)
        {
            Current -= value;
            
            if(Current < 0)
                Current = 0;
        }
    }
}