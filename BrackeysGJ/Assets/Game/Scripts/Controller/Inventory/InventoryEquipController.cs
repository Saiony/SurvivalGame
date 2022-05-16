using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Controller.Player;
using Game.Scripts.Domain.Interface.Items;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Inventory
{
    public class InventoryEquipController : MonoBehaviour
    {
        [SerializeField]
        private EquipItemDisplayController _headEquip = null;
        private EquipItemDisplayController HeadEquip => _headEquip;

        [SerializeField]
        private EquipItemDisplayController _leftHandEquip = null;
        private EquipItemDisplayController LeftHandEquip => _leftHandEquip;

        [SerializeField]
        private EquipItemDisplayController _rightHandEquip = null;
        private EquipItemDisplayController RightHandEquip => _rightHandEquip;

        [SerializeField]
        private EquipItemDisplayController _armorEquip = null;
        private EquipItemDisplayController ArmorEquip => _armorEquip;

        [SerializeField]
        private EquipItemDisplayController _legsEquip = null;
        private EquipItemDisplayController LegsEquip => _legsEquip;

        [SerializeField]
        private EquipItemDisplayController _bootsEquip = null;
        private EquipItemDisplayController BootsEquip => _bootsEquip;

        private Dictionary<EquipmentSlot, EquipItemDisplayController> Equipments { get; set; }

        public void Init(InventoryItemDisplayListener itemListener)
        {
            Equipments = new Dictionary<EquipmentSlot, EquipItemDisplayController>();
            Equipments.Add(EquipmentSlot.Head, HeadEquip);
            Equipments.Add(EquipmentSlot.Left_Hand, LeftHandEquip);
            Equipments.Add(EquipmentSlot.Right_Hand, RightHandEquip);
            Equipments.Add(EquipmentSlot.Torso, ArmorEquip);
            Equipments.Add(EquipmentSlot.Legs, LegsEquip);
            Equipments.Add(EquipmentSlot.Feet, BootsEquip);

            Equipments.ToList().ForEach(x => x.Value.Init(x.Key, itemListener));
        }

        public void Clear()
        {
            Equipments.ToList().ForEach(item => item.Value.Clear());
        }

        public void Display(Dictionary<EquipmentSlot, IEquipment> PlayerEquips)
        {
            var playerEquips = PlayerController.Instance.Items.EquippedItems.Equipments;
            playerEquips.ToList().ForEach(playerEquip =>
            {
                Equipments[playerEquip.Key].DisplayItem(playerEquip.Value);
            });
        }

        public void BlockInvalidSlots(IItem selectedItem)
        {
            var equipsCopy = Equipments.ToDictionary(x => x.Key, x => x.Value);

            if (selectedItem is IEquipment)
            {
                var slot = (selectedItem as IEquipment).Slot;
                equipsCopy.Remove(slot);
            }
            foreach (var equipDisplay in equipsCopy.Values)
            {
                equipDisplay.Block();
            }
        }

        public void UnblockInvalidSlots(IItem selectedItem)
        {
            var equipsCopy = Equipments.ToDictionary(x => x.Key, x => x.Value);

            if (selectedItem is IEquipment)
            {
                var slot = (selectedItem as IEquipment).Slot;
                equipsCopy.Remove(slot);
            }

            foreach (var equipDisplay in equipsCopy.Values)
            {
                equipDisplay.Unblock();
            }
        }
    }
}
