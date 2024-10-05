using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/Decision")]
public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateController controller);
}