using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletConfig bulletConfig;

    private Vector3 targetPosititon;
    private Vector3 direction;
    private float attackRate;



    private string targetTag;

    //private BulletManager manager = new();

    public void Initialize(Vector3 targetPosition,float attackRate) {
        this.attackRate = attackRate;
        this.targetPosititon = targetPosition;
        this.direction = (targetPosition - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        if(HasTraveledMaxDistance()) {
            Destroy(gameObject);
        }
        transform.position += bulletConfig.moveSpeed * Time.deltaTime * direction;

    }

    public bool HasTraveledMaxDistance() {
        float distanceTraveledSq = (targetPosititon - transform.position).sqrMagnitude;
        return distanceTraveledSq >= bulletConfig.maxDistance * bulletConfig.maxDistance;
    }

    public  void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy")) {
            var target = other.gameObject.GetComponent<EnemyController>();
            target.GetHit(attackRate);
        }
    }


}
