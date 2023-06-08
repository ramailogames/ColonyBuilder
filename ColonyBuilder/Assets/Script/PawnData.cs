using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/ Player Data")]
public class PawnData : ScriptableObject
{
   
    public float walkSpeed;
    public float runSpeed;
    public float nextWayPointDistance = 3f;
}
