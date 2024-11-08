using System.Collections.Generic;
using UnityEngine;

public abstract class RangeController : MonoBehaviour
{
    public Collider range;
    public List<EnemyController> enemiesInRange = new();


    void Awake() {
        range = GetComponent<Collider>();
    }
    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerExit(Collider other);



}