using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDB", menuName = "DB/StageDB")]
public class StageDB : ScriptableObject
{
    [Header("Stage")]
    public StageData[] stageData;

    [Header("Enemy")]
    public EnemySummonData[] enemySummonData;
}
