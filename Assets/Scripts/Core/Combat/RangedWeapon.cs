using UnityEngine;

public class RangedWeapon : MonoBehaviour {
    public WeaponConfig weaponConfig;
    public bool CanAttack => Time.time - lastAttackTime >= CooldownTime;
    private float CooldownTime => weaponConfig.cooldownTime;
    private float AttackRate => weaponConfig.attackRate;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;



    private float lastAttackTime;

    public void Attack(EnemyController target)
    {
        if (!CanAttack) return;
        FireBulletTo(target);
        lastAttackTime = Time.time;
    }
    private void FireBulletTo(EnemyController target)
    {
        var bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        var bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.Initialize(target.transform.position,AttackRate);

    }

}
