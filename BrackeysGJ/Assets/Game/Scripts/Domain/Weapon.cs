using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

public class Weapon : Item
{
    public Attack Attack { get; private set; }

    public Weapon(string id, string name, string description, Sprite image, WeaponActions commandEnum, Attack attack, int quantity = 1)
                  : base(id, name, description, image, quantity)
    {
        SetAttack(attack);
        var command = WeaponHelper.NewCommand(commandEnum, Attack);
        SetCommand(command);
    }

    private void SetAttack(Attack attack)
    {
        if (attack == null)
            throw new InvalidOperationException("Attack can't be null");

        Attack = attack;
    }
}