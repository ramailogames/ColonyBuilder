using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/ Resource Data")]
public class ResourceData : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public float checkRadius;
}
