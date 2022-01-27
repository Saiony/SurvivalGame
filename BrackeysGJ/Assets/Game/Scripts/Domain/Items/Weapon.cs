using System;
using BrackeysGJ.Assets.Game.Scripts.Domain.Interface.Items;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Domain.Items
{
    public class Weapon : Equipment, IWeapon
    {
        public Attack Attack { get; private set; }

        public Weapon(string id, string name, string description, Sprite image, WeaponActions commandEnum, Attack attack, 
                      EquipmentSlot slot, GameObject prefab, int quantity = 1)
                      : base(id, name, description, image, slot, quantity, prefab)
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
}