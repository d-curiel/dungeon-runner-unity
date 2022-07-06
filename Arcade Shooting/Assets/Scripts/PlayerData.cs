using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data")]
public class PlayerData : ScriptableObject
{
    private int currentScore = 0;
    private float currentDistance = 0;

    private int maxScore;
    private float maxDistance;

    private bool isAlive = true;

    public int CurrentScore { get; set; }
    public float CurrentDistance { get; set; }
    public int MaxScore { get; set; }
    public float MaxDistance { get; set; }

    public bool IsAlive { get; set; }

}
