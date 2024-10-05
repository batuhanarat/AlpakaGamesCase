using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    public float CooldownTime { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float AttackRangeAsRadius { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Damage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public LayerMask LayerMask { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string TargetTag { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Transform WeaponMuzzleTransform;
    //public BulletPrefab BulletPrefab;

    private float turnSpeed = 10f;
    private float bulletSpeed = 20;

    private Collider[] targetBuffer = new Collider[50];

    public void Shoot()
    {
        int numberOfTargets = Physics.OverlapSphereNonAlloc(transform.position, AttackRangeAsRadius, targetBuffer, LayerMask);
        if (numberOfTargets == 0) return;
      //  HitDamageForAllInRange(numberOfTargets);
        FindNearestTarget(numberOfTargets);
    }

    /*private void PistolShoot(Transform targetPosition) {
        var prefabInstance = Instantiate(BulletPrefab,
            WeaponMuzzleTransform,
            Quaternion.identity);

            var rb = prefabInstance.GetComponent<Rigidbody>();

            Vector3 direction = (targetPosition.position - WeaponMuzzleTransform.position).normalized;



            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, turnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            rb.velocity = transform.forward * bulletSpeed;

    }
    */
/*
    private void HitDamageForAllInRange(int numberOfTargets)
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            Collider targetCollider = targetBuffer[i];
            var target = targetCollider.GetComponent<Humanoid>();
            if (target != null)
            {
                target.TakeDamage(Damage);
            }
        }
    }
    */

    private Collider FindNearestTarget(int numberOfTargets)
    {
        Collider nearestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;


        for (int i = 0; i < numberOfTargets; i++)
        {
            Collider targetCollider = targetBuffer[i];


            if (targetCollider.CompareTag(TargetTag))
            {

                Vector3 directionToTarget = targetCollider.transform.position - transform.position;
                float distanceSqr = directionToTarget.sqrMagnitude;

                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    nearestTarget = targetCollider;
                }
            }
        }


        if (nearestTarget != null)
        {
            return nearestTarget;
        }
        return null;
    }
}

public interface IWeapon {
    public float CooldownTime { get; set; }
    public float AttackRangeAsRadius { get; set; }
    public float Damage { get; set; }
    public LayerMask LayerMask { get; set; }
    public string TargetTag { get; set; }
    public void Shoot();
}