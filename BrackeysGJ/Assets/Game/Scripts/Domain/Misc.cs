using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

public class Misc : Item
{
    public Misc(string id, string name, string description, Sprite image, int quantity = 1) : base(id, name, description, image, quantity)
    {
    }
}