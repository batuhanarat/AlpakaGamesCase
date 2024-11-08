using DependencyInjection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour, IDependencyProvider
{


    [Provide]
    public ScenesManager ProvideScenesManager(){
        return this;
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
