using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using DependencyInjection;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] private List<Upgrade> upgrades = new();
    [SerializeField] private UpgradeStats _upgradeData;
    [SerializeField] private SoundData upgradeIterationSound;
    [SerializeField] private SoundData upgradeSound;
    #endregion

    #region Private Variables
    private Color originalColor;
    private Color yellowColor = Color.yellow;
    private HashSet<int> selectedIndexes = new HashSet<int>();
    private SoundBuilder soundBuilder;
    #endregion

    #region Dependencies

    [Inject] Wallet wallet;
    [Inject] UpgradeUIManager upgradeUIManager;
    [Inject] SoundManager soundManager;
    [Inject] ScenesManager scenesManager;

    #endregion


    public float UpgradeCost
    {
        get => _upgradeData.CostOfUpgrade;
        private set => _upgradeData.CostOfUpgrade = value;
    }
    public float UpgradeScaler => _upgradeData.UpgradeScaler;



    void Start()
    {
        upgradeUIManager.Prepare(UpgradeCost, wallet.CanAfford(UpgradeCost));
        soundBuilder = soundManager.CreateSoundBuilder();
    }
    public void TryStartUpgradeSequence()
    {
        var isSuccessful = wallet.TryWithdraw(UpgradeCost);
        if(isSuccessful) {
            IncreaseUpgradeCost();
            StartCoroutine(UpgradeSequence());
        } else {
            StartCoroutine(Wait2SecondsAndLoadStartScene());
        }
    }
    private void IncreaseUpgradeCost()
    {
          UpgradeCost +=  UpgradeCost * UpgradeScaler;
    }
    private void PlayUpgradeSound(SoundData soundData)
    {
        soundBuilder
            .WithRandomPitch()
            .WithPosition(transform.position)
            .Play(soundData);
    }

    private IEnumerator UpgradeSequence()
    {
        originalColor = upgrades[0].Renderer.color;
        for (int iteration = 0; iteration < 5; iteration++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, upgrades.Count);
            } while (selectedIndexes.Contains(randomIndex));

            selectedIndexes.Add(randomIndex);
            SpriteRenderer currentRenderer = upgrades[randomIndex].Renderer;

            currentRenderer.color = yellowColor;
            PlayUpgradeSound(upgradeIterationSound);

            yield return new WaitForSeconds(0.2f);

            currentRenderer.color = originalColor;
        }

        int finalIndex;
        do
        {
            finalIndex = Random.Range(0, upgrades.Count);
        } while (selectedIndexes.Contains(finalIndex));

        SpriteRenderer selectedRenderer = upgrades[finalIndex].Renderer;
        selectedRenderer.color = yellowColor;

        var upgrade =  upgrades[finalIndex];
        upgrade.UpgradeWeapon();
        PlayUpgradeSound(upgradeSound);



        StartCoroutine(Wait2SecondsAndLoadStartScene());
    }

    private IEnumerator Wait2SecondsAndLoadStartScene() {
        yield return new WaitForSeconds(2f);
        scenesManager.LoadStartScene();
    }
}
