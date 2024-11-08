public interface IHealth
{
    float CurrentHealth { get; }
    void GetDamage(float damage);
    void Die();
}

public class EnemyHealth : IHealth
{
    public float CurrentHealth { get; private set; }

    public EnemyHealth(int initialHealth) {
        CurrentHealth = initialHealth;
    }
    public void Die()
    {
        RaiseDiedEvent();
    }
    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;
        RaiseHitEvent(damage);
        if(CurrentHealth<=0) {
            Die();
        }
    }
    private void RaiseHitEvent(float damage)
    {
        EventBus<EnemyHitEvent>.Raise(new EnemyHitEvent{
            damage = damage});
    }
    private void RaiseDiedEvent()
    {
        EventBus<EnemyDiedEvent>.Raise(new EnemyDiedEvent{});
    }
}

public class PlayerHealth : IHealth
{
    public float CurrentHealth { get ; private set; }

    public PlayerHealth(float initialHealth) {
        CurrentHealth = initialHealth;
    }

    public void Die()
    {
        RaiseDiedEvent();
    }

    public void GetDamage(float damage)
    {
        CurrentHealth--;
        RaiseHitEvent();
        if(CurrentHealth<=0) {
            Die();
        }
    }

    private void RaiseHitEvent()
    {
        EventBus<PlayerHitEvent>.Raise(new PlayerHitEvent{
            newHealth = CurrentHealth});
    }
    private void RaiseDiedEvent()
    {
        EventBus<PlayerDiedEvent>.Raise(new PlayerDiedEvent{});
    }
}