using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Message;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Message
{
    public class HpMessage : IHpMessage
    {
        public int Hp { get; private set; }

        public HpMessage(int hp)
        {
            Hp = hp;
        }
    }

    public class StaminaMessage : IStaminaMessage
    {
        public int Stamina { get; private set; }

        public StaminaMessage(int stamina)
        {
            Stamina = stamina;
        }
    }
}