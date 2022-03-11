using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;
using BrackeysGJ.Assets.Game.Scripts.Domain.Player;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Message
{
    public class HpMessage : IHpMessage
    {
        public Hp Hp { get; private set; }

        public HpMessage(Hp hp)
        {
            Hp = hp;
        }
    }

    public class StaminaMessage : IStaminaMessage
    {
        public Stamina Stamina { get; private set; }

        public StaminaMessage(Stamina stamina)
        {
            Stamina = stamina;
        }
    }

    public class FoodLevelMessage : IFoodLevelMessage
    {
        public FoodLevel FoodLevel { get; private set; }

        public FoodLevelMessage(FoodLevel foodLevel)
        {
            FoodLevel = foodLevel;
        }
    }
}