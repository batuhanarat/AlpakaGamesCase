using UnityEngine;
public class FollowingBullet : MonoBehaviour
{
public float LifeTimeInSeconds;
private float currentTime = 0;
public float BulletSpeed;
private bool isFired;
private float _damage;
private Transform target;
    /*
    void Update()
    {
    if (!isFired) return;
    currentTime += Time.deltaTime;
    if (target != null)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += BulletSpeed * Time.deltaTime * direction;
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            target.GetComponent<EnemyController>().GetHit(_damage);
            ServiceProvider.Pool.ReturnToPool(PoolableType.Bullet,gameObject);
        }
    }
    if (currentTime >= LifeTimeInSeconds)
        {
            ServiceProvider.Pool.ReturnToPool(PoolableType.Bullet,gameObject);
        }
    }
    */
    public void Fire(Transform targetLocation, float damage)
    {
        isFired = true;
        target = targetLocation;
        _damage = damage;
    }
}