using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private TextMeshProUGUI gemText;

    public PlayerHealthUI playerHealthUI { get; private set; }
    public PlayerKillCountUI playerKillCountUI { get; private set; }
    public PlayerWalletUI playerWalletUI { get; private set; }

    public void Prepare(float initalHealth, int initialGem) {
        playerHealthUI = new PlayerHealthUI(healthText,initalHealth);
        playerKillCountUI = new PlayerKillCountUI(killText);
        playerWalletUI = new PlayerWalletUI(gemText, initialGem);
    }
    public void UpdatePlayerKill() {
        playerKillCountUI.UpdatePlayerKill();
    }

    public void UpdatePlayerHealth(float newHealth) {
        playerHealthUI.UpdatePlayerHealth(newHealth);
    }
    public void UpdatePlayerWallet(int newGem) {
        playerWalletUI.UpdatePlayerGemCount(newGem);
    }

}

public class PlayerHealthUI {

    private TextMeshProUGUI _healthText;

    public PlayerHealthUI(TextMeshProUGUI healthText,float playerHealth) {
        _healthText = healthText;
        UpdatePlayerHealth(playerHealth);
    }

    public void UpdatePlayerHealth(float newHealth) {
        _healthText.text = newHealth.ToString();
    }
}

public class PlayerKillCountUI{
    private TextMeshProUGUI _killText;
    private int _killCount;

    public PlayerKillCountUI(TextMeshProUGUI killText) {
        _killText = killText;
        _killCount = 0;
        killText.text = _killCount.ToString();
    }

    public void UpdatePlayerKill() {
        _killCount++;
        _killText.text = _killCount.ToString();
    }
}
public class PlayerWalletUI {

    private TextMeshProUGUI _gemText;
    private int _gemCount;

    public PlayerWalletUI(TextMeshProUGUI gemText, int playerGemCount) {
        _gemText = gemText;
        UpdatePlayerGemCount(playerGemCount);
    }

    public void UpdatePlayerGemCount(float newGemCount) {
        _gemText.text = newGemCount.ToString();
    }
}

