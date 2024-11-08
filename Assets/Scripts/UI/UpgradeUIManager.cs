using DependencyInjection;
using TMPro;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour, IDependencyProvider
{
    [Provide] public UpgradeUIManager ProvideUpgradeManager() {
        return this;
    }
    [SerializeField] private TextMeshProUGUI upgradeCountText;

    public void Prepare(float cost, bool canAfford ) {
        upgradeCountText.text = cost.ToString();
        if(canAfford) {
            upgradeCountText.color = Color.green;
        } else {
            upgradeCountText.color = Color.red;
        }
    }

}