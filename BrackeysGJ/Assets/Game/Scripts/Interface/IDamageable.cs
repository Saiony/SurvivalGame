public interface IDamageable : IBaseInteractable
{
    int Life { get; }

    void OnDamage(int damage);
}