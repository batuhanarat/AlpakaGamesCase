using UnityEngine;

public interface IHumanoidController
{
    public Transform TargetLocation { get; set; }
    public Transform DynamicWeaponHolder{ get; set; }
    public Transform StaticWeaponHolder{ get; set; }
    public LayerMask TargetLayer { get ; set; }


}