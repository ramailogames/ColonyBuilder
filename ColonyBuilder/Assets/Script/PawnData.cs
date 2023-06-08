using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/ Player Data")]
public class PawnData : ScriptableObject
{
    public float moveSpeed;
    public float nextWayPointDistance = 3f;
}
