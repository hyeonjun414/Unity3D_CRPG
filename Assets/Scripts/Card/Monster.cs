﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Move,
    Attack
}
public enum MonsterOwner
{
    Player,
    Enemy
}
public class Monster : LivingEntity
{
    [Header("Monster")]
    public float moveDelayTime = 1f;
    public MonsterState state;
    public MonsterOwner owner;
    public CardData data;

    [Header("BattleTile")]
    public BattleTile returnTile;
    public Monster target;
    public BattleTile curTile;

    [Header("Effect")]
    public GameObject allyEffect;
    public GameObject enemyEffect;


    private Animator anim;
    private CapsuleCollider cc;
    private Rigidbody rb;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        SetUp();
    }
    public override void SetUp()
    {
        base.SetUp();
        curTile = returnTile;
        if (owner == MonsterOwner.Player)
        {
            enemyEffect.gameObject.SetActive(false);
        }
        else
        {
            allyEffect.gameObject.SetActive(false);
        }

    }
    public IEnumerator MoveRoutine()
    {
        BattleStage bs = StageManager.Instance.stage;
        state = MonsterState.Move;

        List<Vector2> moveList = StageManager.Instance.PathFinding(this, target);
        if(moveList.Count == 0)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(MoveRoutine());
            yield break;
        }

        if (moveList.Count <= 2)
        {
            if (target == null)
                BattleStart();
            else
            {
                transform.rotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
                StartCoroutine(AttackRoutine());
            }

            yield break;
        }

        curTile.state = TileState.NONE;
        curTile.monster = null;
        curTile = bs.battleMap[(int)moveList[1].x, (int)moveList[1].y];
        curTile.state = TileState.STAY;
        curTile.monster = this;

        yield return StartCoroutine(moveTile(moveList));
        //yield return new WaitForSeconds(moveDelayTime);
        StartCoroutine(MoveRoutine());
    }

    public IEnumerator AttackRoutine()
    {
        anim.SetTrigger("Attack");
        target.Hit(damage);
        yield return new WaitForSeconds(attackDelayTime);
        StartCoroutine(MoveRoutine());
    }
    public override void Attack()
    {
    }

    public override void Hit(float damage)
    {
        HP -= damage;
        GameManager.Instance.CreateDamage((int)damage, transform.position);
    }
    IEnumerator moveTile(List<Vector2> tiles)
    {

        BattleTile nextTile = StageManager.Instance.stage.battleMap[(int)tiles[1].x, (int)tiles[1].y];


        Vector3 curPos = transform.position;
        Vector3 distPos = nextTile.transform.position;
        float curTime = 0f;
        anim.SetBool("IsMove", true);
        transform.rotation = Quaternion.LookRotation((distPos - curPos).normalized);
        while (true)
        {
            if(curTime >= moveDelayTime - 0.5f)
            {
                break;
            }
            curTime += Time.deltaTime;
            
            transform.position = Vector3.Lerp(curPos, distPos, curTime/(moveDelayTime - 0.5f));
            yield return null;
        }
        anim.SetBool("IsMove", false);
    }

    public void BattleStart()
    {
        target = FindingTarget();

        if (target == null) return;
        StopAllCoroutines();
        StartCoroutine(MoveRoutine());
    }
    public void BattleEnd()
    {
        StopAllCoroutines();
        if(owner == MonsterOwner.Player)
        {
            Destroy(gameObject, 1f);
            anim.SetTrigger("Die");
        }
        else
        {
            Destroy(gameObject, 1f);
            anim.SetTrigger("Die");
        }
    }

    public Monster FindingTarget()
    {
        List<Monster> monList = null;
        if (owner == MonsterOwner.Player)
            monList = StageManager.Instance.EnemyMonster;
        else
            //return null;
            monList = StageManager.Instance.AllyMonster;
        
        int rand = Random.Range(0, monList.Count);

        return monList[rand];
    }
    public override void Die()
    {
        base.Die();

        StageManager.Instance.EraseMonster(this);
        


    }
}