using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Player;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;
    private Animator Animator => _animator;

    [SerializeField]
    private PlayerController _playerController = null;
    private PlayerController PlayerController => _playerController;

    private PlayerAnimationType currentState { get; set; }

    public void ChangeAnimationState(PlayerAnimationType animationType)
    {
        if (currentState == animationType)
            throw new InvalidOperationException("CurrentState is equal to new animation");

        Animator.Play(animationType.ToString());
        currentState = animationType;
    }

    public void ReleaseInput()
    {
        InputHandler.Instance.EnableInput();
        PlayerController.HandController.EndAction();
    }

    public void Water()
    {
        PlayerController.DoTheActualWaterThing();
    }

    public void Plant()
    {
        PlayerController.DoTheActualPlantThing();
    }

    public void Plow()
    {
        PlayerController.DoTheActualPlowThing();
    }

    public void Attack()
    {

    }

    public void AttackComParametro(System.Object x)
    {

    }
}

//Must match animation names
public enum PlayerAnimationType
{
    Uknown,
    Idle,
    Running,
    Plowing,
    Watering,
    Sowing
}
