using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/StaticAreaWeapon")]
public class StaticAreaWeapon : WeaponData
{
    [SerializeField] private GameObject areaBulletPrefab;

    void Start() {

    }
    public override void Shoot(Transform attacker, LayerMask targetLayer, IHumanoidController controller)
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(attacker.position, attackRange, targetLayer);

        if (enemiesInRange.Length > 0)
        {
            GameObject bulletInstance = Instantiate(areaBulletPrefab, attacker.position, Quaternion.identity);

            var staticAreaBullet = bulletInstance.GetComponent<StaticAreaBullet>();

            if (staticAreaBullet != null)
            {
                staticAreaBullet.Fire(enemiesInRange, attackRange, damage);
            }
        }
    }


}