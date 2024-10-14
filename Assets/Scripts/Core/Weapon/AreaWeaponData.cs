using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/Weapons/Area Weapon")]
public class AreaWeaponData : WeaponData
{

    public override void Shoot(Transform attacker, LayerMask targetLayer, IHumanoidController controller)
    {
        Collider[] hitColliders = Physics.OverlapSphere(attacker.position, attackRange, targetLayer);
        if (hitColliders.Length > 0)
        {
            GameManager.CurrentState = GameState.COMBAT;
           // InitializeDamageZone(hitColliders,attacker);
        } else {
            GameManager.CurrentState = GameState.PATROL;
        }
    }
    /* private void InitializeDamageZone(Collider[] colliders, Transform attacker)
    {
        GameObject forceZoneInstance = Instantiate(bulletPrefab,attacker.position,Quaternion.identity);
        var forceZone = forceZoneInstance.GetComponent<IceStormForce>();
        forceZone.Fire(colliders,attackRange,damage);
    }
   */
}