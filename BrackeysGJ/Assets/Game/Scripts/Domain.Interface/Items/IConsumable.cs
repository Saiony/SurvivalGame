using System;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items
{
    public interface IConsumable : IItem
    {
        int HungerSatisfied { get; }
        int HealthGiven { get; }
    }
}
