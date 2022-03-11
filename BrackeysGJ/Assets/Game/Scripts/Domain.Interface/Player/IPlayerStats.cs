using BrackeysGJ.Assets.Game.Scripts.Domain.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Player
{
    public interface IPlayerStats
    {
        bool Dead { get; set; }
        bool Running { get; set; }
        
        Hp Hp { get; }
        Stamina Stamina { get; }
        FoodLevel FoodLevel { get; }
        Speed Speed { get; }
    }
}