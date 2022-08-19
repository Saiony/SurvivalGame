using System;
using BrackeysGJ.Assets.Game.Scripts.ScriptableObjects.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Player
{
    public class FoodLevel
    {
        public int Max { get; private set; }
        public int Current { get; private set; }
        public FoodLevelSO Config { get; private set; }
        public HungerStatus Status => GetHungerStatus();

        private FoodLevel()
        {
            Max =  0;
            Current = 0;
        }

        public FoodLevel(int current, int max, FoodLevelSO foodLevelSO) : this()
        {
            SetMax(max);
            SetCurrent(current);
            SetConfig(foodLevelSO);
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

        private void SetConfig(FoodLevelSO config)
        {
            if(config == null)
                throw new InvalidOperationException("Connfig can't be null");

            Config = config;
        }

        public void Decrease()
        {
            Current -= Config.FoodLevelPerTick;
            
            if(Current < 0)
                Current = 0;
        }

        public void Increase(int value)
        {
            Current += value;

            if(Current > Max)
                Current = Max;
        }

        private HungerStatus GetHungerStatus()
        {
            if((Current <= Config.SatisfiedMilestone * Max) && (Current > Config.NormalMilestone * Max))
                return HungerStatus.Satisfied;
            else if(Current > Config.HungryMilestone * Max)
                return HungerStatus.Normal;
            else if((Current <= Config.HungryMilestone * Max) && (Current > Config.StarvingMilestone * Max))
                return HungerStatus.Hungry;
            else if(Current <= Config.StarvingMilestone * Max)
                return HungerStatus.Starving;
            else
                return HungerStatus.Unknown;
        }
    }

    public enum HungerStatus
    {
        Unknown = 0,
        Starving = 1,
        Hungry = 2,
        Normal = 3,
        Satisfied = 4
    }
}