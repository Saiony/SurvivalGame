using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;
using System.Collections;
using Game.Scripts.ScriptableObjects.Environment;
using System.Collections.Generic;

public class TreeController : MonoBehaviour, IDamageable
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject _treePrefab = null;
    private GameObject TreePrefab => _treePrefab;

    [SerializeField]
    private GameObject _logPrefab = null;
    private GameObject LogPrefab => _logPrefab;

    [SerializeField]
    private GameObject _logInHalfPrefab = null;
    private GameObject LogInHalfPrefab => _logInHalfPrefab;

    [SerializeField]
    private GameObject _stumpPrefab = null;
    private GameObject StumpPrefab => _stumpPrefab;

    [SerializeField]
    private GameObject _woodItemPrefab = null;
    private GameObject WoodItemPrefab => _woodItemPrefab;

    [Header("State")]
    [SerializeField]
    private TreeState _state = TreeState.Unknown;
    private TreeState State => _state;

    [SerializeField]
    private Collider _detectionCollider = null;
    public Collider DetectionCollider => _detectionCollider;

    [SerializeField]
    private TreeSO _treeSO = null;
    private TreeSO TreeSO => _treeSO;

    public int Life { get; private set; }
    private Dictionary<DamageType, int> Resistances { get; set; }
    private bool Alive = true;

    private void Awake()
    {
        SetLife();
        SetResistances();
    }

    public void ReceiveAttack(Attack attack)
    {
        if (!Alive)
            return;

        foreach (var damage in attack.Damages)
        {
            var multiplier = 1;
            if (Resistances.ContainsKey(damage.Key))
                multiplier = Resistances[damage.Key];

            Life -= damage.Value * multiplier;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOPunchScale(Vector3.one * 0.03f, 0.3f, 7, 5));

        if (Life <= 0)
        {
            StartCoroutine(Die());
            return;
        }
        Debug.Log("Tree Chopped \tLife: " + Life);
    }

    private IEnumerator Die()
    {
        Alive = false;
        yield return new WaitForSeconds(0.15f);
        switch (State)
        {
            case TreeState.Tree:
                var logOffset = Vector3.up * 1.4f;
                Instantiate(LogPrefab, transform.position + logOffset, Quaternion.Euler(UnityEngine.Random.Range(-3f, 3f), 0, UnityEngine.Random.Range(-1.5f, 1.5f)));
                Instantiate(StumpPrefab, transform.position, transform.rotation);
                break;
            case TreeState.Log:
                var logInHalfOffset = transform.TransformDirection(Vector3.up) * 0.5f;
                Instantiate(LogInHalfPrefab, transform.position + logInHalfOffset, transform.rotation);
                Instantiate(LogInHalfPrefab, transform.position - logInHalfOffset, transform.rotation);
                break;
            case TreeState.LogInHalf:
                Instantiate(WoodItemPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            case TreeState.Stump:
                Instantiate(WoodItemPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            default:
                throw new InvalidOperationException("Invalid TreeState: " + State);
        }
        Destroy(gameObject);
    }

    private void SetLife()
    {
        switch (State)
        {
            case TreeState.Tree:
                Life = TreeSO.TreeLife;
                break;
            case TreeState.Log:
                Life = TreeSO.LogLife;
                break;
            case TreeState.LogInHalf:
                Life = TreeSO.LogInHalfLife;
                break;
            case TreeState.Stump:
                Life = TreeSO.StumpLife;
                break;
            default:
                throw new InvalidOperationException("Invalid tree state: " + State);
        }
    }

    private void SetResistances()
    {
        Resistances = new Dictionary<DamageType, int>();
        if (TreeSO.DamagesType.Count != TreeSO.DamageMultiplier.Count)
            throw new InvalidOperationException("Both lists must have the same length");

        for (int i = 0; i < TreeSO.DamagesType.Count; i++)
        {
            if (TreeSO.DamageMultiplier[i] <= 0)
                throw new InvalidOperationException("Invalid damage: " + TreeSO.DamageMultiplier[i]);
            if (TreeSO.DamagesType[i] == DamageType.Unknown)
                throw new InvalidOperationException("Invalid type: " + TreeSO.DamagesType[i]);

            Resistances.Add(TreeSO.DamagesType[i], TreeSO.DamageMultiplier[i]);
        }
    }

    private enum TreeState
    {
        Unknown = 0,
        Tree = 1,
        Log = 2,
        LogInHalf = 3,
        Stump = 4
    }
}
