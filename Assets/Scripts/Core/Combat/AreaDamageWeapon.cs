using System.Collections.Generic;
using UnityEngine;

public class AreaDamageWeapon : MonoBehaviour {
    public WeaponConfig weaponConfig;
    public bool CanAttack => Time.time - lastAttackTime >= CooldownTime;
    private float CooldownTime => weaponConfig.cooldownTime;
    private float AttackRate => weaponConfig.attackRate;
    private float lastAttackTime;

    public void Attack(List<EnemyController> targets)
    {
        if (!CanAttack || targets.Count == 0) return;

        foreach(var target in targets) {
            target.GetHit(AttackRate);
        }
    }

}
