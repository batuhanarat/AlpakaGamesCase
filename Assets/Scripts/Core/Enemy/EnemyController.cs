using System;
using System.Collections.Generic;
using Game.Managers;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour, IHumanoidController
{

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private StateController stateController;
    [SerializeField] private EnemyAnimator animator;
    [SerializeField] private GameObject wayPoints;
    [SerializeField] private Weapon Weapon;
    [SerializeField] private Transform _dynamicWeaponHolder;

    private AudioManager audioManager;
    private UIManager uiManager;
    private IHealth Health;
    public Transform DynamicWeaponHolder  {
        get => _dynamicWeaponHolder;
        set => _dynamicWeaponHolder = value;
    }
    [SerializeField] private Transform _staticWeaponHolder;
    public Transform StaticWeaponHolder  {
        get => _staticWeaponHolder;
        set => _staticWeaponHolder = value;
    }
    [SerializeField] private LayerMask _targetLayer;
    public LayerMask TargetLayer
    {
        get => _targetLayer;
        set => _targetLayer = value;
    }
    public Transform TargetLocation { get ; set; }
    private bool _isDead = false;

    void Awake()
    {
        stateController = GetComponent<StateController>();
        Prepare();
        Health.CurrentHealth = enemyStats.MaxHealth;
        InstantiateWeapons();

    }

    private void InstantiateWeapons()
    {
        Weapon = Instantiate(Weapon);
        if (Weapon is StaticWeapon)
        {
            Weapon.transform.SetParent(DynamicWeaponHolder, false);
        }
        else
        {
            Weapon.transform.SetParent(StaticWeaponHolder, false);
        }
    }

    void Start()
    {
        audioManager = ServiceProvider.AudioManager;
        uiManager = ServiceProvider.UIManager;
        List<Transform> waypoints = InstantiateWaypoints();
        stateController.SetUpAI(true, waypoints, this);
    }

    private List<Transform> InstantiateWaypoints()
    {
        var waypointgo = Instantiate(wayPoints);
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in waypointgo.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public void Prepare() {
        Health = new EnemyHealth();
        Health.Prepare(() => {
            Die();
        });
    }

    public void Die() {
        _isDead = true;
        stateController.Deactivate();
        animator.DeadAnimation();
        //audioManager.PlayAudio();
        uiManager.UpdatePlayerKill();
        Gem.Create(transform,100);
        ServiceProvider.Pool.ReturnToPool(PoolableType.Enemy,gameObject);
    }

    public void Attack()
    {
        Weapon.Shoot(animator,TargetLayer,this);
    }

    public void GetHit(float damage)
    {
        if(_isDead) return;
        Health.GetHit(damage);
        DamagePopUp.Create(transform, damage, damage > 50);
    }
}