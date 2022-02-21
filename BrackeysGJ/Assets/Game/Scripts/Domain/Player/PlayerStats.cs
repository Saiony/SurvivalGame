using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Player
{
    public class PlayerStats : IPlayerStats
    {
        public int Life { get; private set; }
        public int Stamina { get; private set; }

        private PlayerStats()
        {
            Life = 0;
            Stamina = 0;
        }

        public PlayerStats(int life, int stamina) : this()
        {
            SetLife(life);
            SetStamina(stamina);
        }

        private void SetLife(int life)
        {
            if(life < 0)
                throw new InvalidOperationException("Life can't be negative");
            
            Life = life;
        }

        private void SetStamina(int stamina)
        {
            if(Stamina < 0)
                throw new InvalidOperationException("Stamina can't be negative");
            
            Stamina = stamina;
        }

        public void DecreaseLife(int value)
        {
            Life -= value;
        }
    }
}