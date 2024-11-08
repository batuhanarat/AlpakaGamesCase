using UnityEngine;

[CreateAssetMenu(fileName ="WeaponConfig", menuName ="Configs/WeaponConfig")]
public class WeaponConfig : ScriptableObject {
    public float cooldownTime;
    public float attackRange;
    public float attackRate;

}