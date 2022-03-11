using System;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Player
{
    public class Hp
    {
        public int Max { get; private set; }
        public int Current { get; private set; }

        private Hp()
        {
            Max = 0;
            Current = 0;
        }

        public Hp(int current, int max) : this()
        {
            SetMax(max);
            SetCurrent(current);
        }

        private void SetMax(int max)
        {
            if(max <= 0)
                throw new InvalidOperationException("Max must be a positive value");
            
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

        public void Increase(int value)
        {
            Current += value;

            if(Current > Max)
                Current = Max;
        }
    }
}