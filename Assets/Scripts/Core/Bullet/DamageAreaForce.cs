using UnityEngine;

public class DamageAreaForce : MonoBehaviour
{
    public float expansionSpeed;
    private bool _isFired;
    private Collider[] _targetsColliders;
    private float _maxRadius;
    private int _damage;
    private float _currentScale = 0f;
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if(!_isFired) return;

        if (_currentScale < _maxRadius)
        {
            _currentScale += expansionSpeed * Time.deltaTime;
            transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
        } else
        {
            foreach(var collider in _targetsColliders)
            {
                var enemy = collider.GetComponent<EnemyController>();
                enemy.GetHit(_damage);
            }
            Destroy(gameObject);
        }

    }
    public void Fire(Collider[] targetColliders,float range, int damage)
    {
        _isFired = true;
        _maxRadius = range;
        _damage = damage;
        _targetsColliders = targetColliders;
    }
}