using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable Data")]
public class CollectableItem : ScriptableObject
{
    [SerializeField]
    private int value;

    public int Value { get { return value; } }
}
