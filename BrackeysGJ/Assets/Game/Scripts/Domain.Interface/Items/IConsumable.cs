using System;

namespace Game.Scripts.Domain.Interface.Items
{
    public interface IConsumable : IItem
    {
        int HungerSatisfied { get; }
        int HealthGiven { get; }
    }
}
