using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Player
{
    public class PlayerStats : IPlayerStats
    {
        public bool Dead { get; set; }
        public Hp Hp { get; private set; }
        public Stamina Stamina { get; private set; }
        public FoodLevel FoodLevel {get; private set; }
        public Speed Speed { get; private set; }
        public bool Running { get; set; }


        private PlayerStats()
        {
            Dead = false;
            Hp = null;
            Stamina = null;
            Speed = null;
        }

        public PlayerStats(Hp life, Stamina stamina, FoodLevel hunger, Speed speed) : this()
        {
            SetLife(life);
            SetStamina(stamina);
            SetHunger(hunger);
            SetSpeed(speed);
        }

        private void SetLife(Hp life)
        {
            if(life == null)
                throw new InvalidOperationException("Life can't be null");
            
            Hp = life;
        }

        private void SetStamina(Stamina stamina)
        {
            if(stamina == null)
                throw new InvalidOperationException("Stamina can't be null");
            
            Stamina = stamina;
        }

        private void SetHunger(FoodLevel hunger)
        {
            if(hunger == null)
                throw new InvalidOperationException("Hunger can't be null");

            FoodLevel = hunger;
        }

        private void SetSpeed(Speed speed)
        {
            if(speed == null)
                throw new InvalidOperationException("Speed can't be null");
            
            Speed = speed;
        }
    }
}