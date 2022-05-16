using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    [Header("Summon")]
    public EnemySummonData summonData;
    public int waveCount = 0;

    private void Start()
    {
        SetUp();
    }
    public void SummonMonster()
    {
        BattleStage bs = StageManager.Instance.stage;
        List<int> wave = null;
        switch (waveCount)
        {
            case 0: wave = summonData.wave1; break;
            case 1: wave = summonData.wave2; break;
            case 2: wave = summonData.wave3; break;
        }
        foreach(int waveIndex in wave)
        {
            BattleTile bt = bs.battleTiles[waveIndex];
            MonsterData md = (MonsterData)summonData.monsters[Random.Range(0, summonData.monsters.Count)];
            Monster monster = Instantiate(md.monster, bt.transform.position, Quaternion.LookRotation(Vector3.left));
            monster.returnTile = bt;
            monster.owner = MonsterOwner.Enemy;
            bt.monster = monster;
            bt.state = TileState.STAY;
            
        }
    }

    public override void Attack()
    {
    }

    public override void Hit(float damage)
    {
        HP -= damage;
        GameManager.Instance.CreateDamage((int)damage, transform.position);
    }
}
