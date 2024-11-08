using UnityEngine;

[CreateAssetMenu(fileName ="BulletConfig", menuName ="Configs/BulletConfig")]
public class BulletConfig : ScriptableObject {
    public float maxDistance;
    public float moveSpeed;
}