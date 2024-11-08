using System.Collections.Generic;
using AudioSystem;
using DependencyInjection;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour,IHumanoidController, IDependencyProvider
{
    [Provide]
    public PlayerController ProvidePlayer() {
        return this;
    }
    [Header("Components")]
    private PlayerAnimator animator;
    private CharacterController characterController;

    [Header("Dependencies")]
    [SerializeField] private MobileJoystick mobileJoystick;
    private IHealth _health;

    [Header("Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _staticWeaponHolder;
    [SerializeField] private Transform _dynamicWeaponHolder;
    [SerializeField] private WalletData walletData;
    private const float MAX_HEALTH = 10;

    [Inject] SoundManager soundManager;
    [Inject] IWallet wallet;
    [SerializeField] SoundData attackSound;

    private EventBinding<PlayerDiedEvent> playerDiedBinding;

    public Transform DynamicWeaponHolder  {
        get => _dynamicWeaponHolder;
        set => _dynamicWeaponHolder = value;
    }
    public Transform StaticWeaponHolder  {
        get => _staticWeaponHolder;
        set => _staticWeaponHolder = value;
    }
    public LayerMask TargetLayer
    {
        get => _targetLayer;
        set => _targetLayer = value;
    }
    public Transform TargetLocation {get; set;}
    public Transform myTransform { get => transform;}

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimator>();
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


    void Update()
    {

        ControlMovement();
     //   ControlAttack();

    }
    private void ControlMovement()
    {
        gameObject.transform.position = new Vector3(transform.position.x,0,transform.position.z);
        Vector3 correctedMoveVector =  mobileJoystick.GetMoveVector();
        correctedMoveVector.z = correctedMoveVector.y;
        correctedMoveVector.y = 0;
        animator.ManageMovementAnimation(correctedMoveVector,TargetLocation);

        Vector3 moveVector = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        characterController.Move(moveVector);
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
