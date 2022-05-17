﻿using System.Collections;
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
        BattleStage bs = StageManager.Instance.stage;
        List<int> wave = null;

        // 적의 웨이브 카운트에 해당하는 리스트를 넣어준다.
        switch (waveCount)
        {
            case 0: wave = summonData.wave1; break;
            case 1: wave = summonData.wave2; break;
            case 2: wave = summonData.wave3; break;
        }

        // 리스트에 들어있는 타일 위치에 맞춰서 몬스터를 소환해준다.
        foreach(int waveIndex in wave)
        {
            // 배틀 스테이지에서 인덱스에 해당하는 배틀타일을 가져온다.
            BattleTile bt = bs.battleTiles[waveIndex];
            // 소환 데이터의 몬스터 리스트에서 랜덤하게 하나를 소환한다.
            MonsterData md = (MonsterData)summonData.monsters[Random.Range(0, summonData.monsters.Count)];
            GameObject go = Instantiate(md.monster, bt.transform.position, Quaternion.LookRotation(Vector3.left));
            Monster monster = go.GetComponent<Monster>();
            monster.returnTile = bt;
            monster.owner = MonsterOwner.Enemy;

            // 데이터를 통해 초기 설정을 해준다.
            monster.InputData(md);

            // 배틀타일에 생성한 데이터의 정보를 넣어준다.
            bt.monster = monster;
            bt.state = TileState.STAY;
            
        }
    }

    public override int Attack()
    {
        anim.SetTrigger("Attack");
        return damage;
    }

    public override void Hit(int damage)
    {
        HP -= damage;
        anim.SetTrigger("Hit");
        GameManager.Instance.CreateDamage((int)damage, transform.position);
    }
}
