using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Helper;
using UnityEngine;

public class SeasonWheelVfx : MonoBehaviour
{
    [SerializeField]
    private Transform _arrow = null;
    private Transform Arrow => _arrow;

    [SerializeField]
    private CanvasGroup[] _seasons = null;
    private CanvasGroup[] Seasons => _seasons;

    public Tween SetCurrent(int seasonIndex)
    {
        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < Seasons.Length; i++)
        {
            if (i == seasonIndex)
                seq.Join(Seasons[i].DOFade(1f, 0.3f));
            else
                seq.Join(Seasons[i].DOFade(0.03f, 0.3f));
        }
        return seq;
    }
}
