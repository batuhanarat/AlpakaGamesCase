using System.Collections.Generic;
using Game.Managers;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour,IHumanoidController
{
    [Header("Components")]
    private PlayerAnimator animator;
    private CharacterController characterController;

    [Header("Dependencies")]
    [SerializeField] private MobileJoystick mobileJoystick;
    [SerializeField] private List<Weapon> Weapons = new List<Weapon>();
    private Wallet _wallet;
    private IHealth _health;
    private UIManager _uiManager;
    private GameManager _gameManager;

    [Header("Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _staticWeaponHolder;
    [SerializeField] private Transform _dynamicWeaponHolder;
    [SerializeField] private WalletData walletData;
    private const float MAX_HEALTH = 10;

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

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimator>();
        Utilities.SetPlayerTransform(gameObject.transform);
    }

    void Start()
    {
        _uiManager = ServiceProvider.UIManager;
        _gameManager = ServiceProvider.GameManager;
        InstantiateHealth();
        InstantiateWallet();
        InstantiateWeapons();
        _uiManager.Prepare(MAX_HEALTH, _wallet.GetTotalGemCount());
    }

    private void InstantiateWallet()
    {
        _wallet = new Wallet(walletData, _uiManager.UpdatePlayerWallet);
    }

    private void InstantiateHealth()
    {
        _health = new PlayerHealth
        {
            CurrentHealth = MAX_HEALTH
        };
        _health.Prepare(() =>
        {
            Die();
        });
        if (_health is PlayerHealth playerHealth)
        {
            playerHealth.SetUICallback(_uiManager.UpdatePlayerHealth);
        }
    }
    private void InstantiateWeapons()
    {
        for(int i = 0 ; i<Weapons.Count; i++) {
            Weapons[i] = Instantiate(Weapons[i]);
            if(Weapons[i] is StaticWeapon) {
                Weapons[i].transform.SetParent(StaticWeaponHolder, false);
            } else {
                Weapons[i].transform.SetParent(DynamicWeaponHolder, false);
            }
        }
    }
    void Update()
    {
        if(_gameManager.CanPlay) {
            ControlMovement();
            ControlAttack();
        }
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
    private void ControlAttack() {
        Attack();
    }
    private void Attack() {
        foreach(var _weapon in Weapons) {
            _weapon.Shoot(animator,TargetLayer,this);
        }
    }
    public void CollectGem(int value) {
        _wallet.AddToWallet(value);
    }

    public void GetHit() {
        _health.GetHit(0);
    }
    private void Die() {

        _uiManager.ShowLosePanel();
        _gameManager.CanPlay = false;

    }
}
