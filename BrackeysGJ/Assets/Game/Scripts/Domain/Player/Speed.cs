using System;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Player
{
    public class Speed
    {
        public int Value { get; private set; }

        private Speed()
        {
            Value = 5;
        }

        public Speed(int speed) : this()
        {
            SetValue(speed);
        }
        
        private void SetValue(int speed)
        {
            if(speed <= 0)
                throw new InvalidOperationException("Speed must be a positive value");

            Value = speed;
        }

        public void Increase(int value)
        {
            Value += value;
        }

        public void Decrease(int value)
        {
            Value -= value;

            if(Value < 0)
                Value = 0;
        }
    }
}