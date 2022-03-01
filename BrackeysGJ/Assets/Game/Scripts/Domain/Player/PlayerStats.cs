using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Player
{
    public class PlayerStats : IPlayerStats
    {
        public bool Dead { get; set; }
        public Hp Hp { get; private set; }
        public Stamina Stamina { get; private set; }

        private PlayerStats()
        {
            Dead = false;
            Hp = null;
            Stamina = null;
        }

        public PlayerStats(Hp life, Stamina stamina) : this()
        {
            SetLife(life);
            SetStamina(stamina);
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
                throw new InvalidOperationException("Stamin can't be null");
            
            Stamina = stamina;
        }
    }
}