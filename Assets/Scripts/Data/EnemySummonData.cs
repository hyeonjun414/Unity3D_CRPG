using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Summon Data", menuName = "Enemy/Summon Data")]
public class EnemySummonData : ScriptableObject
{
    public List<CardData> monsters;
}
