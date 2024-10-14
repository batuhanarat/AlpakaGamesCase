using System;
using Game.Managers;
using UnityEngine;

public class Gem : MonoBehaviour
{

    public static Gem Create(Transform enemyTransform, int value)
    {
        var gem = ServiceProvider.AssetLib.GetAsset<Gem>(AssetType.GEM);
        gem.transform.position = enemyTransform.position;
        gem.Setup(value);

        return gem;
    }
      private SphereCollider _triggerCollider;

    private void Awake()
    {
        // Add a larger trigger collider
        _triggerCollider = gameObject.AddComponent<SphereCollider>();
        _triggerCollider.isTrigger = true;
        _triggerCollider.radius = 1f; // Adjust this value as needed
    }

    private int _value;

    private void Setup(int value)
    {
        _value = value;
    }
     private void Update()
    {
        // Check for player in trigger area
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _triggerCollider.radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                var playerController = hitCollider.gameObject.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.CollectGem(_value);
                    Debug.Log("Gem collected");
                    Destroy(gameObject);
                    return; // Exit after collecting
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
         Debug.Log("trigger entered");
        if (other.gameObject.CompareTag("Player"))
        {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.CollectGem(_value);
            Debug.Log("gem should be collected");
            Destroy(gameObject);
        }
    }

}
