using UnityEngine;

public interface IHumanoidController
{
    public Transform DynamicWeaponHolder{ get; set; }
    public Transform StaticWeaponHolder{ get; set; }
    public LayerMask TargetLayer { get ; set; }
}