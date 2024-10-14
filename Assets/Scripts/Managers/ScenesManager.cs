
using Game.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour, IProvidable
{

    private void Awake()
    {
        ServiceProvider.Register(this);
    }


    public void LoadStartScene() {
        LoadScene(Scene.START_SCENE);
    }
    public void LoadGameScene() {
        LoadScene(Scene.GAME_SCENE);
    }
    public void LoadUpgradeScene() {
        LoadScene(Scene.UPGRADE_SCENE);
    }
    private void LoadScene(Scene scene) {
        SceneManager.LoadScene((int) scene);
    }
}
public enum Scene {
    START_SCENE = 0,
    GAME_SCENE = 1,
    UPGRADE_SCENE = 2
}
