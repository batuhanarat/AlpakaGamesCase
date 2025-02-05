using UnityEngine;

public abstract class EnemyAction : ScriptableObject
{
    public abstract void Act(StateController controller);
}
