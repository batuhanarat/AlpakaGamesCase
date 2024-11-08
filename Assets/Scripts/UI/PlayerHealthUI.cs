using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    private EventBinding<PlayerHitEvent> playerHitEventBinding;
    private float initialHealth = 10f;

    void Awake( ) {
        _healthText.text = initialHealth.ToString();
    }
    void OnEnable() {
        playerHitEventBinding = new EventBinding<PlayerHitEvent>(OnPlayerHealthChanged);
        EventBus<PlayerHitEvent>.Register(playerHitEventBinding);
    }
    void OnDisable() {
        EventBus<PlayerHitEvent>.Deregister(playerHitEventBinding);
    }

    public void OnPlayerHealthChanged(PlayerHitEvent playerHitEvent) {
        _healthText.text = playerHitEvent.newHealth.ToString();
    }
}