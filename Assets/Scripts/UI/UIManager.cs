using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    private EventBinding<PlayerDiedEvent> playerDiedBinding;

    void OnEnable()
    {
        playerDiedBinding = new EventBinding<PlayerDiedEvent>(ShowLosePanel);
        EventBus<PlayerDiedEvent>.Register(playerDiedBinding);
    }
    void OnDisable()
    {
        EventBus<PlayerDiedEvent>.Deregister(playerDiedBinding);
    }
    public void ShowLosePanel()
    {
        LosePanel.SetActive(true);
    }



}