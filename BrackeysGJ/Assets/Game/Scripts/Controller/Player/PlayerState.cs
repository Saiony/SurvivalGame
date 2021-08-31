using System;
using System.Collections;
using System.Linq;
using Game.Scripts.Controller.Player;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.Player
{
    public interface IPlayerState
    {
        void End();
    }

    public class PlayerIdleState : IPlayerState
    {
        public void End()
        {
        }
    }

    public class PlayerAttackState : IPlayerState, IDetectionListener
    {
        private Attack Attack { get; set; }
        private HandController HandController { get; set; }

        public void BeginAttack(PlayerController player, Attack attack, HandController HandController)
        {
            SetAttack(attack);
            SetHandController(HandController);

            InputHandler.Instance.DisableInput();
            player.Animator.SetTrigger("Attacking_Trigger");

            player.Animator.SetTrigger("Attacking");
        }

        public void EnableAttack()
        {
            HandController.EnableDetection(this);
        }

        public void DisableAttack()
        {
            HandController.DisableDetection();
        }

        public void End()
        {

        }

        public void OnDetect(IDamageable interactable)
        {
            //Faz o ataque de fato
            interactable.ReceiveAttack(Attack);
        }

        private void SetAttack(Attack attack)
        {
            if (attack == null)
                throw new InvalidOperationException("Attack can't be null");

            Attack = attack;
        }

        private void SetHandController(HandController handController)
        {
            if (handController == null)
                throw new InvalidOperationException("HandController can't be null");

            HandController = handController;
        }
    }
}
