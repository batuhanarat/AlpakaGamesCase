using UnityEngine;

public class IceStormForce : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    private float _maxRadius;
    private float _damage;
    private float _duration;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particleSystem.main;
        _duration = mainModule.duration;
    }

    public void Fire(Collider[] targetColliders,float range, float damage)
    {
        Debug.Log("We should see the particle system");
        //transform.localScale = transform.localScale * range;
        //var shape = particleSystem.shape;
        //shape.radius = range;
        _damage = damage;
        particleSystem.Play();
        foreach(var collider in targetColliders)
        {
            var enemy = collider.GetComponent<EnemyController>();
            enemy.GetHit(_damage);
        }
        Destroy(gameObject,_duration);
    }
}