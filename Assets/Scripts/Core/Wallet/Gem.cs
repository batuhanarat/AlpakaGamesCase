using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private static GameObject gemPrefab;
    public static Gem Create(Transform enemyTransform, int value)
    {
        var gem = Instantiate(gemPrefab).GetComponent<Gem>();
        gem.transform.position = enemyTransform.position;
        gem.Setup(value);

        return gem;
    }
    private SphereCollider _triggerCollider;

    private void Awake()
    {
        _triggerCollider = gameObject.AddComponent<SphereCollider>();
        _triggerCollider.isTrigger = true;
        _triggerCollider.radius = 1f;
    }

    private int _value;

    private void Setup(int value)
    {
        _value = value;
    }
    private void Update()
    {
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
        if (other.gameObject.CompareTag("Player"))
        {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.CollectGem(_value);
            Destroy(gameObject);
        }
    }

}
