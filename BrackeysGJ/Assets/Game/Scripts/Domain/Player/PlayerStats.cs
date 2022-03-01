using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Player
{
    public class PlayerStats : IPlayerStats
    {
        public bool Dead { get; set; }
        public Hp Hp { get; private set; }
        public Stamina Stamina { get; private set; }
        public int Speed { get; private set; }
        public bool Running { get; set; }

        private PlayerStats()
        {
            Dead = false;
            Hp = null;
            Stamina = null;
            Speed = 0;
        }

        public PlayerStats(Hp life, Stamina stamina, int speed) : this()
        {
            SetLife(life);
            SetStamina(stamina);
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

        public void SetSpeed(int speed)
        {
            if(speed <= 0)
                throw new InvalidOperationException("Speed must be a positive value");

            Speed = speed;
        }
    }
}