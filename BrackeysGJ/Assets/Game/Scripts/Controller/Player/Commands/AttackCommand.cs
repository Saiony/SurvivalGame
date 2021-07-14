using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Game.Scripts.Controller.Player
{
    public class AttackCommand : Command
    {
        public List<Attack> Attacks { get; private set; }
        public override string Name
        { get; set; } = "Attack";

        private AttackCommand()
        {
            Attacks = new List<Attack>();
        }

        public AttackCommand(List<Attack> attacks)
        {
            SetAttacks(attacks);
        }

        private void SetAttacks(List<Attack> attacks)
        {
            if (attacks == null)
                throw new InvalidOperationException("Attacks can't be null");

            Attacks = attacks.ToList();
        }

        public override void Execute()
        {
            PlayerController.Instance.PlayAttackAnimation();
        }
    }
}