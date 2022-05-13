using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Enemy Spawn")]
    public List<Enemy> enemyList;
    public int spawnCount;
    public GameObject pointsParent;
    public List<Transform> spawnPoints;

    private void Start()
    {
        Transform[] points = pointsParent.GetComponentsInChildren<Transform>();
        foreach (Transform t in points)
        {
            spawnPoints.Add(t);
        }

        EnemySpawn();
    }
    public void EnemySpawn()
    {
        int randPoint = 0;
        int randEnemy = 0;
        for (int i = 0; i < spawnCount; i++)
        {
            randPoint = Random.Range(0, spawnPoints.Count);
            randEnemy = Random.Range(0, enemyList.Count);

            Instantiate(enemyList[randEnemy], spawnPoints[randPoint].position+enemyList[randEnemy].transform.position, Quaternion.identity);
        }
    }
}
