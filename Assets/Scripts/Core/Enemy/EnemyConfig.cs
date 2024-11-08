using UnityEngine;

[CreateAssetMenu(fileName ="EnemyConfig", menuName ="Configs/EnemyConfig")]
public class EnemyConfig : ScriptableObject {
    public GameObject prefab;
    public EnemyType type;
    public int health;
    public int damage;
    public float attackRange;
    public float moveSpeed;
}