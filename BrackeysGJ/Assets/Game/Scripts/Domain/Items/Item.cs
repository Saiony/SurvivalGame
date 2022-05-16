using System;
using Game.Scripts.Controller.Player;
using Game.Scripts.Controller.Player.Commands;
using Game.Scripts.Domain.Interface.Items;
using UnityEngine;

namespace Game.Scripts.Domain.Items
{
    public abstract class Item : IItem
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Image { get; private set; }
        public Command Command { get; private set; }
        public int Quantity { get; private set; }
        public GameObject Prefab { get; private set; }

        public Item()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Image = null;
            Command = null;
            Quantity = 0;
        }

        public Item(string id, string name, string description, Sprite image, int quantity, GameObject prefab = null) : this()
        {
            SetId(id);
            SetName(name);
            SetDescription(description);
            SetImage(image);
            SetQuantity(quantity);
            SetPrefab(prefab);
        }
        
        private void SetId(string id)
        {
            Id = new Guid(id);
        }

        private void SetName(string name)
        {
            if (name == null)
                throw new InvalidOperationException("Name can't be null");

            Name = name;
        }

        private void SetDescription(string description)
        {
            if (description == null)
                throw new InvalidOperationException("Description can't be null");

            Description = description;
        }

        private void SetImage(Sprite image)
        {
            if (image == null)
                throw new InvalidOperationException("Image can't be null");

            Image = image;
        }


        private void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity must be a positive value");

            Quantity = quantity;
        }

        private void SetPrefab(GameObject prefab)
        {
            Prefab = prefab;
        }

        public void IncrementQuantity(int quantity)
        {
            SetQuantity(Quantity + quantity);
        }

        public bool DecrementQuantity(int quantity)
        {
            var value = Quantity - quantity;
            if (value <= 0)
                return false;

            SetQuantity(value);
            return true;
        }

        public virtual void Use()
        {
            if (Command == null)
                return;

            Command.Execute();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return ((Item)obj).Id == Id;
        }

        public void SetCommand(Command command)
        {
            if (command == null)
                return;

            Command = command;
        }
    }
}