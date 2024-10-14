using System.Collections;
using System.Collections.Generic;
using Game.Managers;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> upgradeContent = new List<GameObject>();
    [SerializeField] private WalletData walletData;
    [SerializeField] private UpgradeUIManager upgradeUIManager;
    [SerializeField] private UpgradeData upgradeData;


    private Color originalColor;
    private Color yellowColor = Color.yellow;
    private HashSet<int> selectedIndexes = new HashSet<int>();

    void Start() {
        upgradeUIManager.Prepare(upgradeData.CostOfUpgrade, upgradeData.CostOfUpgrade <= walletData.totalValue);
    }

    public void TryStartUpgradeSequence()
    {
        if(upgradeData.CostOfUpgrade<= walletData.totalValue){
            upgradeData.CostOfUpgrade +=  upgradeData.CostOfUpgrade * upgradeData.UpgradeScaler;
            StartCoroutine(UpgradeSequence());
        } else {
            StartCoroutine(Wait2SecondsAndLoadStartScene());
        }

    }

    private IEnumerator UpgradeSequence()
    {

        originalColor = upgradeContent[0].GetComponent<UpgradeUI>().frame.color;

        for (int iteration = 0; iteration < 5; iteration++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, upgradeContent.Count);
            } while (selectedIndexes.Contains(randomIndex));

            selectedIndexes.Add(randomIndex);
            SpriteRenderer currentRenderer = upgradeContent[randomIndex].GetComponent<UpgradeUI>().frame;

            currentRenderer.color = yellowColor;

            yield return new WaitForSeconds(0.2f);

            currentRenderer.color = originalColor;
        }

        int finalIndex;
        do
        {
            finalIndex = Random.Range(0, upgradeContent.Count);
        } while (selectedIndexes.Contains(finalIndex));

        SpriteRenderer selectedRenderer = upgradeContent[finalIndex].GetComponent<UpgradeUI>().frame;
        selectedRenderer.color = yellowColor;

        var upgrade =  upgradeContent[finalIndex];
        upgrade.GetComponent<UpgradeUI>().Upgrade();


        StartCoroutine(Wait2SecondsAndLoadStartScene());
    }

    private IEnumerator Wait2SecondsAndLoadStartScene() {
        yield return new WaitForSeconds(2f);
        ServiceProvider.ScenesManager.LoadStartScene();
    }
}
