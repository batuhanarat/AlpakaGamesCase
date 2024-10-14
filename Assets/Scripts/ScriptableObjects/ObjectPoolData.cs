using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AlpakaGamesCase/PoolData")]
public class ObjectPoolData : ScriptableObject
{
    public List<PoolableItemData> pool;

}

[Serializable]
public class PoolableItemData{
    public PoolableType Type;
    public GameObject Prefab;
    public int Size;
}

public enum PoolableType{
    Enemy,
    Bullet
}