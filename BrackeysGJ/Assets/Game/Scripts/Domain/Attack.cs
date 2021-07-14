
using System;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using Game.Scripts.ScriptableObjects;
using UnityEngine;

[Serializable]
public class Attack
{
    private Dictionary<AttackType, int> Damages { get; set; }

    private Attack()
    {
        Damages = new Dictionary<AttackType, int>();
    }

    public Attack(List<AttackType> attackTypes, List<int> damages) : this()
    {
        SetAttacks(attackTypes, damages);
    }

    private void SetAttacks(List<AttackType> attackTypes, List<int> damages)
    {

    }

    private void SetDamage(int damage)
    {
        if (damage <= 0)
            throw new InvalidOperationException("Invalid damage: " + damage);
        Damages = damage;
    }

    private void SetType(AttackType type)
    {
        if (type == AttackType.Unknown)
            throw new InvalidOperationException("Invalid type: " + type);
        Type = type;
    }
}

public enum AttackType
{
    Unknown = 0,
    Slash = 1,
    Chop = 2
}