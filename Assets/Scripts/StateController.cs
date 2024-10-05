using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public EnemyStats enemyStats;
    public Transform eyes;
    public State remainState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public IWeapon weapon;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;
    private bool _aiActive;
    public State CurrentState;


    void Awake()
    {
        weapon = GetComponent<IWeapon>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetUpAI(bool aiActivation, List<Transform> wayPoints) {
        wayPointList = wayPoints;
        _aiActive = aiActivation;
        if(_aiActive) {
            navMeshAgent.enabled = true;
        } else {
            navMeshAgent.enabled = false;
        }
    }

    void Update()
    {
        if(!_aiActive) return;
        CurrentState.UpdateState(this);
    }
    public void TransitionToState(State nextState)
    {
        if(nextState != remainState) {
            CurrentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration) {
        stateTimeElapsed += Time.deltaTime;
        return stateTimeElapsed>=duration;

    }
    private void OnExitState() {
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