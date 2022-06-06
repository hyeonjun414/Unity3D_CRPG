using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "Data/Enemy Stage Data")]
public class EnemyStageData : StageData
{
    [Header("Enemy Stage")]
    public GameObject[] enemyMaps;
    public GameObject[] enemyBattleStages;
}
