using System;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;

public static class WeaponHelper
{
    public static Command NewCommand(WeaponActions actionEnum, List<Attack> attacks)
    {
        switch (actionEnum)
        {
            case WeaponActions.Attack:
                return new AttackCommand(attacks);
            default:
                throw new InvalidOperationException("Invalid Tool Action");
        }
    }
}

public enum WeaponActions
{
    Unknown = 0,
    Attack = 1,
    Chop = 2
}

