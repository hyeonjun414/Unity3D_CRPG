using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Data", menuName = "data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy")]
    public int level;
    public int minSummonCount;
    public int maxSummonCount;
    public int Hp;
    public int damage;

    [Header("Summon")]
    public EnemySummonData summonData;
}
