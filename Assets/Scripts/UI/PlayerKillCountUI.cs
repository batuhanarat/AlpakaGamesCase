using TMPro;
using UnityEngine;

public class PlayerKillCountUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _killText;
    private int _killCount;
    private EventBinding<EnemyDiedEvent> enemyDiedEventBinding;

    void Start() {
        _killText.text = _killCount.ToString();
    }
    void OnEnable() {
        enemyDiedEventBinding = new EventBinding<EnemyDiedEvent>(OnPlayerKillCountChanged);
        EventBus<EnemyDiedEvent>.Register(enemyDiedEventBinding);
    }
    void OnDisable() {
        EventBus<EnemyDiedEvent>.Deregister(enemyDiedEventBinding);
    }

    public PlayerKillCountUI(TextMeshProUGUI killText) {
        _killText = killText;
        _killCount = 0;
        killText.text = _killCount.ToString();
    }

    public void OnPlayerKillCountChanged() {
        _killCount++;
        _killText.text = _killCount.ToString();
    }
}