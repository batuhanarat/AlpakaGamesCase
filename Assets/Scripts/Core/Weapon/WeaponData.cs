using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public string weaponName;
    public float cooldownTime;
    public float attackRange;
    public float damage;
    public abstract void Shoot(Transform attacker, LayerMask targetLayer, IHumanoidController controller);
    public void Upgrade(UpgradeType upgradeType, float upgradeScale)
    {
        switch(upgradeType) {

            case UpgradeType.RANGE_UPGRADE:
                attackRange += upgradeScale*attackRange;
            break;

            case UpgradeType.COOLDOWN_UPGRADE:
                cooldownTime -= upgradeScale*cooldownTime;
            break;

            case UpgradeType.ATTACK_UPGRADE:
                damage += upgradeScale*damage;
            break;

        }
    }
}