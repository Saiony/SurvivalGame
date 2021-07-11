using UnityEngine;
using DG.Tweening;
using System;

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

    public int Life { get; private set; }

    private void Awake()
    {
        Life = 1;
    }

    private bool Alive = true;

    public void OnDamage(int damage)
    {
        Debug.Log("Tree Chopped \tLife: " + Life);
        if (!Alive)
            return;

        Life--;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOPunchScale(Vector3.one * 0.03f, 0.3f, 7, 5));

        if (Life <= 0)
            Die();
    }

    private void Die()
    {
        Alive = false;

        switch (State)
        {
            case TreeState.Tree:
                var logOffset = Vector3.up * 1.4f;
                Instantiate(LogPrefab, transform.position + logOffset, Quaternion.Euler(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-1.5f, 1.5f)));
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


    private enum TreeState
    {
        Unknown = 0,
        Tree = 1,
        Log = 2,
        LogInHalf = 3,
        Stump = 4
    }
}
