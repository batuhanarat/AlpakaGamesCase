using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public WeaponData weaponData;
    public UpgradeType upgradeType;
    public float upgradeScale;
    [SerializeField]  public SpriteRenderer frame;

    public void Upgrade() {
        weaponData.Upgrade(upgradeType,upgradeScale);
    }


}
