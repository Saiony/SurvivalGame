using System;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Player.Commands;
using UnityEngine;

namespace Game.Scripts.Domain.Interface.Items
{
    public interface IItem
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        Sprite Image { get; }
        Command Command { get; }
        int Quantity { get; }
        void IncrementQuantity(int quantity);
        bool DecrementQuantity(int quantity);
        void Use();
    }
}
