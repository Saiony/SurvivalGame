using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class AttackCommand : Command
    {
        public Attack Attack { get; private set; }
        public override string Name { get; set; } = "Attack";

        private AttackCommand()
        {
            Attack = null;
        }

        public AttackCommand(Attack attack) : this()
        {
            SetAttack(attack);
        }

        private void SetAttack(Attack attack)
        {
            if (attack == null)
                throw new InvalidOperationException("Attack can't be null");

            Attack = attack;
        }

        public override void Execute()
        {
            PlayerController.Instance.Attack(Attack);
        }
    }
}