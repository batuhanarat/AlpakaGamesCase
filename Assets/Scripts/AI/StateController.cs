using System.Collections.Generic;
using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector] public NavMeshAgent navMeshAgent;
    private EnemyController _controller;

    [Header("Dependencies")]
    private GameManager _gameManager;

    [Header("Variables")]
    public State CurrentState;
    public State RemainState;
    public Transform chaseTarget;
    public EnemyStats enemyStats;
    public Transform eyes;
    public bool OnAlarm;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public float stateTimeElapsed;
    private bool _aiActive;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void SetUpAI(bool aiActivation, List<Transform> wayPoints, EnemyController controller) {
        OnAlarm = true;
        _gameManager = ServiceProvider.GameManager;
        wayPointList = wayPoints;
        _controller = controller;
        _aiActive = aiActivation;
        if(_aiActive) {
            navMeshAgent.enabled = true;
        } else {
            navMeshAgent.enabled = false;
        }
    }
    void Update()
    {
        if(!_aiActive || !_gameManager.CanPlay) return;
        CurrentState.UpdateState(this);
    }
    public void TransitionToState(State nextState)
    {
        if(nextState != RemainState) {
            CurrentState = nextState;
            OnExitState();
        }
    }
    public void ManageAttack( ) {
        _controller.Attack();
    }
    public void Deactivate() {
        _aiActive = false;
    }
    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return stateTimeElapsed>=duration;
    }
    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
    void OnDrawGizmos()
    {
        if(CurrentState != null && eyes != null) {
            Gizmos.color =CurrentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        }
    }
}