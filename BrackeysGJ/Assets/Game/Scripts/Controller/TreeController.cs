using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Controller.Interact;
using UnityEngine;
using DG.Tweening;

public class TreeController : Interactable
{
    [SerializeField]
    private Transform _log = null;
    private Transform Log => _log;

    [SerializeField]
    private Transform _leaves = null;
    private Transform Leaves => _leaves;

    private int Life = 3;
    private bool Alive = true;

    protected override void OnInteract(Vector3 pos)
    {
    }

    protected override void OnPlayerEnter()
    {
    }

    protected override void OnPlayerExit()
    {
    }

    protected override void OnChop(Vector3 pos)
    {
        Debug.Log("Tree Chopped \tLife: " + Life);
        if (!Alive)
            return;

        Life--;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOPunchScale(Vector3.one * 0.03f, 0.3f, 7, 5));

        if (Life <= 0)
            seq.Append(Die());
    }

    private Tween Die()
    {
        Alive = false;
        Sequence seq = DOTween.Sequence();

        seq.Append(Leaves.DOScale(0, 0.35f));
        seq.Append(Log.DOLocalRotate(new Vector3(0, 0, -100), 1f));
        return seq;
    }
}
