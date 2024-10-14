using Game.Managers;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/Ranged Weapon")]
public class RangedWeaponData : WeaponData
{
    public override void Shoot(Transform attacker, LayerMask targetLayer,IHumanoidController controller)
    {
        Collider[] hitColliders = Physics.OverlapSphere(attacker.position, attackRange, targetLayer);
        if (hitColliders.Length > 0)
        {
            GameManager.CurrentState = GameState.COMBAT;
            Transform nearestTarget = FindNearestTargetInWeaponRange(attacker.position, hitColliders);
            if (nearestTarget != null)
            {
                FaceTarget(attacker, nearestTarget,controller);
                FireBullet(attacker, nearestTarget);
            }
        } else {
            GameManager.CurrentState = GameState.PATROL;
        }
    }
    private Transform FindNearestTargetInWeaponRange(Vector3 attackerPosition, Collider[] targets)
    {
        Transform nearestTarget = null;
        float nearestDistanceSqr = Mathf.Infinity;

        foreach (Collider target in targets)
        {
            Vector3 directionToTarget = target.transform.position - attackerPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < nearestDistanceSqr)
            {
                nearestDistanceSqr = dSqrToTarget;
                nearestTarget = target.transform;
            }
        }

        return nearestTarget;
    }
    private void FireBullet(Transform attacker, Transform target)
    {
        var bullet = ServiceProvider.Pool.SpawnFromPool(PoolableType.Bullet);
        bullet.transform.SetPositionAndRotation(attacker.position, Quaternion.identity);
        FollowingBullet followingBullet = bullet.GetComponent<FollowingBullet>();
        followingBullet.Fire(target, damage);
    }
    private void FaceTarget(Transform attacker, Transform targetPosition,IHumanoidController controller)
    {
        Vector3 directionToTarget = (targetPosition.position - attacker.position).normalized;

        directionToTarget.y = 0;

        if (directionToTarget != Vector3.zero)
        {
            controller.TargetLocation = targetPosition;

        }
    }




}