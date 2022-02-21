namespace BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Player
{
    public interface IPlayerStats
    {
        int Life { get; }
        int Stamina { get; }

        void DecreaseLife(int value);
    }
}