using AudioSystem;
using DependencyInjection;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IDependencyProvider
{
    [Provide]
    public PlayerController ProvidePlayer() {
        return this;
    }
    [Header("Components")]
    private PlayerMovementController movementController;

    [Header("Dependencies")]

    private IHealth _health;

    [Header("Variables")]
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _staticWeaponHolder;
    [SerializeField] private Transform _dynamicWeaponHolder;
    [SerializeField] private WalletData walletData;
    private const float MAX_HEALTH = 10;

    [Inject] SoundManager soundManager;
    [Inject] IWallet wallet;
    [SerializeField] SoundData attackSound;

    private EventBinding<PlayerDiedEvent> playerDiedBinding;
    public Transform myTransform { get => transform;}

    void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
    }
    void Start()
    {
        _health = new PlayerHealth(MAX_HEALTH);
        wallet.Initialize(walletData);
    }
    void OnEnable() {
        playerDiedBinding = new EventBinding<PlayerDiedEvent>(Die);
        EventBus<PlayerDiedEvent>.Register(playerDiedBinding);
    }
    void OnDisable() {
        EventBus<PlayerDiedEvent>.Deregister(playerDiedBinding);
    }
    public void PlayAttackSound() {
        SoundBuilder soundBuilder = soundManager.CreateSoundBuilder();

        soundBuilder
            .WithRandomPitch()
            .WithPosition(transform.position)
            .Play(attackSound);
    }

    public void CollectGem(int value) {
        wallet.Deposit(value);
    }
    public void GetHit(float defaultDamage = 1) {
        _health.GetDamage(defaultDamage);
    }
    private void Die(PlayerDiedEvent playerDiedEvent) {
        Destroy(gameObject,2f);
    }
}

public enum PlayerState {
    PATROL,
    COMBAT
}
