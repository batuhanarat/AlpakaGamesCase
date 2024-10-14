
using System;

public interface IHealth
{
    float CurrentHealth { get;  set; }
    void Prepare(Action OnKilled);
    void GetHit(float damage);
    void Die();
}

public class EnemyHealth : IHealth
{
    public float CurrentHealth { get; set; }
    private Action _onKilled;
    public void Prepare(Action OnKilled)
    {
        _onKilled = OnKilled;
    }
    public void Die()
    {
        _onKilled();
    }
    public void GetHit(float damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth<=0) {
            Die();
        }
    }
}

public class PlayerHealth : IHealth
{
    public float CurrentHealth { get ; set; }
    private Action<float> onDamage;
    private Action onKilled;

    public void Die()
    {
        onKilled();
    }

    public void GetHit(float damage)
    {
        CurrentHealth--;
        onDamage(CurrentHealth);
        if(CurrentHealth<=0) {
            Die();
        }
    }

    public void SetUICallback(Action<float> OnDamage) {
        onDamage = OnDamage;
    }

    public void Prepare(Action OnKilled)
    {
        onKilled = OnKilled;
    }
}