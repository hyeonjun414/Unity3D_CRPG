using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    [Header("Summon")]
    public EnemySummonData summonData;
    public int waveCount = 0;

    private Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SetUp();
    }
    public void SummonMonster()
    {
        // 현재 배틀스테이지를 가져온다.
        BattleStage bs = BattleManager.Instance.stage;
        List<int> wave = null;

        // 적의 웨이브 카운트에 해당하는 리스트를 넣어준다.
        switch (waveCount)
        {
            case 0: wave = summonData.wave1; break;
            case 1: wave = summonData.wave2; break;
            case 2: wave = summonData.wave3; break;
        }
        int randCount = Random.Range(3, 6);
        for(int i = 0; i < randCount;)
        {
            BattleTile bt = bs.battleTiles[Random.Range(0, bs.battleTiles.Count/2)];
            if(bt.monster == null)
            {
                MonsterData md = (MonsterData)summonData.monsters[Random.Range(0, summonData.monsters.Count)];
                bt.monster = SummonManager.Instance.SummonMonster(md, bt, MonsterOwner.Enemy);
                i++;
            }
        }
/*        // 리스트에 들어있는 타일 위치에 맞춰서 몬스터를 소환해준다.
        foreach(int waveIndex in wave)
        {
            // 배틀 스테이지에서 인덱스에 해당하는 배틀타일을 가져온다.
            BattleTile bt = bs.battleTiles[waveIndex];
            // 소환 데이터의 몬스터 리스트에서 랜덤하게 하나를 소환한다.
            MonsterData md = (MonsterData)summonData.monsters[Random.Range(0, summonData.monsters.Count)];
            
            // 배틀타일에 생성한 데이터의 정보를 넣어준다.
            bt.monster = SummonManager.Instance.SummonMonster(md, bt, MonsterOwner.Enemy);
        }*/
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
