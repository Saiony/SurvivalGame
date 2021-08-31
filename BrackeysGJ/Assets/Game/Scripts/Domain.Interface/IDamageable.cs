public interface IDamageable : IBaseInteractable
{
    int Life { get; }

    void ReceiveAttack(Attack attack);
}