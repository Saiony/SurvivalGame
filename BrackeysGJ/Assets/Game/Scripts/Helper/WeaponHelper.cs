using System;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;

public static class WeaponHelper
{
    public static Command NewCommand(WeaponActions actionEnum, Attack attack)
    {
        switch (actionEnum)
        {
            case WeaponActions.Attack:
                return new AttackCommand(attack);
            default:
                throw new InvalidOperationException("Invalid Tool Action");
        }
    }
}

public enum WeaponActions
{
    Unknown = 0,
    Attack = 1
}

