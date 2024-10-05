using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private bool isDead;
    [SerializeField] private StateController stateController;
    [SerializeField] private List<Transform> wayPoints;

    void Awake()
    {
        stateController = GetComponent<StateController>();
    }

    void Start()
    {
        stateController.SetUpAI(true,wayPoints);
    }

    private void Attack() {

    }

    private void GetHit() {

    }
}