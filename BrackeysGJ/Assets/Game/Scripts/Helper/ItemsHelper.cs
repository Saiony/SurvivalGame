using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrackeysGJ.Assets.Game.Scripts.Domain.Items;
using Game.Scripts.ScriptableObjects;

namespace Game.Scripts.Helper
{
    public static class ItemsHelper
    {
        public static Item CreateItem(ItemSO itemSO)
        {
            switch (itemSO)
            {
                case ToolSO t:
                    var tool = new Tool(t.Id, t.Name, t.Description, t.Image, t.Command);
                    return tool;
                case ConsumableSO c:
                    var consumable = new Consumable(c.Id, c.Name, c.Description, c.Image, c.Command, c.HungerSatisfied, c.HealthGiven);
                    return consumable;
                case MiscSO m:
                    var misc = new Misc(m.Id, m.Name, m.Description, m.Image);
                    return misc;
                case WeaponSO w:
                    var attack = new Attack(w.DamagesType, w.DamagesValue);
                    var weapon = new Weapon(w.Id, w.Name, w.Description, w.Image,
                                            w.Command, attack, w.Slot, w.Prefab);
                    return weapon;
                default:
                    throw new InvalidOperationException("Invalid item type");
            }
        }
    }
}