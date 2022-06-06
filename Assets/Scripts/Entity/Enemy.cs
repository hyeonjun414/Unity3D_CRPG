using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    [Header("Summon")]
    public EnemyData enemyData;

    private Animator anim;

    public void SetData(EnemyData data)
    {
        enemyData = data;
        maxHp = data.Hp;

        anim = GetComponentInChildren<Animator>();
        SetUp();
    }
    public void SummonMonster()
    {
        // 현재 배틀스테이지를 가져온다.
        BattleStage bs = BattleManager.Instance.stage;

        int randCount = Random.Range(enemyData.minSummonCount, enemyData.maxSummonCount+1);
        for(int i = 0; i < randCount;)
        {
            BattleTile bt = bs.battleTiles[Random.Range(0, bs.battleTiles.Count/2)];
            if(bt.monster == null)
            {
                MonsterData md = (MonsterData)enemyData.summonData.monsters[Random.Range(0, enemyData.summonData.monsters.Count)];
                bt.monster = SummonManager.Instance.SummonMonster(md, bt, MonsterOwner.Enemy);
                i++;
            }
        }
    }

    public override LivingEntity Attack()
    {
        anim.SetTrigger("Attack");
        return this;
    }

    public override void Hit(LivingEntity entity)
    {
        HP -= entity.damage;
        anim.SetTrigger("Hit");
        StartCoroutine(KnockbackRoutine());
        GameManager.Instance.CreateText(entity.damage, transform.position, TextType.Damage);
    }

    public IEnumerator KnockbackRoutine()
    {
        Vector3 dest = transform.position - transform.forward * 2;
        float curTime = 0f;
        float endTime = 1f;
        while(true)
        {
            if (curTime > endTime)
                break;

            curTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, dest, curTime);
            yield return null;
        }
    }
}
