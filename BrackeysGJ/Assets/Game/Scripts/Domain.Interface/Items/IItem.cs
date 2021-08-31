using System;
using Game.Scripts.Controller.Player;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items
{
    public interface IItem
    {
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
