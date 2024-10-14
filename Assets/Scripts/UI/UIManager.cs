using Game.Managers;
using UnityEngine;

public class UIManager : MonoBehaviour, IProvidable
{
    [SerializeField] private PlayerUIManager playerUIManager;
    [SerializeField] private GameObject LosePanel;
    private void Awake()
    {
        ServiceProvider.Register(this);
    }

    public void Prepare(float initialHealth, int initialGem) {
        playerUIManager.Prepare(initialHealth,initialGem);
    }

    public void UpdatePlayerKill() {
        playerUIManager.UpdatePlayerKill();
    }

    public void UpdatePlayerHealth(float newHealth) {
        playerUIManager.UpdatePlayerHealth(newHealth);
    }

    public void UpdatePlayerWallet(int newGem) {
        playerUIManager.UpdatePlayerWallet(newGem);
    }

    public void ShowLosePanel() {
        LosePanel.SetActive(true);
    }



}