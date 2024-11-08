using UnityEngine;

[CreateAssetMenu(fileName = "DefaultPlacer", menuName ="Placements/DefaultPlacer")]
public class PlacementStrategy : ScriptableObject {
    public virtual Vector3 SetPosition(Vector3 origin) => origin;
}

[CreateAssetMenu(fileName = "RandomCirclePlacer", menuName ="Placements/RandomCirclePlacer")]

public class RandomCirclePlacer : PlacementStrategy {
    public float minDistance;
    public float maxDistance = 10f;

    public override Vector3 SetPosition(Vector3 origin)
    {
        return origin.RandomPointInAnnulus(minDistance, maxDistance);
    }
}