using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

public class Weapon : Item
{
    public List<Attack> Attacks { get; private set; }

    public Weapon(string id, string name, string description, Sprite image, WeaponActions commandEnum, List<Attack> attacks, int quantity = 1) : base(id, name, description, image, quantity)
    {
        SetAttacks(attacks);
        var command = WeaponHelper.NewCommand(commandEnum, Attacks);
        SetCommand(command);
    }

    private void SetAttacks(List<Attack> attacks)
    {
        if (attacks == null)
            throw new InvalidOperationException("Attacks can't be null");

        Attacks = attacks.ToList();
    }
}