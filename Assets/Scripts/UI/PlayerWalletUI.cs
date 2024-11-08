using TMPro;
using UnityEngine;

public class PlayerWalletUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI balanceText;
    private EventBinding<BalanceChangedEvent> balanceChangedBinding;

    void OnEnable() {
        balanceChangedBinding = new EventBinding<BalanceChangedEvent>(OnBalanceChanged);
        EventBus<BalanceChangedEvent>.Register(balanceChangedBinding);
    }
    void OnDisable() {
        EventBus<BalanceChangedEvent>.Deregister(balanceChangedBinding);
    }

    public void OnBalanceChanged(BalanceChangedEvent newGemCount) {
        balanceText.text = newGemCount.newBalance.ToString();
    }
}