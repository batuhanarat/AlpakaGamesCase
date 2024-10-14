using UnityEngine;

public class StaticAreaBullet : MonoBehaviour
{
    private float _damage;
    private Collider[] _targetsColliders;


    public void Fire(Collider[] targetColliders, float range, float damage)
    {
        _damage = damage;
        _targetsColliders = targetColliders;

        foreach (Collider target in _targetsColliders)
        {
            var enemy = target.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.GetHit(_damage);
            }
        }
    }

}

