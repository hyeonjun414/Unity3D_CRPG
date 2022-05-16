using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Summon Data", menuName = "Enemy/Summon Data")]
public class EnemySummonData : ScriptableObject
{
    [Header("Wave1")]
    public List<int> wave1;
    [Header("Wave1")]
    public List<int> wave2;
    [Header("Wave1")]
    public List<int> wave3;

    public List<CardData> monsters;
    
}
