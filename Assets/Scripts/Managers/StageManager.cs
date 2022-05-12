using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Enemy Spawn")]
    public List<Enemy> enemyList;
    public int spawnCount;
    public List<Transform> spawnPoints;
}
