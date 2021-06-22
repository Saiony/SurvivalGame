using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

public class Consumable : Item
{
    public Consumable(string id, string name, string description, Sprite image, ConsumableActions commandEnum, int quantity = 1) : base(id, name, description, image, quantity)
    {
        var command = ConsumableHelper.NewCommand(commandEnum);
        SetCommand(command);
    }
}