using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private EnemyAnimator animator;
    [SerializeField] public Transform rangeFirePoint;
    [SerializeField] public Material red_material;

    public NavMeshAgent navMeshAgent;

    private IHealth Health;

    [SerializeField] private LayerMask _targetLayer;
    public LayerMask TargetLayer
    {
        get => _targetLayer;
        set => _targetLayer = value;
    }
    public Transform TargetLocation { get ; set; }
    private bool _isDead = false;

    void Awake()
    {
        Health = new EnemyHealth(enemyConfig.health);
    }

    public void Die() {
        _isDead = true;
        animator.DeadAnimation();
        Gem.Create(transform,100);
       // ServiceProvider.Pool.ReturnToPool(PoolableType.Enemy,gameObject);
    }

    public void Attack()
    {
       // Weapon.Shoot(animator,TargetLayer,this);
    }
    public void GetHit(float damage)
    {
        Health.GetDamage(damage);
      //  DamagePopUp.Create(transform, damage, damage > 50);
    }
}