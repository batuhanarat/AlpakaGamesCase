using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public WeaponData weaponData;
    public UpgradeType upgradeType;
    public float upgradeScale;
    public SpriteRenderer Renderer { get; set; }

    void Awake() {
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void UpgradeWeapon() {
        weaponData.Upgrade(upgradeType,upgradeScale);
    }


}
