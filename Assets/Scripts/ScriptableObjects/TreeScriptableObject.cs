using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tree Behavior", menuName = "Tree Behavior")]
public class TreeScriptableObject : ScriptableObject
{
    public float growthSpeed = 1.0f;
    public float luckyTree = 1f;
    public float hitTree = 3f;
    public float dayMultiplier = 1.0f;
    public float nightMultiplier = 0.5f;

}
